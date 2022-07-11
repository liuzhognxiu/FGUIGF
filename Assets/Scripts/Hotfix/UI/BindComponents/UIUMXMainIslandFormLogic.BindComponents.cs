using UnityEngine;
using TMPro;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using UnityEngine.EventSystems;

//自动生成于：2022/5/18 18:09:27
namespace MetaArea.Hotfix
{
	public partial class UIUMXMainIslandFormLogic
	{
		private RectTransform m_Transform_LeftInfo;
		private RectTransform m_RectTransform_LeftInfo;
		private RectTransform m_Transform_Message;
		private RectTransform m_RectTransform_Message;
		private TextMeshProUGUI m_TMP_Text_Message;
		private RectTransform m_Transform_Preset;
		private RectTransform m_RectTransform_Preset;
		private Toggle m_Toggle_Preset;
		private RectTransform m_Transform_Custom;
		private RectTransform m_RectTransform_Custom;
		private Toggle m_Toggle_Custom;
		private RectTransform m_Transform_ScrollView;
		private RectTransform m_RectTransform_ScrollView;
		private Mask m_Mask_ScrollView;
		private ScrollRect m_ScrollRect_ScrollView;
		private EnhancedScroller m_EnhancedScroller_ScrollView;
		private RectTransform m_Transform_PutAway;
		private RectTransform m_RectTransform_PutAway;
		private Button m_Button_PutAway;
		private Image m_Image_PutAway;
		private RectTransform m_Transform_Find;
		private RectTransform m_RectTransform_Find;
		private Image m_Image_Find;
		private TMP_InputField m_TMP_InputField_Find;
		private RectTransform m_Transform_Camera;
		private RectTransform m_RectTransform_Camera;
		private Camera m_Camera_Camera;
		private RectTransform m_Transform_Menu;
		private RectTransform m_RectTransform_Menu;
		private Image m_Image_Menu;
		private EventTrigger m_EventTrigger_Menu;
		private RectTransform m_Transform_Community;
		private RectTransform m_RectTransform_Community;
		private Button m_Button_Community;
		private Image m_Image_Community;
		private EventTrigger m_EventTrigger_Community;
		private RectTransform m_Transform_MyIsland;
		private RectTransform m_RectTransform_MyIsland;
		private Button m_Button_MyIsland;
		private Image m_Image_MyIsland;
		private EventTrigger m_EventTrigger_MyIsland;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_LeftInfo = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_LeftInfo = autoBindTool.GetBindComponent<RectTransform>(1);
			m_Transform_Message = autoBindTool.GetBindComponent<RectTransform>(2);
			m_RectTransform_Message = autoBindTool.GetBindComponent<RectTransform>(3);
			m_TMP_Text_Message = autoBindTool.GetBindComponent<TextMeshProUGUI>(4);
			m_Transform_Preset = autoBindTool.GetBindComponent<RectTransform>(5);
			m_RectTransform_Preset = autoBindTool.GetBindComponent<RectTransform>(6);
			m_Toggle_Preset = autoBindTool.GetBindComponent<Toggle>(7);
			m_Transform_Custom = autoBindTool.GetBindComponent<RectTransform>(8);
			m_RectTransform_Custom = autoBindTool.GetBindComponent<RectTransform>(9);
			m_Toggle_Custom = autoBindTool.GetBindComponent<Toggle>(10);
			m_Transform_ScrollView = autoBindTool.GetBindComponent<RectTransform>(11);
			m_RectTransform_ScrollView = autoBindTool.GetBindComponent<RectTransform>(12);
			m_Mask_ScrollView = autoBindTool.GetBindComponent<Mask>(13);
			m_ScrollRect_ScrollView = autoBindTool.GetBindComponent<ScrollRect>(14);
			m_EnhancedScroller_ScrollView = autoBindTool.GetBindComponent<EnhancedScroller>(15);
			m_Transform_PutAway = autoBindTool.GetBindComponent<RectTransform>(16);
			m_RectTransform_PutAway = autoBindTool.GetBindComponent<RectTransform>(17);
			m_Button_PutAway = autoBindTool.GetBindComponent<Button>(18);
			m_Image_PutAway = autoBindTool.GetBindComponent<Image>(19);
			m_Transform_Find = autoBindTool.GetBindComponent<RectTransform>(20);
			m_RectTransform_Find = autoBindTool.GetBindComponent<RectTransform>(21);
			m_Image_Find = autoBindTool.GetBindComponent<Image>(22);
			m_TMP_InputField_Find = autoBindTool.GetBindComponent<TMP_InputField>(23);
			m_Transform_Camera = autoBindTool.GetBindComponent<RectTransform>(24);
			m_RectTransform_Camera = autoBindTool.GetBindComponent<RectTransform>(25);
			m_Camera_Camera = autoBindTool.GetBindComponent<Camera>(26);
			m_Transform_Menu = autoBindTool.GetBindComponent<RectTransform>(27);
			m_RectTransform_Menu = autoBindTool.GetBindComponent<RectTransform>(28);
			m_Image_Menu = autoBindTool.GetBindComponent<Image>(29);
			m_EventTrigger_Menu = autoBindTool.GetBindComponent<EventTrigger>(30);
			m_Transform_Community = autoBindTool.GetBindComponent<RectTransform>(31);
			m_RectTransform_Community = autoBindTool.GetBindComponent<RectTransform>(32);
			m_Button_Community = autoBindTool.GetBindComponent<Button>(33);
			m_Image_Community = autoBindTool.GetBindComponent<Image>(34);
			m_EventTrigger_Community = autoBindTool.GetBindComponent<EventTrigger>(35);
			m_Transform_MyIsland = autoBindTool.GetBindComponent<RectTransform>(36);
			m_RectTransform_MyIsland = autoBindTool.GetBindComponent<RectTransform>(37);
			m_Button_MyIsland = autoBindTool.GetBindComponent<Button>(38);
			m_Image_MyIsland = autoBindTool.GetBindComponent<Image>(39);
			m_EventTrigger_MyIsland = autoBindTool.GetBindComponent<EventTrigger>(40);
		}
	}
}
