using UnityEngine;
using UnityEngine.UI;

//自动生成于：2022/5/10 21:38:22
namespace MetaArea.Hotfix
{

	public partial class UIMainPlayerFormLogic
	{

		private RectTransform m_Transform_PlayerManager;
		private RectTransform m_RectTransform_PlayerManager;
		private Button m_Button_PlayerManager;
		private Image m_Image_PlayerManager;
		private RectTransform m_Transform_EquipManager;
		private RectTransform m_RectTransform_EquipManager;
		private Button m_Button_EquipManager;
		private Image m_Image_EquipManager;
		private RectTransform m_Transform_ItemManager;
		private RectTransform m_RectTransform_ItemManager;
		private Button m_Button_ItemManager;
		private Image m_Image_ItemManager;
		private RectTransform m_Transform_Camera;
		private RectTransform m_RectTransform_Camera;
		private Camera m_Camera_Camera;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_PlayerManager = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_PlayerManager = autoBindTool.GetBindComponent<RectTransform>(1);
			m_Button_PlayerManager = autoBindTool.GetBindComponent<Button>(2);
			m_Image_PlayerManager = autoBindTool.GetBindComponent<Image>(3);
			m_Transform_EquipManager = autoBindTool.GetBindComponent<RectTransform>(4);
			m_RectTransform_EquipManager = autoBindTool.GetBindComponent<RectTransform>(5);
			m_Button_EquipManager = autoBindTool.GetBindComponent<Button>(6);
			m_Image_EquipManager = autoBindTool.GetBindComponent<Image>(7);
			m_Transform_ItemManager = autoBindTool.GetBindComponent<RectTransform>(8);
			m_RectTransform_ItemManager = autoBindTool.GetBindComponent<RectTransform>(9);
			m_Button_ItemManager = autoBindTool.GetBindComponent<Button>(10);
			m_Image_ItemManager = autoBindTool.GetBindComponent<Image>(11);
			m_Transform_Camera = autoBindTool.GetBindComponent<RectTransform>(12);
			m_RectTransform_Camera = autoBindTool.GetBindComponent<RectTransform>(13);
			m_Camera_Camera = autoBindTool.GetBindComponent<Camera>(14);
		}
	}
}
