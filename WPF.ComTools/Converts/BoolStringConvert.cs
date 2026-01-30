
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF.ComTools.Converts
{
    public class BoolStringConvert : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;
            return value.ToString() == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return "0";
            if(value is bool b)
            {
                if (b)
                {
                    return parameter.ToString();
                }
            }
            return "0";
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
