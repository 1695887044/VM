using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    public class FolderNode : ModelBase, INode
    {
        public string Name { get; set; } = "文件夹";

        public eProjectType NodeType => eProjectType.Folder;

        public INode Parent { get; set; }

        public ObservableCollection<INode> Children { get; set; } = new ObservableCollection<INode>();


        public string IconText => "\uf07c";

        public string Remark { get; set; } = "文件储存";

        public string Tag { get; set; }
    }
}
