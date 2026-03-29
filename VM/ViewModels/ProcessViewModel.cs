using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;
using VM.IPlugin;
using VM.IPlugin.Consts;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.IPlugin.Views;
using VM.Shard.Services;
using VM.Start.Core.Interfaces;
using VM.Start.Models.Nodes;
using VM.Start.Services;

namespace VM.Start.ViewModels
{
    public class ProcessViewModel:BindableBase, IDropTarget
    {
        #region 流程栏按钮命令声明
         public DelegateCommand ExecuteFlowOnceCommand { get; init; }
         public DelegateCommand RunContinuousCommand { get; init; }
         public DelegateCommand StopFlowCommand { get; init; }

        public DelegateCommand<string> MenuOperateCommand { get; init; }
        private readonly PrismProvider prism;
        #endregion

        private readonly IDialogService dialogService;
        private readonly GlobalVariableService globalVarService;
        private readonly ILoggerService loggerService;
        private readonly IMessageService messageService;
        private readonly ISolutionManager solutionManager;
        private ToolNodeBase _currentNode;
        private ToolNodeBase selectNodeItem;

        public ToolNodeBase SelectNodeItem
        {
            get { return selectNodeItem; }
            set { selectNodeItem = value; RaisePropertyChanged(); }
        }

        public DelegateCommand<ToolNodeBase> DoubleClickCommand { get; init; }
        private ObservableCollection<ToolNodeBase> _processDatas;

        public ObservableCollection<ToolNodeBase> ProcessDatas
        {
            get { return _processDatas; }
            set { _processDatas = value;  RaisePropertyChanged(); }
        }





        public ProcessViewModel(PrismProvider prism , IDialogService dialogService,GlobalVariableService globalVarService,ILoggerService loggerService,IMessageService messageService, ISolutionManager solutionManager)
        {

            DoubleClickCommand = new DelegateCommand<ToolNodeBase>(NodeShow);
            ExecuteFlowOnceCommand = new DelegateCommand(ExecuteFlowOnce);
            MenuOperateCommand = new DelegateCommand<string>(MenuOperate);
            this.prism = prism;
            this.dialogService = dialogService;
            this.globalVarService = globalVarService;
            this.loggerService = loggerService;
            this.messageService = messageService;
            this.solutionManager = solutionManager;
            solutionManager.SelectedNodeChanged += (s,e)=> { 
                if(e is ContainerNodeBase<ToolNodeBase> containerNodeBase)
                {
                    ProcessDatas = containerNodeBase.Children;
                }
            };
        }
        /// <summary>
        /// 菜单栏命令
        /// </summary>
        /// <param name="obj"></param>
        private void MenuOperate(string obj)
        {
            if (SelectNodeItem == null) return;

            switch (obj)
            {
                case "重命名":
                   SelectNodeItem.Name =   messageService.ShowPropertyView(SelectNodeItem.Name);
                    break;
                case "编辑注释":
                    SelectNodeItem.Remark = messageService.ShowPropertyView(SelectNodeItem.Remark);
                    break;
                case "禁用": break;
                case "粘贴": break;
                case "删除":
                    solutionManager.CurrentSolution.Children.Remove(SelectNodeItem);
                    int tempi = 1;
                    foreach (var processData in ProcessDatas)
                    {
                        processData.SortId = tempi;
                        tempi = tempi + 1;
                    }
                    break;
                default:
                    break;
            }
           
        }

        private void ExecuteFlowOnce()
        {
            
        }
        /// <summary>
        /// 打开的时候 订阅打开变量视图事件
        /// </summary>
        /// <param name="node"></param>
        private void NodeShow(ToolNodeBase node)
        {
            if (node.View == null) return;
            var content = Activator.CreateInstance(node.View) as FrameworkElement;
            if (content == null) return;
            _currentNode = node;
            node.ViewModel.OpenVarLinkViewEvent += OpenVarLinkView; 
             _ = new PluginView().ShowView(content, node.ViewModel,node.Name,node.IconText);
            node.ViewModel.OpenVarLinkViewEvent -= OpenVarLinkView;
        }
        /// <summary>
        /// 打开视图,传参 传一个委托用于筛选显示变量,关闭的时候 要调用链接变量改变的方法 接口要提供一个方法 用来接收变量改变后的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenVarLinkView(object? sender, IOpenLinkargs e)
        {
            //根据传入的参数筛选数据
            globalVarService.RefreshDisplayVarList(e.Fiter,_currentNode);
            //打开弹窗 确认后 通知对应后台  作出VieModel的变量改变处理
            dialogService.ShowDialog("VarLinkView", (s) => {
                if (s.Result != ButtonResult.OK) return;
                s.Parameters.ContainsKey(GlobalConst.LinkVarEventParamterKey);
                IDataPort v = s.Parameters.GetValue<IDataPort>(GlobalConst.LinkVarEventParamterKey);
                VarChangedEventParamModel varEvent = new VarChangedEventParamModel();
                varEvent.varValue = v;
                e.CallBack?.Invoke(varEvent);
            });
        }
        #region 控件拖拽
        /// <summary>
        /// 控件拖动
        /// </summary>
        /// <param name="args"></param>
        public void DragOver(IDropInfo dropInfo)
        {
            // 如果没有抓取到数据，或者没有落点集合，直接拒绝
            if (dropInfo.Data == null || dropInfo.TargetCollection == null) return;

            bool isInternalMove = dropInfo.DragInfo.SourceCollection == dropInfo.TargetCollection;

            if (isInternalMove)
            {
                dropInfo.Effects = DragDropEffects.Move;
                // 允许在项之间插入
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            }
            else
            {
                dropInfo.Effects = DragDropEffects.Copy;
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
            }
        }
        /// <summary>
        /// 控件落下
        /// </summary>
        /// <param name="dropInfo"></param>
        public void Drop(IDropInfo args)
        {
            if (args.Effects != DragDropEffects.Copy && args.Effects != DragDropEffects.Move) return;
            if (args.Effects == DragDropEffects.Copy && args.Data is ToolNodeBase node)
            {
                ProcessDatas.Add(createProcessNode(node));
                loggerService.LogInfo($"添加模块{node.Name}");
                return;
            }
            if (args.Effects == DragDropEffects.Move  && args.Data is ToolNodeBase d && args.TargetItem is ToolNodeBase t)
            {
                var a = d.SortId;
                ProcessDatas[d.SortId-1] = t;
                ProcessDatas[t.SortId-1] = d;
                d.SortId = t.SortId;
                t.SortId = a;
            }
        }
        /// <summary>
        /// 根据节点创建流程节点 创建后 模块初始化  注册输入类型 注册输出类型
        /// </summary>
       #endregion
        private ToolNodeBase createProcessNode(INode args)
        {
            ToolNode node = new()
            {
                Name = args.Name,
                UpdateTime = DateTime.Now,
                SortId = ProcessDatas?.Count > 0 ? ProcessDatas.Last().SortId + 1 : 1,
                Tag = args.Tag,
                
                IconText = args.IconText,
                ViewModel = (ModuleViewModelBase)prism.Container.Resolve(PluginService.PluginDic_Module[args.Tag].ViewModelType),
                View = PluginService.PluginDic_Module[args.Tag].ViewType,
                Remark = args.Remark
            };
            node.ViewModel.ModuleInit();
            return node;
        }
    }
}
