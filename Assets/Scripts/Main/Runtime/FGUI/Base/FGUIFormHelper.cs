﻿using GameFramework.UI;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class FGUIFormHelper : MonoBehaviour
    {
        private ResourceComponent m_ResourceComponent = null;
        
        private void Start()
        {
            m_ResourceComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();
            if (m_ResourceComponent == null)
            {
                Log.Fatal("Resource component is invalid.");
                return;
            }
        }
        
        /// <summary>
        /// 实例化界面。
        /// </summary>
        /// <param name="uiFormAsset">要实例化的界面资源。</param>
        /// <returns>实例化后的界面。</returns>
        public object InstantiateUIForm(object uiFormAsset)
        {
            return Instantiate((Object)uiFormAsset);
        }
        
        /// <summary>
        /// 创建界面。
        /// </summary>
        /// <param name="uiFormInstance">界面实例。</param>
        /// <param name="uiGroup">界面所属的界面组。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面。</returns>
        public FGUIForm CreateUIForm(object uiFormInstance,Transform _parent)
        {
            GameObject gameObject = uiFormInstance as GameObject;
            if (gameObject == null)
            {
                Log.Error("FGUI form instance is invalid.");
                return null;
            }

            Transform transform = gameObject.transform;
            transform.SetParent(_parent);
            HotfixFGUIForm hotfix = gameObject.GetComponent<HotfixFGUIForm>();
            if (hotfix != null)
            {
                hotfix.GetOrAddUIFormLogic();
            }
            return gameObject.GetOrAddComponent<FGUIForm>();
        }
        
        /// <summary>
        /// 释放界面。
        /// </summary>
        /// <param name="uiFormAsset">要释放的界面资源。</param>
        /// <param name="uiFormInstance">要释放的界面实例。</param>
        public void ReleaseUIForm(object uiFormAsset, object uiFormInstance)
        {
            m_ResourceComponent.UnloadAsset(uiFormAsset);
            Destroy((Object)uiFormInstance);
        }
    }
}