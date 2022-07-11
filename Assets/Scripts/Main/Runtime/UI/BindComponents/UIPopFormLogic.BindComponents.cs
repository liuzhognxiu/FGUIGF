using UnityEngine;
using TMPro;
using MetaArea;
using UnityEngine.UI;

//自动生成于：2022/5/17 12:54:43
namespace MetaArea
{
	public partial class UIDialogFormLogic
	{
		private RectTransform m_Transform_Title;
		private RectTransform m_RectTransform_Title;
		private TextMeshProUGUI m_TMP_Text_Title;
		private RectTransform m_Transform_Message;
		private RectTransform m_RectTransform_Message;
		private TextMeshProUGUI m_TMP_Text_Message;
		private RectTransform m_Transform_Confirm;
		private RectTransform m_RectTransform_Confirm;
		private CommonButton m_CommonButton_Confirm;
		private RectTransform m_Transform_ConfirmBorder;
		private RectTransform m_RectTransform_ConfirmBorder;
		private Image m_Image_ConfirmBorder;
		private RectTransform m_Transform_ConFirmText;
		private RectTransform m_RectTransform_ConFirmText;
		private TextMeshProUGUI m_TMP_Text_ConFirmText;
		private RectTransform m_Transform_Cancel;
		private RectTransform m_RectTransform_Cancel;
		private CommonButton m_CommonButton_Cancel;
		private RectTransform m_Transform_CancelBorder;
		private RectTransform m_RectTransform_CancelBorder;
		private Image m_Image_CancelBorder;
		private RectTransform m_Transform_CancelText;
		private RectTransform m_RectTransform_CancelText;
		private TextMeshProUGUI m_TMP_Text_CancelText;
		private RectTransform m_Transform_Other;
		private RectTransform m_RectTransform_Other;
		private CommonButton m_CommonButton_Other;
		private RectTransform m_Transform_OhterBorder;
		private RectTransform m_RectTransform_OhterBorder;
		private Image m_Image_OhterBorder;
		private RectTransform m_Transform_OtherText;
		private RectTransform m_RectTransform_OtherText;
		private TextMeshProUGUI m_TMP_Text_OtherText;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_Title = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_Title = autoBindTool.GetBindComponent<RectTransform>(1);
			m_TMP_Text_Title = autoBindTool.GetBindComponent<TextMeshProUGUI>(2);
			m_Transform_Message = autoBindTool.GetBindComponent<RectTransform>(3);
			m_RectTransform_Message = autoBindTool.GetBindComponent<RectTransform>(4);
			m_TMP_Text_Message = autoBindTool.GetBindComponent<TextMeshProUGUI>(5);
			m_Transform_Confirm = autoBindTool.GetBindComponent<RectTransform>(6);
			m_RectTransform_Confirm = autoBindTool.GetBindComponent<RectTransform>(7);
			m_CommonButton_Confirm = autoBindTool.GetBindComponent<CommonButton>(8);
			m_Transform_ConfirmBorder = autoBindTool.GetBindComponent<RectTransform>(9);
			m_RectTransform_ConfirmBorder = autoBindTool.GetBindComponent<RectTransform>(10);
			m_Image_ConfirmBorder = autoBindTool.GetBindComponent<Image>(11);
			m_Transform_ConFirmText = autoBindTool.GetBindComponent<RectTransform>(12);
			m_RectTransform_ConFirmText = autoBindTool.GetBindComponent<RectTransform>(13);
			m_TMP_Text_ConFirmText = autoBindTool.GetBindComponent<TextMeshProUGUI>(14);
			m_Transform_Cancel = autoBindTool.GetBindComponent<RectTransform>(15);
			m_RectTransform_Cancel = autoBindTool.GetBindComponent<RectTransform>(16);
			m_CommonButton_Cancel = autoBindTool.GetBindComponent<CommonButton>(17);
			m_Transform_CancelBorder = autoBindTool.GetBindComponent<RectTransform>(18);
			m_RectTransform_CancelBorder = autoBindTool.GetBindComponent<RectTransform>(19);
			m_Image_CancelBorder = autoBindTool.GetBindComponent<Image>(20);
			m_Transform_CancelText = autoBindTool.GetBindComponent<RectTransform>(21);
			m_RectTransform_CancelText = autoBindTool.GetBindComponent<RectTransform>(22);
			m_TMP_Text_CancelText = autoBindTool.GetBindComponent<TextMeshProUGUI>(23);
			m_Transform_Other = autoBindTool.GetBindComponent<RectTransform>(24);
			m_RectTransform_Other = autoBindTool.GetBindComponent<RectTransform>(25);
			m_CommonButton_Other = autoBindTool.GetBindComponent<CommonButton>(26);
			m_Transform_OhterBorder = autoBindTool.GetBindComponent<RectTransform>(27);
			m_RectTransform_OhterBorder = autoBindTool.GetBindComponent<RectTransform>(28);
			m_Image_OhterBorder = autoBindTool.GetBindComponent<Image>(29);
			m_Transform_OtherText = autoBindTool.GetBindComponent<RectTransform>(30);
			m_RectTransform_OtherText = autoBindTool.GetBindComponent<RectTransform>(31);
			m_TMP_Text_OtherText = autoBindTool.GetBindComponent<TextMeshProUGUI>(32);
		}
	}
}
