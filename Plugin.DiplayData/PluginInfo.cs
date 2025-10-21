using Plugin.DiplayData.Properties;
using VM.IPlugin;

namespace Plugin.DiplayData
{
     internal class PluginInfo : IPlutionInfo
    {
        public string PluginName => "DiplayData";
        public string PluginIcon => Resources.Icon;
        public string Category => "常用工具";
        public string DisplayName => "数据展示";
        public string Description => "DiplayData";
        public IModuleViewBase ViewType => null;
        public ModuleViewModelBase ViewModelType => null;
        public int Code => 200;

    }
}
