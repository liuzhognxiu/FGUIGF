using GameFramework;
using GameFramework.Event;

namespace MetaArea
{
    public class OpenFGUIFormSuccessEventArgs: GameEventArgs
    {
        /// <summary>
        /// 打开界面成功事件编号。
        /// </summary>
        public static readonly int EventId = typeof(OpenFGUIFormSuccessEventArgs).GetHashCode();

        public override int Id => EventId;
        public FGUIForm fguiForm
        {
            get;
            private set;
        }

        public OpenFGUIFormSuccessEventArgs()
        {
            fguiForm = null;
        }

        public static OpenFGUIFormSuccessEventArgs Create(FGUIForm fguiForm)
        {
            var l_openFGuiFormSuccessEventArgs= ReferencePool.Acquire<OpenFGUIFormSuccessEventArgs>();
            l_openFGuiFormSuccessEventArgs.fguiForm = fguiForm;
            return l_openFGuiFormSuccessEventArgs;
        }
        
        public override void Clear()
        {
            fguiForm = null;
        }
    }
}