Util = {}

function Util.DicToTable(CSharpDic)
    --将C#的Dic转成Lua的Table
    local dic = {}
    if CSharpDic then
        local iter = CSharpDic:GetEnumerator()
        while iter:MoveNext() do
            local k = iter.Current.Key
            local v = iter.Current.Value
            dic[k] = v
        end
    end
    return dic
end

function Util.FileExists(path)
    local file = io.open(path, "rb")
    if file then file:close() end
    return file ~= nil
end

function Util.ReadFile(path)
    local file = io.open(path, "r")
    if file then
        file:seek("set",0)
        local content = file:read("*a")
        io.close(file)
        return content
    end
    return nil
end

function Util.WriteInsert(writer, src, rangePattern, insertStr, insertPattern, indentInc)
    local i,j = string.find(src, rangePattern)
    if not i then
        Error("插入"..insertStr.."失败")
        return false
    end
    local part1 = string.sub(src, 0, j)
    local part2 = string.sub(src, j + 1)
    i,j = string.find(part1, insertPattern)
    writer:writeln(string.rTrim(string.sub(part1, 0, i - 1)))
    for index = 1, indentInc do
        writer:incIndent()
    end
    writer:writeln(insertStr)
    writer:writeln(string.hTrim(string.sub(part1, i)))
    writer:writeln(string.hTrim(part2))
    return true
end

function Util.IsCustomComponent(className, packageName)
    return string.lower(className) ~= string.lower(packageName)
end