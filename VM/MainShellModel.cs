using VM.Start.Services;

namespace VM.Start
{
  public  class MainShellModel:BindableBase
    {
        private string _actTime;
        private readonly IRegionManager region;
        private readonly PrismProvider prism;
        public DelegateCommand<string> ViewCommand { get; init; }
        public DelegateCommand LoadedCommand { get; init; }
        public string ActTime
        {
            get { return _actTime; }
            set { _actTime = value; RaisePropertyChanged(); }
        }

        public Timer DateTimer { get; init; } 

        public MainShellModel(PrismProvider prism)
        {
            DateTimer =new Timer( (s) => { ActTime=DateTime.Now.ToString(); }, null, 0, 100);
            this.prism = prism;
            ViewCommand=new DelegateCommand<string>(ViewExecute);
            LoadedCommand = new(() => { prism.RegionManager.RequestNavigate("MainContent", "DockView"); });
        }

        private void ViewExecute(string obj)
        {
           
        }

        private void LoginCallBack(IDialogResult result)
        {
            
        }
    }
}
