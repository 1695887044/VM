
using Plugin.DiplayData.Properties;
using VM.Shard.Events;

namespace Plugin.DiplayData
{
    [Module(ModuleName = "DiplayData")]

    public class ModuleBase : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //加载完成后 事件通知主界面  使其注册到主界面中
            var eventAggregator = containerProvider.Resolve<IEventAggregator>();
            PolutionInfo info = new PolutionInfo()
            {
                PluginName = "DiplayData",
                Category = "图像处理",
                DisplayName = "数据显示",
                Description = "DiplayData",
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
