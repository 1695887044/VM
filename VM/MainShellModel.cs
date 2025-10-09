using AvalonDock;
using AvalonDock.Layout;
using System.Windows;
using System.Windows.Media;
using VM.Shard.Extensions;
using VM.Start.Models;
using VM.Start.Services;
using VM.Start.Views;

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
        public DelegateCommand<string> ViewCommand { get; init; }
        public DelegateCommand<Object> LoadedCommand { get; init; }
        public DelegateCommand<string> AppComs { get; init; }


        public MainShellModel(PrismProvider prism, SystemInfo systemInfo)
        {
            this.prism = prism;
            ProjectInfo = systemInfo;
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
                    IDialogParameters keyValues = new DialogParameters();
                    keyValues.Add("Title","新建解决方案");
                    keyValues.Add("Message", "创建新的解决方案会覆盖掉当前已有的解决方案，确认继续？");
                    keyValues.Add("MsgType", 1);
                    prism.DialogService.ShowDialog("MessageView", keyValues ,(s) => {

                    });
                    break;
            }
        }

        private void LoginCallBack(IDialogResult result)
        {
            
        }
    }
}
