using Plugin.Blob.Properties;
using VM.IPlugin;

namespace Plugin.Blob
{
     internal class PluginInfo : IPlutionInfo
    {
        public string PluginName => "Blob";
        public string PluginIcon => Resources.Icon;
        public string Category => "图像处理";
        public string DisplayName => "斑点分析";
        public string Description => "Blob";
        public IModuleViewBase ViewType => null;
        public ModuleViewModelBase ViewModelType => null;
        public int Code => 200;

    }
}
