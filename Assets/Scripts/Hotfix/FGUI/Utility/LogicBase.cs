namespace MetaArea.Hotfix
{
    public abstract class LogicBase
    {
        /// <summary>
        /// 刷新UI界面
        /// </summary>
        protected abstract void RefreshUI();

        /// <summary>
        /// 刷新UIData
        /// </summary>
        /// <param name="data"></param>
        protected abstract void RefreshData(object data = null);
        
        /// <summary>
        /// 注册按钮事件
        /// </summary>
        protected abstract void SubscribeEvents();

        /// <summary>
        /// 移除按钮事件
        /// </summary>
        protected abstract void UnSubscribeEvents();
    }
}