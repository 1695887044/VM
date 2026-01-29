using System.Windows;
using System.Windows.Controls;

namespace DemoThemes.ThemeCs
{
    [TemplatePart(Name = "PART_AddBtn", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SubBtn", Type = typeof(Button))]
    public class LabelValueControl : Control
    {
        private Button addBtn, subBtn;
        public string LeftText
        {
            get { return (string)GetValue(LeftTextProperty); }
            set { SetValue(LeftTextProperty, value); }
        }
        public static readonly DependencyProperty LeftTextProperty =
            DependencyProperty.Register("LeftText", typeof(string), typeof(LabelValueControl), new PropertyMetadata(string.Empty));



        public string RightText
        {
            
            get { return (string)GetValue(RightTextProperty); }
            set { SetValue(RightTextProperty, value); }
        }

        public static readonly DependencyProperty RightTextProperty =
            DependencyProperty.Register("RightText", typeof(string), typeof(LabelValueControl), new PropertyMetadata(string.Empty));




        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(LabelValueControl), new PropertyMetadata(false));




        public Double Value
        {
            get { return (Double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Double), typeof(LabelValueControl), new PropertyMetadata(0.0, UpDateButtonState));

        private static void UpDateButtonState(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           if(d is LabelValueControl ctl)
            {
                ctl._UpDateButtonState();
                
            }
        }

        public Double Step
        {
            get { return (Double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(Double), typeof(LabelValueControl), new PropertyMetadata(1.0));



        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(LabelValueControl), new PropertyMetadata(100.0));



        public Double MinValue
        {
            get { return (Double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(Double), typeof(LabelValueControl), new PropertyMetadata(0.0));


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            addBtn = this.GetTemplateChild("PART_AddBtn") as Button;
            subBtn = this.GetTemplateChild("PART_SubBtn") as Button;
            addBtn.Click += (s, e) => this.Value += Step;
            subBtn.Click += (s, e) => this.Value -= Step;
        }
        void _UpDateButtonState()
        {
            if (this.Value <= MinValue)
            {
                this.Value = MinValue;
                addBtn.IsEnabled = false;
                subBtn.IsEnabled = true;
            }
            if (this.Value >= MaxValue)
            {
                this.Value = MaxValue;
                addBtn.IsEnabled = false;
                subBtn.IsEnabled = true;
            }
        }
    }
}
