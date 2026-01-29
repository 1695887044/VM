

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.Controls
{
    public class LabelLinkPath : Control
    {
        Button? LinkButton, ClearButton;
        TextBlock? HeadTextBlock;
        TextBox? ContentBox;
        Tuple<string, string> P1;
        Tuple<string, string> P2;
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



        public string LinkParam
        {
            get { return (string)GetValue(LinkParamProperty); }
            set { SetValue(LinkParamProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkParam.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkParamProperty =
            DependencyProperty.Register("LinkParam", typeof(string), typeof(LabelLinkPath), new PropertyMetadata(string.Empty));



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
                if(e.NewValue is IVarValue _Var)
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

        private void UpdateTextValue(IVarValue _var)
        {
            ContentBox.IsReadOnly = true;
            ContentBox.Text = _var.LinkPath + "/" + _var.Name;
        }

        public ICommand OperatorCommand
        {
            get { return (ICommand)GetValue(OperatorCommandProperty); }
            set { SetValue(OperatorCommandProperty, value); }
        }

        public static readonly DependencyProperty OperatorCommandProperty =
            DependencyProperty.Register("OperatorCommand", typeof(ICommand), typeof(LabelLinkPath));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            LinkButton = this.GetTemplateChild("PART_LinkButton") as Button;
            ClearButton = this.GetTemplateChild("PART_ClearButton") as Button;
            HeadTextBlock = this.GetTemplateChild("PART_HeadTextBlock") as TextBlock;
            ContentBox = this.GetTemplateChild("PART_ContentBox") as TextBox;
            P1 = new Tuple<string, string>("Link", this.LinkParam);
            P2 = new Tuple<string, string>("Clear", this.LinkParam);
            LinkButton.Click += (s, e) =>
            {

                OperatorCommand?.Execute(P1);
            };
            ClearButton.Click += (s, e) =>
            {
                OperatorCommand?.Execute(P2);
            };
        }



    }
}
