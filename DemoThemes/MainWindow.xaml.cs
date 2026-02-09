using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using W.UI.Attributes;

namespace DemoThemes
{
    public enum MyEm
    {
        ee1,
        ee2,
        ee3,
        ee4,
        ee5,
        ee6,
        ee7,
        e88,
        e89,
        e90,
        e91,
    }

    public class DataModel
    {
        public ICommand OperatorCommand { get; set; }

        [SuperDisplay(
            Name = "可读可写",
            Description = "当前可读可写",
            GroupPath = "默认分组/整数组/阈值"
        )]
        [NumericRange(0, 255, 2)]
        public int IntRangeValue { get; set; }

        [SuperDisplay(
            Name = "可读可写",
            Description = "当前可读可写",
            GroupPath = "默认分组/整数组/阈值"
        )]
        [NumericRange(0, 255, 2)]
        public int Int2RangeValue { get; set; }

        [SuperDisplay(
            Name = "可读可写",
            Description = "当前可读可写",
            GroupPath = "默认分组/整数组"
        )]
        public int IntValue { get; set; }

        [SuperDisplay(
            Name = "可读",
            Description = "当前可读",
            GroupPath = "默认分组/整数组",
            IsReadOnly = true
        )]
        public int IntRead { get; set; }

        [SuperDisplay(
            Name = "可读可写",
            Description = "当前可读可写",
            GroupPath = "默认分组/布尔组"
        )]
        public bool BoolValue { get; set; }

        [SuperDisplay(
            Name = "可读",
            Description = "当前可读",
            GroupPath = "默认分组/布尔组",
            IsReadOnly = true
        )]
        public bool BoolRead { get; set; }

        [Command(
            Command = nameof(OperatorCommand),
            CommandParam = "A",
            AncestorType = typeof(Window),
            Mode = System.Windows.Data.RelativeSourceMode.FindAncestor
        )]
        [SuperDisplay(
            Name = "命令按钮",
            Description = "命令按南三阿萨",
            GroupPath = "默认分组/布尔组",
            IsReadOnly = true
        )]
        public bool BoolCommand { get; set; }

        [SuperDisplay(
            Name = "可选枚举",
            Description = "命令按南三阿萨",
            GroupPath = "默认分组/枚举组"
        )]
        public MyEm MyProperty { get; set; }

        [SuperDisplay(
            Name = "只读枚举",
            Description = "命令按南三阿萨",
            GroupPath = "默认分组/枚举组",
            IsReadOnly = true
        )]
        public MyEm MyPrope2rty { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public DataModel Data { get; set; } = new();
        public double Test1 { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Test1.ToString());
        }
    }
}
