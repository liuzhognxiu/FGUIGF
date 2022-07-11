using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public static partial class AwaitableExtensions
    {
        /// <summary>
        /// 打开界面（可等待）
        /// </summary>
        public static ETTask<UIForm> OpenUIFormAsync(this UIComponent uiComponent, UIFormId uiFormId, object userData = null)
        {
            return uiComponent.OpenUIFormAsync((int) uiFormId, userData);
        }
    }
}