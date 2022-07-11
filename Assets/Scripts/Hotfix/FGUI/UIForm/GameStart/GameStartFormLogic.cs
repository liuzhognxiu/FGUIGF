/** generated class success. **/

using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public partial class FGUIGameStartFormLogic : FGUIPanelFrom
    {

        private ProcedureLogin _procedureLogin;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            //流程注册测试
            _procedureLogin = userData as ProcedureLogin;
            if (_procedureLogin == null)
            {
                Log.Error("Procedure login is invalid when openning login form.");
                return;
            }
            GetBindComponents();
            
            btnStart.onClick.Add(BtnGameStart);
        }

        private void BtnGameStart()
        {
            GameEntry.Event.Fire(this, JoinToGameArgs.Create());
        }


        protected override void OnOpen()
        {
            base.OnOpen();
        }

        protected override void OnClose()
        {
            base.OnClose();
            btnStart.onClick.Remove(BtnGameStart);
        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
        }
    }
}