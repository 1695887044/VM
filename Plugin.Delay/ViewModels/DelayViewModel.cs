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
        Icon = "M725.232941 290.936471a30.117647 30.117647 0 0 0-30.117647 30.117647h-245.76a30.117647 30.117647 0 0 0-60.235294 0A178.296471 178.296471 0 0 0 572.235294 493.628235a178.296471 178.296471 0 0 0 183.115294-172.574117 30.117647 30.117647 0 0 0-30.117647-30.117647z M632.470588 720.414118a30.117647 30.117647 0 0 0 30.117647-30.117647v-33.430589a30.117647 30.117647 0 0 0-60.235294 0v33.430589a30.117647 30.117647 0 0 0 30.117647 30.117647z M966.174118 879.134118h-39.755294a27.708235 27.708235 0 0 0 6.324705-17.468236c0-174.381176-87.04-322.258824-207.510588-373.458823 113.242353-48.489412 196.969412-182.512941 206.305883-343.341177h34.635294a30.117647 30.117647 0 1 0 0-60.235294H298.767059a30.117647 30.117647 0 0 0 0 60.235294h34.635294c9.336471 160.828235 93.063529 294.851765 206.305882 343.341177-120.470588 51.2-207.510588 199.077647-207.510588 373.458823a27.708235 27.708235 0 0 0 6.324706 17.468236H298.767059a30.117647 30.117647 0 1 0 0 60.235294h667.407059a30.117647 30.117647 0 0 0 0-60.235294zM871.604706 144.865882a438.814118 438.814118 0 0 1-25.901177 122.277647 42.767059 42.767059 0 0 0-6.023529 0H435.2A28.310588 28.310588 0 0 0 421.647059 271.058824a429.477647 429.477647 0 0 1-27.105883-125.590589z m-424.357647 180.705883h370.447059c-43.971765 74.089412-110.832941 120.470588-185.22353 120.470588s-141.251765-45.778824-185.223529-119.868235zM602.352941 531.877647v30.117647a30.117647 30.117647 0 0 0 60.235294 0v-30.117647c92.461176 16.263529 168.96 105.411765 197.571765 224.978824H404.781176c28.611765-119.567059 105.110588-208.715294 197.571765-224.978824z m-210.823529 331.294118a432.489412 432.489412 0 0 1 2.409412-44.574118h476.16a432.489412 432.489412 0 0 1 3.312941 43.068235 30.117647 30.117647 0 0 0 6.324706 17.468236H386.108235a30.117647 30.117647 0 0 0 5.421177-17.468236z"
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
