using GameFramework.Event;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityStandardAssets.Cameras;
using ProcedureOwner = GameFramework.Fsm.IFsm<MetaArea.Hotfix.ProcedureManager>;

namespace MetaArea.Hotfix
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        
        public void StartGame()
        {
            m_StartGame = true;
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            m_StartGame = false;

        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartGame)
            {
                procedureOwner.SetData<VarByte>("GameMode", (byte)GameMode.Battle);
                ChangeState<ProcedureMain>(procedureOwner);
            }
        }


        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }
        }
    }
}
