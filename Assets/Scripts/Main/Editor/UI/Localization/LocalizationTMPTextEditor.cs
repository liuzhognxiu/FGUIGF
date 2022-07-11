using System;
using GameFramework.Localization;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace MetaArea.Editor
{
    [CustomEditor(typeof(LocalizationTMPText))]
    public class LocalizationTMPTextEditor : UnityEditor.Editor

    {
        private SerializedProperty m_LocalizationKey;
        private SerializedProperty m_Text;

        private bool m_IsShowLocalizationButton = false;
        private Language m_CurrentLanguage = Language.ChineseSimplified;
        private bool m_IsShowEditorPreView = false;
        private string m_EditorPreViewValue ;
        private Vector2 m_EditorPreViewValueScroll ;

        private void OnEnable()
        {
            m_LocalizationKey = serializedObject.FindProperty("m_LocalizationKey");
            m_Text = serializedObject.FindProperty("m_Text");
            if (m_Text.objectReferenceValue == null)
            {
                m_Text.objectReferenceValue =
                    ((LocalizationTMPText) serializedObject.targetObject).GetComponent<TMP_Text>();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void OnDisable()
        {
            HandleText();
        }

        private void HandleText()
        {
            TMP_Text text = m_Text.objectReferenceValue as TMP_Text;
            if (!string.IsNullOrEmpty(m_LocalizationKey.stringValue) && text!=null && !string.IsNullOrEmpty(text.text))
            {
                text.text = string.Empty;
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(m_LocalizationKey);
            if (string.IsNullOrEmpty(m_LocalizationKey.stringValue))
            {
                EditorGUILayout.HelpBox("Localization Key is empty.",MessageType.Warning);
            }
            GUI.enabled = false;
            EditorGUILayout.PropertyField(m_Text);
            GUI.enabled = true;

            serializedObject.ApplyModifiedProperties();

            m_IsShowLocalizationButton = EditorGUILayout.Foldout(m_IsShowLocalizationButton, "Localization Preview");

            if (m_IsShowLocalizationButton)
            {
                EditorGUILayout.BeginHorizontal();
                m_CurrentLanguage = (Language) EditorGUILayout.EnumPopup("Language", m_CurrentLanguage);
                if (GUILayout.Button("Reload"))
                {
                    EditorLocalizationManager.Instance.ReLoad(m_CurrentLanguage);
                }
                EditorGUILayout.EndHorizontal();

                if (GUILayout.Button("Text PreView"))
                {
                    TMP_Text text = m_Text.objectReferenceValue as TMP_Text;
                    if (text != null)
                    {
                        text.text = EditorLocalizationManager.Instance.GetString(m_CurrentLanguage,
                            m_LocalizationKey.stringValue);
                        text.SetAllDirty();
                        text.ForceMeshUpdate();
                    }
                }
                if (GUILayout.Button("Editor PreView"))
                {
                    m_EditorPreViewValue = EditorLocalizationManager.Instance.GetString(m_CurrentLanguage,
                        m_LocalizationKey.stringValue);
                    m_IsShowEditorPreView = true;
                }

                if (m_IsShowEditorPreView)
                {
                    m_EditorPreViewValueScroll = EditorGUILayout.BeginScrollView(m_EditorPreViewValueScroll);
                    EditorGUILayout.TextArea(m_EditorPreViewValue,GUILayout.MaxHeight(100));
                    EditorGUILayout.EndScrollView();
                    if (GUILayout.Button("Close Preview"))
                    {
                        m_IsShowEditorPreView = false;
                        m_EditorPreViewValue = string.Empty;
                    }
                }
            }
        }
    }
}