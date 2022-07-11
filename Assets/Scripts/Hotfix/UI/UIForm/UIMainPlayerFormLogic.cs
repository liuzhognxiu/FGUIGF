using GameFramework.Resource;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public partial class UIMainPlayerFormLogic : UGuiFormLogic
    {
        [SerializeField] private GameObject MyCharacter = null;

        private string PlayerName = "Male Mage 01";

        private TurnTarget turnTarget;

        protected override void OnInit(object userdata)
        {
            base.OnInit(userdata);

            GetBindComponents(gameObject);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            turnTarget = this.m_Camera_Camera.GetComponent<TurnTarget>();

            m_Button_PlayerManager.onClick.AddListener(OnNoOpenClick);
            m_Button_EquipManager.onClick.AddListener(OnNoOpenClick);
            m_Button_ItemManager.onClick.AddListener(OnNoOpenClick);


            GameEntry.Resource.LoadAsset(AssetUtility.GetPlayerAsset(PlayerName), Constant.AssetPriority.UIFormAsset,
                new LoadAssetCallbacks(
                    (assetName, asset, duration, E) =>
                    {
                        MyCharacter = Instantiate((GameObject)asset, turnTarget.PlayerPos);
                        turnTarget.target = MyCharacter.transform;
                    },
                    (assetName, status, errorMessage, E) =>
                    {
                        Log.Error("Can not load PlayerModel '{0}' from '{1}' with error message '{2}'.", PlayerName,
                            assetName, errorMessage);
                    }));

            GameEntry.UI.OpenUIForm(UIFormId.MainMenuForm);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            m_Button_PlayerManager.onClick.RemoveListener(OnNoOpenClick);
            m_Button_EquipManager.onClick.RemoveListener(OnNoOpenClick);
            m_Button_ItemManager.onClick.RemoveListener(OnNoOpenClick);
        }


        private void OnNoOpenClick()
        {
            GameEntry.UI.OpenDialog(new DialogParams()
            {
                Mode = 2,
                Title = GameEntry.Localization.GetString("NotOpen"),
                Message = GameEntry.Localization.GetString("Coming Soon"),
            });
        }
    }
}