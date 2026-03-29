using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Markup;
using VM.IPlugin;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Nodes
{
    public interface INode
    {
        long Id { get; set; }
        Guid Uid { get; }

        string Name { get; set; }
        E_NodeType NodeType { get; }
        string IconText { get;  }
        string Remark { get; set; }
        string Tag { get; set; }
        int SortId {  get; set; }

        INode Parent { get; set; }


    }

    public interface IContainerNode<T> 
    {
        ObservableCollection<T> Children { get; }

    }

    public abstract class NodeBase : BindableBase, INode
    {
        public Guid Uid { get; private set; } = Guid.NewGuid();
        public int SortId { get; set; }
        public abstract E_NodeType NodeType { get; }

        [JsonIgnore]
        public INode Parent { get; set; }


        private long _id;
        public long Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _name ;
        public string Name
        {
            get { return _name; }
            set {  SetProperty(ref _name, value); }
        }

        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { SetProperty(ref _remark, value); }
        }

        private string _tag;
        public string Tag
        {
            get { return _tag; }
            set { ; SetProperty(ref _tag, value); }
        }
        //private bool _isSelected;
        //public bool IsSelected
        //{
        //    get { return _isSelected; }
        //    set { SetProperty(ref _isSelected, value); }
        //}

        public bool IsEnable { get; set; } = true;
        public abstract  string  IconText { get; set; }

        // --- 时间戳 ---
        private DateTime _createTime = DateTime.Now; // 默认当前时间
        public DateTime CreateTime => _createTime;
        private DateTime _updateTime ;
        public DateTime UpdateTime
        {
            get { return _updateTime; }
            set { SetProperty(ref _updateTime, value); }
        }
    }

    public abstract class ToolNodeBase : NodeBase
    {
        /// <summary>
        /// 执行模块
        /// </summary>
        public ModuleViewModelBase ViewModel { get; set; }


        public Type View { get; init; }
        /// <summary>
        /// 运行时间
        /// </summary>
        public Double CostTime { get; set; }
        /// <summary>
        /// 运行中
        /// </summary>
        public bool IsRuning { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public E_NodeState State { get; set; }
    }
    public abstract class ContainerNodeBase<T> : NodeBase, IContainerNode<T>
    {
        public  ObservableCollection<T> Children { get; set; } = new ObservableCollection<T>();
        // 👈 核心魔法：当 JSON 引擎把这个节点以及它的 Children 刚刚在内存里还原完时，自动触发此方法！
        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            if (Children == null) return;
            foreach (var child in Children)
            {
                if (child is INode node)
                {
                    node.Parent = this; 
                }
            }
        }
    }

}
