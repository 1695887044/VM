using Plugin.DiplayData.Properties;
using Plugin.DiplayData.ViewModels;
using Plugin.DiplayData.Views;
using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace Plugin.DiplayData
{
     internal class PluginInfo : IPlutionInfo
    {
        public string PluginName => "DiplayData";
        public string PluginIcon => Resources.Icon;
        public string Category => "图像处理";
        public string DisplayName => "数据展示";
        public string Description => "DiplayData";
        public IModuleViewBase ViewType => new DisplayDataView();
        public ModuleViewModelBase ViewModelType => new DisplayDataViewModel();
        public int Code => 200;

    }
}
