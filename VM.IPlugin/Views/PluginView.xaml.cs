using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using VM.IPlugin.ModuleEvent;
namespace VM.IPlugin.Views
{
    /// <summary>
    /// PluginView.xaml 的交互逻辑
    /// </summary>
    public partial class PluginView : Window
    {
         ModuleViewModelBase ActVm;
        public PluginView()
        {
            InitializeComponent();
        }
        public void Init(FrameworkElement element)
        {
            this.PART_View.Content = element;
            this.Width = element.Width;
            this.Height = element.Height + 80;
            if (element.DataContext is ModuleViewModelBase vm)
            {
                ActVm = vm;
                BindingOperations.SetBinding(t1, Run.TextProperty, new Binding("DisplayTime") { Source = vm });
                BindingOperations.SetBinding(t2, Run.TextProperty, new Binding("State") { Source = vm });
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ActVm.ModuleStateChanged += ActVm_ModuleStateChanged;
            ActVm.Execute();
        }

        private void ActVm_ModuleStateChanged(object? sender, ModuleEventArgs e)
        {
            ActVm.State = e.ActState;
        }

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

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed) this.DragMove();
        }
    }
}
