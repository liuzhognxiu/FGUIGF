/** generated class success. **/

using FairyGUI;

namespace MetaArea.Hotfix
{
    public class ExtensionConfig
    {
        public ExtensionConfig()
        {
            ComponentExtension();
            LoaderExtension();
        }

        public void ComponentExtension()
        {
            #region auto generate not change
            UIObjectFactory.SetPackageItemExtension("ui://CommonComponent/TestCom", typeof(TestComItem));
            UIObjectFactory.SetPackageItemExtension("ui://CommonComponent/GreenButton", typeof(GreenButtonItem));
            #endregion
            }

        public void LoaderExtension()
        {
        }
        
    }
}