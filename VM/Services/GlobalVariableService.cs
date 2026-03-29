using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Shell;
using VM.IPlugin.Models.VarModels;
using VM.Start.Core.Interfaces;
using VM.Start.Models;
using VM.Start.Models.Nodes;

namespace VM.Start.Services
{
    /// <summary>
    /// 全局变量管理服务  变量显示  多个模块 多种类型
    /// </summary>
    public class GlobalVariableService:BindableBase
    {
        private ObservableCollection<DataPortGroup> _globalDataPort = new();

        public ObservableCollection<DataPortGroup> GlobalDataPort
        {
            get { return _globalDataPort; }
            set { _globalDataPort = value; RaisePropertyChanged(); }
        }


        private  ObservableCollection<DataPortGroup> _displayVarList=new();
        private readonly ISolutionManager solutionManager;

        public ObservableCollection<DataPortGroup> DisplayVarList
        {
            get { return _displayVarList; }
            set { _displayVarList = value; RaisePropertyChanged(); }
        }
        public GlobalVariableService(ISolutionManager solutionManager)
        {
            this.solutionManager = solutionManager;
            GlobalDataPort.Add(InitializeDefaultGlobalVars());
        }
        /// <summary>
        /// 根据传入的筛选条件 刷新显示的变量列表 刷新🔗地址
        /// </summary>
        /// <param name="Fiter"></param>
        public void RefreshDisplayVarList(Func<IDataPort, bool> Fiter,ToolNodeBase node)
        {
            DisplayVarList.Clear();
            if (solutionManager.CurrentSelectedNode is IContainerNode<ToolNodeBase> container)
            {
                foreach (var item in container.Children)
                {
                    if (item.SortId >= node.SortId ||
                        item.ViewModel.ModuleData.VarOut == null ||
                        item.ViewModel.ModuleData.VarOut.Count == 0) continue;
                    DataPortGroup tempModuleList = new DataPortGroup();
                    foreach (var data in item.ViewModel.ModuleData.VarOut.Where(Fiter))
                    {
                        data.SourcePath = $"{item.Name}{item.SortId - 1}";
                        tempModuleList.Ports.Add(data);
                    }
                    if (tempModuleList.Ports.Count != 0)
                    {
                        tempModuleList.DisplayName = $"{item.Name}-{item.SortId - 1}";
                        DisplayVarList.Add(tempModuleList);
                    }
                }
            }
            //查找全局变量列表
            foreach (DataPortGroup list in GlobalDataPort)
            {
                DataPortGroup tempList = list.Clone() as DataPortGroup;
                if (tempList == null) continue;
                tempList.Ports.Clear();
                foreach (var item in list.Ports.Where(Fiter))
                {
                    tempList.Ports.Add(item);
                }
                if(tempList.Ports.Count != 0)
                {
                    DisplayVarList.Add(tempList);
                }
            }
        }

        private DataPortGroup InitializeDefaultGlobalVars(int len =20)
        {
            // 1. 创建一个全局变量分组
            var systemGlobalGroup = new DataPortGroup
            {
                ModuleNo = 0, // 全局变量的 ModuleNo 通常设为 0 或特殊标记
                DisplayName = "全局变量 (System)",
                Remarks = "默认提供"
            };
            
            systemGlobalGroup.Ports.Add(CreateGlobalPort<List<bool>>($"布尔数组", $"全局变量", new ()));
            systemGlobalGroup.Ports.Add(CreateGlobalPort<List<int>>($"整数数组", $"全局变量", new()));
            systemGlobalGroup.Ports.Add(CreateGlobalPort<List<float>>($"浮点数组", $"全局变量", new ()));
            systemGlobalGroup.Ports.Add(CreateGlobalPort<List<string>>($"字符数组", $"全局变量", new ()));

            return systemGlobalGroup;
        }
        private IDataPort CreateGlobalPort<T>(string name, string uiName, T defaultValue)
        {
            var port = new DataPort<T>();

            port.SourcePath = $"Global.{name}";
            port.Name = name ;
            port.Value = defaultValue;
            return port;
        }
        public void AddUserGlobalVariable(string uiName, Type dataType, object initialValue)
        {
            // 1. 查找或创建专门存放“用户自定义”的变量组
            var userGroup = GlobalDataPort.FirstOrDefault(g => g.DisplayName == "自定义变量 (User)");
            if (userGroup == null)
            {
                userGroup = new DataPortGroup
                {
                    ModuleNo = 999, // 设个特殊的编号
                    DisplayName = "自定义变量",
                    Remarks = "用户手动创建的全局变量"
                };
                GlobalDataPort.Add(userGroup);
            }

            // 2. 查重防呆
            if (userGroup.Ports.Any(p => p.Name == uiName))
            {
                throw new Exception($"已存在名为 {uiName} 的全局变量！");
            }

            // 3. 动态工厂：根据用户选择的类型，实例化泛型端口！
            IDataPort newPort = null;
            string fullPath = $"Global.{uiName}"; // 强制规范全局变量的路径名

            if (dataType == typeof(int))
            {
                int val = Convert.ToInt32(initialValue ?? 0);
                newPort = new DataPort<int>(uiName, uiName, val) { SourcePath = fullPath };
            }
            else if (dataType == typeof(double))
            {
                double val = Convert.ToDouble(initialValue ?? 0.0);
                newPort = new DataPort<double>(uiName, uiName, val) { SourcePath = fullPath };
            }
            else if (dataType == typeof(string))
            {
                string val = Convert.ToString(initialValue ?? "");
                newPort = new DataPort<string>(uiName, uiName, val) { SourcePath = fullPath };
            }
            else if (dataType == typeof(bool))
            {
                bool val = Convert.ToBoolean(initialValue ?? false);
                newPort = new DataPort<bool>(uiName, uiName, val) { SourcePath = fullPath };
            }
            // ... 未来可以在这里扩展 Point2D 等复杂类型

            // 4. 将新变量加入集合，WPF 界面瞬间自动刷新出新的一行！
            if (newPort != null)
            {
                userGroup.Ports.Add(newPort);
            }
        }
    }
}
