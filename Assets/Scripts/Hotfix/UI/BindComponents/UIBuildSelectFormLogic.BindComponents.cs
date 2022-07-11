using UnityEngine;
using UnityEngine.UI;

//自动生成于：2022/5/10 21:15:31
namespace MetaArea.Hotfix
{

	public partial class UIBuildSelectFormLogic
	{

		private RectTransform m_Transform_Bag;
		private RectTransform m_RectTransform_Bag;
		private RectTransform m_Transform_Layout;
		private RectTransform m_RectTransform_Layout;
		private GridLayoutGroup m_GridLayoutGroup_Layout;
		private RectTransform m_Transform_OpenBag;
		private RectTransform m_RectTransform_OpenBag;
		private Button m_Button_OpenBag;
		private Image m_Image_OpenBag;

		private void GetBindComponents(GameObject go)
		{
			ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();

			m_Transform_Bag = autoBindTool.GetBindComponent<RectTransform>(0);
			m_RectTransform_Bag = autoBindTool.GetBindComponent<RectTransform>(1);
			m_Transform_Layout = autoBindTool.GetBindComponent<RectTransform>(2);
			m_RectTransform_Layout = autoBindTool.GetBindComponent<RectTransform>(3);
			m_GridLayoutGroup_Layout = autoBindTool.GetBindComponent<GridLayoutGroup>(4);
			m_Transform_OpenBag = autoBindTool.GetBindComponent<RectTransform>(5);
			m_RectTransform_OpenBag = autoBindTool.GetBindComponent<RectTransform>(6);
			m_Button_OpenBag = autoBindTool.GetBindComponent<Button>(7);
			m_Image_OpenBag = autoBindTool.GetBindComponent<Image>(8);
		}
	}
}
