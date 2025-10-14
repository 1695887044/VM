using System.Collections.ObjectModel;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public class ProcessNode : ModelBase, IProcessNode
    {
        public int SortId { get; set; }
        public object ModuleBase { get; set; }
        public string CostTime { get; set; }
        public bool IsRuning { get; set; }
        public int State { get; set; }

        public string Name { get; set; }

        public eProjectType NodeType { get; set; }

        public INode Parent { get; set; }

        public ObservableCollection<INode> Children { get; set; } = new();

        public string IconText { get; set; }

        public string Remark { get; set; }
    }
}
