using System;

using System.Reflection;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class HuatuoHelper : HotfixHelperBase
    {
        private Assembly m_HotfixAssembly;
        private object m_HotfixGameEntry;
        public override object GetHotfixGameEntry => m_HotfixGameEntry;
        public override Assembly HotfixAssembly => m_HotfixAssembly;

        public override async ETTask Initialize()
        {
            TextAsset dllAsset = await GameEntry.Resource.LoadAssetAsync<TextAsset>(AssetUtility.GetHotfixDLLAsset("Hotfix.dll"));
            byte[] dll = dllAsset.bytes;
            Log.Info("hotfix dll加载完毕");
            m_HotfixAssembly = Assembly.Load(dll);
            string typeFullName = "MetaArea.Hotfix.HotfixGameEntry";
            Type hotfixInit = m_HotfixAssembly.GetType(typeFullName);
            m_HotfixGameEntry = Activator.CreateInstance(hotfixInit);
        }

        public override void Enter()
        {
            var start = m_HotfixGameEntry.GetType().GetMethod("Start",BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            start?.Invoke(m_HotfixGameEntry, null);
        }

        public override void ShutDown()
        {
        }
    }
}