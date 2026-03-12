using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace VM.Start.Behaviors
{
    /// <summary>
    /// 用于实现无边框窗口拖拽的附加行为
    /// 可以挂载在任何 UIElement 上（如 Border, Grid 等）
    /// </summary>
    public class WindowDragBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            // 订阅鼠标左键按下事件
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            // 释放事件，防止内存泄漏
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 如果用户点的是按钮之类的控件，不要触发拖拽
            if (e.OriginalSource is System.Windows.Controls.Button)
                return;

            // 沿着视觉树向上寻找承载这个控件的 Window
            Window parentWindow = Window.GetWindow(this.AssociatedObject);

            if (parentWindow != null && e.ButtonState == MouseButtonState.Pressed)
            {
                // 调用系统原生的窗口拖拽 API
                parentWindow.DragMove();
            }
        }
    }
}
