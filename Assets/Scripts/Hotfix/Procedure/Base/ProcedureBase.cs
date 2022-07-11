using GameFramework.Fsm;

namespace MetaArea.Hotfix
{
    public abstract class ProcedureBase : FsmState<ProcedureManager>
    {
        /// <summary>状态初始化时调用。</summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected override void OnInit(IFsm<ProcedureManager> procedureOwner) => base.OnInit(procedureOwner);

        /// <summary>进入状态时调用。</summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected override void OnEnter(IFsm<ProcedureManager> procedureOwner) => base.OnEnter(procedureOwner);

        /// <summary>状态轮询时调用。</summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected override void OnUpdate(
            IFsm<ProcedureManager> procedureOwner,
            float elapseSeconds,
            float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        /// <summary>离开状态时调用。</summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        protected override void OnLeave(
            IFsm<ProcedureManager> procedureOwner,
            bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }

        /// <summary>状态销毁时调用。</summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected override void OnDestroy(IFsm<ProcedureManager> procedureOwner) => base.OnDestroy(procedureOwner);
    }
}