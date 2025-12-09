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
                set { imageSourcePath = value; RaisePropertyChanged(); }
            }
        private HWindowControlWPF imageControl;

        public HWindowControlWPF ImageControl
        {
            get { return imageControl; }
            set { imageControl = value; RaisePropertyChanged(); }
        }

        #endregion

        public GrabImageViewModel()
        {
            ImageControl= new HWindowControlWPF();
            initCommands();
        }

        private void initCommands()
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImage);
            LinkViewCommand =new DelegateCommand(OpenLink);
        }

        private void OpenLink()
        {
            OpenLinkargs openLinkargs = new OpenLinkargs();
            openLinkargs.guid = Paramer.ModuleGuid;
            openLinkargs.name = "GrabImage";
            openLinkargs.Fiter = (s => s.DataType == "image");
            OpenVarLinkView(openLinkargs);
        }

        /// <summary>
        /// 触发选择图片
        /// </summary>
        private void ExecuteSelectImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp";
            if (openFileDialog.ShowDialog() != true) return;
            ImageSourcePath.Value= openFileDialog.FileName;
            ImageControl.HalconWindow.DispImage(new HImage(ImageSourcePath.Value));
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
            
        }
    }
}
