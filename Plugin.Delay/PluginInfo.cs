using Plugin.Delay.Properties;
using Plugin.Delay.ViewModels;
using Plugin.Delay.Views;
using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace Plugin.Delay
{
     internal class PluginInfo : IPlutionInfo
    {
        public string PluginName => "Delay";
        public string PluginIcon => Resources.Icon;
        public string Category => "常用工具";
        public string DisplayName => "延时工具";
        public string Description => "Delay";
        public IModuleViewBase ViewType => new DelayView();
        public ModuleViewModelBase ViewModelType => new DelayViewModel();
        public int Code => 200;

    }
}
