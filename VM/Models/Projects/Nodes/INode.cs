using System.Collections.ObjectModel;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public interface INode
    {
        string Name { get; set; }

        eProjectType  NodeType { get; }

        INode Parent { get; }

        ObservableCollection<INode> Children { get; }

        String IconText { get; }

        string Remark { get; set; } 

        string Tag { get; set; }
    }
}
