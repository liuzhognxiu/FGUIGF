Directory = CS.System.IO.Directory
Path = CS.System.IO.Path
File = CS.System.IO.File
Error = function(msg)
    App.consoleView:LogError(tostring(msg))
end

local pluginDir = ""
--包名
local codePkgName = ""
local writer = {}
local classes = {}
--全局发布设置
local publishSettings = {}
--全局自定义属性
local customProperties = {}
--界面代码导出路径
local exportViewPath = ""
--UI资源导出路径
local exportResPath = ""
--配置代码导出路径
local exportConfigPath = ""

local tblName2Class = {}

local function genCode(handler, pluginPath)
    --插件目录
    pluginDir = string.PathConversion(pluginPath)

    --convert chinese to pinyin, remove special chars etc.
    codePkgName = handler:ToFilename(handler.pkg.name)
    --工程根路径
    --根据传入的plugin路径来获取
    local workDir = string.PathConversion(Path.GetFullPath(pluginPath.."../../../../../../"))

    customProperties  = Util.DicToTable(handler.project:GetSettings("CustomProperties").elements)
    --路径
    exportViewPath = workDir..customProperties.CodeDir..'/'..codePkgName
    exportResPath = customProperties.ResDir..'/UI'..codePkgName
    exportConfigPath = workDir..customProperties.ConfigDir

    writer = CodeWriter.new({fileMark = string.format("/** %s. **/", customProperties.FileMark)})

    publishSettings = handler.project:GetSettings("Publish").codeGeneration
    classes = handler:CollectClasses(publishSettings.ignoreNoname, publishSettings.ignoreNoname, nil)

    RefreshName2Class(codePkgName, classes)
    fprint("workDir"..workDir)
    if not handler.genCode then
        fprint("[publish setting] genCode not open")
        return
    end
    handler.genCode = false

    CheckFileDir()

--     CreatePkgResPathConfig()
    CreateExtensionConfig()
    CreateView()
    CreateFormLogic()

    if customProperties.ConsoleClear == "True" then
        App.consoleView:Clear()
    end

end

-- function CreatePkgResPathConfig()
--     local templatePath = pluginDir..'/Template/PkgResPathConfig.txt'
--     local template = Util.ReadFile(templatePath)
--     if not template then
--         Error("找不到PkgResPathConfig模板文件")
--         return
--     end
-- 
--     local className = 'PkgResPathConfig'
--     local exportPath = exportConfigPath..'/'..className..'.cs'
-- 
--     if Util.FileExists(exportPath) then
--         local content = Util.ReadFile(exportPath)
--         if not string.isEmpty(content) then
--             content = string.hTrim(string.gsub(content, "/%*%*.-".. customProperties.FileMark ..".-%*%*/", ''))
--             local insertTxt = string.format('{ "%s" , "%s" },', codePkgName, exportResPath)
--             local rangePattern = '%#region auto generate not change.*%#endregion'
--             local insertPattern = '%#endregion'
--             writer:reset()
--             if not string.find(content, codePkgName) and Util.WriteInsert(writer, content, rangePattern, insertTxt, insertPattern, 3) then
--                 writer:save(exportPath)
--             end
--         end
--     else
--         writer:reset()
--         writer:writeln(template)
--         writer:save(exportPath)
--     end
-- end

function CreateExtensionConfig()
    local templatePath = pluginDir..'/Template/ExtensionConfig.txt'
    local template = Util.ReadFile(templatePath)
    if not template then
        Error("找不到ExtensionConfig模板文件")
        return
    end

    local className = 'ExtensionConfig'
    local exportPath = exportConfigPath..'/'..className..'.cs'

    if Util.FileExists(exportPath) then
        local content = Util.ReadFile(exportPath)
        if not string.isEmpty(content) then
            content = string.hTrim(string.gsub(content, "/%*%*.-".. customProperties.FileMark ..".-%*%*/", ""))
            local rangePattern = 'UIObjectFactory.SetPackageItemExtension%("ui://'..codePkgName..'/%w+", typeof%(%w+%)%);%s*'
            local replace = ''
            while true do
                if not string.find(content, rangePattern) then
                    break
                end
                content = string.gsub(content, rangePattern, replace)
            end
            writer:reset()
            writer:writeln(content)
            writer:save(exportPath)

            for i=0,classes.Count-1 do
                local classInfo = classes[i]
                local ClassName = string.firstToUpper(classInfo.className)
                local bCustomComponent = Util.IsCustomComponent(classInfo.className, codePkgName)

                if bCustomComponent then
                    content = Util.ReadFile(exportPath)
                    content = string.hTrim(string.gsub(content, "/%*%*.-".. customProperties.FileMark ..".-%*%*/", ""))
                    rangePattern = '%#region auto generate not change.*%#endregion'
                    local insertPattern = '%#endregion'
                    local insertTxt = string.format('UIObjectFactory.SetPackageItemExtension("ui://%s/%s", typeof(%s));',
                            codePkgName, classInfo.resName, ClassName..'Item')
                    writer:reset()
                    if Util.WriteInsert(writer, content, rangePattern, insertTxt, insertPattern, 3) then
                        writer:save(exportPath)
                    end
                end
            end

        end
    else
        writer:reset()
        writer:writeln(template)
        writer:save(exportPath)
    end
