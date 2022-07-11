/** generated class success. **/

using FairyGUI;

namespace MetaArea.Hotfix
{
    public partial class GreenButtonItem : GButton
    {
    
        public FGUIGreenButtonItemLogic ItemLogic;
        #region declare start, auto generate not change
        public GTextField text_Message;
        #endregion declare end
        public override void ConstructFromXML(FairyGUI.Utils.XML cxml)
        {
            base.ConstructFromXML(cxml);
			#region define start, auto generate not change
            text_Message = GetChild("text_Message") as GTextField;
            #endregion define end
            ItemLogic =  new FGUIGreenButtonItemLogic(this);
        }
    }
}