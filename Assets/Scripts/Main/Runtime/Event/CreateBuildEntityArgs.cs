using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public sealed class CreateBuildEntityArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(CreateBuildEntityArgs).GetHashCode();

        public override void Clear()
        {
        }

        public override int Id => EventId;


        public int EntityId;

        public static CreateBuildEntityArgs Create(int EntityId)
        {
            CreateBuildEntityArgs createBuildItemArgs = ReferencePool.Acquire<CreateBuildEntityArgs>();
            createBuildItemArgs.EntityId = EntityId;
            return createBuildItemArgs;
        }
    }
}