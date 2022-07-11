using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameFramework;
using MetaArea;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditorInternal;
using UnityEngine;

namespace MetaArea
{
    [CodeGenerator]
    public class UIFormCodeGenerator : CodeGeneratorBase
    {
        private bool m_IsGenAutoBindCode = false;
        private bool m_IsGenMainCode = false;
        
        private readonly GameObjectRecordableList m_GameObjectRecordableList;

        public override void Draw()
        {
            m_GameObjectRecordableList.HandleGameObjectsListUI();
            IsHotfix = EditorGUILayout.ToggleLeft("热更层代码", IsHotfix);
            m_IsGenMainCode = EditorGUILayout.ToggleLeft("生成主体代码", m_IsGenMainCode);
            m_IsGenAutoBindCode = EditorGUILayout.ToggleLeft("生成自动绑定组件代码", m_IsGenAutoBindCode);
            EditorGUILayout.LabelField("自动生成代码路径", GetCodeFolder());
        }
        public override bool GenCode()
        {
            if (m_GameObjectRecordableList.GameObjects.Count == 0)
            {
                EditorUtility.DisplayDialog("警告", "请选择界面的游戏物体", "OK");
                return false;
            }

            for (int i = 0; i < m_GameObjectRecordableList.GameObjects.Count; i++)
            {
                if (m_IsGenMainCode)
                {
                    SetCodeName("UI"+m_GameObjectRecordableList.GameObjects[i].name+"Logic");
                    GenCode(m_GameObjectRecordableList.GameObjects[i]);
                }

                if (m_IsGenAutoBindCode)
                {
                    BindComponentCodeGenerator generator = new BindComponentCodeGenerator(
                        MainCodeFolder + "/BindComponents",
                        HotfixCodeFolder + "/BindComponents", IsHotfix);
                    generator.SetCodeName("UI"+m_GameObjectRecordableList.GameObjects[i].name+"Logic");
                    generator.IsPartial = true;
                    ComponentAutoBindTool component = m_GameObjectRecordableList.GameObjects[i].GetOrAddComponent<ComponentAutoBindTool>();
                    IAutoBindRuleHelper helper =
                        (IAutoBindRuleHelper) ComponentAutoBindToolUtility.CreateHelperInstance(ComponentAutoBindToolUtility.GetTypeNames()[0]);
                    component.SetRuleHelper(helper);
                    component.AutoBindComponent();
                    generator.GenCode(component);
                }
            }

            return true;
        }
        public override string GetCodePath()
        {
            return IsHotfix
                ? Utility.Path.GetRegularPath(Path.Combine(HotfixProjectPath, HotfixCodeFolder,"UIForm", CodeName+Suffix))
                : Utility.Path.GetRegularPath(Path.Combine(MainCodeFolder,"UIForm", CodeName+Suffix));
        }

        public override string GetCodeFolder()
        {
            return IsHotfix
                ? Utility.Path.GetRegularPath(Path.Combine(HotfixProjectPath, HotfixCodeFolder, "UIForm"))
                : Utility.Path.GetRegularPath(Path.Combine(MainCodeFolder, "UIForm"));
        }

        void GenCode(GameObject go)
        {
            string codePath = GetCodePath();
            string nameSpace = IsHotfix ? "MetaArea.Hotfix" : "MetaArea";
            if (IsHotfix)
            {
                PrefabStage prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
                if (prefabStage == null)
                {
                    HotfixUGuiFormConfig form = go.GetOrAddComponent<HotfixUGuiFormConfig>();
                    FieldInfo hotfixUGuiFormName = typeof(HotfixUGuiFormConfig).GetField("m_HotfixUGuiFormName",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    hotfixUGuiFormName?.SetValue(form, "UI"+go.name+"Logic");
                    PrefabInstanceStatus status = PrefabUtility.GetPrefabInstanceStatus(go);
                    if (status == PrefabInstanceStatus.Connected)
                    {
                        PrefabUtility.ApplyPrefabInstance(go, InteractionMode.AutomatedAction);
                    }
                }
                else
                {
                    HotfixUGuiFormConfig form = prefabStage.prefabContentsRoot.GetOrAddComponent<HotfixUGuiFormConfig>();
                    FieldInfo hotfixUGuiFormName = typeof(HotfixUGuiFormConfig).GetField("m_HotfixUGuiFormName",
                        BindingFlags.NonPublic | BindingFlags.Instance);
                    hotfixUGuiFormName?.SetValue(form, "UI"+prefabStage.prefabContentsRoot.name+"Logic");
                    EditorSceneManager.MarkSceneDirty(prefabStage.scene);
                }
            }
            if (!Directory.Exists(GetCodeFolder()))
            {
                Directory.CreateDirectory(GetCodeFolder());
            }

            using (StreamWriter sw = new StreamWriter(codePath))
            {
                sw.WriteLine("using System.Collections;");
                sw.WriteLine("using System.Collections.Generic;");
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("");
                //命名空间
                sw.WriteLine("namespace " + nameSpace);
                sw.WriteLine("{");

                //类名
                sw.WriteLine($"\tpublic partial class {CodeName} : UGuiFormLogic");
                sw.WriteLine("\t{");

                //OnInit
                sw.WriteLine($"\t\tprotected override void OnInit(object userdata)");
                sw.WriteLine("\t\t{");
                sw.WriteLine($"\t\t\tbase.OnInit(userdata);");
                if (m_IsGenAutoBindCode)
                {
                    sw.WriteLine($"\t\t\tGetBindComponents(gameObject);");
                }

                sw.WriteLine("\t\t}");
                sw.WriteLine("\t}");
                sw.WriteLine("}");
            }
        }


        public UIFormCodeGenerator() : base("Assets/Scripts/Main/Runtime/UI",
            "UI")
        {
            m_GameObjectRecordableList = new GameObjectRecordableList();
        }
    }
}