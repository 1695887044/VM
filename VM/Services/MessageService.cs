
using VM.IPlugin.Services;

namespace VM.Start.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDialogService dialogService;

        public MessageService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }
        public string InputShow(string title, string msg)
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", title);
            dialogParameters.Add("Msg", msg);
            string str = string.Empty;
            dialogService.ShowDialog("InputView", dialogParameters, s => { 
             if (s.Parameters.ContainsKey("msg"))
                {
                    str= s.Parameters["msg"].ToString();
                }
            });
            return str;
        }

        public void Show(string msg)
        {
           
        }
    }
}
