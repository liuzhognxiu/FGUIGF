using GameFramework.DataTable;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public static class FGUIExtension
    {
        public static int? OpenUIForm(this FGUIComponent fguiComponent, int uiFormId, object userData = null)
        {
            IDataTable<DRFGUIForm> dtUIForm = GameEntry.DataTable.GetDataTable<DRFGUIForm>();
            DRFGUIForm drUIForm = dtUIForm.GetDataRow(uiFormId);
            if (drUIForm == null)
            {
                Log.Warning("Can not load FGUI form '{0}' from data table.", uiFormId.ToString());
                return null;
            }
            string PackageName = AssetUtility.GetUIBytesAsset(drUIForm.AssetName);
            string PrefabName = AssetUtility.GetFGUIFormPrefabAsset(drUIForm.AssetName);
            return fguiComponent.OpenUIForm(PrefabName, PackageName);
        }
    }
}