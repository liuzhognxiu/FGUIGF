using EnhancedUI.EnhancedScroller;
using UnityEngine;

namespace MetaArea.Hotfix
{
    public partial class UITeleportPointItemLogic : EnhancedScrollerCellView
    {
        public void Init()
        {
            GetBindComponents(gameObject);
        }
        
        public void SetData(TeleportPointData data)
        {
            m_RawImage_Icon.texture = data.Icon;
            m_TMP_Text_Name.text = data.Name;
            m_TMP_Text_DevProgress.text = data.Progress;
        }
    }

    public class TeleportPointData
    {
        private Texture m_Icon;
        private string m_Name;
        private string m_Progress;
        //todo  传送点数据位置
        
        public Texture Icon => m_Icon;
        public string Name => m_Name;
        public string Progress => m_Progress;
        
        public TeleportPointData(Texture mIcon, string mName, string mProgress)
        {
            m_Icon = mIcon;
            m_Name = mName;
            m_Progress = mProgress;
        }
    }
}