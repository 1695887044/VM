using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace W.UI.Core.Converters
{
    public class BooleanToVisibilityConverter : BaseMarkupConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                if ( parameter!=null )
                {
                    return !b ? Visibility.Collapsed : Visibility.Visible;
                }
                return b ? Visibility.Collapsed : Visibility.Visible;
            }
              
            return Visibility.Visible;
        }
    }
}
