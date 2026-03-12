
using System.Windows.Interop;
using VM.Shard.Services;

namespace VM.Start.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDialogService dialogService;

        public MessageService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public bool ShowConfirmation(string message, bool ReadOnlay = false, Log_Level level = Log_Level.Info, string title = "提示")
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", title);
            dialogParameters.Add("Data", message);
            dialogParameters.Add("level", level);
            dialogParameters.Add("ReadOnly", ReadOnlay);
            bool _res =false;
            dialogService.ShowDialog("MessageView", dialogParameters,(s)=> { 
                s.Parameters.ContainsKey("Result");
                _res = s.Parameters.GetValue<bool>("Result");
            });
            return _res;
        }

        public Task<TResult> ShowDialogAsync<TViewModel, TResult>(TViewModel viewModel) where TViewModel : class
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string message, bool ReadOnlay = false, Log_Level level = Log_Level.Info, string title = "提示")
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", title);
            dialogParameters.Add("Data", message);
            dialogParameters.Add("level", level);
            dialogParameters.Add("ReadOnly", ReadOnlay);
            dialogService.ShowDialog("MessageView", dialogParameters);
            
        }

        public T ShowPropertyView<T>( T data, bool ReadOnlay = false, Log_Level level = Log_Level.Info, string title = "提示")
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", title);
            dialogParameters.Add("Data", data);
            dialogParameters.Add("level", level);
            dialogParameters.Add("ReadOnly", ReadOnlay);
            T? _res =default(T);
            dialogService.ShowDialog("MessageView", dialogParameters, (s) => {
                s.Parameters.ContainsKey("Data");
                _res = s.Parameters.GetValue<T>("Data");
            });
            return _res;
        }
    }
}
