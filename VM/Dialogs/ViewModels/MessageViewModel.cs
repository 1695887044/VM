using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Start.Dialogs.ViewModels
{
    public class MessageViewModel:IDialogAware
    {
        public Object Data { get; set; } = "消息提示";

        public string Title { get; set; }= "弹窗";

        public int MsgType { get; set; } = 1;

        private bool _isReadOnly;

        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }



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
            IDialogParameters keyValuePairs = new DialogParameters();
            keyValuePairs.Add("Result", result);
            keyValuePairs.Add("Data", Data);
            RequestClose.Invoke(keyValuePairs, result);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title=parameters["Title"].ToString();
            Data = parameters["Data"].ToString();
            IsReadOnly =parameters.ContainsKey("ReadOnly") && bool.TryParse(parameters["ReadOnly"].ToString(), out bool readOnly) && readOnly;
            _ = Int32.TryParse(parameters["level"].ToString(), out int MsgType);
        }
    }
}
