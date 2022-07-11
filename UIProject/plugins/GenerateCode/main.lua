require(PluginPath..'/Util')
require(PluginPath..'/Extend')

local genCode = require(PluginPath..'/GenCode_CSharp')

function onPublish(handler)
    genCode(handler, PluginPath) --do it myself
end

function onDestroy()
-------do cleanup here-------
end