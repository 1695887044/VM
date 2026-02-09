using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Text.RegularExpressions;

namespace W.UI.Controls
{
    public class PasswordBoxEx : Control
    {
        private PasswordBox _passwordBox;
        private bool _isUpdating;

        static PasswordBoxEx()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBoxEx), new FrameworkPropertyMetadata(typeof(PasswordBoxEx)));
        }

        // 密码属性（支持双向绑定）
        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxEx),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

        // 水印
        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(PasswordBoxEx), new PropertyMetadata("请输入密码"));

        // 正则表达式
        public string ValidationRegex
        {
            get => (string)GetValue(ValidationRegexProperty);
            set => SetValue(ValidationRegexProperty, value);
        }

        public static readonly DependencyProperty ValidationRegexProperty =
            DependencyProperty.Register("ValidationRegex", typeof(string), typeof(PasswordBoxEx), new PropertyMetadata(null));

        // 验证状态
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            private set => SetValue(IsValidProperty, value);
        }

        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register("IsValid", typeof(bool), typeof(PasswordBoxEx), new PropertyMetadata(true));

        // 是否显示明文
        public bool IsPasswordVisible
        {
            get => (bool)GetValue(IsPasswordVisibleProperty);
            set => SetValue(IsPasswordVisibleProperty, value);
        }

        public static readonly DependencyProperty IsPasswordVisibleProperty =
            DependencyProperty.Register("IsPasswordVisible", typeof(bool), typeof(PasswordBoxEx), new PropertyMetadata(false));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _passwordBox = GetTemplateChild("InternalPasswordBox") as PasswordBox;
            if (_passwordBox != null)
            {
                _passwordBox.Password = Password;
                _passwordBox.PasswordChanged += (s, e) => {
                    if (!_isUpdating)
                    {
                        _isUpdating = true;
                        Password = _passwordBox.Password;
                        _isUpdating = false;
                    }
                };
            }
        }

        private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (PasswordBoxEx)d;
            string newPwd = e.NewValue?.ToString() ?? "";

            // 1. 同步内部 PasswordBox
            if (ctrl._passwordBox != null && !ctrl._isUpdating)
            {
                ctrl._isUpdating = true;
                ctrl._passwordBox.Password = newPwd;
                ctrl._isUpdating = false;
            }

            // 2. 正则验证逻辑
            if (!string.IsNullOrEmpty(ctrl.ValidationRegex))
                ctrl.IsValid = Regex.IsMatch(newPwd, ctrl.ValidationRegex);
            else
                ctrl.IsValid = true;
        }
    }
}