end

function CreateView()
    local winTemplatePath = pluginDir..'/Template/UIFormLogicBindComponent.txt'
    local winTemplate = Util.ReadFile(winTemplatePath)
    if not winTemplate then
        Error("找不到UIFormLogicBindComponent模板文件")
        return
    end
    local comTemplatePath = pluginDir..'/Template/UIItem.txt'
    local comTemplate = Util.ReadFile(comTemplatePath)
    if not comTemplate then
        Error("找不到UIItem模板文件")
        return
    end

    for i=0,classes.Count-1 do
        local classInfo = classes[i]
        local members = classInfo.members
        local bCustomComponent = Util.IsCustomComponent(classInfo.className, codePkgName)
        local ClassName = string.firstToUpper(classInfo.className)
        local ViewName = bCustomComponent and (ClassName..'Item') or (ClassName..'Form')

        local dirPath = exportViewPath..'/'..ViewName
        local exportPath = dirPath..'.cs'
        local tmpPath = dirPath..'/'..ViewName..'.cs'
        if classes.Count > 1 then
            if Directory.Exists(dirPath) and File.Exists(exportPath) then
                File.Move(exportPath, tmpPath)
            end
            exportPath = tmpPath
        else
            if File.Exists(tmpPath) then
                File.Move(tmpPath, exportPath)
            end
        end

        local prefix = bCustomComponent and "" or "MainView."
        local content = ""

        if not Util.FileExists(exportPath) then
            if bCustomComponent then
                content = comTemplate
            else
                content = winTemplate
            end

            content = string.gsub(content, '{ClassName}', ClassName)
            content = string.gsub(content, '{BaseType}', bCustomComponent and classInfo.superClassName or "FGUIPanelFrom")
            writer:reset()
            writer:writeln(content)
            writer:save(exportPath)
        end

        local insertTxt = ""
        content = Util.ReadFile(exportPath)
        content = string.hTrim(string.gsub(content, "/%*%*.-".. customProperties.FileMark ..".-%*%*/", ""))
        local rangePattern = '%#region declare start, auto generate not change.*%#endregion declare end'
        local insertPattern = '%#endregion declare end'
        local replace = "#region declare start, auto generate not change\n\t\t#endregion declare end"
        local tmp = string.gsub(content, rangePattern, replace)
        for j=0,members.Count-1 do
            local memberInfo = members[j]
            local memberType = GetMemberType(memberInfo)
            insertTxt = insertTxt..string.format('public %s %s;', memberType, memberInfo.varName)
            if j ~= members.Count-1 then
                insertTxt = insertTxt..'\n\t\t'
            end
        end
        writer:reset()
        if Util.WriteInsert(writer, tmp, rangePattern, insertTxt, insertPattern,2) then
            writer:save(exportPath)
        end

        insertTxt = ""
        content = Util.ReadFile(exportPath)
        content = string.hTrim(string.gsub(content, "/%*%*.-".. customProperties.FileMark ..".-%*%*/", ""))
        rangePattern = '%#region define start, auto generate not change.*%#endregion define end'
        insertPattern = '%#endregion define end'
        replace = "#region define start, auto generate not change\n\t\t\t#endregion define end"
        tmp = string.gsub(content, rangePattern, replace)
        for j=0,members.Count-1 do
            local memberInfo = members[j]
            local memberType = GetMemberType(memberInfo)

            if memberInfo.group==0 then
                if publishSettings.getMemberByName then
                    insertTxt = insertTxt..string.format('%s = %sGetChild("%s") as %s;', memberInfo.varName, prefix, memberInfo.name, memberType)
                else
                    insertTxt = insertTxt..string.format('%s = %sGetChildAt(%s) as %s;', memberInfo.varName, prefix, memberInfo.index, memberType)
                end
            elseif memberInfo.group==1 then
                if publishSettings.getMemberByName then
                    insertTxt = insertTxt..string.format('%s = %sGetController("%s");', memberInfo.varName, prefix, memberInfo.name)
                else
                    insertTxt = insertTxt..string.format('%s = %sGetControllerAt(%s);', memberInfo.varName, prefix, memberInfo.index)
                end
            else
                if publishSettings.getMemberByName then
                    insertTxt = insertTxt..string.format('%s = %sGetTransition("%s");', memberInfo.varName, prefix, memberInfo.name)
                else
                    insertTxt = insertTxt..string.format('%s = %sGetTransitionAt(%s);', memberInfo.varName, prefix, memberInfo.index)
                end
            end
            if j ~= members.Count-1 then
                insertTxt = insertTxt..'\n\t\t\t'
            end
        end
        writer:reset()
        if Util.WriteInsert(writer, tmp, rangePattern, insertTxt, insertPattern,3) then
            writer:save(exportPath)
        end
    end
