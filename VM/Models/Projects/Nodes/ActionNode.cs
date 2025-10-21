using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public class ActionNode : ModelBase,INode
    {
        public string Name { get; set; } = "模块";

        public eProjectType NodeType => eProjectType.Process;

        public INode Parent { get; set; }

        public ObservableCollection<INode> Children { get; set; }=new ObservableCollection<INode>();


        public string IconText => "\uf0ae";

        public string Remark { get; set; } = "模块";

        public string Tag { get; set; }
    }
}
