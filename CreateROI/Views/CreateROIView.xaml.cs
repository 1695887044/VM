using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VM.IPlugin;

namespace CreateROI.Views
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
