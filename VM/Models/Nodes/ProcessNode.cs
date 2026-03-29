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
    /// <summary>
    /// 流程模块
    /// </summary>
    public class ProcessNode : ContainerNodeBase<ToolNodeBase>
    {
        public override E_NodeType NodeType => E_NodeType.Process;




        public override string IconText { get; set; } = Icons.Icon_Precess;

        public ProcessNode()
        {
            Name = "流程模块";
        }
    }
}
