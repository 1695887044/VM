using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace W.UI.Core.Converters
{
    // 宽度转 GridLength
    public class WidthToGridLengthConverter : BaseMarkupConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => new GridLength((double)value);
    }

    // 宽度转可见性（用于文字隐藏）
    public class WidthToVisibilityConverter : BaseMarkupConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (double)value > 100 ? Visibility.Visible : Visibility.Collapsed;
    
    }

    // 图标缩放转换器：宽度越小，图标越小（160px以上为1, 0px为0.5）
    public class WidthToScaleConverter : BaseMarkupConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)value;
            if (width >= 160) return 1.0;
            double scale = 0.6 + (width / 160.0) * 0.4;
            return Math.Max(0.6, scale);
        }
    }

}
