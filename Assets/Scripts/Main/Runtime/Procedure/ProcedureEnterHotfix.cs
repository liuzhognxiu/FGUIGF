
using GameFramework.Fsm;
using GameFramework.Procedure;

namespace MetaArea
{
    public class ProcedureEnterHotfix : ProcedureBase
    {
        public override bool UseNativeDialog => false;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Hotfix.StartHotfixLogic();
        }
    }
}