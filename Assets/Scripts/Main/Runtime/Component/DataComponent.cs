using System;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public sealed class DataComponent : GameFrameworkComponent
    {
        private EventComponent m_EventComponent = null;

        public int MainPlayerID = 1;

        public string UserToken = "";

        public string UserID = "";

        public string UID = "";
        
        private void Start()
        {
            m_EventComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();
            if (m_EventComponent == null)
            {
                Log.Fatal("Event component is invalid.");
                return;
            }
        }
    }
    
}