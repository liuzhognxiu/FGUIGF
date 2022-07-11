using GameFramework;
using GameFramework.Event;

namespace MetaArea
{
    public class OpenFGUIFormFailureEventArgs:GameEventArgs
    {
        /// <summary>
        /// 打开界面失败事件编号。
        /// </summary>
        public static readonly int EventId = typeof(OpenFGUIFormFailureEventArgs).GetHashCode();
        public override int Id => EventId;

        public int SerialId
        {
            get;
            private set;
        }

        public string ErrorMassage
        {
            get;
            private set;
        }

        public string FGuiFormAssetName
        {
            get;
            private set;
        }

        public OpenFGUIFormFailureEventArgs()
        {
            SerialId = 0;
            ErrorMassage = null;
            FGuiFormAssetName = null;
        }

        public static OpenFGUIFormFailureEventArgs Create(int _serialId, string _fGuiFormAssetName, string _errorMassage)
        {
            var l_openFGuiFormFailureEventArgs= ReferencePool.Acquire<OpenFGUIFormFailureEventArgs>();
            l_openFGuiFormFailureEventArgs.SerialId = _serialId;
            l_openFGuiFormFailureEventArgs.FGuiFormAssetName = _fGuiFormAssetName;
            l_openFGuiFormFailureEventArgs.ErrorMassage = _errorMassage;
            return l_openFGuiFormFailureEventArgs;
        }

        public override void Clear()
        {
            SerialId = 0;
            ErrorMassage = null;
            FGuiFormAssetName = null;
        }
    }
}