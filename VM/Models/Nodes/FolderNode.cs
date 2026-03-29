using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Com.Enums;
using VM.Shard.Resources;

namespace VM.Start.Models.Nodes
{
    public class FolderNode : ContainerNodeBase<INode>
    {

        public override E_NodeType NodeType => E_NodeType.Folder;


        public override string IconText { get; set; } = Icons.Icon_Folder;
        public FolderNode()
        {
            Name = "文件夹";
        }


    }
}
