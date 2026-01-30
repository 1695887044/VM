using System.ComponentModel.DataAnnotations;
using HalconDotNet;
using Microsoft.Win32;
using VM.IPlugin;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.GrabImage.ViewModels
{
    internal class GrabImageViewModel : ModuleViewModelBase
    {
        #region  //Commands
        public DelegateCommand SelectImageCommand { get; private set; }
        public DelegateCommand LinkViewCommand { get; private set; }
        #endregion

        #region //Props
        private VarValue<string> imageSourcePath = new();
        public VarValue<string> ImageSourcePath
        {
            get { return imageSourcePath; }
            set
            {
                imageSourcePath = value;
                RaisePropertyChanged();
            }
        }

        private HImage displayImage;
        private readonly IEventAggregator eventAggregator;

        [Display(Name = "输出图像", Description = "采集-源图像")]
        public HImage DisplayImage
        {
            get { return displayImage; }
            set
            {
                displayImage = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        public GrabImageViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            initCommands();
        }

        private void initCommands()
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImage);
            LinkViewCommand = new DelegateCommand(OpenLink);
        }

        private void OpenLink()
        {
            OpenLinkargs openLinkargs = new OpenLinkargs();
            openLinkargs.guid = ModuleData.ModuleGuid;
            openLinkargs.name = "GrabImage";
            openLinkargs.Fiter = (s => s.DataType == "HImage");
            OpenVarLinkView(openLinkargs);
        }

        /// <summary>
        /// 触发选择图片
        /// </summary>
        private void ExecuteSelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            if (openFileDialog.ShowDialog() != true)
                return;
            var img = new HImage();
            ImageSourcePath.Value = openFileDialog.FileName;
            img.ReadImage(openFileDialog.FileName);
            ModuleData.SetVarValue<HImage>(
                "DisplayImage",
                (
                    s =>
                    {
                        s.Value = img;
                        DisplayImage = s.Value;
                        eventAggregator.GetEvent<RefreshUIEvent<HImage>>().Publish(s);
                    }
                )
            );
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
            return true;
        }

        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
            if (changedEvent.varValue is VarValue<HImage> linkvar)
            {
                linkvar.OnValueChanged += Linkvar_OnValueChanged;
                DisplayImage = linkvar.Value;
            }
        }

        private void Linkvar_OnValueChanged(object? sender, HImage e)
        {
            DisplayImage = e;
        }
    }
}
