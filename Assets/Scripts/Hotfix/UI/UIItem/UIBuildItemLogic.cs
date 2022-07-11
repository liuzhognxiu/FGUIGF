using GameFramework.DataTable;
using GameFramework.Resource;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public class UIBuildItemLogic : MonoBehaviour
    {
        public Image BuildItemImg;

        public Text BuildName;

        public DREntity drEntity;

        public Button ItemButton;

        private void Start()
        {
            if (drEntity != null)
            {
                BuildName.text = drEntity.ShowName;
                GameEntry.Resource.LoadAsset(AssetUtility.GetUITextureAsset(drEntity.AssetName.Split('/')[1]),
                    Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                        (assetName, asset, duration, userTempData) =>
                        {
                            Texture2D temp = (Texture2D) asset;
                            BuildItemImg.sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height),
                                new Vector2(0.5f, 0.5f));
                        },
                        (assetName, status, errorMessage, userTempData) =>
                        {
                            Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", "BuildItem",
                                assetName, errorMessage);
                        }));
            }

            ItemButton = gameObject.GetComponent<Button>();
            if (ItemButton)
            {
                ItemButton.onClick.AddListener(ItemOnClick);
            }
        }

        private void ItemOnClick()
        {
            IDataTable<DRUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRUIForm>();
            DRUIForm drUIForm = dtUIForm.GetDataRow(105);
            if (drUIForm != null)
            {
                UIForm uiForm = GameEntry.UI.GetUIForm(AssetUtility.GetUIFormAsset(drUIForm.AssetName));
                if (uiForm)
                {
                    UIBuildSelectFormLogic uiBuildSelectFormLogic = (UIBuildSelectFormLogic) uiForm.Logic;
                    uiBuildSelectFormLogic.Bag.SetActive(false);
                }
            }

            GameEntry.Event.Fire(this, CreateBuildItemArgs.Create(drEntity, GameEntry.Entity.GenerateSerialBuildId()));
        }
    }
}