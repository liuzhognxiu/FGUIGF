using FairyGUI;

namespace MetaArea
{
    public abstract class FGUIPanelFrom : FGUIFormLogic
    {
        private GComponent m_MainView;

        public GComponent MainView => m_MainView;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_MainView = GetComponent<UIPanel>().ui;
            m_MainView.SetPivot(0.5f,0.5f);
            m_MainView.Center();
            m_MainView.MakeFullScreen();
        }

        protected internal override void OnOpen()
        {
            base.OnOpen();
        }

        public void Close()
        {
            GameEntry.FGUI.CloseUIForm(this);
        }
    }
}