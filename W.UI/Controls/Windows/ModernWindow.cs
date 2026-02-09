using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Shell;
using W.UI.Core.Helper;

namespace W.UI.Controls
{
    public class ModernWindow : Window
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        static ModernWindow()
        {
            // 告诉 WPF 去 Themes/Generic.xaml 找样式
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ModernWindow),
                new FrameworkPropertyMetadata(typeof(ModernWindow))
            );
        }

        public ModernWindow()
        {
            // 配置 WindowChrome：这是自定义标题栏且保留原生特性的标准做法
            WindowChrome.SetWindowChrome(this, new WindowChrome
            {
                CaptionHeight = 45,
                ResizeBorderThickness = new Thickness(6),
                GlassFrameThickness = new Thickness(1), // 保留原生阴影，防止出现黑边
                UseAeroCaptionButtons = false
            });

            this.SourceInitialized += OnSourceInitialized;
        }
        private void OnSourceInitialized(object sender, EventArgs e)
        {
            // 尝试开启 Win11 原生材质
            var hwnd = new WindowInteropHelper(this).Handle;
            int trueValue = 1;
            int backdropType = 2; // 2 代表 Mica

            // 设置沉浸式深色模式（这里设为 false 强制浅色）
            DwmSetWindowAttribute(hwnd, 20, ref trueValue, sizeof(int));
            // 开启云母效果
            DwmSetWindowAttribute(hwnd, 38, ref backdropType, sizeof(int));
        }


        // 必须在 OnApplyTemplate 中应用初始状态
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
             if(this.GetTemplateChild("PART_Min") is Button b1)
            {
                b1.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            }
            if (this.GetTemplateChild("PART_Close") is Button b2)
            {
                b2.Click += (s, e) => { this.Close(); };
            }

            this.Loaded += ((s, e) => LoadCommand?.Execute(s));
            this.Closed += ((s, e) => CloseCommand?.Execute(s));
        }





        public double HeaderHeight
        {
            get => (double)GetValue(HeaderHeightProperty);
            set => SetValue(HeaderHeightProperty, value);
        }
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(ModernWindow), new PropertyMetadata(45.0));


        public bool CloseForm
        {
            get { return (bool)GetValue(CloseFormProperty); }
            set { SetValue(CloseFormProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseForm.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseFormProperty =
            DependencyProperty.Register("CloseForm", typeof(bool), typeof(ModernWindow), new PropertyMetadata(false,OnCloseFormChanged));

        private static void OnCloseFormChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window win) win.Close();
        }

        public ICommand LoadCommand
        {
            get { return (ICommand)GetValue(LoadCommandProperty); }
            set { SetValue(LoadCommandProperty, value); }
        }

        public static readonly DependencyProperty LoadCommandProperty =
            DependencyProperty.Register("LoadCommand", typeof(ICommand), typeof(ModernWindow), new PropertyMetadata(null));


        // 4. 关闭命令
        public ICommand CloseCommand
        {
            get => (ICommand)GetValue(CloseCommandProperty);
            set => SetValue(CloseCommandProperty, value);
        }
        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register(
                "CloseCommand",
                typeof(ICommand),
                typeof(ModernWindow),
                new PropertyMetadata(null)
            );
    }
}
