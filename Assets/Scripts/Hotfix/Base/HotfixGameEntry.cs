using System;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace MetaArea.Hotfix
{
    public class HotfixGameEntry
    {
         /// <summary>
        ///   热更流程管理器
        /// </summary>
        public ProcedureManager Procedure { get; private set; }

        public void Start()
        {
            Procedure = new ProcedureManager();
            //初始化流程管理器
            var procedure = new List<ProcedureBase>();
            var typeBase = typeof(ProcedureBase);
            var assembly = GameEntry.Hotfix.GetHotfixAssembly();
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                {
                    procedure.Add((ProcedureBase)Activator.CreateInstance(type));
                }
            }
            Procedure.Initialize(GameFrameworkEntry.GetModule<IFsmManager>(), procedure.ToArray());
         
            // //开始热更新层入口流程
            Procedure.StartProcedure<ProcedurePreload>();
        }
    }
}