using Plugin.GrabImage.Common;
using Plugin.GrabImage.ViewModels;
using Plugin.GrabImage.Views;
using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace Plugin.GrabImage
{
    internal class PluginInfo : IPlutionInfo
    {
        public string PluginName => "GrabImage";
        public string PluginIcon => IconConst.IconText;
        public string Category => "图像处理";
        public string DisplayName => "图像采集";
        public string Description => "GrabImage";
        public Type ViewType => typeof(GrabImageView);
        public Type ViewModelType => typeof(GrabImageViewModel);
        public int Code => 200;

       
    }
}
