using HalconDotNet;
using Plugin.PerProcessing.Model;
using Plugin.PerProcessing.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.PerProcessing.ViewModels
{
    public class PerPrecessViewModel : ModuleViewModelBase
    {
        private bool useRoi;

        public bool UseRoi
        {
            get { return useRoi; }
            set { useRoi = value; RaisePropertyChanged(); }
        }

        private string linkPath;

        public string LinkPath
        {
            get { return linkPath; }
            set { linkPath = value; RaisePropertyChanged(); }
        }

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

        private HImage hImageMemory;

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
                var openLinkargs = new OpenLinkargs();
                openLinkargs.Fiter = (s => s.DataType == "HImage");
                openLinkargs.CallBack = s => {

                    if (s.varValue is VarValue<HImage> d)
                    {
                        CurImage = d;
                        CurrentHImage = d.Value;
                    }
                };
            }
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
            if(_linkvar != null && _linkvar.Value != null)
            {
                CurrentHImage = _linkvar.Value;
            }
            if (CurrentHImage == null) return false;
            HImage TempOutImage = hImageMemory == null ? CurrentHImage.Clone() : hImageMemory.Clone();
            HImage TempInImage = new HImage();
            foreach (var tool in M_ToolData)
            {
                if (!tool.IsEnabled) continue;
                TempInImage = new HImage(TempOutImage);
                TempOutImage = ComMethods.MethodService.ImageOperator(TempInImage,tool);
            }        
            ModuleData.SetVarValue<HImage>("预处理图像", (s =>
            {
                s.Value = TempOutImage;
                CurrentHImage = TempOutImage;
            }
            ));
            return true;

        }
        VarValue<HImage> _linkvar;
        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
            if (changedEvent.varValue is VarValue<HImage> linkvar)
            {
                _linkvar = linkvar;
                _linkvar.OnValueChanged += Linkvar_OnValueChanged;
                LinkPath = $"{_linkvar.LinkPath}&&{_linkvar.Name}";
                CurrentHImage = null;
               CurrentHImage = _linkvar.Value;
            }
        }
        private void Linkvar_OnValueChanged(object? sender, HImage e)
        {
            CurrentHImage = e;
        }
    }
}
