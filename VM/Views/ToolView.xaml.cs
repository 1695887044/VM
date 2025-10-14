using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VM.Shard.Helper;
namespace VM.Start.Views
{
    /// <summary>
    /// ToolView.xaml 的交互逻辑
    /// </summary>
    public partial class ToolView : UserControl
    {
        public ToolView()
        {
            InitializeComponent();
        }
        public static ToolView Ins = new();

        private void TreeView_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            //获取鼠标位置的TreeViewItem 然后选中
            if (!(sender is TreeView ctl)) return;
            Point pt = e.GetPosition(ctl);
            HitTestResult result = VisualTreeHelper.HitTest(ctl, pt);
            if(result == null) return;
            TreeViewItem treeViewItem = WPFElementHelper.FindVisualParent<TreeViewItem>(result.VisualHit);
            if(treeViewItem == null) return;
            treeViewItem.Focus();
        }
    }
}
