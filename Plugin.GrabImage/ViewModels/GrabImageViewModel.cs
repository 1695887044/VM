using System.ComponentModel.DataAnnotations;
using HalconDotNet;
using Microsoft.Win32;
using Plugin.GrabImage.Common;
using Plugin.GrabImage.Views;
using VM.IPlugin;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;
using VM.Shard.Services;

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


        #region //Props

        private LocalImageViewModel _localImageViewModel;
        private DirectoryGrabViewModel _directoryGrabViewModel;
        private CameraSettingViewModel _cameraSettingViewModel;
        private LocalImageViewModel LocalImageVM =>
            _localImageViewModel ??= new LocalImageViewModel(dialogService);

        private DirectoryGrabViewModel DirectoryGrabVM =>
            _directoryGrabViewModel ??= new DirectoryGrabViewModel(dialogService);

        private CameraSettingViewModel CameraSettingVM =>
            _cameraSettingViewModel ??= new CameraSettingViewModel();

        private HImage displayImage;
        private readonly IEventAggregator eventAggregator;
        private readonly ISuperDialogService dialogService;


       [Display(Name = "输出图像", Description = "采集-源图像")]
        public HImage DisplayImage
        {
            get { return displayImage; }
            set
            {
                displayImage?.Dispose();
                displayImage = value;
                RaisePropertyChanged();
            }
        }
        private BaseSettingViewModel grabViewModel;

        public BaseSettingViewModel GrabViewModel
        {
            get { return grabViewModel; }
            set
            {
                
                if (grabViewModel == value) return;
                if (grabViewModel != null)
                {
                    grabViewModel.OnImageChanged -= GrabViewModel_OnImageChanged;
                }
                grabViewModel = value;
                if (grabViewModel != null)
                {
                    grabViewModel.OnImageChanged += GrabViewModel_OnImageChanged;
                }
                RaisePropertyChanged();
            }
        }

        public DelegateCommand<string> ChangeModeCommand { get; set; }


        #endregion


        public GrabImageViewModel(IEventAggregator eventAggregator,ISuperDialogService dialogService)
        {
            this.eventAggregator = eventAggregator;
            this.dialogService = dialogService;
            ChangeModeCommand = new DelegateCommand<string>(ChangeMode);
        }

        private void GrabViewModel_OnImageChanged(string obj)
        {
            HImage image = new HImage();
            image.ReadImage(obj);
            DisplayImage = image;
            var data = ModuleData.SetVarValue(nameof(DisplayImage), DisplayImage);
            eventAggregator.GetEvent<RefreshUIEvent<HImage>>().Publish(data);
        }

        private void ChangeMode(string obj)
        {
            GrabViewModel = obj switch
            {
                "1" => LocalImageVM,
                "2" => DirectoryGrabVM,
                "3" => CameraSettingVM,
                _ => throw new ArgumentException("未知的采集模式") 
            };
        }
        protected override bool Execute()
        {
            return true;
        }
    }
}
