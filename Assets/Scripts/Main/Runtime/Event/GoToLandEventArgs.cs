using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public sealed class GoToLandEventArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(GoToLandEventArgs).GetHashCode();

        public override void Clear()
        {
        }

        public override int Id => EventId;

        public HexCell StartCell;

        /// <summary>
        /// 开始进入岛屿的时间。
        /// </summary>
        /// <param name="cell">内部事件。</param>
        public static GoToLandEventArgs Create(HexCell cell)
        {
            GoToLandEventArgs goToLandEventArgs = ReferencePool.Acquire<GoToLandEventArgs>();
            goToLandEventArgs.StartCell = cell;
            return goToLandEventArgs;
        }
    }
}
