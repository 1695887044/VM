
using VM.Shard.Services;
using VM.Start.Services;

namespace VM.Start.ViewModels
{
    public class logViewModel:BindableBase
    {
        public  LogService service { get; init; }

        public DelegateCommand<string> OperatorCommmand { get; init; }


        public logViewModel(ILoggerService service)
        {
            this.service = service as LogService;
            OperatorCommmand = new DelegateCommand<string>(OPeratorMethod);
        }

        private void OPeratorMethod(string obj)
        {
            service.CrtType = obj switch
            {
                "warn" => Shard.Services.Log_Level.Warn,
                "Error" => Shard.Services.Log_Level.Error,
                "Fatal" => Shard.Services.Log_Level.Fatal,
                _ => Shard.Services.Log_Level.Info,
            };

        }
    }
}
