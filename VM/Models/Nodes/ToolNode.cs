using System.Collections.ObjectModel;
using System.Windows;
using VM.IPlugin;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Nodes
{
    /// <summary>
    /// 单个执行模块
    /// </summary>
    public class ToolNode : ToolNodeBase
    {





        public override string IconText { get; set; }




        public override E_NodeType NodeType => E_NodeType.Tool;

        public ToolNode()
        {
            Name = "节点";
        }
    }
}
