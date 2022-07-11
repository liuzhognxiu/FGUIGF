namespace MetaArea
{
    public class DialogParams
    {
        /// <summary>
        /// 模式，即按钮数量。取值 1、2、3。
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 弹出窗口时是否暂停游戏。
        /// </summary>
        public bool PauseGame { get; set; }

        /// <summary>
        /// 确认按钮文本。
        /// </summary>
        public string ConfirmText { get; set; }

        /// <summary>
        /// 取消按钮文本。
        /// </summary>
        public string CancelText { get; set; }

        /// <summary>
        /// 中立按钮文本。
        /// </summary>
        public string OtherText { get; set; }

        /// <summary>
        /// 用户自定义数据。
        /// </summary>
        public string UserData { get; set; }

        /// <summary>
        /// 返回任务
        /// </summary>
        private ETTask<PopResult> m_Task;

        public ETTask<PopResult> Task => m_Task ??= ETTask<PopResult>.Create(true);
    }
}