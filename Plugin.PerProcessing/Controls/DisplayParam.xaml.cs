using Plugin.PerProcessing.Model;
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

namespace Plugin.PerProcessing.Controls
{
    /// <summary>
    /// DisplayParam.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayParam : UserControl
    {
        public DisplayParam()
        {
            InitializeComponent();
            //this.DataContext = this;
        }



        public IToolData ToolDataItem
        {
            get { return (IToolData)GetValue(ToolDataItemProperty); }
            set { SetValue(ToolDataItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToolDataItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToolDataItemProperty =
            DependencyProperty.Register("ToolDataItem", typeof(IToolData), typeof(DisplayParam), new PropertyMetadata( ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d != null && d is DisplayParam view)
            {
                ;
            }
        }
    }
}
