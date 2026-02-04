using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Enums;
using VM.IPlugin.Models;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.Delay.ViewModels
{
    [Serializable]
    public class DelayViewModel : ModuleViewModelBase, ILinkable
    {
        public DelegateCommand<LinkPathParam> LinkPathCommand { get; init; }

        private VarValue<int> setDelayValue;

        public VarValue<int> SetDelayValue
        {
            get { return setDelayValue; }
            set
            {
                setDelayValue = value;
                RaisePropertyChanged();
            }
        }

        private int crtTime;

        [Display(Name = "定时器输出")]
        public int CrtTime
        {
            get { return crtTime; }
            set
            {
                crtTime = value;
                RaisePropertyChanged();
            }
        }

        public DelayViewModel()
        {
            LinkPathCommand = new(OpenLink);
        }

        private void OpenLink(LinkPathParam param)
        {
            OpenVarLinkView<int>(s => SetDelayValue = s.varValue);
        }

        protected override bool Execute()
        {
            for (int i = 0; i < SetDelayValue.Value / 10; i++)
            {
                CrtTime = i * 10;
                System.Threading.Thread.Sleep(10);
            }
            return true;
        }
    }
}
