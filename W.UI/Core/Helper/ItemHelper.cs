using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace W.UI.Core.Helper
{
    public class ItemHelper
    {
        public static readonly DependencyProperty DoubleClickCommandProperty =
        DependencyProperty.RegisterAttached("DoubleClickCommand", typeof(ICommand), typeof(ItemHelper),
            new PropertyMetadata(null, OnDoubleClickCommandChanged));

        public static void SetDoubleClickCommand(DependencyObject d, ICommand value) => d.SetValue(DoubleClickCommandProperty, value);
        public static ICommand GetDoubleClickCommand(DependencyObject d) => (ICommand)d.GetValue(DoubleClickCommandProperty);

        private static void OnDoubleClickCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control)
            {
                control.MouseDoubleClick -= Control_MouseDoubleClick;
                if (e.NewValue != null)
                {
                    control.MouseDoubleClick += Control_MouseDoubleClick;
                }
            }
        }

        private static void Control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var control = sender as Control;
            var command = GetDoubleClickCommand(control);
            var param = control.DataContext; // 默认传递当前的 DataContext (即 Item 对象)

            if (command != null && command.CanExecute(param))
            {
                command.Execute(param);
            }
        }
    }
}
