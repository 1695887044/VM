using Prism.Ioc;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using VM.Shard.Helper;
using VM.Shard.Services;
using VM.Start;
using VM.Start.Core.Interfaces;
using VM.Start.Core.IOC;
using VM.Start.Dialogs.ViewModels;
using VM.Start.Dialogs.Views;
using VM.Start.Models;
using VM.Start.Services;
using VM.Start.ViewModels;
using VM.Start.Views;

namespace VM
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            Container.Resolve<PluginService>().InitPlugin();
            App.Current.DispatcherUnhandledException += (s, e) => MessageBox.Show(e.Exception.Message);
            TaskScheduler.UnobservedTaskException += (s, e) => MessageBox.Show(e.Exception.Message);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            };
            return Container.Resolve<MainShell>();
          
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LogView,logViewModel>();
            containerRegistry.RegisterForNavigation<DockView, DockViewModel>();
            containerRegistry.RegisterForNavigation<MainShell, MainShellModel>();
            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>();
            containerRegistry.RegisterDialog<LoginView>();
            containerRegistry.RegisterDialog<VarLinkView, VarLinkViewModel>();
            containerRegistry.RegisterSingleton<PrismProvider>();
            containerRegistry.RegisterSingleton<SystemInfo>();
            containerRegistry.RegisterSingleton<IMessageService, MessageService>();
            containerRegistry.RegisterSingleton<ISuperDialogService, SuperDialogService>();
            containerRegistry.RegisterSingleton<PluginService>().RegisterSingleton<GlobalVariableService>();
            containerRegistry.RegisterSingleton<ILoggerService, LogService>();
            containerRegistry.RegisterSingleton<ISolutionManager, SolutionService>();
            containerRegistry.RegisterDialog<GlobalVariableView, GlobalVariableViewModel>("GlobalView");
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            string solutionPath = PathHelper.GetSolutionPath() + @"\Modules";
            return new DirectoryModuleCatalog() { ModulePath = solutionPath };

        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
        }

    }

}
