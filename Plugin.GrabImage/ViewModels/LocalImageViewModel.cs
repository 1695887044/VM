using HalconDotNet;
using Microsoft.Win32;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Services;

namespace Plugin.GrabImage.ViewModels
{
    public class LocalImageViewModel:BaseSettingViewModel
    {
        public DelegateCommand SelectImageCommand { get; private set; } 

        private string imageSourcePath;
        private readonly ISuperDialogService superDialogService;

        public string ImageSourcePath
        {
            get { return imageSourcePath; }
            set
            {
                imageSourcePath = value;
                RaisePropertyChanged();
            }
        }

        public LocalImageViewModel( ISuperDialogService superDialogService)
        {
            SelectImageCommand = new DelegateCommand(ExecuteSelectImage);
            this.superDialogService = superDialogService;
        }
        /// <summary>
        /// 触发选择图片
        /// </summary>
        private void ExecuteSelectImage()
        {
            ImageSourcePath = superDialogService.GetFilePath("Image files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp");
            RaiseImageChanged(ImageSourcePath);
           // DisplayImage = image;

        }

    }
}
