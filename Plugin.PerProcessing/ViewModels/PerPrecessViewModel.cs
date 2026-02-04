using HalconDotNet;
using Microsoft.Win32;
using Plugin.PerProcessing.Model;
using Plugin.PerProcessing.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.PerProcessing.ViewModels
{
    public class PerPrecessViewModel : ModuleViewModelBase
    {


        private VarValue<HImage> curImage;
        
        public VarValue<HImage> CurImage
        {
            get { return curImage; }
            set { curImage = value; RaisePropertyChanged(); }
        }

        private IToolData currentData;

        public IToolData CurrentItem
        {
            get { return currentData; }
            set { currentData = value; RaisePropertyChanged(); }
        }

     

        private HImage currentHImage;
        [Display(Name = "输出图像")]
        public HImage CurrentHImage
        {
            get { return currentHImage; }
            set { currentHImage = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<IToolData> m_ToolData =new();
        private readonly IEventAggregator aggregator;

        public ObservableCollection<IToolData> M_ToolData
        {
            get { return m_ToolData; }
            set { m_ToolData = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<LinkPathParam> LinkPathCommand { get; private set; }
        public DelegateCommand<string> DataOperateCommand { get; private set; }
        public DelegateCommand<string> ToolDataOperateCommand { get; private set; }
        public PerPrecessViewModel(IEventAggregator aggregator)
        {
            LinkPathCommand = new DelegateCommand<LinkPathParam>(LinkMethod);
            DataOperateCommand = new DelegateCommand<string>(ChangedToolMethod);
            ToolDataOperateCommand =new DelegateCommand<string>(ToolDataOperate);
            this.aggregator = aggregator;

        }

        private void ToolDataOperate(string obj)
        {
            if(obj == "D")
            {
                M_ToolData.Remove(CurrentItem);
                CurrentItem = null;
            }
        }
        /// <summary>
        /// 增加模块
        /// </summary>
        /// <param name="obj"></param>
        private void ChangedToolMethod(string obj)
        {
            IToolData? data = ToolFactory.CreateTool(obj);
            if (data == null) return;
            data.Name = obj;
            M_ToolData.Add(data);
            CurrentItem= data;
        }

        private void LinkMethod(LinkPathParam p)
        {
           if(p.PathType == VM.IPlugin.Enums.LinkPathType.Link)
            {
                OpenVarLinkView<HImage>(s => CurImage = s.varValue);
                ModuleData.SetVarValue(nameof(CurrentHImage), CurImage.Value);
            }
        }
        private HImage hImageMemory;
        protected override bool Execute()
        {
            if (CurImage == null || CurImage.Value == null) return false;
            HImage TempOutImage = hImageMemory == null ? CurImage.Value.Clone() : hImageMemory.Clone();
            HImage TempInImage = new HImage();
            foreach (var tool in M_ToolData)
            {
                if (!tool.IsEnabled) continue;
                TempInImage = new HImage(TempOutImage);
                TempOutImage = ComMethods.MethodService.ImageOperator(TempInImage,tool);
            }
            ModuleData.SetVarValue(nameof(CurrentHImage), TempOutImage);
            return true;

        }

    }
}
