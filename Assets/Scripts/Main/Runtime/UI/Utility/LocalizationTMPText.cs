using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace MetaArea
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public class LocalizationTMPText  : MonoBehaviour,ILocalizationComponent
    {
        [SerializeField]
        private string m_LocalizationKey;
        
        [SerializeField] 
        private TMP_Text m_Text;
        
        public string LocalizationKey => m_LocalizationKey;

        public TMP_Text Text => m_Text;

        public void Localization()
        {
            if (m_Text == null)
            {
                throw new NullReferenceException("LocalizationTMPText m_Text can  not be null.");
            }
            if (string.IsNullOrEmpty(m_LocalizationKey))
            {
                Debug.LogWarning("LocalizationKey is null or empty! Please check it.");
                return;
            }

            m_Text.text = GameEntry.Localization.GetString(LocalizationKey);
        }
    }
}