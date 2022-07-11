using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


namespace UnityStandardAssets.CrossPlatformInput
{
    [ExecuteInEditMode]
    public class MobileControlRig : MonoBehaviour
    {
   
        private void Start()
        {
            UnityEngine.EventSystems.EventSystem system = GameObject.FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            if (system == null)
            {
                GameObject o = new GameObject("EventSystem");

                o.AddComponent<UnityEngine.EventSystems.EventSystem>();
                o.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
            }
        }


 


    
    }
}