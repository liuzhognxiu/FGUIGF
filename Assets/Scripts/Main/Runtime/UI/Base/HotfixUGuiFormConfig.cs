using System;
using GameFramework;
using UnityEngine;

namespace MetaArea
{
    /// <summary>
    /// 热更新层UGUI界面
    /// </summary>
    [DisallowMultipleComponent]
    public class HotfixUGuiFormConfig : MonoBehaviour
    {
        /// <summary>
        /// 对应的热更新层UGUI界面类名
        /// </summary>
        [SerializeField] private string m_HotfixUGuiFormName;

        public UGuiFormLogic GetOrAddUIFormLogic()
        {
            UGuiFormLogic uGuiForm = gameObject.GetComponent<UGuiFormLogic>();
            if (uGuiForm == null)
            {
                string hotfixUGuiFormFullName = Utility.Text.Format("{0}.{1}", "MetaArea.Hotfix", m_HotfixUGuiFormName);
                Type type = GameEntry.Hotfix.GetHotfixAssembly().GetType(hotfixUGuiFormFullName);
                uGuiForm = gameObject.AddComponent(type) as UGuiFormLogic;
            }

            return uGuiForm;
        }
    }
}