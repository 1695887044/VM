using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Shard.Services;
using VM.Start.Core.Interfaces;
using VM.Start.Models;
using VM.Start.Models.Nodes;

namespace VM.Start.Services
{
    /// <summary>
    /// 所有类的新建都经过这里
    /// </summary>
    public class SolutionService : BindableBase, ISolutionManager
    {
        private readonly IMessageService _messageService;

        private SolutionNode _currentSolution;
        private bool _isDirty;

        public List<SolutionNode> SolutionNodes { get; } = new();

        public SolutionService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public SolutionNode CurrentSolution
        {
            get { return _currentSolution; }
            private set
            {
                _currentSolution = value;
                RaisePropertyChanged();
            }
        }

        public bool IsDirty => _isDirty;

        private INode _currentSelectedNode;

        public INode CurrentSelectedNode
        {
            get { return _currentSelectedNode; }
            set
            {
                if (SetProperty(ref _currentSelectedNode, value))
                {
                    SelectedNodeChanged?.Invoke(this, CurrentSelectedNode);
                }
            }
        }

        // --- 3. 事件定义 ---
        public event EventHandler SolutionLoaded;
        public event EventHandler<INode> SelectedNodeChanged;
        public event EventHandler NodeStructureChanged;

        // --- 4. 生命周期管理 ---
        public void CreateSolution()
        {
            // 如果当前有未保存的工程，先弹窗警告
            if (_currentSolution != null && _isDirty)
            {
                _messageService.ShowMessage(
                    "创建新的解决方案会覆盖掉当前未保存的更改，确认继续？",
                    true
                );
            }
            //创建
            CreateNew();
        }

        void CreateNew()
        {
            var solutionNode = new SolutionNode();

            //添加默认属性
            solutionNode.Children.Add(new ProcessNode() { Name = "初始化" });
            solutionNode.Children.Add(new ProcessNode() { Name = "回原" });
            solutionNode.Children.Add(new ProcessNode() { Name = "主流程" });

            SolutionNodes.Add(solutionNode);
            // 重置状态
            _isDirty = true; // 刚建好也是未保存状态
            CurrentSolution = solutionNode;
            CurrentSelectedNode = null;
            // 广播事件
            SolutionLoaded?.Invoke(this, EventArgs.Empty);
            NodeStructureChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Close()
        {
            if (_currentSolution != null && _isDirty)
            {
                _messageService.ShowMessage("当前解决方案未保存，是否在关闭前保存？", true);
                SaveAsync().Wait(); // 实际开发中最好确保是在异步上下文中 await
            }

            _currentSolution = null;
            CurrentSelectedNode = null;
            _isDirty = false;

            SolutionLoaded?.Invoke(this, EventArgs.Empty);
            NodeStructureChanged?.Invoke(this, EventArgs.Empty);
        }

        // --- 5. 节点操作管理 (必定触发脏标志和 UI 刷新) ---
        public void AddNode<T>(ContainerNodeBase<T> parentNode, T newNode)
        {
            if (parentNode == null || newNode == null)
                return;
            parentNode.Children.Add(newNode);
            SetDirtyAndNotify();
        }

        public void RemoveNode<T>(ContainerNodeBase<T> nodeToRemove, T Node)
        {
            if (nodeToRemove == null || nodeToRemove.Parent == null)
                return;
            nodeToRemove.Children.Remove(Node);
            SetDirtyAndNotify();
        }

        public void MoveNode<T>(ContainerNodeBase<T> sourceNode, INode targetParentNode)
        {
            if (sourceNode == null || targetParentNode == null)
                return;
            SetDirtyAndNotify();
        }

        public INode CloneNode(INode nodeToClone)
        {
            // 工业界最稳妥的深拷贝方案是：先序列化成 JSON，再反序列化成新对象
            // 这样可以彻底切断内存引用！
            throw new NotImplementedException("等待 JSON 序列化功能接入...");
        }

        // --- 6. 核心动作 ---
        public async Task RunNodeAsync(INode node)
        {
            if (node == null)
                return;

            // 假设 INode 定义了 Execute() 或 RunAsync()
            // await node.RunAsync();
            throw new NotImplementedException("等待执行引擎接口打通...");
        }

        public async Task<bool> SaveAsync()
        {
            if (_currentSolution == null || string.IsNullOrEmpty(_currentSolution.SavePath))
                return false;

            return await SaveAsAsync(_currentSolution.SavePath);
        }

        public async Task<bool> SaveAsAsync(string newFilePath)
        {
            if (_currentSolution == null)
                return false;

            try
            {
                // TODO: 在这里使用 Newtonsoft.Json 将 _currentSolution 序列化并写入文件
                // string json = JsonConvert.SerializeObject(_currentSolution, Formatting.Indented);
                // await File.WriteAllTextAsync(newFilePath, json);

                _currentSolution.SavePath = newFilePath; // 更新当前路径
                _isDirty = false; // 保存成功，洗白！
                return true;
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage($"保存失败: {ex.Message}", false);
                return false;
            }
        }

        public Task<bool> LoadAsync(string filePath)
        {
            // TODO: 读取文件并反序列化
            throw new NotImplementedException("等待 JSON 反序列化功能接入...");
        }

        // --- 辅助方法 ---
        private void SetDirtyAndNotify()
        {
            _isDirty = true;
            NodeStructureChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddNode<T>(IContainerNode<T> parentNode, INode newNode)
        {
            throw new NotImplementedException();
        }

        public void RemoveNode<T>(IContainerNode<T> nodeToRemove)
        {
            throw new NotImplementedException();
        }

        public void AddNode<T>(ContainerNodeBase<T> parentNode, INode newNode)
        {
            throw new NotImplementedException();
        }

        public void RemoveNode<T>(ContainerNodeBase<T> nodeToRemove)
        {
            throw new NotImplementedException();
        }
    }
}
