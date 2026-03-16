using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using NLog;
using Plugin.HalconScript.Core.Helper;
using Plugin.HalconScript.Models;
using Plugin.HalconScript.Services;
using Plugin.HalconScript.Views;
using ScintillaNET;
using VM.Halcon.Base;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models.VarModels;
using VM.Shard.Attritubess;
using VM.Shard.Services;

namespace Plugin.HalconScript.ViewModels
{

    [Serializable]
    [PluginInfo(
        DisplayName = "Halcon脚本",
        PluginName = "Halcon脚本",
        View = typeof(MainView),
        ViewModel = typeof(MainViewModel),
        Category = "检测识别",
        Icon = "M374.36361787 505.58195146c-3.18204229-7.63582324-11.95199648-11.24507549-19.58781972-8.0630332L168.59213018 575.1201043c-12.17954268 5.0761582-12.31984688 22.28059336-0.22484795 27.55461797l185.79243251 81.01617099c7.58275928 3.30615879 16.41027393-0.1600919 19.71643184-7.74285029 3.30615879-7.58275928-0.1600919-16.41027393-7.74285029-19.71643184L212.54083349 589.2558207l153.75975118-64.08694863C373.93640703 521.98682891 377.54566016 513.21687471 374.36361787 505.58195146z M871.81274727 575.1201043L685.6290793 497.51891826c-7.63582324-3.18204229-16.40487744 0.42720997-19.58781972 8.0630332-3.18204229 7.63582324 0.42720997 16.40487744 8.0630332 19.58781973l153.75975116 64.08694863-153.59336366 66.97489132c-7.58275928 3.30615879-11.04900908 12.13367344-7.7428503 19.71643183 3.30615879 7.58275928 12.13367344 11.04900908 19.71643184 7.74285029l185.79243252-81.01617099C884.13169502 597.40069854 883.99228906 580.1962625 871.81274727 575.1201043z M564.4830166 384.60317539c-7.95510615-2.26736279-16.24208818 2.34381094-18.50945097 10.29981709l-105.18368555 369.09175606c-2.26736279 7.95510615 2.34381094 16.24298731 10.29981709 18.50945097s16.24208818-2.34381094 18.50945098-10.29981709l105.18368554-369.09175606C577.0501956 395.15751933 572.43902276 386.87053818 564.4830166 384.60317539z M62.01888682 62l0 898.68778945 898.68778945 0L960.70667714 241.73773789l1e-8-29.95599023L960.70667714 62 62.01888682 62zM91.97487705 930.7318001L91.97487705 241.73773789l14.97844424 0 823.79736563 0 0 688.99406221L91.97487705 930.7318001zM930.75068692 211.78084854L106.95332128 211.78084854 91.97487705 211.78084854 91.97487705 91.95599023l838.77491074 0L930.74978779 211.78084854z"
    )]
    public class MainViewModel : ModuleViewModelBase
    {
        private readonly IScriptParserService _scriptParser;
        private readonly IKeywordService _keywordService;
        private readonly ILoggerService _logger;
        public DelegateCommand<string> OperatorCommand { get; init; }
        public DelegateCommand<LinkPathParam> LabelLinkCommand { get; init; }
        public ObservableCollection<IHalconVariable> HalconIn { get; } = new();
        public ObservableCollection<IHalconVariable> HalconOut { get; } = new();
        private string haclonName;

        public string HaclonName
        {
            get { return haclonName; }
            set { haclonName = value; RaisePropertyChanged(); }
        }


        private String halconCode;

        public String HalconCode
        {
            get { return halconCode; }
            set { halconCode = value;RaisePropertyChanged(); }
        }

        public MainViewModel( ILoggerService logger)
        {
            _scriptParser = new HalconXmlParserService() ;
            _keywordService = new HalconKeywordService();
            _logger = logger;

            OperatorCommand = new DelegateCommand<string>(OperatorMethod);
            LabelLinkCommand = new DelegateCommand<LinkPathParam>(OpenLink);
        }


        private void OpenLink(LinkPathParam obj)
        {
           if(obj.PathType == VM.IPlugin.Enums.LinkPathType.Link)
            {
                
            }
        }

        private void OperatorMethod(string obj)
        {
            if (obj.Equals("Import"))
            {
                ImportScript();
            }
            if (obj.Equals("Export")) { }
        }

        void ImportScript()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择halcon文件";
            openFileDialog.Filter = "halcon文件|*.hdev;*.hdvp|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == true)
            {

               loadHalconScript(openFileDialog.FileName);
            }
        }

        private void loadHalconScript(string fileName)
        {
            var procedure = _scriptParser.ParseFromFile(fileName);
          //  HalconName = procedure.Name;
            HalconCode = procedure.Body;
         
            // 更新变量集合
            HalconIn.Clear();
            HalconOut.Clear();
            procedure.IconicInputs.ForEach(n => HalconIn.Add(new HalconVariable(n, 0)));
            procedure.ControlInputs.ForEach(n => HalconIn.Add(new HalconVariable(n, 1)));
            procedure.IconicOutputs.ForEach(n => HalconOut.Add(new HalconVariable(n, 0)));
            procedure.ControlOutputs.ForEach(n => HalconOut.Add(new HalconVariable(n, 1)));

            ModuleData.VarOut.RemoveAll(s => s.Category == 1);
            _logger.LogInfo($"成功导入脚本: {procedure.Name}");
        }

        protected override bool Execute()
        {
            
            return true;
        }
    }
}
