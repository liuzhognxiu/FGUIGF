using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    public sealed class CreateBuildSuccessArgs : GameEventArgs
    {

        public static readonly int EventId = typeof(CreateBuildSuccessArgs).GetHashCode();

        public override void Clear()
        {
        }

        public override int Id => EventId;

        public Entity Entity;

        public static CreateBuildSuccessArgs Create(Entity Entity)
        {
            CreateBuildSuccessArgs createBuildSuccessArgs = ReferencePool.Acquire<CreateBuildSuccessArgs>();
            createBuildSuccessArgs.Entity = Entity;
            return createBuildSuccessArgs;
        }
    }
}