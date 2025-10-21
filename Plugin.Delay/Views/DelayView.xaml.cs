
using System.Windows.Controls;
using VM.IPlugin;

namespace Plugin.Delay.Views
{
    /// <summary>
    /// DelayView.xaml 的交互逻辑
    /// </summary>
    public partial class DelayView : UserControl, IModuleViewBase
    {
        public DelayView()
        {
            InitializeComponent();
        }

        public void CancelView()
        {
            //this.Close();
        }


        public void InitView(ModuleViewModelBase model = null)
        {
            if (model == null) return;
            this.DataContext = model;
        }

        public void ShowView()
        {
           // this.ShowDialog();
        }
    }
}
