using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace W.UI.Core.Behaviors
{
    public static class ScrollBehavior
    {
        public static readonly DependencyProperty PassThroughScrollProperty =
            DependencyProperty.RegisterAttached(
                "PassThroughScroll",
                typeof(bool),
                typeof(ScrollBehavior),
                new PropertyMetadata(false, OnPassThroughScrollChanged));

        public static bool GetPassThroughScroll(DependencyObject obj) => (bool)obj.GetValue(PassThroughScrollProperty);
        public static void SetPassThroughScroll(DependencyObject obj, bool value) => obj.SetValue(PassThroughScrollProperty, value);

        private static void OnPassThroughScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if ((bool)e.NewValue)
                {
                    element.PreviewMouseWheel += Element_PreviewMouseWheel;
                }
                else
                {
                    element.PreviewMouseWheel -= Element_PreviewMouseWheel;
                }
            }
        }

        private static void Element_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Handled) return;

            e.Handled = true;

            // 2. 包装一个全新的滚轮事件
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = UIElement.MouseWheelEvent,
                Source = sender
            };

            // 3. 找到它的父级，强行把事件往上层抛，让外层的 ScrollViewer 去滚动！
            var parent = VisualTreeHelper.GetParent((DependencyObject)sender) as UIElement;
            parent?.RaiseEvent(eventArg);
        }
    }
}