end

function CreateFormLogic()
    local templatePath = pluginDir..'/Template/UIFormLogic.txt'
    local template = Util.ReadFile(templatePath)
    if not template then
        Error("找不到UIFormLogic模板文件")
        return
    end
    
    local UIItemLogicPath = pluginDir..'/Template/UIItemLogic.txt'
        local UIItemLogictemplate = Util.ReadFile(UIItemLogicPath)
        if not UIItemLogictemplate then
            Error("找不到UIItemLogic模板文件")
            return
        end

    for i=0,classes.Count-1 do
        local classInfo = classes[i]
        local bCustomComponent = Util.IsCustomComponent(classInfo.className, codePkgName)
        local ClassName = string.firstToUpper(classInfo.className)
        local ViewName = ClassName..(bCustomComponent and 'Item' or 'Form')

        local dirPath = exportViewPath..'/'..ViewName
        local exportPath = exportViewPath..'/'..ViewName..'Logic.cs'
        local tmpPath = dirPath..'/'..ViewName..'Logic.cs'
        if classes.Count > 1 then
            if File.Exists(exportPath) then
                File.Move(exportPath, tmpPath)
            end
            exportPath = tmpPath
        else
            if Directory.Exists(dirPath) and File.Exists(tmpPath) then
                File.Move(tmpPath, exportPath)
                Directory.Delete(dirPath)
            end
        end

        if bCustomComponent then
            if not Util.FileExists(exportPath) then
               local content = string.gsub(UIItemLogictemplate, '{ClassName}', ClassName)
               content = string.gsub(content, '{ViewName}', ViewName)
               content = string.gsub(content, '{className}', string.firstToLower(classInfo.className))
               writer:reset()
               writer:writeln(content)
               writer:save(exportPath)
            end
        else
            if not Util.FileExists(exportPath) then
                local content = string.gsub(template, '{ClassName}', ClassName)
                content = string.gsub(content, '{ViewName}', ViewName)
                content = string.gsub(content, '{className}', string.firstToLower(classInfo.className))
                writer:reset()
                writer:writeln(content)
                writer:save(exportPath)
            end
        end
        
    end
end

function CheckFileDir()
    if not Directory.Exists(exportViewPath) then
        Directory.CreateDirectory(exportViewPath)
    end

    fprint("[publish setting] genCode open"..exportViewPath)
    if classes.Count > 1 then
        for i=0,classes.Count - 1 do
            local classInfo = classes[i]
            local bCustomComponent = Util.IsCustomComponent(classInfo.className, codePkgName)
            local className = bCustomComponent and (classInfo.className..'Item') or (classInfo.className..'Form')
            className = string.firstToUpper(className)
            local exportPathName = exportViewPath..'/'..className
            if not Directory.Exists(exportPathName) then
                Directory.CreateDirectory(exportPathName)
            end
        end
    end
end

-- 刷新下通用组件及当前导出文件class映射
function RefreshName2Class(codePkgName, classes)
    UpdateComComponentInfo(codePkgName, classes)
    for i=0,classes.Count-1 do
        local classInfo = classes[i]
        local ClassName = string.firstToUpper(classInfo.className)
        if Util.IsCustomComponent(classInfo.className, codePkgName) then
            tblName2Class[ClassName] = ClassName..'Item'
        end
    end
end

-- 通用组件需要记录下class信息
function UpdateComComponentInfo(codePkgName, classes)
    local outputPath = pluginDir.."/ComComponentInfo.txt"
    if codePkgName == "ComComponent" then
        -- 通用组件，记录下
        local file = io.open(outputPath, "w+")
        for i=0, classes.Count - 1 do
            local classInfo = classes[i]
            local ClassName = string.firstToUpper(classInfo.className)
            local ViewName = ClassName..'Item'
            local line = ClassName.."|"..ViewName.."\n"
            file:write(line)
        end
        file:close()
    else
        -- 非通用读取下记录的通用信息
        local file = io.open(outputPath, "r")
        local lines = string.split(file:read("*a"), "\n")
        for i=1,#lines do
            local line = lines[i]
            local lineArray = string.split(line, "|")
            if #lineArray == 2 then
                tblName2Class[lineArray[1]] = lineArray[2]
            end
        end
        file:close()
    end
end

-- 获取成员的类型字符串
function GetMemberType(memberInfo)
    local tmp = string.firstToUpper(memberInfo.type)
    local memberType = tblName2Class[tmp] and tblName2Class[tmp] or tmp
    -- 存在内嵌通用组件的类型，需要再根据res获取下类型
    if memberInfo.res then
        local type = tblName2Class[string.firstToUpper(memberInfo.res.name)]
        if type then
            memberType = type
        end
    end

    return memberType
end

return genCode