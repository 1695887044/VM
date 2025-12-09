namespace VM.IPlugin.ModuleEvent
{
    public class PluginEvent : PubSubEvent<IPlutionInfo> { }
    public interface IPlutionInfo
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get;}

        /// <summary>
        /// 插件图标
        /// </summary>
        public string PluginIcon { get;}

        /// <summary>
        /// 分组
        /// </summary>
        public string Category { get;}
        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get;  }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// 视图类型
        /// </summary>
        public IModuleViewBase ViewType { get; }
        /// <summary>
        /// 视图后台类型
        /// </summary>

        public ModuleViewModelBase ViewModelType { get; }


        public int Code { get;  }
    }
}
