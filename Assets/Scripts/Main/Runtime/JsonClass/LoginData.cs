namespace Main.Runtime.JsonClass
{
    public class SingleData
    {
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string userId { get; set; }
    }

    public class loginData
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SingleData singleData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string listData { get; set; }
    }
}