using HalconDotNet;
using Microsoft.Win32;
using System.ComponentModel;
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

namespace HalconDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

      

        public MainWindow()
        {
            InitializeComponent();
            //init();
        }

        private void init()
        {
          var hImage = new HImage();
            hImage.ReadImage(@"D:\\开发的三轴涂胶软件源码\\视频图片\\视频图片\\视频图片\\1.PNG");
            hsmart.HImage = hImage;
            hsmart.TopText = "加载图像成功";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            init();
        }
    }
}