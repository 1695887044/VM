using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Com.Enums;
using VM.Shard.Resources;

namespace VM.Start.Models.Nodes
{
    public class SolutionNode: ContainerNodeBase<INode>
    {

        public string SavePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "Solutions";


        public override E_NodeType NodeType => E_NodeType.Solution;



        public override string IconText { get; set; }=Icons.Icon_Solution;

        public SolutionNode()
        {
            Name = "解决方案";
        }

    }
}
