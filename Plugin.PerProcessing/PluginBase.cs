
using Plugin.PerProcessing.ViewModels;
using Plugin.PerProcessing.Views;
using VM.IPlugin.ModuleEvent;

namespace Plugin.PerProcessing
{
    [Module(ModuleName = "PerProcessing")]
    public class PluginBase : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //加载完成后 事件通知主界面  使其注册到主界面中
            var eventAggregator = containerProvider.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<PluginEvent>().Publish(new PluginInfo());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PerPrecessView, PerPrecessViewModel>();
        }
    }
}
