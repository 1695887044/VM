using System.Windows.Threading;
using VM.Start.Core.IOC;

namespace VM.Start.Services
{
    public sealed class PrismProvider
    {
        public PrismProvider(
            IContainerExtension container,
            IRegionManager regionManager,
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IModuleManager moduleManager)
        {
            Container = container;
            RegionManager = regionManager;
            DialogService = dialogService;
            EventAggregator = eventAggregator;
            ModuleManager = moduleManager;

            Dispatcher = System.Windows.Application.Current.Dispatcher;
        }


        /// <summary>
        /// 容器
        /// </summary>
        public  IContainerExtension Container { get; private set; }

        /// <summary>
        /// 区域管理器接口
        /// </summary>
        public  IRegionManager RegionManager { get; private set; }

        /// <summary>
        /// 对话框管理器
        /// </summary>
        public  IDialogService DialogService { get; private set; }

        /// <summary>
        /// 事件聚合器
        /// </summary>
        public  IEventAggregator EventAggregator { get; private set; }

        /// <summary>
        /// 模块管理器
        /// </summary>
        public  IModuleManager ModuleManager { get; private set; }

        public  Dispatcher Dispatcher { get; private set; }

    }
}
