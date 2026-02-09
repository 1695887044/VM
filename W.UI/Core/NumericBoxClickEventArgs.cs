using System.Windows;

namespace W.UI.Core
{
    public class NumericBoxClickEventArgs : RoutedEventArgs
    {
        public bool SkipStepChange { get; set; }
        public NumericBoxClickEventArgs(RoutedEvent routedEvent) : base(routedEvent) { }
    }
}
