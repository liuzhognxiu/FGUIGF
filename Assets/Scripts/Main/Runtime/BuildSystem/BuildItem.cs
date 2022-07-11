using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Collections.Generic;
#endif

namespace MetaArea
{
    [CreateAssetMenu(fileName = "BuildItem", menuName = "Building/Item", order = 1)]
    public class BuildItem : ScriptableObject
    {
        public string Name = "";
        public Sprite UiPicture;
        public GameObject Prefab;
        public GameObject ghostCache;

        public bool IsHave = true;

        public bool isValid()
        {
            if (Name == "")
                Debug.LogWarning("Build Item name is null");

            if (UiPicture == null)
            {
                Debug.LogError("item: " + name + " has null UiPicture!");
                return false;
            }

            if (Prefab == null)
            {
                Debug.LogError("item: " + name + " has null Prefab!");
                return false;
            }

            if (ghostCache == null)
            {
                Debug.LogError("item: "+ name +" has no cached ghost please regenerate it");
            }
            
            return true;
        }

#if UNITY_EDITOR

        const string cachePath = "Assets/GameMain/Entities/BuildItemGhost";

        public Material ghostMaterial;

        public void CreateGhost()
        {
            DeleteOldGhost();

            if (Prefab == null) return;

            CreateFolder(cachePath);
            
            GameObject g = Instantiate(Prefab);

            RemoveAllExceptMeshes(g);

            ReplaceMaterials(g);
            
#pragma warning disable 618
            ghostCache = PrefabUtility.CreatePrefab(cachePath +"/"+ Prefab.name + "_ghost.prefab", g);
#pragma warning restore 618
            ghostCache.transform.localRotation = Prefab.transform.localRotation;
            DestroyImmediate(g);
            
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();

        }

        public void DeleteOldGhost()
        {
            if (ghostCache == null) return;

            AssetDatabase.DeleteAsset(cachePath + "/" + ghostCache.name);
            AssetDatabase.Refresh();
        }

        void RemoveAllExceptMeshes(GameObject g)
        {
            var comps = g.GetComponentsInChildren<Component>();

           for (int i = 0; i < comps.Length; i++)
           {
                if (comps[i] is MeshRenderer || comps[i] is SkinnedMeshRenderer ||
                    comps[i] is Transform || comps[i] is MeshFilter /*|| comps[i] is TextMesh*/ )
                {
                    continue;
                }
                DestroyImmediate(comps[i]);
           }
            
        }

        void ReplaceMaterials(GameObject g)
        {
            if (ghostMaterial == null) return;  

            foreach (var mr in g.GetComponentsInChildren<MeshRenderer>())
            {
                mr.sharedMaterials = createMarArr(mr.sharedMaterials.Length);
            }

            foreach (var mr in g.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                mr.sharedMaterials = createMarArr(mr.sharedMaterials.Length);
            }

        }


        Material[] createMarArr(int cout)
        {
            Material[] mat = new Material[cout];
            for (int i = 0; i < mat.Length; i++)
            {
                mat[i] = ghostMaterial;
            }

            return mat;
        }


        void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Debug.Log("Created directory: " + path);
                Directory.CreateDirectory(path);
                AssetDatabase.Refresh();

#pragma warning disable 618
                PrefabUtility.CreatePrefab(cachePath + "/_DONT_TOUCH_THIS_FOLDER.prefab", new GameObject());
#pragma warning restore 618
            }
        }


        public void SetAutomaticName(GameObject g)
        {
            string res = g.name;

            List<int> breakPos = new List<int>();
            //find parts line nP cR ...
            for (int i = 1; i < res.Length; i++)
            {
                if (char.IsUpper(res[i]) && char.IsLower(res[i - 1])) 
                {
                    breakPos.Add(i);
                }
            }
            for (int i = 0; i < breakPos.Count; i++)
            {
                res = res.Insert(breakPos[i], " ");
            }

            res = res.Replace('_', ' ');

            Name = res;

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            Debug.Log("Name set to: " + res);

        }


        public void SetAutomaticPreview(GameObject g)
        {
            string path = ObjectPreview.CreateAndSaveAssetPreview(g);
            if (path == "") return;

            UiPicture = (Sprite)AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));

            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            Debug.Log("Preview Set to " + path );
        }
        
        public void SetAutomaticMaterial()
        {

            ghostMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/GameMain/Materials/GhostObjectMaterial.mat", typeof(Material));
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            Debug.Log("Default Material set");
        }

#endif


    }
}
