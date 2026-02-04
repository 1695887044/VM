using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using VM.IPlugin.ModuleEvent;
namespace VM.IPlugin.Views
{
    /// <summary>
    /// PluginView.xaml 的交互逻辑
    /// </summary>
    public partial class PluginView : Window
    {
        const string InfoPath= " M512 224m-64 0a64 64 0 1 0 128 0 64 64 0 1 0-128 0Z M544 392h-64c-4.4 0-8 3.6-8 8v464c0 4.4 3.6 8 8 8h64c4.4 0 8-3.6 8-8V400c0-4.4-3.6-8-8-8z";
        const string defaultTitle = "插件编辑";
        public string IconText { get; set; }  
        ModuleViewModelBase ActVm;
        public PluginView()
        {
            InitializeComponent();
        }
        public  bool? ShowView(FrameworkElement View, ModuleViewModelBase ViewModel, string title = defaultTitle,string IocnPath= InfoPath )
        {
            this.PART_View.Content = View;
            this.Width = View.Width;
            //this.Height = View.Height;
            IconText = string.IsNullOrEmpty(InfoPath) ? InfoPath : IocnPath;
            View.DataContext = ViewModel;
            ActVm = ViewModel;
            IconCtl.Data = Geometry.Parse(IocnPath);
            titleCtl.Text = string.IsNullOrEmpty(title) ? defaultTitle : title;
            BindingOperations.SetBinding(t1, Run.TextProperty, new Binding("DisplayTime") { Source = ActVm });
            BindingOperations.SetBinding(t2, Run.TextProperty, new Binding("State") { Source = ActVm });
            return this.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => ActVm.ExecuteModule();


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ActVm.Cancel();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ActVm.Confirm();
            this.Close();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed) this.DragMove();
        }
    }
}
