using AvalonDock;
using System.Windows;
using VM.Shard.Extensions;
using VM.Shard.Services;
using VM.Start.Core.Interfaces;
using VM.Start.Models;
using VM.Start.Services;

namespace VM.Start
{
  public  class MainShellModel:BindableBase
    {

        private SystemInfo _projectInfo;

        public SystemInfo ProjectInfo
        {
            get { return _projectInfo; }
            set { _projectInfo = value; RaisePropertyChanged(); }
        }

        private Window _window;
        private DockingManager _layoutRoot;
        private readonly IRegionManager region;
        private readonly PrismProvider prism;
        private readonly IMessageService messageService;
        public  ISolutionManager SolutionManager { get; init; }

        public DelegateCommand<string> ViewCommand { get; init; }
        public DelegateCommand<Object> LoadedCommand { get; init; }
        public DelegateCommand<string> AppComs { get; init; }

        public MainShellModel(PrismProvider prism, SystemInfo systemInfo,IMessageService messageService, ISolutionManager solutionManager)
        {
            this.prism = prism;
            ProjectInfo = systemInfo;
            this.messageService = messageService;
            this.SolutionManager = solutionManager;
            ViewCommand =new DelegateCommand<string>(ViewExecute);
            LoadedCommand = new DelegateCommand<Object>(LoadedExecute);
            AppComs = new DelegateCommand<string>(appCommands);
        }

        private void appCommands(string obj)
        {
            if (_window is null || string.IsNullOrEmpty(obj)) return;
            switch (obj)
            {
                case "mini":
                    _window.WindowState = WindowState.Minimized;
                    break;
                case "max":
                    _window.WindowState = _window.WindowState == WindowState.Maximized? WindowState.Normal : WindowState.Maximized;
                    break;
                case "close":
                    _window.Close();
                    break;
            }
        }

        private void LoadedExecute(object obj)
        {
            prism.RegionManager.RequestNavigate("MainContent", "DockView");
            if (obj is RoutedEventArgs routed)
            {
                if (routed.Source is Window win)
                {
                    _window = win;
                    getLayoutCtl(win);
                }
            }
           
            
        }

        private void getLayoutCtl(Window win)
        {
            _layoutRoot =  VisualTreeExt.FindChild<DockingManager>(_window);
            
        }

        private void ViewExecute(string obj)
        {
            switch (obj)
            {
                case "NewSolution":
                    SolutionManager.CreateSolution();
                    break;
                case "GlobalVar":
                    prism.DialogService.ShowDialog("GlobalView"); 
                    break;
            }
        }

        private void LoginCallBack(IDialogResult result)
        {
            
        }
    }
}
