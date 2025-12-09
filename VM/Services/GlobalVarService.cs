using System.Collections.ObjectModel;
using VM.IPlugin.Models.VarModels;
using VM.Start.Models;

namespace VM.Start.Services
{
    /// <summary>
    /// 全局变量管理服务  变量显示  多个模块 多种类型
    /// </summary>
    public class GlobalVarService:BindableBase
    {

        public  List<ModuleVarList> GlobalVarList = new();

        private  ObservableCollection<ModuleVarList> _displayVarList=new();

        public ObservableCollection<ModuleVarList> DisplayVarList
        {
            get { return _displayVarList; }
            set { _displayVarList = value; RaisePropertyChanged(); }
        }

        public GlobalVarService()
        {
            ModuleVarList moduleVarList = new ModuleVarList();
            moduleVarList.DisplayName = "全局变量表1";
            moduleVarList.Remarks = "全局变量";
            moduleVarList.ModuleNo = 1;
            ObservableCollection<IVarValue> vars = new ObservableCollection<IVarValue>();
            vars.Add(new VarValue<int> { DataType = "int",LinkPath= "全局变量表1", Name = "测试数据1", Value = 110, Note = "整数类型" });
            vars.Add(new VarValue<int> { DataType = "int", LinkPath = "全局变量表1", Name = "测试数据2", Value = 180, Note = "整数类型" });
            vars.Add(new VarValue<string> { DataType = "string", LinkPath = "全局变量表1", Name = "测试数据11", Value = "测试字符串", Note = "字符串类型" });
            vars.Add(new VarValue<bool> { DataType = "bool", LinkPath = "全局变量表1", Name = "测试数据5", Value = false, Note = "布尔类型" });
            moduleVarList.VarModels = vars;
            GlobalVarList.Add(moduleVarList);
             moduleVarList = new ModuleVarList();
            moduleVarList.DisplayName = "全局变量表2";
            moduleVarList.Remarks = "全局变量";
            moduleVarList.ModuleNo = 2;
             vars = new ObservableCollection<IVarValue>();
            vars.Add(new VarValue<int> { DataType = "int", Name = "测试数据111", LinkPath = "全局变量表2", Value = 2220, Note = "整数类型" });
            vars.Add(new VarValue<int> { DataType = "int", Name = "测试数据112", LinkPath = "全局变量表2", Value = 3330, Note = "整数类型" });
            vars.Add(new VarValue<string> { DataType = "string", Name = "测试数据11", LinkPath = "全局变量表2", Value = "测试字符串", Note = "字符串类型" });
            vars.Add(new VarValue<bool> { DataType = "bool", Name = "测试数据15", LinkPath = "全局变量表2", Value = false, Note = "布尔类型" });
            moduleVarList.VarModels = vars;
            GlobalVarList.Add(moduleVarList);
        }
        /// <summary>
        /// 根据传入的筛选条件 刷新显示的变量列表
        /// </summary>
        /// <param name="Fiter"></param>
        public void RefreshDisplayVarList(Func<IVarValue, bool> Fiter)
        {
            DisplayVarList.Clear();
            foreach (ModuleVarList list in GlobalVarList)
            {
                ModuleVarList tempList = list.Clone() as ModuleVarList;
                if (tempList == null) continue;
                tempList.VarModels.Clear();
                foreach (var item in list.VarModels.Where(Fiter))
                {
                    tempList.VarModels.Add(item);
                }
                DisplayVarList.Add(tempList);
            }
        }
    }
}
