using Prism.Events;

namespace VM.Shard.Events
{
    public class PluginEvent : PubSubEvent<PolutionInfo>
    {
     
    }
    public class PolutionInfo
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get; set; }

        /// <summary>
        /// 插件图标
        /// </summary>
        public string PluginIcon { get; set; }

        /// <summary>
        /// 分组
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 视图类型
        /// </summary>

        public Type ViewType { get; set; }
        /// <summary>
        /// 视图后台类型
        /// </summary>

        public Type ViewModelType { get; set; }


        public int Code { get; set; }
    }
}
