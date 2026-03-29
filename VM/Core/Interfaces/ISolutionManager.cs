using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Start.Models.Nodes;

namespace VM.Start.Core.Interfaces
{
    public interface ISolutionManager
    {
        /// <summary>
        /// 当前正在运行的解决方案根节点
        /// </summary>
        SolutionNode CurrentSolution { get; }

        /// <summary>
        /// 当前用户在界面上（TreeView）鼠标选中的那个节点
        /// (极其重要：右侧的属性面板全靠它来联动展示参数)
        /// </summary>
        INode CurrentSelectedNode { get; set; }

        /// <summary>
        /// 脏标志：解决方案是否被修改过且未保存？(用于在关闭软件时弹窗提示“是否保存”)
        /// </summary>
        bool IsDirty { get; }



        /// <summary>
        /// 新建解决方案
        /// </summary>
        void CreateSolution();

        /// <summary>
        /// 异步加载解决方案 (因为读取几十个视觉流程的 Json 可能会卡顿，必须用异步)
        /// </summary>
        Task<bool> LoadAsync(string filePath);

        /// <summary>
        /// 保存当前解决方案
        /// </summary>
        Task<bool> SaveAsync();

        /// <summary>
        /// 另存为
        /// </summary>
        Task<bool> SaveAsAsync(string newFilePath);

        /// <summary>
        /// 关闭当前解决方案 (释放所有内存、相机句柄等)
        /// </summary>
        void Close();

        /// <summary>
        /// 在指定的父节点下添加新节点
        /// </summary>
        void AddNode<T>(ContainerNodeBase<T> parentNode, INode newNode);

        /// <summary>
        /// 移除节点 (并自动处理该节点下的所有子节点清理)
        /// </summary>
        void RemoveNode<T>(ContainerNodeBase<T> nodeToRemove);

        /// <summary>
        /// 移动节点 (用于支持 WPF 树形菜单的拖拽 - Drag & Drop)
        /// </summary>
        void MoveNode<T>(ContainerNodeBase<T> sourceNode, INode targetParentNode);

        /// <summary>
        /// 克隆/复制节点 (比如用户配好了一个贼复杂的“相机找边流程”，想直接复制一份)
        /// </summary>
        INode CloneNode(INode nodeToClone);


        // ==========================================
        // 4. 引擎调度交互 (Execution Bridge)
        // ==========================================
        // 注意：Manager 本身不执行视觉算法，它只负责告诉引擎“去跑哪个节点”

        /// <summary>
        /// 运行指定的流程或方法
        /// </summary>
        Task RunNodeAsync(INode node);


        // ==========================================
        // 5. 全局事件通知 (Global Events)
        // ==========================================
        // 供 UI 层或其他插件订阅，实现界面联动刷新

        /// <summary>
        /// 解决方案加载完成事件
        /// </summary>
        event EventHandler SolutionLoaded;

        /// <summary>
        /// 选中节点发生变化事件 
        /// </summary>
        event EventHandler<INode> SelectedNodeChanged;

    }
}
