
using System.Windows.Controls;
using VM.Halcon.Controls;

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
        public List<string> ImageEdits { get; set; } = new ();
        public VisionViewModel()
        {
            ImageEdits.Add("sa");
        }
    }
}
