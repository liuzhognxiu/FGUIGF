using UnityEngine;
using UnityEngine.UI;

//自动生成于：2022/5/18 1:14:42
namespace MetaArea.Hotfix
{
	public partial class UIKeyCodeCotrolFormLogic
	{
		private RectTransform m_Transform_ForwardButton;
		private RectTransform m_RectTransform_ForwardButton;
		private Button m_Button_ForwardButton;
		private Image m_Image_ForwardButton;
		private RectTransform m_Transform_RightButton;
		private RectTransform m_RectTransform_RightButton;
		private Button m_Button_RightButton;
		private Image m_Image_RightButton;
		private RectTransform m_Transform_LeftButton;
		private RectTransform m_RectTransform_LeftButton;
		private Button m_Button_LeftButton;
		private Image m_Image_LeftButton;
		private RectTransform m_Transform_BackButton;
		private RectTransform m_RectTransform_BackButton;
		private Button m_Button_BackButton;
		private Image m_Image_BackButton;
		private RectTransform m_Transform_JumpButton;
		private RectTransform m_RectTransform_JumpButton;
		private Button m_Button_JumpButton;
		private Image m_Image_JumpButton;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_ForwardButton = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_ForwardButton = autoBindTool.GetBindComponent<RectTransform>(1);
			m_Button_ForwardButton = autoBindTool.GetBindComponent<Button>(2);
			m_Image_ForwardButton = autoBindTool.GetBindComponent<Image>(3);
			m_Transform_RightButton = autoBindTool.GetBindComponent<RectTransform>(4);
			m_RectTransform_RightButton = autoBindTool.GetBindComponent<RectTransform>(5);
			m_Button_RightButton = autoBindTool.GetBindComponent<Button>(6);
			m_Image_RightButton = autoBindTool.GetBindComponent<Image>(7);
			m_Transform_LeftButton = autoBindTool.GetBindComponent<RectTransform>(8);
			m_RectTransform_LeftButton = autoBindTool.GetBindComponent<RectTransform>(9);
			m_Button_LeftButton = autoBindTool.GetBindComponent<Button>(10);
			m_Image_LeftButton = autoBindTool.GetBindComponent<Image>(11);
			m_Transform_BackButton = autoBindTool.GetBindComponent<RectTransform>(12);
			m_RectTransform_BackButton = autoBindTool.GetBindComponent<RectTransform>(13);
			m_Button_BackButton = autoBindTool.GetBindComponent<Button>(14);
			m_Image_BackButton = autoBindTool.GetBindComponent<Image>(15);
			m_Transform_JumpButton = autoBindTool.GetBindComponent<RectTransform>(16);
			m_RectTransform_JumpButton = autoBindTool.GetBindComponent<RectTransform>(17);
			m_Button_JumpButton = autoBindTool.GetBindComponent<Button>(18);
			m_Image_JumpButton = autoBindTool.GetBindComponent<Image>(19);
		}
	}
}
