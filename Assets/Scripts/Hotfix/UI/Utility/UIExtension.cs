using GameFramework.DataTable;
using GameFramework.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public static class UIExtension
    {
        public static bool HasUIForm(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            return uiComponent.HasUIForm((int)uiFormId, uiGroupName);
        }
        
        public static UGuiFormLogic GetUIForm(this UIComponent uiComponent, UIFormId uiFormId, string uiGroupName = null)
        {
            return uiComponent.GetUIForm((int)uiFormId, uiGroupName);
        }
        public static int? OpenUIForm(this UIComponent uiComponent, UIFormId uiFormId, object userData = null)
        {
            return uiComponent.OpenUIForm((int)uiFormId, userData);
        }
    }
}
