
using HalconDotNet;
using System.Windows;
using System.Windows.Controls;
using VM.Halcon.Controls;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace VM.Start.ViewModels
{
    public class VisionViewModel: BindableBase
    {
        private int columnCount = 1;

        public int ColumnCount
        {
            get { return columnCount; }
            set { columnCount = value; RaisePropertyChanged(); }
        }
        private int rowCount =1;

        public int RowCount
        {
            get { return rowCount; }
            set { rowCount = value; RaisePropertyChanged(); }
        }
        private HImage displayImage;

        public HImage DisplayImage
        {
            get { return displayImage; }
            set { displayImage = value;  RaisePropertyChanged(); }
        }

        public List<string> ImageEdits { get; set; } = new ();
        public VisionViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<RefreshUIEvent<HImage>>().Subscribe(s => { 
                DisplayImage = s.Value;
            
            });
        }
    }
}
