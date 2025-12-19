
using CreateROI.ViewModels;
using CreateROI.Views;
using VM.IPlugin.ModuleEvent;

namespace CreateROI
{
    public class PluginInfo : IPlutionInfo
    {
        public string PluginName => "CreateROI";
        public string PluginIcon => "M290.9 402.4l109.4 122.5 71.6-64 105.7-94.5 69.9 78.3 68-68.1-66.7-74.7-63.5-71-177.4 158.4-47.3-53-62.1-69.5-158.9 142V194.2h704v189.6h96V98.2h-896v704h319.8v-96H139.6V537.5z" +
            "M969.2 586L930 546.8l0.1-0.1-114.7-114.8-34-34-342.3 342.3 0.1 0.1h-0.1V928.2h187.8L969.2 586zM534.1 779.5l247.3-247.3 53.6 53.6-247.4 247.4h-53.5v-53.7z";
        public string Category => "检测识别";
        public string DisplayName => "创建ROI";
        public string Description => "CreateROI";
        public Type ViewType => typeof(CreateROIView);
        public Type ViewModelType => typeof(CreateROIViewModel);
        public int Code => 200;
    }
}
