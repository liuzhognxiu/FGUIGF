using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public abstract class FGUIFormLogic : MonoBehaviour
    {
        private bool m_Available = false;
        private bool m_Visible = false;
        private FGUIForm _mFguiForm;

        public FGUIForm fguiForm => _mFguiForm;
        
        /// <summary>
        /// 获取或设置界面名称。
        /// </summary>
        public string Name
        {
            get { return gameObject.name; }
            set { gameObject.name = value; }
        }
        
        /// <summary>
        /// 获取界面是否可用。
        /// </summary>
        public bool Available => m_Available;

        /// <summary>
        /// 获取或设置界面是否可见。
        /// </summary>
        public bool Visible
        {
            get => m_Available && m_Visible;
            set
            {
                if (!m_Available)
                {
                    Log.Warning("UI form '{0}' is not available.", Name);
                    return;
                }

                if (m_Visible == value)
                {
                    return;
                }

                m_Visible = value;
                InternalSetVisible(value);
            }
        }
        protected internal virtual void OnInit(object userData)
        {
            _mFguiForm = GetComponent<FGUIForm>();
        }

        protected internal virtual void OnOpen()
        {
            m_Available = true;
            Visible = true;
        }

        protected internal virtual void OnClose()
        {
            Visible = false;
            m_Available = false;
        }

        protected internal virtual void OnRecycle()
        {
            
        }
        
        /// <summary>
        /// 设置界面的可见性。
        /// </summary>
        /// <param name="visible">界面的可见性。</param>
        protected virtual void InternalSetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}