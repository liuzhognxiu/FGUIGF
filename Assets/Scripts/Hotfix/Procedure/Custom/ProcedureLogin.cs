using GameFramework.Event;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityStandardAssets.Cameras;
using ProcedureOwner = GameFramework.Fsm.IFsm<MetaArea.Hotfix.ProcedureManager>;

namespace MetaArea.Hotfix
{
    public class ProcedureLogin : ProcedureBase
    {
        private FGUIGameStartFormLogic gameStart;
        
        private bool _isChangeToMenu = false;

        protected override async void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            await GameEntry.Scene.LoadSceneAsync(AssetUtility.GetSceneAsset("Login"));

            FGUIForm form = await GameEntry.FGUI.OpenFGUIFormAsync((int)UIFormId.GameStartForm, this);
            gameStart = form.Logic as FGUIGameStartFormLogic;
            if (gameStart == null)
            {
                Log.Error("Login form is invalid when entering procedure login.");
                return;
            }
            
            GameEntry.Event.Subscribe(JoinToGameArgs.EventId, ChangeToMenu);
        }

        private void ChangeToMenu(object sender, GameEventArgs e)
        {
            _isChangeToMenu = true;
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(JoinToGameArgs.EventId, ChangeToMenu);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (_isChangeToMenu)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Menu"));
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }
    }
}