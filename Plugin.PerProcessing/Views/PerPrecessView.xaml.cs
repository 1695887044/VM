using System.Windows.Controls;
using VM.IPlugin;

namespace Plugin.PerProcessing.Views
{
    /// <summary>
    /// PerPrecessView.xaml 的交互逻辑
    /// </summary>
    public partial class PerPrecessView : IModuleViewBase
    {
        public PerPrecessView()
        {
            InitializeComponent();
        }

        public void CancelView()
        {

        }

        public void InitView(ModuleViewModelBase model = null)
        {
            this.DataContext = model;
        }

        public void ShowView()
        {

        }

    }
}
