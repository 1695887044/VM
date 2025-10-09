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
using VM.Start.Common;

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
            TreeViewItem treeViewItem = WPFElementTool.FindVisualParent<TreeViewItem>(result.VisualHit);
            if(treeViewItem == null) return;
            treeViewItem.Focus();
        }
    }
}
