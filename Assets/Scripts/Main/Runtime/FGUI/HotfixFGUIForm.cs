using System;
using GameFramework;
using UnityEngine;

namespace MetaArea
{
    /// <summary>
    /// 热更新层FGUI界面
    /// </summary>
    [DisallowMultipleComponent]
    public class HotfixFGUIForm :  MonoBehaviour
    {
        /// <summary>
        /// 对应的热更新层FGUI界面类名
        /// </summary>
        [SerializeField] private string m_HotfixFGuiFormName;
        
        public FGUIFormLogic GetOrAddUIFormLogic()
        {
            FGUIFormLogic uGuiForm = gameObject.GetComponent<FGUIFormLogic>();
            if (uGuiForm == null)
            {
                string hotfixUGuiFormFullName = Utility.Text.Format("{0}.{1}", "MetaArea.Hotfix", m_HotfixFGuiFormName);
                Type type = GameEntry.Hotfix.GetHotfixAssembly().GetType(hotfixUGuiFormFullName);
                uGuiForm = gameObject.AddComponent(type) as FGUIFormLogic;
            }

            return uGuiForm;
        }
    }
}