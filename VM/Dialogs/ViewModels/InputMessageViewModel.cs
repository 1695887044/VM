using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Start.Dialogs.ViewModels
{
    internal class InputMessageViewModel : BindableBase, IDialogAware
    {
        public string Title { get; set; }
        public string ContentText { get; set; }

        public DelegateCommand ConfirmCommand { get; init; }
        public DelegateCommand CancelCommand { get; init; }
        public InputMessageViewModel()
        {
            ConfirmCommand = new DelegateCommand(confirm);
            CancelCommand = new DelegateCommand(cancel);
        }

        private void cancel()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("msg", string.Empty);
            RequestClose.Invoke(parameters);
        }

        private void confirm()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("msg", ContentText);
            RequestClose.Invoke(parameters);

        }

        public DialogCloseListener RequestClose {  get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters["Title"].ToString();
            ContentText = parameters["Msg"].ToString();
        }
    }
}
