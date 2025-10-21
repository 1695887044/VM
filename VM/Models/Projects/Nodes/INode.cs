using System.Collections.ObjectModel;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public interface INode
    {
        string Name { get; }

        eProjectType  NodeType { get; }

        INode Parent { get; }

        ObservableCollection<INode> Children { get; }

        String IconText { get; }

        string Remark { get; }

        string Tag { get; set; }
    }
}
