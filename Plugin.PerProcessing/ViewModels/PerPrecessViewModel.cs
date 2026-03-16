using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using HalconDotNet;
using Microsoft.Win32;
using Plugin.PerProcessing.Model;
using Plugin.PerProcessing.Services;
using Plugin.PerProcessing.Views;
using Prism.Dialogs;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;
using W.UI.Controls;

namespace Plugin.PerProcessing.ViewModels
{
    [Serializable]
    [PluginInfo(
        DisplayName = "图像预处理",
        PluginName = "PerProcessing",
        View = typeof(PerPrecessView),
        ViewModel = typeof(PerPrecessViewModel),
        Category = "检测识别",
        Icon = "M53.999 37v796.434h150.767c28.433 99.567 113.766 170.666 219 170.666 105.267 0 190.6-71.1 219.033-170.666h321.434V37H53.999z m201.967 739.567c11.366-85.333 82.466-150.766 167.8-150.766 85.333 0 156.467 65.433 167.833 150.766H255.966z m651.366 0h-256c-5.7-59.733-34.133-110.933-76.8-147.933l82.5-96.7c17.067 5.7 34.134 8.533 51.2 8.533a140.834 140.834 0 0 0 142.2-142.233 140.834 140.834 0 0 0-142.2-142.2c-56.9 0-105.267 34.133-128 79.633l-170.666-42.666a113.05 113.05 0 0 0-113.8-108.1 114.108 114.108 0 0 0-113.767 113.8c0 62.566 51.2 113.766 113.766 113.766 45.534 0 82.5-25.6 102.4-62.566l170.667 42.666v5.667c0 39.833 17.067 76.8 42.667 102.4L529.033 594.5a210.398 210.398 0 0 0-102.4-25.6c-119.467 0-216.201 91.034-227.567 207.667h-85.334V93.901h796.433v682.666h-2.833zM622.9 401.101c0-48.367 36.967-85.334 85.334-85.334 48.332 0 85.333 36.967 85.333 85.334 0 48.333-37 85.333-85.333 85.333-48.367 0-85.334-39.833-85.334-85.333z m-270.233-99.567c0 31.3-25.6 56.9-56.9 56.9-31.233-0.034-56.833-25.634-56.833-56.9s25.6-56.9 56.866-56.9c31.3 0 56.9 25.6 56.9 56.9z"
    )]
    public class PerPrecessViewModel : ModuleViewModelBase
    {
        private DataPort<HImage> curImage;

        public DataPort<HImage> CurImage
        {
            get { return curImage; }
            set
            {
                curImage = value;
                RaisePropertyChanged();
            }
        }

        private IToolData currentData;

        public IToolData CurrentItem
        {
            get { return currentData; }
            set
            {
                currentData = value;
                RaisePropertyChanged();
            }
        }

        private HImage currentHImage;

        [Display(Name = "输出图像")]
        public HImage CurrentHImage
        {
            get { return currentHImage; }
            set
            {
                currentHImage = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<IToolData> m_ToolData = new();
        private readonly IEventAggregator aggregator;
        private readonly IDialogService dialogService;

        public ObservableCollection<IToolData> M_ToolData
        {
            get { return m_ToolData; }
            set
            {
                m_ToolData = value;
                RaisePropertyChanged();
            }
        }
        public DelegateCommand<LinkPathParam> LinkPathCommand { get; private set; }
        public DelegateCommand<string> DataOperateCommand { get; private set; }
        public DelegateCommand<string> ToolDataOperateCommand { get; private set; }

        public PerPrecessViewModel(IEventAggregator aggregator, IDialogService dialogService)
        {
            LinkPathCommand = new DelegateCommand<LinkPathParam>(LinkMethod);
            DataOperateCommand = new DelegateCommand<string>(ChangedToolMethod);
            ToolDataOperateCommand = new DelegateCommand<string>(ToolDataOperate);
            this.aggregator = aggregator;
            this.dialogService = dialogService;
        }

        private void ToolDataOperate(string obj)
        {
            if (obj == "D")
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
            //显示原图
            if (obj.Equals("显示原图"))
            {
                CurrentHImage = CurImage.Value;
                return;
            }

            IToolData? data = ToolFactory.CreateTool(obj);
            if (data == null)
                return;
            data.Name = obj;
            M_ToolData.Add(data);
            CurrentItem = data;
        }

        private void LinkMethod(LinkPathParam p)
        {
            if (p.PathType == VM.IPlugin.Enums.LinkPathType.Link)
            {
                OpenVarLinkView<HImage>(s => CurImage = s.varValue);
                CurrentHImage = CurImage.Value;
                ModuleData.SetVarValue(CurrentHImage, CurImage.Value);
            }
        }

        private HImage hImageMemory;

        protected override bool Execute()
        {
            if (CurImage == null || CurImage.Value == null)
                return false;
            HImage TempOutImage =
                hImageMemory == null ? CurImage.Value.Clone() : hImageMemory.Clone();
            HImage TempInImage = new HImage();
            foreach (var tool in M_ToolData)
            {
                if (!tool.IsEnabled)
                    continue;
                TempInImage = new HImage(TempOutImage);
                TempOutImage = ComMethods.MethodService.ImageOperator(TempInImage, tool);
            }
            CurrentHImage = TempOutImage;
            ModuleData.SetVarValue(CurrentHImage, TempOutImage);
            return true;
        }
    }
}
