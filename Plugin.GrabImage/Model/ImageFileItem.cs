using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.GrabImage.Model
{
    public class ImageFileItem : BindableBase
    {
        private bool _isChecked = true;
        public bool IsChecked
        {
            get => _isChecked;
            set { _isChecked = value; RaisePropertyChanged(); }
        }

        private int _index;
        public int Index
        {
            get => _index;
            set { _index = value; RaisePropertyChanged(); }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set { _fileName = value; RaisePropertyChanged(); }
        }

        public string FilePath { get; set; } // 完整物理路径，不需要通知 UI
        public DateTime CreationTime { get; set; } // 用于按时间排序
    }
}
