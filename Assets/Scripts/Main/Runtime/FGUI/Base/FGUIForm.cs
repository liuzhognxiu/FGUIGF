using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public class FGUIForm : MonoBehaviour
    {
        private int m_SerialId;
        private FGUIFormLogic m_Logic;

        private string m_FGUIFormAssetName;

        public FGUIFormLogic Logic
        {
            get { return m_Logic; }
            set { m_Logic = value; }
        }

        public int SerialId => m_SerialId;
        
        /// <summary>
        /// 获取界面实例。
        /// </summary>
        public object Handle
        {
            get
            {
                return gameObject;
            }
        }
        
        public void OnInit(int serialId,string fguiFormAssetName,bool isNewInstance,object userData = null)
        {
            m_FGUIFormAssetName = fguiFormAssetName;
            m_SerialId = serialId;
            
            if (!isNewInstance)
            {
                return;
            }

            m_Logic = GetComponent<FGUIFormLogic>();
            if (m_Logic == null)
            {
                Log.Error("UI form '{0}' can not get UI form logic.", fguiFormAssetName);
                return;
            }

            try
            {
                m_Logic.OnInit(userData);
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[]{0}' OnInit with exception '{1}'.",  fguiFormAssetName, exception.ToString());
            }
        }

        public void OnOpen()
        {
            try
            {
                m_Logic.OnOpen();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[]{0}' OnInit with exception '{1}'.",  m_FGUIFormAssetName, exception.ToString());
            }
        }

        public void OnClose()
        {
            try
            {
                m_Logic.OnClose();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[]{0}' OnInit with exception '{1}'.",  m_FGUIFormAssetName, exception.ToString());
            }
        }

        public void OnRecycle()
        {
            try
            {
                m_Logic.OnRecycle();
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[]{0}' OnInit with exception '{1}'.",  m_FGUIFormAssetName, exception.ToString());
            }
        }
    }
}