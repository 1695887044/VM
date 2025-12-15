

using System.Collections.ObjectModel;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    /// <summary>
    /// 流程模块
    /// </summary>
    public class MethodNode : ModelBase, INode
    {
        public string Name { get; set; } = "方法";

        public eProjectType NodeType => eProjectType.Method;

        public INode Parent {  get; set; }

        public ObservableCollection<INode> Children { get;  }

        public ObservableCollection<IProcessNode> Nodes { get; set; } = new();

        public string IconText { get; set; } ="\uf1c3";

        public string Remark { get; set; }
        public string Tag { get; set; }
    }
}
