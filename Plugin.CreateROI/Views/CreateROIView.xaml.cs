
using VM.IPlugin;

namespace Plugin.CreateROI.Views
{
    /// <summary>
    /// CreateROIView.xaml 的交互逻辑
    /// </summary>
    public partial class CreateROIView : IModuleViewBase
    {
        public CreateROIView()
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
