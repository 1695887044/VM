using Microsoft.Win32;
using Plugin.GrabImage.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Services;

namespace Plugin.GrabImage.ViewModels
{
    public class DirectoryGrabViewModel: BaseSettingViewModel
    {
        // 1. 界面绑定的基础属性
        private string _folderPath;
        public string FolderPath
        {
            get => _folderPath;
            set { _folderPath = value; RaisePropertyChanged();  }
        }

        private bool _isLoop = true;
        public bool IsLoop
        {
            get => _isLoop;
            set { _isLoop = value; RaisePropertyChanged(); }
        }

        private bool _isAutoSwitch = false;
        public bool IsAutoSwitch
        {
            get => _isAutoSwitch;
            set { _isAutoSwitch = value; RaisePropertyChanged(); }
        }

        // 当前选中的图像项（绑定到 DataGrid 的 SelectedItem）
        private ImageFileItem _selectedItem;
        private readonly ISuperDialogService superDialogService;

        public ImageFileItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaiseImageChanged(SelectedItem.FilePath);
            }
        }

        public ObservableCollection<ImageFileItem> ImageFiles { get; } = new ObservableCollection<ImageFileItem>();

        public DelegateCommand BrowseFolderCommand { get; }

        public DirectoryGrabViewModel(ISuperDialogService superDialogService)
        {
            BrowseFolderCommand = new DelegateCommand(LoadImagesFromDirectory);
            this.superDialogService = superDialogService;
        }

        private void LoadImagesFromDirectory()
        {
            FolderPath = superDialogService.GetFolderPath();
            var files = Directory.GetFiles(FolderPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".gif") || s.EndsWith(".png") || s.EndsWith(".bmp") || s.EndsWith(".jpg") || s.EndsWith(".eps") || s.EndsWith(".tif"));

            int i = 1;
            ImageFiles.Clear();
            foreach (var file in files)
            {
                ImageFiles.Add(new ImageFileItem
                {
                    Index = i++,
                    FileName = Path.GetFileName(file),
                    FilePath = file,
                    CreationTime = File.GetCreationTime(file)
                });
            }

            if (ImageFiles.Any()) SelectedItem = ImageFiles.First();
        }
    }
}
