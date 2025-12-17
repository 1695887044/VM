using HalconDotNet;
using HandyControl.Controls;
using Plugin.PerProcessing.Model;
using Plugin.PerProcessing.Services;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows;
using VM.Halcon.Extensions;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.PerProcessing.ViewModels
{
    public class PerPrecessViewModel : ModuleViewModelBase
    {
        
        private ObservableCollection<DrawingObjectInfo> roiData = new();

        public ObservableCollection<DrawingObjectInfo> RoiData
        {
            get { return roiData; }
            set { roiData = value; RaisePropertyChanged(); }
        }

        private ImageInfo imageInfo = new();

        public ImageInfo ImageInfo
        {
            get { return imageInfo; }
            set { imageInfo = value; RaisePropertyChanged(); }
        }


        private HImage currentHImage;

        public HImage CurrentHImage
        {
            get { return currentHImage; }
            set { currentHImage = value;RaisePropertyChanged(); }
        }
        private ObservableCollection<ToolModel> m_ToolData =new();
        private readonly IEventAggregator aggregator;

        public ObservableCollection<ToolModel> M_ToolData
        {
            get { return m_ToolData; }
            set { m_ToolData = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<string> LinkPathCommand { get; private set; }
        public DelegateCommand<string> DataOperateCommand { get; private set; }
        public DelegateCommand<string> ToolDataOperateCommand { get; private set; }
        public PerPrecessViewModel(IEventAggregator aggregator)
        {
            LinkPathCommand = new DelegateCommand<string>(LinkMethod);
            DataOperateCommand = new DelegateCommand<string>(ChangedToolMethod);
            ToolDataOperateCommand =new DelegateCommand<string>(ToolDataOperate);
            this.aggregator = aggregator;
        }

        private void ToolDataOperate(string obj)
        {


        }

        private void ChangedToolMethod(string obj)
        {
            M_ToolData.Add(new ToolModel()
            {
                DisplayString = "二值化",
                Name = "二值化",
                Note = "二值化",
                ToolName= "二值化"
            });

        }

        private void LinkMethod(string obj)
        {
            OpenLinkargs openLinkargs = new OpenLinkargs();
            openLinkargs.guid = ModuleData.ModuleGuid;
            openLinkargs.name = "GrabImage";
            openLinkargs.Fiter = (s => s.DataType == "HImage");
            OpenVarLinkView(openLinkargs);
        }

        public override bool Cancel()
        {
            return true;
        }

        public override bool Confirm()
        {
            return true;
        }

        public override bool Execute()
        {
            if(ImageInfo.Image == null) return false;
            HImage TempOutImage = new HImage();
            HImage TempInImage = ImageInfo.Image.Clone();
            bool mem = false;
            foreach (var tool in M_ToolData)
            {
                if (mem)
                {
                    TempInImage = new HImage(TempOutImage);
                }
                mem = true;
                if (!tool.IsEnabled) continue;
                switch (tool.ToolName)
                {
                    case "二值化":
                        ComMethods.MethodService.Threshold(TempInImage, out TempOutImage,20,40,false);
                        break;
                    default:
                        break;
                }
            }
          
            ModuleData.SetVarValue<HImage>("图像", (s =>
            {
                s.Value = TempOutImage;
                CurrentHImage = TempOutImage;
            }
));
            return true;

        }

        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
            if (changedEvent.varValue is VarValue<HImage> linkvar)
            {
                linkvar.OnValueChanged += Linkvar_OnValueChanged;
                CurrentHImage = linkvar.Value;
            }
        }
        private void Linkvar_OnValueChanged(object? sender, HImage e)
        {
            CurrentHImage = e;
        }
        public override void RegisterOut()
        {
            base.RegisterOut();
            ModuleData.AppendOutVar("图像", "HImage", CurrentHImage);
        }
    }
}
