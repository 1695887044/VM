using System.Windows;
using System.Windows.Controls;

namespace VM.IPlugin.Controls
{
    public class EditValueBlock : Control
    {

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(EditValueBlock), new PropertyMetadata(string.Empty));



        public bool IsReadOnlay
        {
            get { return (bool)GetValue(IsReadOnlayProperty); }
            set { SetValue(IsReadOnlayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnlay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlayProperty =
            DependencyProperty.Register("IsReadOnlay", typeof(bool), typeof(EditValueBlock), new PropertyMetadata(false));




        public Double MaxValue
        {
            get { return (Double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(Double), typeof(EditValueBlock), new PropertyMetadata(999.0));



        public double MinVavlue
        {
            get { return (double)GetValue(MinVavlueProperty); }
            set { SetValue(MinVavlueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinVavlue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinVavlueProperty =
            DependencyProperty.Register("MinVavlue", typeof(double), typeof(EditValueBlock), new PropertyMetadata(0.0));




        public Double CurrentValue
        {
            get { return (Double)GetValue(CurrentValueProperty); }
            set { SetValue(CurrentValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentValueProperty =
            DependencyProperty.Register("CurrentValue", typeof(Double), typeof(EditValueBlock), new PropertyMetadata(0.0));



        public Double Step
        {
            get { return (Double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Step.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register("Step", typeof(Double), typeof(EditValueBlock), new PropertyMetadata(5.0));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.GetTemplateChild("PART_IncreaseButton") is Button increaseButton)
            {
                increaseButton.Click += (s, e) =>
                {
                    
                    CurrentValue += Step;
                    CurrentValue = CurrentValue > MaxValue ? MaxValue : CurrentValue;
                    CurrentValue = CurrentValue < MinVavlue ? MaxValue : MinVavlue;
                };
            }
            if (this.GetTemplateChild("PART_DecreaseButton") is Button decreaseButton)
            {
                decreaseButton.Click += (s, e) =>
                {
                    CurrentValue -= Step;
                    CurrentValue = CurrentValue > MaxValue ? MaxValue : CurrentValue;
                    CurrentValue = CurrentValue < MinVavlue ? MaxValue : MinVavlue;
                };
            }

        }
    }
}
