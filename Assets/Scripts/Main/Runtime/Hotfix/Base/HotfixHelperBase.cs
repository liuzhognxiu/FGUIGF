using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MetaArea
{
    public abstract class HotfixHelperBase : MonoBehaviour,IHotFixHelper
    {
        public abstract ETTask Initialize();
        public abstract void Enter();
        public abstract void ShutDown();
        public abstract object GetHotfixGameEntry { get; }
        public abstract Assembly HotfixAssembly { get; }
    }
}