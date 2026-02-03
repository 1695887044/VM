using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.DiplayData.ViewModels
{
    public class DisplayDataViewModel : ModuleViewModelBase, ILinkable
    {
        private VarValue<int> _indexLink;

        public VarValue<int> IndexLink
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

        public override bool Execute()
        {
            return true;
        }
    }
}
