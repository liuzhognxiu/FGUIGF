using UnityEngine;

namespace MetaArea
{
    /// <summary>
    /// 游戏入口
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData { get; private set; }
        public static GodComponent God { get; private set; }
        public static DataComponent Data { get; private set; }
        public static HotfixComponent Hotfix { get; private set; }
        public static StaticResourcesComponent StaticResources { get; private set; }
        public static TimingWheelComponent TimingWheel { get; private set; }

        public static FGUIComponent FGUI  { get; private set; }
        
        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            God = UnityGameFramework.Runtime.GameEntry.GetComponent<GodComponent>();
            Hotfix = UnityGameFramework.Runtime.GameEntry.GetComponent<HotfixComponent>();
            StaticResources = UnityGameFramework.Runtime.GameEntry.GetComponent<StaticResourcesComponent>();
            TimingWheel = UnityGameFramework.Runtime.GameEntry.GetComponent<TimingWheelComponent>();
            Data = UnityGameFramework.Runtime.GameEntry.GetComponent<DataComponent>();
            FGUI = UnityGameFramework.Runtime.GameEntry.GetComponent<FGUIComponent>();
        }
    }
}