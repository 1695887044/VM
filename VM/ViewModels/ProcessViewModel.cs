using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        public DelegateCommand<IProcessNode> DoubleClickCommand { get; init; }

        public ObservableCollection<IProcessNode> ProcessDatas
        {
            get { return processDatas; }
            set { processDatas = value;RaisePropertyChanged(); }
        }


        public ProcessViewModel()
        {
            DoubleClickCommand = new DelegateCommand<IProcessNode>(NodeShow);
            ExecuteFlowOnceCommand = new DelegateCommand(ExecuteFlowOnce);
        }

        private void ExecuteFlowOnce()
        {
            
        }

        private void NodeShow(IProcessNode node)
        {
            if(node.View is FrameworkElement fe)
            {
                var a =  new PluginView();
                a.Init(fe);
                a.ShowDialog();
            }
            //node.View.ShowView();

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
