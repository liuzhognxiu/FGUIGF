using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace MetaArea.Hotfix
{
    public partial class UIMainMenuFormLogic : UGuiFormLogic
    {
        private bool IsOpen = false;

        protected override void OnInit(object userdata)
        {
            base.OnInit(userdata);
            GetBindComponents(gameObject);
        }
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            
            m_Button_WorldMap.onClick.AddListener(OnNoOpenClick);
            m_Button_NTFShop.onClick.AddListener(OnNoOpenClick);
            m_Button_WorldGroup.onClick.AddListener(OnNoOpenClick);
            m_Button_Find.onClick.AddListener(OnNoOpenClick);
            m_Button_Set.onClick.AddListener(OnNoOpenClick);
            m_Button_OpenMenu.onClick.AddListener(OnOpenOrClosePanel);

        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            m_Button_WorldMap.onClick.RemoveListener(OnNoOpenClick);
            m_Button_NTFShop.onClick.RemoveListener(OnNoOpenClick);
            m_Button_WorldGroup.onClick.RemoveListener(OnNoOpenClick);
            m_Button_Find.onClick.RemoveListener(OnNoOpenClick);
            m_Button_Set.onClick.RemoveListener(OnNoOpenClick);
            m_Button_OpenMenu.onClick.RemoveListener(OnOpenOrClosePanel);
        }
#if UNITY_EDITOR || UNITY_STANDALONE
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            {
                if (Input.GetKeyUp(KeyCode.E))
                {
                    GameEntry.UI.OpenUIForm(UIFormId.TouchPlayerForm);
                }
            }
        }
#endif

        private void OnOpenOrClosePanel()
        {
            IsOpen = !IsOpen;
            float x = IsOpen ? -381 : 0;
            DOTween.To(() => m_RectTransform_MenuPanel.anchoredPosition, v => { m_RectTransform_MenuPanel.anchoredPosition = v; },
                new Vector2(x, 0), .2f);

        }

        private async void OnNoOpenClick()
        {
            var result = await GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = GameEntry.Localization.GetString("NotOpen"),
                Message = GameEntry.Localization.GetString("Coming Soon"),
            });
        }
    }
}
