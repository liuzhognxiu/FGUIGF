using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace MetaArea
{
    /// <summary>
    /// 在这里添加角色的控制
    /// </summary>
    public class FreeLookGame : GameBase
    {
        public override GameMode GameMode => GameMode.Battle;

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
            
        }
    }
}