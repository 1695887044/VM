

using VM.IPlugin;

namespace VM.Start.Models.Projects.Nodes
{
    /// <summary>
    /// 流程节点
    /// </summary>
    public interface IProcessNode:INode
    {
        /// <summary>
        /// 排序ID
        /// </summary>
        public int SortId { get; set; }
        /// <summary>
        /// 执行模块
        /// </summary>
        public ModuleViewModelBase ViewModel { get; set; }
        

        public IModuleViewBase View { get; init; }
        /// <summary>
        /// 运行时间
        /// </summary>
        public string CostTime { get; set; } 
        /// <summary>
        /// 运行中
        /// </summary>
        public bool IsRuning { get; set; }
        /// <summary>
        /// 节点状态
        /// </summary>
        public int State { get; set; }
    }
}
