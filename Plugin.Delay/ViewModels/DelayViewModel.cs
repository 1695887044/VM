using System.ComponentModel;

namespace Plugin.Delay.ViewModels
{
    [Category("常用工具")]
    [DisplayName("延时工具")]
    [Description("Delay")]
    [Serializable]
    public class DelayViewModel:BindableBase
    {
        private int _delayTime=1000;
        public int DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; RaisePropertyChanged(); }
        }
        private string _status="就绪";
        public string Status
        {
            get { return _status; }
            set { _status = value; RaisePropertyChanged(); }
        }
        public DelegateCommand StartCommand { get; init; }
        public DelegateCommand StopCommand { get; init; }
        private bool _isRuning=false;
        public DelayViewModel()
        {
            StartCommand = new DelegateCommand(StartExecute, CanStart).ObservesProperty(() => IsRuning);
            StopCommand = new DelegateCommand(StopExecute, CanStop).ObservesProperty(() => IsRuning);
        }
        private void StopExecute()
        {
            _isRuning = false;
            Status = "已停止";
            RaisePropertyChanged(nameof(IsRuning));
        }
        private bool CanStop()
        {
            return IsRuning;
        }
        private void StartExecute()
        {
            _isRuning = true;
            Status = "运行中...";
            RaisePropertyChanged(nameof(IsRuning));
            System.Threading.Tasks.Task.Run(async () =>
            {
                await System.Threading.Tasks.Task.Delay(DelayTime);
                if (IsRuning)
                {
                    Status = "延时完成";
                    _isRuning = false;
                    RaisePropertyChanged(nameof(IsRuning));
                }
            });
        }
        private bool CanStart()
        {
            return !IsRuning;
        }
        public bool IsRuning
        {
            get { return _isRuning; }
        }
    }
}
