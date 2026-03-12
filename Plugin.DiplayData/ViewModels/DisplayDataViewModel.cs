using Plugin.DiplayData.Views;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;

namespace Plugin.DiplayData.ViewModels
{
    [Serializable]
    [PluginInfo(
        DisplayName = "数据展示",
        PluginName = "DiplayData",
        View = typeof(DisplayDataView),
        ViewModel = typeof(DisplayDataViewModel),
        Category = "常用工具",
        Icon = "M0 64l0 640 1024 0 0-640-1024 0zM960 640l-896 0 0-512 896 0 0 512zM672 768l-320 0-32 128-64 64 512 0-64-64z"
    )]
    public class DisplayDataViewModel : ModuleViewModelBase, ILinkable
    {
        private DataPort<int> _indexLink;

        public DataPort<int> IndexLink
        {
            get { return _indexLink; }
            set
            {
                _indexLink = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand<LinkPathParam> LinkPathCommand { get; init; }

        public DisplayDataViewModel()
        {
            LinkPathCommand = new(LinkMethods);
        }

        private void LinkMethods(LinkPathParam param)
        {
            if (param.PathType == VM.IPlugin.Enums.LinkPathType.Link)
            {
                OpenVarLinkView<int>(s => IndexLink = s.varValue);
            }
        }

        protected override bool Execute()
        {
            return true;
        }
    }
}
