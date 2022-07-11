using UnityEngine;
using UnityEngine.UI;

//自动生成于：2022/5/10 21:47:12
namespace MetaArea.Hotfix
{

	public partial class UITouchPlayerFormLogic
	{

		private RectTransform m_Transform_MobileJoystick;
		private RectTransform m_RectTransform_MobileJoystick;
		private Image m_Image_MobileJoystick;
		private RectTransform m_Transform_Jump;
		private RectTransform m_RectTransform_Jump;
		private Image m_Image_Jump;
		private RectTransform m_Transform_ChangeToBuild;
		private RectTransform m_RectTransform_ChangeToBuild;
		private Button m_Button_ChangeToBuild;
		private Image m_Image_ChangeToBuild;
		private RectTransform m_Transform_BuildController;
		private RectTransform m_RectTransform_BuildController;
		private RectTransform m_Transform_Put;
		private RectTransform m_RectTransform_Put;
		private Button m_Button_Put;
		private Image m_Image_Put;
		private RectTransform m_Transform_TurnLeft;
		private RectTransform m_RectTransform_TurnLeft;
		private Button m_Button_TurnLeft;
		private Image m_Image_TurnLeft;
		private RectTransform m_Transform_TurnRight;
		private RectTransform m_RectTransform_TurnRight;
		private Button m_Button_TurnRight;
		private Image m_Image_TurnRight;
		private RectTransform m_Transform_AddScale;
		private RectTransform m_RectTransform_AddScale;
		private Button m_Button_AddScale;
		private Image m_Image_AddScale;
		private RectTransform m_Transform_MinusScale;
		private RectTransform m_RectTransform_MinusScale;
		private Button m_Button_MinusScale;
		private Image m_Image_MinusScale;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_MobileJoystick = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_MobileJoystick = autoBindTool.GetBindComponent<RectTransform>(1);
			m_Image_MobileJoystick = autoBindTool.GetBindComponent<Image>(2);
			m_Transform_Jump = autoBindTool.GetBindComponent<RectTransform>(3);
			m_RectTransform_Jump = autoBindTool.GetBindComponent<RectTransform>(4);
			m_Image_Jump = autoBindTool.GetBindComponent<Image>(5);
			m_Transform_ChangeToBuild = autoBindTool.GetBindComponent<RectTransform>(6);
			m_RectTransform_ChangeToBuild = autoBindTool.GetBindComponent<RectTransform>(7);
			m_Button_ChangeToBuild = autoBindTool.GetBindComponent<Button>(8);
			m_Image_ChangeToBuild = autoBindTool.GetBindComponent<Image>(9);
			m_Transform_BuildController = autoBindTool.GetBindComponent<RectTransform>(10);
			m_RectTransform_BuildController = autoBindTool.GetBindComponent<RectTransform>(11);
			m_Transform_Put = autoBindTool.GetBindComponent<RectTransform>(12);
			m_RectTransform_Put = autoBindTool.GetBindComponent<RectTransform>(13);
			m_Button_Put = autoBindTool.GetBindComponent<Button>(14);
			m_Image_Put = autoBindTool.GetBindComponent<Image>(15);
			m_Transform_TurnLeft = autoBindTool.GetBindComponent<RectTransform>(16);
			m_RectTransform_TurnLeft = autoBindTool.GetBindComponent<RectTransform>(17);
			m_Button_TurnLeft = autoBindTool.GetBindComponent<Button>(18);
			m_Image_TurnLeft = autoBindTool.GetBindComponent<Image>(19);
			m_Transform_TurnRight = autoBindTool.GetBindComponent<RectTransform>(20);
			m_RectTransform_TurnRight = autoBindTool.GetBindComponent<RectTransform>(21);
			m_Button_TurnRight = autoBindTool.GetBindComponent<Button>(22);
			m_Image_TurnRight = autoBindTool.GetBindComponent<Image>(23);
			m_Transform_AddScale = autoBindTool.GetBindComponent<RectTransform>(24);
			m_RectTransform_AddScale = autoBindTool.GetBindComponent<RectTransform>(25);
			m_Button_AddScale = autoBindTool.GetBindComponent<Button>(26);
			m_Image_AddScale = autoBindTool.GetBindComponent<Image>(27);
			m_Transform_MinusScale = autoBindTool.GetBindComponent<RectTransform>(28);
			m_RectTransform_MinusScale = autoBindTool.GetBindComponent<RectTransform>(29);
			m_Button_MinusScale = autoBindTool.GetBindComponent<Button>(30);
			m_Image_MinusScale = autoBindTool.GetBindComponent<Image>(31);
		}
	}
}
