using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using GameFramework;
using GameFramework.Localization;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class EditorLocalizationManager
    {
        private EditorLocalizationManager()
        {
        }

        private static EditorLocalizationManager s_Instance;

        public static EditorLocalizationManager Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = new EditorLocalizationManager();
                }

                return s_Instance;
            }
        }

        private Dictionary<Language, Dictionary<string, string>> m_Dictionary =
            new Dictionary<Language, Dictionary<string, string>>();

        private bool Init(Language currentLanguage, Dictionary<string, string> dictionary)
        {
            string dictionaryString = File.ReadAllText($"Assets/GameMain/Localization/{currentLanguage}/Dictionaries/Default.xml");
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(dictionaryString);
                XmlNode xmlRoot = xmlDocument.SelectSingleNode("Dictionaries");
                XmlNodeList xmlNodeDictionaryList = xmlRoot.ChildNodes;
                for (int i = 0; i < xmlNodeDictionaryList.Count; i++)
                {
                    XmlNode xmlNodeDictionary = xmlNodeDictionaryList.Item(i);
                    if (xmlNodeDictionary.Name != "Dictionary")
                    {
                        continue;
                    }

                    string language = xmlNodeDictionary.Attributes.GetNamedItem("Language").Value;
                    if (language != currentLanguage.ToString())
                    {
                        continue;
                    }

                    XmlNodeList xmlNodeStringList = xmlNodeDictionary.ChildNodes;
                    for (int j = 0; j < xmlNodeStringList.Count; j++)
                    {
                        XmlNode xmlNodeString = xmlNodeStringList.Item(j);
                        if (xmlNodeString.Name != "String")
                        {
                            continue;
                        }

                        string key = xmlNodeString.Attributes.GetNamedItem("Key").Value;
                        string value = xmlNodeString.Attributes.GetNamedItem("Value").Value;
                        if (dictionary.ContainsKey(key))
                        {
                            Debug.LogWarning($"Can not add raw string with key '{key}' which may be invalid or duplicate.");
                            return false;
                        }
                        else
                        {
                            dictionary.Add(key, value);
                        }
                    }
                }

                m_Dictionary.Add(currentLanguage, dictionary);

                return true;
            }
            catch (Exception exception)
            {
                Debug.LogWarning($"Can not parse dictionary data with exception '{exception.ToString()}'." );
                return false;
            }
        }

        public string GetString(Language language, string key)
        {
            if (!m_Dictionary.TryGetValue(language, out var dictionary))
            {
                dictionary = new Dictionary<string, string>();
                if (!Init(language, dictionary))
                {
                    return null;
                }
            }

            dictionary.TryGetValue(key, out var value);
            return value;
        }

        public void ReLoad(Language currentLanguage)
        {
            if (m_Dictionary.TryGetValue(currentLanguage, out var dictionary))
            {
                m_Dictionary.Remove(currentLanguage);
            }
            dictionary = new Dictionary<string, string>();
            Init(currentLanguage, dictionary);
        }
    }
}