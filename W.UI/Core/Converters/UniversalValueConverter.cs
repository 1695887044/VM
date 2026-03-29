using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace W.UI.Core.Converters
{
    /// <summary>
    /// 万能数据值转换器：负责将底层数据优雅地展示到 UI 上
    /// </summary>
    public class UniversalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            // 1. 如果本身就是字符串，直接返回（防止字符串被当成 char 数组拆分）
            if (value is string str) return str;

            // 2. 🚨 核心魔法：如果是集合/数组类型 (List<int>, List<bool> 等)
            if (value is IEnumerable enumerable)
            {
                var items = new List<string>();
                foreach (var item in enumerable)
                {
                    items.Add(item?.ToString() ?? "null");
                }

                // 渲染成类似: [ 1, 2, 3 ] 的高级格式
                return $"[ {string.Join(", ", items)} ]";
            }

            // 3. 基础类型 (int, double, bool) 直接返回其本身的值
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 当用户在界面上手动修改后，传回到底层的值
            // 提示：如果你在界面上敲了 "[ 1, 2 ]"，这里会收到字符串 "[ 1, 2 ]"
            // 由于完整的反序列化需要知道具体的底层类型，这部分通常交由底层的数据端口拦截处理
            return value;
        }
    }
}
