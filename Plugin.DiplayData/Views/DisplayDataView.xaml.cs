using System.Windows.Controls;
using VM.IPlugin;


namespace Plugin.DiplayData.Views
{
    /// <summary>
    /// DisplayDataView.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayDataView : UserControl,IModuleViewBase
    {
        public DisplayDataView()
        {
            InitializeComponent();
        }

        public void CancelView()
        {
            
        }

        public void InitView(ModuleViewModelBase model = null)
        {
            if (model == null) return;
            this.DataContext = model;
        }

        public void ShowView()
        {
            
        }
    }
}
