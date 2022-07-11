using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public partial class UIMenuFormLogic : UGuiFormLogic
    {
        [SerializeField]
        private GameObject m_QuitButton = null;

        private ProcedureMenu m_ProcedureMenu = null;

        protected override void OnInit(object userdata)
        {
            base.OnInit(userdata);
            GetBindComponents(gameObject);
        }
        public void OnStartButtonClick()
        {
            m_ProcedureMenu.StartGame();
        }

        public void OnSettingButtonClick()
        {
        }

        public void OnAboutButtonClick()
        {
            GameEntry.UI.OpenUIForm(UIFormId.AboutForm);
        }

        public async void OnQuitButtonClick()
        {
            var result = await GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = GameEntry.Localization.GetString("AskQuitGame.Title"),
                Message = GameEntry.Localization.GetString("AskQuitGame.Message"),
            });
            switch (result)
            {
                case PopResult.Confirm:
                    UnityGameFramework.Runtime.GameEntry.Shutdown(ShutdownType.Quit);
                    break;
                case PopResult.Cancel:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);

            m_ProcedureMenu = (ProcedureMenu) userData;
            if (m_ProcedureMenu == null)
            {
                Log.Warning("ProcedureMenu is invalid when open MenuForm.");
                return;
            }

            m_QuitButton.SetActive(Application.platform != RuntimePlatform.IPhonePlayer);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(bool isShutdown, object userData)
#else
        protected internal override void OnClose(bool isShutdown, object userData)
#endif
        {
            m_ProcedureMenu = null;

            base.OnClose(isShutdown, userData);
        }
    }
}
