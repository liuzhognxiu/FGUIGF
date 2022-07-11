using UnityEngine;
using UnityEditor;
using System.IO;

namespace MetaArea.Editor.BuildSystem
{
    public class ObjectPreviewWindow : EditorWindow
    {
        private static readonly string LoadPath = "Assets/GameMain/Entities/BuildItem";

        [MenuItem("Build System/Create Objects Preview")]
        public static void CreateObjectsPreview()
        {
            string[] guis = null;
            guis = AssetDatabase.FindAssets("t:Prefab", new string[] {LoadPath});
            for (int i = 0; i < guis.Length; i++)
            {
                GameObject temp = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guis[i]));
                ObjectPreview.CreateAndSaveAssetPreview(temp);
                MetaArea.BuildItem obj = ScriptableObject.CreateInstance<MetaArea.BuildItem>();
                if (temp != obj.Prefab)
                {
                    obj.Prefab = temp;

                    if (temp == null) return;

                    if (string.IsNullOrEmpty( obj.Name.Trim()))
                        obj.SetAutomaticName(temp);
                    if (obj.UiPicture == null)
                        obj.SetAutomaticPreview(temp);
                    if (obj.ghostMaterial == null)
                        obj.SetAutomaticMaterial();

                    obj.CreateGhost();
                }
            }
        }
    }
}