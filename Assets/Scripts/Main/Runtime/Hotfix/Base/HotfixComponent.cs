using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class HotfixComponent : GameFrameworkComponent
    {
        [SerializeField] private string m_HotfixHelperTypeName = "MetaArea.NotHotfixHelper";
        [SerializeField] private HotfixHelperBase m_CustomHotfixHelper;
        private void Start()
        {
            m_CustomHotfixHelper = Helper.CreateHelper(m_HotfixHelperTypeName, m_CustomHotfixHelper);
            if (m_CustomHotfixHelper == null)
            {
                Log.Error("Can not create hotfix helper.");
                return;
            }

            m_CustomHotfixHelper.name = "Hotfix Helper";
            Transform customHelperTrans = m_CustomHotfixHelper.transform;
            customHelperTrans.SetParent(transform);
            customHelperTrans.localScale = Vector3.one;
        }

        public async void StartHotfixLogic()
        {
            await m_CustomHotfixHelper.Initialize();
            m_CustomHotfixHelper.Enter();
        }
        
        public object GetHotfixGameEntry()
        {
            return m_CustomHotfixHelper.GetHotfixGameEntry;
        }
        
        public Assembly GetHotfixAssembly()
        {
            return m_CustomHotfixHelper.HotfixAssembly;
        }
    }
}