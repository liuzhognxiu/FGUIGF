/** generated class success. **/

using FairyGUI;

namespace MetaArea.Hotfix
{
    public partial class TestComItem : GComponent
    {
    
        public FGUITestComItemLogic ItemLogic;
        #region declare start, auto generate not change
        public GreenButtonItem btn_Center;
        #endregion declare end
        public override void ConstructFromXML(FairyGUI.Utils.XML cxml)
        {
            base.ConstructFromXML(cxml);
			#region define start, auto generate not change
            btn_Center = GetChild("btn_Center") as GreenButtonItem;
            #endregion define end
            ItemLogic =  new FGUITestComItemLogic(this);
        }
    }
}