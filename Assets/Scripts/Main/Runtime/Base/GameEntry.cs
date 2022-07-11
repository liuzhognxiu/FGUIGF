using UnityEngine;
using UnityGameFramework.Runtime;

namespace MetaArea
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        private void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
            ETTask.ExceptionHandler += e => Log.Error(e);
            AwaitableExtensions.SubscribeEvent();
        }
    }
}