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
        Category = "检测识别"
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
