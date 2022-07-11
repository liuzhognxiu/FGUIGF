using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using MetaArea;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public sealed class CreateBuildItemArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(CreateBuildItemArgs).GetHashCode();

        public override void Clear()
        {
        }

        public override int Id => EventId;

        public DREntity DrEntity;

        public int EntityId;

        public static CreateBuildItemArgs Create(DREntity drEntity, int EntityId)
        {
            CreateBuildItemArgs createBuildItemArgs = ReferencePool.Acquire<CreateBuildItemArgs>();
            createBuildItemArgs.DrEntity = drEntity;
            createBuildItemArgs.EntityId = EntityId;
            return createBuildItemArgs;
        }
    }
}