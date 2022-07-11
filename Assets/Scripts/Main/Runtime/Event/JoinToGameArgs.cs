using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public sealed class JoinToGameArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(JoinToGameArgs).GetHashCode();

        public override void Clear()
        {
        }

        public override int Id => EventId;

        public static JoinToGameArgs Create()
        {
            JoinToGameArgs joinToGameArgs = ReferencePool.Acquire<JoinToGameArgs>();
            return joinToGameArgs;
        }
    }
}