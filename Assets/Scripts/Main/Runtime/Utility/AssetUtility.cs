using GameFramework;

namespace MetaArea
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Configs/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDataTableAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/DataTables/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDictionaryAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.{2}", GameEntry.Localization.Language.ToString(), assetName, fromBytes ? "bytes" : "xml");
        }
        
        public static string GetFontAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Fonts/FontAssets/{0}/{1}.asset",GameEntry.Localization.Language, assetName);
        }

        public static string GetSceneAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Scenes/{0}.unity", assetName);
        }

        public static string GetMusicAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Music/{0}.mp3", assetName);
        }

        public static string GetSoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
        }

        public static string GetEntityAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Entities/{0}.prefab", assetName);
        }    
        
        public static string GetPlayerAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Entities/Player/{0}.prefab", assetName);
        }

        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UIForms/{0}.prefab", assetName);
        }

        public static string GetUIItemAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UIItems/{0}.prefab", assetName);
        }
        
        public static string GetUISoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
        }
        
        public static string GetUITextureAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UISprites/Items/{0}.png", assetName);
        }
        
        public static string GetHotfixDLLAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/HotfixDLL/{0}.bytes", assetName);
        }
        
        public static string GetUIBytesAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/FGUIAssets/{0}/{0}_fui.bytes", assetName);
        }
        
        public static string GetUIAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/FGUIForms/{0}/{0}.png", assetName);
        }
        
        public static string GetFGUIFormPrefabAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/FGUIForms/{0}Form.prefab", assetName);
        }
        
        public static string GetUIAsset(string packageName, string assetName, string extension)
        {
            return Utility.Text.Format("Assets/ArtRes/LoadResources/UI/UI{0}/{1}{2}", packageName, assetName, extension);
        }
    }
}
