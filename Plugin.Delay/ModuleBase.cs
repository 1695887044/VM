using Plugin.Delay.Properties;
using Plugin.Delay.ViewModels;
using Plugin.Delay.Views;
using VM.Shard.Events;

namespace Plugin.Delay
{
    [Module(ModuleName = "Delay")]

    public class ModuleBase : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //加载完成后 事件通知主界面  使其注册到主界面中
            var eventAggregator = containerProvider.Resolve<IEventAggregator>();
            PolutionInfo info = new PolutionInfo()
            {
                PluginName = "Delay",
                Category = "常用工具",
                DisplayName = "延时工具",
                Description = "Delay",
                PluginIcon = Resources.Icon,
                ViewType = typeof(DelayView),
                ViewModelType = typeof(DelayViewModel),
                Code = 200
            };
            eventAggregator.GetEvent<PluginEvent>().Publish(info);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            containerRegistry.RegisterForNavigation<DelayView, DelayViewModel>();
        }
    }
}
