using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace W.UI.Controls
{
    [TemplatePart(Name = "PART_Input", Type = typeof(TextBox))]
    public class GenericPropertyEditor : Control
    {
        static GenericPropertyEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GenericPropertyEditor),
                new FrameworkPropertyMetadata(typeof(GenericPropertyEditor)));
        }

        #region Dependency Properties

        public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(GenericPropertyEditor), new PropertyMetadata("Property"));

    

        public object Value { get => GetValue(ValueProperty); set => SetValue(ValueProperty, value); }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(object), typeof(GenericPropertyEditor), new PropertyMetadata(null, OnValueTypeChanged));

        public bool CanClear { get => (bool)GetValue(CanClearProperty); set => SetValue(CanClearProperty, value); }
        public static readonly DependencyProperty CanClearProperty = DependencyProperty.Register("CanClear", typeof(bool), typeof(GenericPropertyEditor), new PropertyMetadata(true));

        public bool IsLinkEnabled { get => (bool)GetValue(IsLinkEnabledProperty); set => SetValue(IsLinkEnabledProperty, value); }
        public static readonly DependencyProperty IsLinkEnabledProperty = DependencyProperty.Register("IsLinkEnabled", typeof(bool), typeof(GenericPropertyEditor), new PropertyMetadata(false));

        public bool IsEditable { get => (bool)GetValue(IsEditableProperty); private set => SetValue(IsEditableProperty, value); }
        public static readonly DependencyProperty IsEditableProperty = DependencyProperty.Register("IsEditable", typeof(bool), typeof(GenericPropertyEditor), new PropertyMetadata(true));

        public ICommand LinkCommand { get => (ICommand)GetValue(LinkCommandProperty); set => SetValue(LinkCommandProperty, value); }
        public static readonly DependencyProperty LinkCommandProperty = DependencyProperty.Register("LinkCommand", typeof(ICommand), typeof(GenericPropertyEditor));


        // 水印提示文字
        public string Placeholder { get => (string)GetValue(PlaceholderProperty); set => SetValue(PlaceholderProperty, value); }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(GenericPropertyEditor), new PropertyMetadata("请输入..."));

        // 是否验证通过
        public bool IsValid { get => (bool)GetValue(IsValidProperty); set => SetValue(IsValidProperty, value); }
        public static readonly DependencyProperty IsValidProperty =
            DependencyProperty.Register("IsValid", typeof(bool), typeof(GenericPropertyEditor), new PropertyMetadata(true));

        // 错误提示信息
        public string ErrorMessage { get => (string)GetValue(ErrorMessageProperty); set => SetValue(ErrorMessageProperty, value); }
        public static readonly DependencyProperty ErrorMessageProperty =
            DependencyProperty.Register("ErrorMessage", typeof(string), typeof(GenericPropertyEditor), new PropertyMetadata(null));
        #endregion

        private static void OnValueTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GenericPropertyEditor editor)
            {
                // 工业逻辑：判断是否为可直接编辑的简单类型
                if (e.NewValue == null)
                {
                    editor.IsEditable = true;
                    return;
                }

                Type t = e.NewValue.GetType();
                editor.IsEditable = t.IsPrimitive || t == typeof(string) || t == typeof(decimal);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_ClearBtn") is Button btn)
                btn.Click += (s, e) => Value = null;
        }
    }
}
