

using System.Collections.ObjectModel;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public class MethodNode : ModelBase, INode
    {
        public string Name { get; set; } = "方法";

        public eProjectType NodeType => eProjectType.Method;

        public INode Parent {  get; set; }

        public ObservableCollection<INode> Children { get; set; } = new ObservableCollection<INode>();


        public string IconText { get; set; } ="\uf1c3";

        public string Remark { get; set; } = "文件储存";
    }
}
