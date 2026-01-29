

using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using VM.Shard.Services;

namespace VM.Start.Converts
{
    public class LogToColorConvert : MarkupExtension,IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Log_Level level)
            {
                return level switch
                {
                    Log_Level.Info => new SolidColorBrush(Colors.LightGray),
                    Log_Level.Debug => new SolidColorBrush(Colors.Gray),
                    Log_Level.Warn => new SolidColorBrush(Colors.Orange),
                    Log_Level.Error => new SolidColorBrush(Colors.Red),
                    _ => new SolidColorBrush(Colors.LightGray)
                };
            }
            return new SolidColorBrush(Colors.LightGray);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
