using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public class SolutionNode:INode
    {
        public string Name { get; set; } = "解决方案";

        public string SavePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Solutions";

        public NodeType NodeType => NodeType.Solution;

        public INode Parent { get; }

        public ObservableCollection<INode> Children { get; set; } = new ObservableCollection<INode>();


        public string IconText => "\uf07c";

        public string Remark { get; set; } = "解决方案";

        public string Tag { get; set; }


    }
}
