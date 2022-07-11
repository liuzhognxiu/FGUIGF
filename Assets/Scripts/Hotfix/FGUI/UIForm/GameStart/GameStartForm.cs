/** generated class success. **/

//注意：不为组件中使用默认名称的元素生成代码

using System;
using FairyGUI;

namespace MetaArea.Hotfix
{
    public partial class FGUIGameStartFormLogic
    {
        #region declare start, auto generate not change

        public GButton btnStart;
        public GGraph Star;
        public GreenButtonItem Btn_Test1;
        public TestComItem Panel_Test2;

        #endregion declare end
        
        void GetBindComponents()
        {
            #region define start, auto generate not change

            btnStart = MainView.GetChild("btnStart") as GButton;
            Star = MainView.GetChild("Star") as GGraph;
            Btn_Test1 = MainView.GetChild("Btn_Test1") as GreenButtonItem;
            Panel_Test2 = MainView.GetChild("Panel_Test2") as TestComItem;
            #endregion define end
        }
    }
}