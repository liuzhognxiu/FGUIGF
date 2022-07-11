using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public partial class UIDialogFormLogic : UGuiFormLogic
    {
        private ETTask<PopResult> m_ResultTask; 
        private bool m_PauseGame = false;
        public void OnConfirmButtonClick()
        {
            Close();

            if (m_ResultTask == null) return;
            ETTask<PopResult> temp = m_ResultTask;
            temp.SetResult(PopResult.Confirm);
            m_ResultTask = null;
        }

        public void OnCancelButtonClick()
        {
            Close();

            if (m_ResultTask == null) return;
            ETTask<PopResult> temp = m_ResultTask;
            temp.SetResult(PopResult.Cancel);
            m_ResultTask = null;
        }

        public void OnOtherButtonClick()
        {
            Close();

            if (m_ResultTask == null) return;
            ETTask<PopResult> temp = m_ResultTask;
            temp.SetResult(PopResult.Other);
            m_ResultTask = null;
        }

        protected override void OnInit(object userdata)
        {
            base.OnInit(userdata);
            GetBindComponents(gameObject);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            DialogParams dialogParams = (DialogParams) userData;
            if (dialogParams == null || dialogParams.Task == null)
            {
                Log.Warning("PopFormParams is invalid.");
                return;
            }

            RefreshDialogMode(dialogParams.Mode);
            m_TMP_Text_Title.text = dialogParams.Title;
            m_TMP_Text_Message.text = dialogParams.Message;
            m_PauseGame = dialogParams.PauseGame;
            RefreshPauseGame();
			
            RefreshConfirmText(dialogParams.ConfirmText);
            RefreshCancelText(dialogParams.CancelText);
            RefreshOtherText(dialogParams.OtherText);
            m_ResultTask = dialogParams.Task;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            if (m_PauseGame)
            {
                GameEntry.Base.ResumeGame();
            }
            m_TMP_Text_Title.text = string.Empty;
            m_TMP_Text_Message.text = string.Empty;
            m_PauseGame = false;
            RefreshConfirmText(string.Empty);
            RefreshCancelText(string.Empty);
            RefreshOtherText(string.Empty);

            base.OnClose(isShutdown, userData);

        }
		
        private void RefreshDialogMode(int mode)
        {
            switch (mode)
            {
                case 1:
                {
                    m_Transform_Confirm.gameObject.SetActive(true);
                    m_Transform_Confirm.anchoredPosition = Vector2.zero;
                    m_Transform_ConfirmBorder.sizeDelta = new Vector2(400, 60);
					
                    m_Transform_Cancel.gameObject.SetActive(false);
                    m_Transform_Other.gameObject.SetActive(false);
                }
                    break;
                case 2:
                {
                    m_Transform_Confirm.gameObject.SetActive(true);
                    m_Transform_Confirm.anchoredPosition = new Vector2(-150,0);
                    m_Transform_ConfirmBorder.sizeDelta = new Vector2(280, 60);
			
                    m_Transform_Cancel.gameObject.SetActive(true);
                    m_Transform_Cancel.anchoredPosition = new Vector2(150,0);
                    m_Transform_CancelBorder.sizeDelta = new Vector2(280, 60);
					
                    m_Transform_Other.gameObject.SetActive(false);
                }
                    break;
                case 3:
                {
                    m_Transform_Confirm.gameObject.SetActive(true);
                    m_Transform_Confirm.anchoredPosition = new Vector2(-200,0);
                    m_Transform_ConfirmBorder.sizeDelta = new Vector2(180, 60);
					
                    m_Transform_Cancel.gameObject.SetActive(true);
                    m_Transform_Cancel.anchoredPosition = Vector2.zero;
                    m_Transform_CancelBorder.sizeDelta = new Vector2(180, 60);
					
                    m_Transform_Other.gameObject.SetActive(true);
                    m_Transform_Other.anchoredPosition = new Vector2(200,0);
                    m_Transform_OhterBorder.sizeDelta = new Vector2(180, 60);
                }
                    break;
            }
        }
		
        private void RefreshPauseGame()
        {
            if (m_PauseGame)
            {
                GameEntry.Base.PauseGame();
            }
        }

        private void RefreshConfirmText(string confirmText)
        {
            if (string.IsNullOrEmpty(confirmText))
            {
                confirmText = GameEntry.Localization.GetString("Dialog.ConfirmButton");
            }

            m_TMP_Text_ConFirmText.text = confirmText;
        }

        private void RefreshCancelText(string cancelText)
        {
            if (string.IsNullOrEmpty(cancelText))
            {
                cancelText = GameEntry.Localization.GetString("Dialog.CancelButton");
            }

            m_TMP_Text_CancelText.text = cancelText;
        }

        private void RefreshOtherText(string otherText)
        {
            if (string.IsNullOrEmpty(otherText))
            {
                otherText = GameEntry.Localization.GetString("Dialog.OtherButton");
            }

            m_TMP_Text_OtherText.text = otherText;
        }
    }
}