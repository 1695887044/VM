using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace W.UI.Controls
{
    public class IconElement
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.RegisterAttached(
        "Icon", typeof(string), typeof(IconElement), new PropertyMetadata(string.Empty));

        public static void SetIcon(DependencyObject element, string value)
            => element.SetValue(IconProperty, value);

        public static string GetIcon(DependencyObject element)
            => (string)element.GetValue(IconProperty);

        public static readonly DependencyProperty IconSizeProperty = DependencyProperty.RegisterAttached(
            "IconSize", typeof(double), typeof(IconElement), new PropertyMetadata(16.0));

        public static void SetIconSize(DependencyObject element, double value)
            => element.SetValue(IconSizeProperty, value);

        public static double GetIconSize(DependencyObject element)
            => (double)element.GetValue(IconSizeProperty);
    }
}
