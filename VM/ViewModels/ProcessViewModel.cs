using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VM.IPlugin.Consts;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.IPlugin.Views;
using VM.Start.Models.Projects.Nodes;
using VM.Start.Services;

namespace VM.Start.ViewModels
{
    public class ProcessViewModel:BindableBase, IDropTarget
    {
        #region 流程栏按钮命令声明
         public DelegateCommand ExecuteFlowOnceCommand { get; init; }
         public DelegateCommand RunContinuousCommand { get; init; }
         public DelegateCommand StopFlowCommand { get; init; }
        #endregion
        private ObservableCollection<IProcessNode> processDatas = new();
        private readonly IDialogService dialogService;
        private readonly GlobalVarService globalVarService;
        private IProcessNode _currentNode;
        public DelegateCommand<IProcessNode> DoubleClickCommand { get; init; }

        public ObservableCollection<IProcessNode> ProcessDatas
        {
            get { return processDatas; }
            set { processDatas = value;RaisePropertyChanged(); }
        }


        public ProcessViewModel(IDialogService dialogService,GlobalVarService globalVarService)
        {
            DoubleClickCommand = new DelegateCommand<IProcessNode>(NodeShow);
            ExecuteFlowOnceCommand = new DelegateCommand(ExecuteFlowOnce);
            this.dialogService = dialogService;
            this.globalVarService = globalVarService;
        }

        private void ExecuteFlowOnce()
        {
            
        }
        /// <summary>
        /// 打开的时候 订阅打开变量视图事件
        /// </summary>
        /// <param name="node"></param>
        private void NodeShow(IProcessNode node)
        {
            if (!(node.View is FrameworkElement content)) return;
            _currentNode = node;
            node.ViewModel.OpenVarLinkViewEvent += OpenVarLinkView;
             _ = new PluginView().ShowView(content, node.ViewModel);
            node.ViewModel.OpenVarLinkViewEvent -= OpenVarLinkView;
        }
        /// <summary>
        /// 打开视图,传参 传一个委托用于筛选显示变量,关闭的时候 要调用链接变量改变的方法 接口要提供一个方法 用来接收变量改变后的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenVarLinkView(object? sender, OpenLinkargs e)
        {
            //根据传入的参数筛选数据
            globalVarService.RefreshDisplayVarList(e.Fiter);
            //打开弹窗 确认后 通知对应后台  作出VieModel的变量改变处理
            dialogService.ShowDialog("VarLinkView", (s) => {
                if (s.Result != ButtonResult.OK) return;
                s.Parameters.ContainsKey(GlobalConst.LinkVarEventParamterKey);
                VarChangedEventParamModel varEvent = new VarChangedEventParamModel();
                varEvent.varValue= s.Parameters.GetValue<IVarValue>(GlobalConst.LinkVarEventParamterKey);
                _currentNode.ViewModel.OnLinkVarPathChanged(varEvent);
            });
        }
        #region 控件拖拽
        /// <summary>
        /// 控件拖动
        /// </summary>
        /// <param name="args"></param>
        public void DragOver(IDropInfo args)
        {
            args.Effects = args.Data is IProcessNode ? DragDropEffects.Move : args.Data is INode ? DragDropEffects.Copy : DragDropEffects.None;
        }
        /// <summary>
        /// 控件落下
        /// </summary>
        /// <param name="dropInfo"></param>
        public void Drop(IDropInfo args)
        {
            if (args.Effects != DragDropEffects.Copy && args.Effects != DragDropEffects.Move) return;
            if (args.Effects == DragDropEffects.Copy && args.Data is INode node)
            {
                ProcessDatas.Add(createProcessNode(node));
                return;
            }
            if (args.Effects == DragDropEffects.Move  && args.Data is IProcessNode d && args.TargetItem is IProcessNode t)
            {
                var a = d.SortId;
                ProcessDatas[d.SortId-1] = t;
                ProcessDatas[t.SortId-1] = d;
                d.SortId = t.SortId;
                t.SortId = a;
            }
        }
        /// <summary>
        /// 根据节点创建流程节点
        /// </summary>
        /// <param name="node"></param>
        /// <exception cref="NotImplementedException"></exception>
       #endregion
        private ProcessNode createProcessNode(INode args)
        {
            ProcessNode node = new()
            {
                Name = args.Name,
                CreateTime = DateTime.Now,
                Updatetime = DateTime.Now,
                SortId = ProcessDatas.Count > 0 ? ProcessDatas.Last().SortId + 1 : 1,
                Token = Guid.NewGuid(),
                Tag = args.Tag,
                IconText = args.IconText,
                View= PluginService.PluginDic_Module[args.Tag].ViewType,
                ViewModel = PluginService.PluginDic_Module[args.Tag].ViewModelType,
                Remark = args.Remark
            };
            return node;
        }
    }
}
