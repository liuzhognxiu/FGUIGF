using UnityEngine;
using UnityEngine.UI;
using TMPro;

//自动生成于：2022/5/18 13:59:39
namespace MetaArea.Hotfix
{
	public partial class UITeleportPointItemLogic
	{
		private RectTransform m_Transform_Icon;
		private RectTransform m_RectTransform_Icon;
		private RawImage m_RawImage_Icon;
		private RectTransform m_Transform_TeleportPoint;
		private RectTransform m_RectTransform_TeleportPoint;
		private Button m_Button_TeleportPoint;
		private Image m_Image_TeleportPoint;
		private RectTransform m_Transform_Name;
		private RectTransform m_RectTransform_Name;
		private TextMeshProUGUI m_TMP_Text_Name;
		private RectTransform m_Transform_DevProgress;
		private RectTransform m_RectTransform_DevProgress;
		private TextMeshProUGUI m_TMP_Text_DevProgress;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_Icon = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_Icon = autoBindTool.GetBindComponent<RectTransform>(1);
			m_RawImage_Icon = autoBindTool.GetBindComponent<RawImage>(2);
			m_Transform_TeleportPoint = autoBindTool.GetBindComponent<RectTransform>(3);
			m_RectTransform_TeleportPoint = autoBindTool.GetBindComponent<RectTransform>(4);
			m_Button_TeleportPoint = autoBindTool.GetBindComponent<Button>(5);
			m_Image_TeleportPoint = autoBindTool.GetBindComponent<Image>(6);
			m_Transform_Name = autoBindTool.GetBindComponent<RectTransform>(7);
			m_RectTransform_Name = autoBindTool.GetBindComponent<RectTransform>(8);
			m_TMP_Text_Name = autoBindTool.GetBindComponent<TextMeshProUGUI>(9);
			m_Transform_DevProgress = autoBindTool.GetBindComponent<RectTransform>(10);
			m_RectTransform_DevProgress = autoBindTool.GetBindComponent<RectTransform>(11);
			m_TMP_Text_DevProgress = autoBindTool.GetBindComponent<TextMeshProUGUI>(12);
		}
	}
}
