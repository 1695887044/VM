

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DemoThemes.ThemeCs
{
    public class LabelLinkPath : Control
    {
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


            }
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
            LinkButton.Click += (s, e) =>
            {
                OperatorCommand?.Execute("Link");
            };
            ClearButton.Click += (s, e) =>
            {
                OperatorCommand?.Execute("Clear");
            };
        }



    }
}
