using Prism.Ioc;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using VM.Shard.Helper;
using VM.Start;
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
            return Container.Resolve<MainShell>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LogView>();
            containerRegistry.RegisterForNavigation<DockView, DockViewModel>();
            containerRegistry.RegisterForNavigation<MainShell, MainShellModel>();
            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>();
            containerRegistry.RegisterDialog<LoginView>();
            containerRegistry.RegisterSingleton<PrismProvider>();
            containerRegistry.RegisterSingleton<SystemInfo>();
            containerRegistry.RegisterSingleton<PluginService>();
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
