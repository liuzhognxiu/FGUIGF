using UnityEngine;
using UnityEngine.UI;

//自动生成于：2022/5/10 21:40:23
namespace MetaArea.Hotfix
{

	public partial class UIMainMenuFormLogic
	{

		private RectTransform m_Transform_MenuPanel;
		private RectTransform m_RectTransform_MenuPanel;
		private RectTransform m_Transform_OpenMenu;
		private RectTransform m_RectTransform_OpenMenu;
		private Button m_Button_OpenMenu;
		private Image m_Image_OpenMenu;
		private RectTransform m_Transform_WorldMap;
		private RectTransform m_RectTransform_WorldMap;
		private Button m_Button_WorldMap;
		private Image m_Image_WorldMap;
		private RectTransform m_Transform_NTFShop;
		private RectTransform m_RectTransform_NTFShop;
		private Button m_Button_NTFShop;
		private Image m_Image_NTFShop;
		private RectTransform m_Transform_WorldGroup;
		private RectTransform m_RectTransform_WorldGroup;
		private Button m_Button_WorldGroup;
		private Image m_Image_WorldGroup;
		private RectTransform m_Transform_Find;
		private RectTransform m_RectTransform_Find;
		private Button m_Button_Find;
		private Image m_Image_Find;
		private RectTransform m_Transform_Set;
		private RectTransform m_RectTransform_Set;
		private Button m_Button_Set;
		private Image m_Image_Set;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_MenuPanel = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_MenuPanel = autoBindTool.GetBindComponent<RectTransform>(1);
			m_Transform_OpenMenu = autoBindTool.GetBindComponent<RectTransform>(2);
			m_RectTransform_OpenMenu = autoBindTool.GetBindComponent<RectTransform>(3);
			m_Button_OpenMenu = autoBindTool.GetBindComponent<Button>(4);
			m_Image_OpenMenu = autoBindTool.GetBindComponent<Image>(5);
			m_Transform_WorldMap = autoBindTool.GetBindComponent<RectTransform>(6);
			m_RectTransform_WorldMap = autoBindTool.GetBindComponent<RectTransform>(7);
			m_Button_WorldMap = autoBindTool.GetBindComponent<Button>(8);
			m_Image_WorldMap = autoBindTool.GetBindComponent<Image>(9);
			m_Transform_NTFShop = autoBindTool.GetBindComponent<RectTransform>(10);
			m_RectTransform_NTFShop = autoBindTool.GetBindComponent<RectTransform>(11);
			m_Button_NTFShop = autoBindTool.GetBindComponent<Button>(12);
			m_Image_NTFShop = autoBindTool.GetBindComponent<Image>(13);
			m_Transform_WorldGroup = autoBindTool.GetBindComponent<RectTransform>(14);
			m_RectTransform_WorldGroup = autoBindTool.GetBindComponent<RectTransform>(15);
			m_Button_WorldGroup = autoBindTool.GetBindComponent<Button>(16);
			m_Image_WorldGroup = autoBindTool.GetBindComponent<Image>(17);
			m_Transform_Find = autoBindTool.GetBindComponent<RectTransform>(18);
			m_RectTransform_Find = autoBindTool.GetBindComponent<RectTransform>(19);
			m_Button_Find = autoBindTool.GetBindComponent<Button>(20);
			m_Image_Find = autoBindTool.GetBindComponent<Image>(21);
			m_Transform_Set = autoBindTool.GetBindComponent<RectTransform>(22);
			m_RectTransform_Set = autoBindTool.GetBindComponent<RectTransform>(23);
			m_Button_Set = autoBindTool.GetBindComponent<Button>(24);
			m_Image_Set = autoBindTool.GetBindComponent<Image>(25);
		}
	}
}
