using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MetaArea
{
    public interface IHotFixHelper
    {
        ETTask Initialize();
        void Enter();
        void ShutDown();
        object GetHotfixGameEntry { get; }
        Assembly HotfixAssembly { get; }
    }
}