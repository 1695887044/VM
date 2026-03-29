using Plugin.Delay.Views;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Enums;
using VM.IPlugin.Models;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;
using VM.Shard.Resources;

namespace Plugin.Delay.ViewModels
{
    [Serializable]
    [PluginInfo(
        DisplayName = "延时",
        PluginName = "Delay",
        View = typeof(DelayView),
        ViewModel = typeof(DelayViewModel),
        Category = "常用工具",
        Icon =Icons.Icon_Delay
    )]
    public class DelayViewModel : ModuleViewModelBase, ILinkable
    {
        public DelegateCommand<LinkPathParam> LinkPathCommand { get; init; }

        private DataPort<int> setDelayValue;

        public DataPort<int> SetDelayValue
        {
            get { return setDelayValue; }
            set
            {
                setDelayValue = value;
                RaisePropertyChanged();
            }
        }

        private int crtTime;

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
