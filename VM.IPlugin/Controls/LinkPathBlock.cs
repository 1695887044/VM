using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VM.IPlugin.Models.VarModels;


namespace VM.IPlugin.Controls
{
    public class LinkPathBlock:Control
    {


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(LinkPathBlock), new PropertyMetadata(string.Empty));



        public bool IsReadOnlay
        {
            get { return (bool)GetValue(IsReadOnlayProperty); }
            set { SetValue(IsReadOnlayProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnlay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlayProperty =
            DependencyProperty.Register("IsReadOnlay", typeof(bool), typeof(LinkPathBlock), new PropertyMetadata(false));



        public string LinkPathName
        {
            get { return (string)GetValue(LinkPathNameProperty); }
            set { SetValue(LinkPathNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LinkPathName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LinkPathNameProperty =
            DependencyProperty.Register("LinkPathName", typeof(string), typeof(LinkPathBlock), new PropertyMetadata(string.Empty));



        public IVarValue BindVarData
        {
            get { return (IVarValue)GetValue(BindVarDataProperty); }
            set { SetValue(BindVarDataProperty, value); }
        }
        public static readonly DependencyProperty BindVarDataProperty =
            DependencyProperty.Register("BindVarData", typeof(IVarValue), typeof(LinkPathBlock), new PropertyMetadata(null));





        public ICommand LinkPathCommand
        {
            get { return (ICommand)GetValue(LinkPathCommandProperty); }
            set { SetValue(LinkPathCommandProperty, value); }
        }
        public static readonly DependencyProperty LinkPathCommandProperty =
            DependencyProperty.Register("LinkPathCommand", typeof(ICommand), typeof(LinkPathBlock), new PropertyMetadata(null));





    }
}
