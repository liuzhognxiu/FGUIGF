using GameFramework.DataTable;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public partial class UIBuildSelectFormLogic : UGuiFormLogic
    {
        
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            GetBindComponents(gameObject);
            m_Button_OpenBag.onClick.AddListener(OpenBagOnClick);
        }

        private void OpenBagOnClick()
        {
            m_Transform_Bag.gameObject.SetActive(! m_Transform_Bag.gameObject.activeSelf);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            ShowButtonItems();
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetKeyUp(KeyCode.E))
            {
                OpenBagOnClick();
            }
#endif
        }


        private void ShowButtonItems()
        {
            for (int i = 0; i < 18; i++)
            {
                int TypeId = 10000 + i;
                IDataTable<DREntity> drEntities = GameEntry.DataTable.GetDataTable<DREntity>();
                DREntity drEntity = drEntities.GetDataRow(TypeId);

                GameEntry.Resource.LoadAsset(AssetUtility.GetUIItemAsset("UIBuildItem"), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                    (assetName, asset, duration, userTempData) =>
                    {
                        var BuildItem = Object.Instantiate((GameObject) asset, this.transform);
                        BuildItem.transform.SetParent(m_Transform_Layout, false);
                        BuildItem.transform.localScale = Vector3.one;
                        BuildItem.transform.eulerAngles = Vector3.zero;
                        BuildItem.transform.localPosition = Vector3.zero;
                        BuildItem.gameObject.GetComponent<UIBuildItemLogic>().drEntity = drEntity;
                    },
                    (assetName, status, errorMessage, userTempData) =>
                    {
                        Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", "BuildItem", assetName, errorMessage);
                    }));
            }
        }
    }
}