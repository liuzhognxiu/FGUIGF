function string.firstToLower(input)
    return (input:gsub("^%u",string.lower))
end

function string.firstToUpper(input)
    return (input:gsub("^%l",string.upper))
end

function string.rTrim(input)
    return (string.gsub(input, "[ \t\n\r]+$", ""))
end

function string.hTrim(input)
    return (string.gsub(input, "^[ \t\n\r]+", ""))
end

function string.isEmpty(input)
    return input == nil or input == ''
end

function string.PathConversion(input)
    local path = ""
    for i=1,#input do
        --获取当前下标字符串
        local tmp = string.sub(input,i,i)
        --如果为'\\'则替换
        if tmp=='\\' then
            path = path..'/'
        else
            path = path..tmp
        end
    end
    return path
end

function string.contains(input, target)
    local t = {}
    local l = {}
    local index = 0
    for i = 1, string.len(input) do
        table.insert(t, string.byte(string.sub(input, i, i)))
    end

    for i = 1, string.len(target) do
        table.insert(l, string.byte(string.sub(target, i, i)))
    end
    if #l > #t then
        return false
    end

    for k, v1 in pairs(t) do
        index = index + 1
        if v1 == l[1] then
            local bContains = true
            for i = 1, #l do
                if t[index + i - 1] ~= l[i] then
                    bContains = false
                end
            end
            if bContains then
                return bContains
            end
        end
    end
    return false
end

function string.split(input, delimiter)
    input = tostring(input)
    delimiter = tostring(delimiter)
    if (delimiter=='') then return false end
    local pos,arr = 0, {}
    for st,sp in function() return string.find(input, delimiter, pos, true) end do
        table.insert(arr, string.sub(input, pos, st - 1))
        pos = sp + 1
    end
    table.insert(arr, string.sub(input, pos))
    return arr
end