using GongSolutions.Wpf.DragDrop;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using VM.Start.Models.Projects.Nodes;

namespace VM.Start.ViewModels
{
    public class ProcessViewModel:BindableBase, IDropTarget
    {
        private ObservableCollection<IProcessNode> processDatas = new();

        public ObservableCollection<IProcessNode> ProcessDatas
        {
            get { return processDatas; }
            set { processDatas = value;RaisePropertyChanged(); }
        }


        public ProcessViewModel()
        {
        }

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
        private ProcessNode createProcessNode(INode args)
        {
            ProcessNode node = new();
            node.Name = args.Name;
            node.CreateTime = DateTime.Now;
            node.Updatetime = DateTime.Now;
            node.SortId = ProcessDatas.Count > 0 ? ProcessDatas.Last().SortId+1:1;
            node.Token = Guid.NewGuid();
            node.IconText = args.IconText;
            node.ModuleBase = "执行模块";
            node.Remark = args.Remark;
            return node;
        }
    }
}
