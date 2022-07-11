using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityStandardAssets.CrossPlatformInput;
using ProcedureOwner = GameFramework.Fsm.IFsm<MetaArea.Hotfix.ProcedureManager>;


namespace MetaArea.Hotfix
{
    public class ProcedureMain : ProcedureBase
    {
        private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();

        private GameBase m_CurrentGame = null;

        private bool gotoMainMenu = false;

        public void GotoMenu()
        {
            gotoMainMenu = true;
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            m_Games.Add(GameMode.Battle, new FreeLookGame());
        }

        protected override void OnDestroy(ProcedureOwner procedureOwner)
        {
            base.OnDestroy(procedureOwner);

            m_Games.Clear();
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            gotoMainMenu = false;
            GameMode gameMode = (GameMode) procedureOwner.GetData<VarByte>("GameMode").Value;
            m_CurrentGame = m_Games[gameMode];
            m_CurrentGame.Initialize();

#if UNITY_IPHONE || UNITY_ANDROID
            GameEntry.UI.OpenUIForm(UIFormId.TouchPlayerForm, this);
#else
            GameEntry.UI.OpenUIForm(UIFormId.AboutForm, this);
#endif
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            if (m_CurrentGame != null)
            {
                m_CurrentGame.Shutdown();
                m_CurrentGame = null;
            }

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_CurrentGame != null)
            {
                m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
                return;
            }

            if (!gotoMainMenu)
            {
                gotoMainMenu = true;
                ChangeState<ProcedureMenu>(procedureOwner);
            }
        }
    }
}