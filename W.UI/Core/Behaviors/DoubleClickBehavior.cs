using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace W.UI.Core.Behaviors
{
    public static class DoubleClickBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(DoubleClickBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static void SetCommand(DependencyObject d, ICommand value) => d.SetValue(CommandProperty, value);
        public static ICommand GetCommand(DependencyObject d) => (ICommand)d.GetValue(CommandProperty);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGridRow row)
            {
                row.MouseDoubleClick -= OnMouseDoubleClick;
                if (e.NewValue is ICommand)
                    row.MouseDoubleClick += OnMouseDoubleClick;
            }
        }

        private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            var command = GetCommand(row);
            if (command?.CanExecute(row.DataContext) == true)
                command.Execute(row.DataContext);
        }
    }
}
