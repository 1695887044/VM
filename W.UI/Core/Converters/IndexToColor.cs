using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace W.UI.Core.Converters
{
    public class IndexToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var num = System.Convert.ToInt32(value);
            //var brush = ThemeManager.Instance.Resources.TryFindResource<SolidColorBrush>("WD.CircleMenuBrush");
            if (num % 2 == 1) return null;
            //  brush = ThemeManager.Instance.Resources.TryFindResource<SolidColorBrush>("WD.CircleMenuDualBrush");
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}