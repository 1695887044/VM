

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.Enums;

namespace VM.IPlugin.Controls
{
    public class LabelLinkPath : Control
    {
        bool UserEditable;
        Type genericType;
        Button? LinkButton, ClearButton;
        TextBlock? HeadTextBlock;
        TextBox? ContentBox;
        public bool BindMode
        {
            get { return (bool)GetValue(BindModeProperty); }
            set { SetValue(BindModeProperty, value); }
        }

        public static readonly DependencyProperty BindModeProperty =
            DependencyProperty.Register("BindMode", typeof(bool), typeof(LabelLinkPath), new PropertyMetadata(false));


        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(LabelLinkPath), new PropertyMetadata(false));


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(LabelLinkPath), new PropertyMetadata("路径"));



        public object LinkParam
        {
            get { return (object)GetValue(LinkParamProperty); }
            set { SetValue(LinkParamProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkParam.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkParamProperty =
            DependencyProperty.Register("LinkParam", typeof(object), typeof(LabelLinkPath), new PropertyMetadata(null));



        public Object Value
        {
            get { return (Object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Object), typeof(LabelLinkPath), new PropertyMetadata(null, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LabelLinkPath ctl)
            {
                if(e.NewValue is IDataPort _Var)
                {
                    ctl.UpdateTextValue(_Var);
                }
                else
                {
                    ctl.UpDataTextValue();
                }

            }
        }

        private void UpDataTextValue()
        {
            ContentBox.IsReadOnly = false;
            ContentBox.Text = Value.ToString();
        }

        private void UpdateTextValue(IDataPort _var)
        {
            ContentBox.IsReadOnly = true;
            ContentBox.Text = _var.DisplayName + "/" + _var.Name;
        }

        public ICommand OperatorCommand
        {
            get { return (ICommand)GetValue(OperatorCommandProperty); }
            set { SetValue(OperatorCommandProperty, value); }
        }

        public static readonly DependencyProperty OperatorCommandProperty =
            DependencyProperty.Register("OperatorCommand", typeof(ICommand), typeof(LabelLinkPath));
        LinkPathParam p1, p2;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LinkButton = this.GetTemplateChild("PART_LinkButton") as Button;
            ClearButton = this.GetTemplateChild("PART_ClearButton") as Button;
            HeadTextBlock = this.GetTemplateChild("PART_HeadTextBlock") as TextBlock;
            ContentBox = this.GetTemplateChild("PART_ContentBox") as TextBox;
            p1 = new(LinkPathType.Link, this.LinkParam);
            p2 = new(LinkPathType.Clear, this.LinkParam);
            LinkButton.Click += (s, e) =>
            {

                OperatorCommand?.Execute(p1);
            };
            ClearButton.Click += (s, e) =>
            {
                OperatorCommand?.Execute(p2);
            };
        }

        private void updateValue(object sender, TextChangedEventArgs e)
        {
            //解锁用户编辑功能
            if (Value is DataPort<string> strVar && genericType == typeof(string))
            {
                strVar.Value = ContentBox.Text;
            }
            else if (Value is DataPort<int> intVar && genericType == typeof(int))
            {
                if (int.TryParse(ContentBox.Text, out int v))
                {
                    intVar.Value = v;
                }
            }
            else if (Value is DataPort<double> doubleVar && genericType == typeof(double))
            {
                if (double.TryParse(ContentBox.Text, out double v))
                {
                    doubleVar.Value = v;
                }
            }
            else if (Value is DataPort<float> floatVar && genericType == typeof(float))
            {
                if (float.TryParse(ContentBox.Text, out float v))
                {
                    floatVar.Value = v;
                }
            }
            else if (Value is DataPort<bool> boolVar && genericType == typeof(bool))
            {
                if (bool.TryParse(ContentBox.Text, out bool v))
                {
                    boolVar.Value = v;
                }
            }
            else if (Value is DataPort<long> longVar && genericType == typeof(long))
            {
                if (long.TryParse(ContentBox.Text, out long v))
                {
                    longVar.Value = v;
                }
            }
        }

    }
    public record class LinkPathParam
    {
        public LinkPathType PathType { get; set; }
        public Object Param { get; set; }
        public LinkPathParam(LinkPathType pathType, Object param)
        {
            PathType = pathType;
            Param = param;
        }
    }
}
