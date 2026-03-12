using System.ComponentModel.DataAnnotations;
using HalconDotNet;
using Microsoft.Win32;
using Plugin.GrabImage.Common;
using Plugin.GrabImage.Views;
using VM.IPlugin;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;

namespace Plugin.GrabImage.ViewModels
{
    [Serializable]
    [PluginInfo(
        DisplayName = "图像采集",
        PluginName = "GrabImage",
        View = typeof(GrabImageView),
        ViewModel = typeof(GrabImageViewModel),
        Category = "常用工具",
        Icon =IconConst.IconText
    )]
    public class GrabImageViewModel : ModuleViewModelBase
    {
        #region  //Commands
        public DelegateCommand SelectImageCommand { get; private set; }
        #endregion

        #region //Props
        private string imageSourcePath;
        public string ImageSourcePath
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
            ImageSourcePath = openFileDialog.FileName;
            HImage image = new HImage();
            image.ReadImage(ImageSourcePath);
            DisplayImage = image;
            var data = ModuleData.SetVarValue(nameof(DisplayImage), DisplayImage);
            eventAggregator.GetEvent<RefreshUIEvent<HImage>>().Publish(data);
        }

        protected override bool Execute()
        {
            return true;
        }
    }
}
