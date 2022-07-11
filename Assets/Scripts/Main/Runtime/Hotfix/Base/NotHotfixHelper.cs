using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MetaArea.Hotfix;

namespace MetaArea
{
    public class NotHotfixHelper : HotfixHelperBase
    {
        private Assembly m_HotfixAssembly;
        private object m_HotfixGameEntry;
        public override object GetHotfixGameEntry => m_HotfixGameEntry;
        public override Assembly HotfixAssembly => m_HotfixAssembly;

        public override async ETTask Initialize()
        {
            m_HotfixAssembly = Assembly.Load("Hotfix");
            string typeFullName = "MetaArea.Hotfix.HotfixGameEntry";
            Type hotfixGameEntryType = m_HotfixAssembly.GetType(typeFullName);
            m_HotfixGameEntry = Activator.CreateInstance(hotfixGameEntryType);
            await ETTask.CompletedTask;
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