using Plugin.Blob.Properties;
using VM.Shard.Events;

namespace Plugin.Blob
{
    [Module(ModuleName = "Blob")]

    public class ModuleBase : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //加载完成后 事件通知主界面  使其注册到主界面中
            var eventAggregator = containerProvider.Resolve<IEventAggregator>();
            PolutionInfo info = new PolutionInfo()
            {
                PluginName = "Blob",
                Category = "图像处理",
                DisplayName = "斑点分析",
                Description = "Blob",
                PluginIcon = Resources.Icon,
                ViewType = null,
                ViewModelType = null,
                Code = 200
            };
            eventAggregator.GetEvent<PluginEvent>().Publish(info);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

          
        }
    }
}
