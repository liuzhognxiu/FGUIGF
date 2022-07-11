using GameFramework;
using GameFramework.ObjectPool;
using GameFramework.UI;

namespace MetaArea
{
    public class FGUIFormInstanceObject : ObjectBase
    {
        private object m_UIFormAsset;
        private FGUIFormHelper m_UIFormHelper;

        public FGUIFormInstanceObject()
        {
            m_UIFormAsset = null;
            m_UIFormHelper = null;
        }

        public static FGUIFormInstanceObject Create(string name, object uiFormAsset, object uiFormInstance, FGUIFormHelper uiFormHelper)
        {
            if (uiFormAsset == null)
            {
                throw new GameFrameworkException("UI form asset is invalid.");
            }

            if (uiFormHelper == null)
            {
                throw new GameFrameworkException("UI form helper is invalid.");
            }

            FGUIFormInstanceObject uiFormInstanceObject = ReferencePool.Acquire<FGUIFormInstanceObject>();
            uiFormInstanceObject.Initialize(name, uiFormInstance);
            uiFormInstanceObject.m_UIFormAsset = uiFormAsset;
            uiFormInstanceObject.m_UIFormHelper = uiFormHelper;
            return uiFormInstanceObject;
        }

        public override void Clear()
        {
            base.Clear();
            m_UIFormAsset = null;
            m_UIFormHelper = null;
        }

        protected override void Release(bool isShutdown)
        {
            m_UIFormHelper.ReleaseUIForm(m_UIFormAsset, Target);
        }
    }
}