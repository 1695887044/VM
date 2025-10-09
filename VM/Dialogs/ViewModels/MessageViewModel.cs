using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Start.Dialogs.ViewModels
{
    public class MessageViewModel:IDialogAware
    {
        public string Message { get; set; } = "消息提示";

        public string Title { get; set; }= "弹窗";

        public int MsgType { get; set; } = 1;

        ButtonResult result = ButtonResult.None;

        public DelegateCommand<string> ButtomByTagCommand { get; init; }
        public MessageViewModel()
        {
            ButtomByTagCommand = new DelegateCommand<string>(Commands);
        }

        void Commands(string obj)
        {
            if (obj == "cancel") {
                result = ButtonResult.Cancel;
            }
            else if (obj == "ok")
            {
                result = ButtonResult.OK;
            }

            OnDialogClosed();
        }

        public DialogCloseListener RequestClose {  get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose.Invoke(null, result);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title=parameters["Title"].ToString();
            Message = parameters["Message"].ToString();
            _ = Int32.TryParse(parameters["MsgType"].ToString(), out int MsgType);
        }
    }
}
