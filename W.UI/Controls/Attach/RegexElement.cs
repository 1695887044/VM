using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace W.UI.Controls
{
    public static class RegexElement
    {
        // 正则表达式字符串
        public static readonly DependencyProperty PatternProperty =
            DependencyProperty.RegisterAttached("Pattern", typeof(string), typeof(RegexElement), new PropertyMetadata(string.Empty, OnPatternChanged));

        // 是否验证通过 (只读属性供 XAML 使用)
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.RegisterAttached("IsValid", typeof(bool), typeof(RegexElement), new PropertyMetadata(true));

        public static string GetPattern(DependencyObject obj) => (string)obj.GetValue(PatternProperty);
        public static void SetPattern(DependencyObject obj, string value) => obj.SetValue(PatternProperty, value);

        public static bool GetIsValid(DependencyObject obj) => (bool)obj.GetValue(IsValidProperty);
        private static void SetIsValid(DependencyObject obj, bool value) => obj.SetValue(IsValidProperty, value);

        private static void OnPatternChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                textBox.TextChanged -= TextBox_TextChanged;
                textBox.TextChanged += TextBox_TextChanged;
                Validate(textBox);
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox) Validate(textBox);
        }

        private static void Validate(TextBox textBox)
        {
            string pattern = GetPattern(textBox);
            if (string.IsNullOrEmpty(pattern))
            {
                SetIsValid(textBox, true);
                return;
            }

            // 执行正则匹配
            bool isValid = Regex.IsMatch(textBox.Text, pattern);
            SetIsValid(textBox, isValid);
        }
    }
}
