#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Threading;
#endif

namespace MetaArea
{
    public sealed class ObjectPreview
    {
#if UNITY_EDITOR

        public static readonly string savePath = "Assets/GameMain/UI/UISprites/Items";
        
        public static string CreateAndSaveAssetPreview(GameObject Go)
        {
            if (Go == null)
            {
                Debug.LogError("Can't create preview for null object");
                return "";
            }

            Texture2D preview = AssetPreview.GetAssetPreview(Go);

            int tm = 0;
            while (preview == null)
            {
                Thread.Sleep(100);
                preview = AssetPreview.GetAssetPreview(Go);
                tm += 100;
                if (tm >= 3000) 
                    break;
            }

            if (preview == null)
            {
                Debug.LogError("Unable to create preview for object: " + Go.name);
                return "";
            }

            CreateSaveFolder();

            var bytes = preview.EncodeToPNG();
            string name = Go.name + ".png";
            string savePos = savePath + "/" + name;

            if (File.Exists(name)) File.Delete(name);
            File.WriteAllBytes(savePos, bytes);
            Debug.Log("Saved preview: " + name);

            AssetDatabase.Refresh();


            TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath(savePos);
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            EditorUtility.SetDirty(importer);
            AssetDatabase.ImportAsset(savePos);

            AssetDatabase.Refresh();

            return savePos;

        }

        static void CreateSaveFolder()
        {
            if (!Directory.Exists(savePath))
            {
                Debug.Log("Created directory: " + savePath);
                Directory.CreateDirectory(savePath);
                AssetDatabase.Refresh();
            }
        }

#endif
    }
}
