using HalconDotNet;
using Plugin.PerProcessing.Common;
using Plugin.PerProcessing.Model;
using Plugin.PerProcessing.Services;
using System.Collections.ObjectModel;
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

        private IToolData currentData;

        public IToolData CurrentItem
        {
            get { return currentData; }
            set { currentData = value; RaisePropertyChanged(); }
        }

        private HImage currentHImage;

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
            if(CurrentHImage == null) return false;
            HImage TempOutImage = CurrentHImage.Clone();
            HImage TempInImage = new HImage();
            foreach (var tool in M_ToolData)
            {
                TempInImage = new HImage(TempOutImage);
                if (!tool.IsEnabled) continue;
                TempOutImage = ComMethods.MethodService.ImageOperator(TempInImage,tool);
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
