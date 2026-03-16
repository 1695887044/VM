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

namespace Plugin.Delay.ViewModels
{
    [Serializable]
    [PluginInfo(
        DisplayName = "延时",
        PluginName = "Delay",
        View = typeof(DelayView),
        ViewModel = typeof(DelayViewModel),
        Category = "常用工具",
        Icon = "M192.333 145.826c0-44.99 35.837-81.43 79.993-81.43H752.27c44.183 0 79.994 36.44 79.994 81.43v87.936c0 59.448-25.494 115.929-69.831 154.572L620.445 512.249l141.988 123.89a205.125 205.125 0 0 1 69.831 154.572v87.959c0 44.967-35.838 81.435-79.994 81.435H272.326a79.275 79.275 0 0 1-56.573-23.855 82.164 82.164 0 0 1-23.42-57.58v-87.933c0-59.421 25.521-115.876 69.83-154.571l141.985-123.917L262.163 388.36a205.126 205.126 0 0 1-69.83-154.598v-87.936z m559.937-1.999H272.326v89.936a123.067 123.067 0 0 0 41.929 92.743l141.958 123.89a82.05 82.05 0 0 1 27.955 61.829c0 23.775-10.217 46.347-27.955 61.828L314.255 697.994a123.07 123.07 0 0 0-41.929 92.743v88.933H752.27v-88.933a123.08 123.08 0 0 0-41.927-92.743L568.38 574.051a82.036 82.036 0 0 1-27.951-61.828c0-23.776 10.212-46.347 27.951-61.829L710.342 326.53a123.07 123.07 0 0 0 41.927-92.769v-89.934z m0 1.999 M469.347 615.039a41.241 41.241 0 0 1 60.308 0l85.336 89.591c12.185 12.826 15.845 32.103 9.241 48.817-6.604 16.737-22.141 27.643-39.419 27.643H414.157c-17.251 0-32.788-10.906-39.419-27.643a46.432 46.432 0 0 1 9.268-48.817l85.341-89.591z m0 0"
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
