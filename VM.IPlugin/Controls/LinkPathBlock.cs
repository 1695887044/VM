using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.Controls
{
    public class LinkPathBlock : Control
    {
        TextBox textBox;
        bool UserEditable;
        Type genericType;
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header",
            typeof(string),
            typeof(LinkPathBlock),
            new PropertyMetadata(string.Empty)
        );

        public bool IsReadOnlay
        {
            get { return (bool)GetValue(IsReadOnlayProperty); }
            set { SetValue(IsReadOnlayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnlay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlayProperty = DependencyProperty.Register(
            "IsReadOnlay",
            typeof(bool),
            typeof(LinkPathBlock),
            new PropertyMetadata(false)
        );

        public string LinkPathName
        {
            get { return (string)GetValue(LinkPathNameProperty); }
            set { SetValue(LinkPathNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkPathName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkPathNameProperty =
            DependencyProperty.Register(
                "LinkPathName",
                typeof(string),
                typeof(LinkPathBlock),
                new PropertyMetadata(string.Empty)
            );

        public IVarValue BindVarData
        {
            get { return (IVarValue)GetValue(BindVarDataProperty); }
            set { SetValue(BindVarDataProperty, value); }
        }
        public static readonly DependencyProperty BindVarDataProperty = DependencyProperty.Register(
            "BindVarData",
            typeof(IVarValue),
            typeof(LinkPathBlock),
            new PropertyMetadata(null, OnValueChanged)
        );

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LinkPathBlock ctl && e.NewValue is IVarValue data)
            {
                ctl.CheckValueType(data);
            }
        }

        public ICommand LinkPathCommand
        {
            get { return (ICommand)GetValue(LinkPathCommandProperty); }
            set { SetValue(LinkPathCommandProperty, value); }
        }
        public static readonly DependencyProperty LinkPathCommandProperty =
            DependencyProperty.Register(
                "LinkPathCommand",
                typeof(ICommand),
                typeof(LinkPathBlock),
                new PropertyMetadata(null)
            );

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            textBox = this.GetTemplateChild("PART_ValueBox") as TextBox;
        }

        /// <summary>
        /// 检查泛型类型 如果是值类型  就赋予用户编辑的能力
        /// </summary>
        /// <param name="data"></param>
        void CheckValueType(IVarValue data)
        {
            genericType = data.GetType().GetGenericArguments()[0];
            UserEditable = genericType.IsValueType || genericType == typeof(string);
            textBox.IsReadOnly = UserEditable;
            if (UserEditable)
            {
                textBox.TextChanged += updateValue;
            }
            else
            {
                textBox.TextChanged -= updateValue;
            }
        }

        private void updateValue(object sender, TextChangedEventArgs e)
        {
            //解锁用户编辑功能
            if (BindVarData is VarValue<string> strVar && genericType == typeof(string))
            {
                strVar.Value = textBox.Text;
            }
            else if (BindVarData is VarValue<int> intVar && genericType == typeof(int))
            {
                if (int.TryParse(textBox.Text, out int v))
                {
                    intVar.Value = v;
                }
            }
            else if (BindVarData is VarValue<double> doubleVar && genericType == typeof(double))
            {
                if (double.TryParse(textBox.Text, out double v))
                {
                    doubleVar.Value = v;
                }
            }
            else if (BindVarData is VarValue<float> floatVar && genericType == typeof(float))
            {
                if (float.TryParse(textBox.Text, out float v))
                {
                    floatVar.Value = v;
                }
            }
            else if (BindVarData is VarValue<bool> boolVar && genericType == typeof(bool))
            {
                if (bool.TryParse(textBox.Text, out bool v))
                {
                    boolVar.Value = v;
                }
            }
            else if (BindVarData is VarValue<long> longVar && genericType == typeof(long))
            {
                if (long.TryParse(textBox.Text, out long v))
                {
                    longVar.Value = v;
                }
            }
        }
    }
}
