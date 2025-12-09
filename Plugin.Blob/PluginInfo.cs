using Plugin.Blob.Properties;
using Plugin.Blob.ViewModels;
using Plugin.Blob.Views;
using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace Plugin.Blob
{
     internal class PluginInfo : IPlutionInfo
    {
        public string PluginName => "Blob";
        public string PluginIcon => Resources.Icon;
        public string Category => "图像处理";
        public string DisplayName => "斑点分析";
        public string Description => "Blob";
        public IModuleViewBase ViewType => new BlobView();
        public ModuleViewModelBase ViewModelType => new BlobViewModel();
        public int Code => 200;

    }
}
