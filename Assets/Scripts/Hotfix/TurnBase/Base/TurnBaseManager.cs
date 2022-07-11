using System;
using GameFramework;
using GameFramework.Fsm;

namespace MetaArea.Hotfix
{
    /// <summary>
    /// 回合制流程管理器
    /// </summary>
    public sealed class TurnBaseManager
    {
        private IFsmManager m_FsmManager;
        private IFsm<TurnBaseManager> m_TurnBaseFsm;

        public TurnBaseManager()
        {
            m_FsmManager = null;
            m_TurnBaseFsm = null;
        }

        
        /// <summary>
        /// 获取当前回合流程
        /// </summary>
        /// <exception cref="GameFrameworkException"></exception>
        public TurnBaseBase CurrentTurnBase
        {
            get
            {
                if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

                return (TurnBaseBase) m_TurnBaseFsm.CurrentState;
            }
        }
        
        /// <summary>
        ///     获取当前流程持续时间。
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

                return m_TurnBaseFsm.CurrentStateTime;
            }
        }
        
        /// <summary>
        /// 关闭并清理流程管理器。
        /// </summary>
        public void Shutdown()
        {
            if (m_FsmManager != null)
            {
                if (m_TurnBaseFsm != null)
                {
                    m_FsmManager.DestroyFsm(m_TurnBaseFsm);
                    m_TurnBaseFsm = null;
                }

                m_FsmManager = null;
            }
        }
        
        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="fsmManager">有限状态机管理器。</param>
        /// <param name="turnbase">流程管理器包含的流程。</param>
        public void Initialize(IFsmManager fsmManager, params TurnBaseBase[] turnbase)
        {
            if (fsmManager == null)
            {
                throw new GameFrameworkException("FSM manager is invalid.");
            }

            m_FsmManager = fsmManager;
            m_TurnBaseFsm = m_FsmManager.CreateFsm<TurnBaseManager>(this, turnbase);
        }
        
        /// <summary>
        ///     开始流程。
        /// </summary>
        /// <typeparam name="T">要开始的流程类型。</typeparam>
        public void StartProcedure<T>() where T : TurnBaseBase
        {
            if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

            m_TurnBaseFsm.Start<T>();
        }
        
        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">要检查的流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : TurnBaseBase
        {
            if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

            return m_TurnBaseFsm.HasState<T>();
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <param name="turnBaseType">要检查的流程类型。</param>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure(Type turnBaseType)
        {
            if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

            return m_TurnBaseFsm.HasState(turnBaseType);
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">要获取的流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public TurnBaseBase GetProcedure<T>() where T : TurnBaseBase
        {
            if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

            return m_TurnBaseFsm.GetState<T>();
        }

        /// <summary>
        ///     获取流程。
        /// </summary>
        /// <param name="turnBaseType">要获取的流程类型。</param>
        /// <returns>要获取的流程。</returns>
        public TurnBaseBase GetProcedure(Type turnBaseType)
        {
            if (m_TurnBaseFsm == null) throw new GameFrameworkException("You must initialize procedure first.");

            return (TurnBaseBase) m_TurnBaseFsm.GetState(turnBaseType);
        }
    }
}