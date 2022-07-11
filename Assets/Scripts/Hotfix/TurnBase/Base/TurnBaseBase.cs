using GameFramework.Fsm;

namespace MetaArea.Hotfix
{
    public class TurnBaseBase : FsmState<TurnBaseManager>
    {
        /// <summary>状态初始化时调用。</summary>
        /// <param name="turnbaseOwner">流程持有者。</param>
        protected override void OnInit(IFsm<TurnBaseManager> turnbaseOwner) => base.OnInit(turnbaseOwner);

        /// <summary>进入状态时调用。</summary>
        /// <param name="turnbaseOwner">流程持有者。</param>
        protected override void OnEnter(IFsm<TurnBaseManager> turnbaseOwner) => base.OnEnter(turnbaseOwner);

        /// <summary>状态轮询时调用。</summary>
        /// <param name="turnbaseOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void OnUpdate(
            IFsm<TurnBaseManager> turnbaseOwner,
            float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(turnbaseOwner, elapseSeconds, realElapseSeconds);
        }

        /// <summary>离开状态时调用。</summary>
        /// <param name="turnbaseOwner">流程持有者。</param>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        protected override void OnLeave(
            IFsm<TurnBaseManager> turnbaseOwner,
            bool isShutdown)
        {
            base.OnLeave(turnbaseOwner, isShutdown);
        }

        /// <summary>状态销毁时调用。</summary>
        /// <param name="turnbaseOwner">流程持有者。</param>
        protected override void OnDestroy(IFsm<TurnBaseManager> turnbaseOwner) => base.OnDestroy(turnbaseOwner);
    }
}