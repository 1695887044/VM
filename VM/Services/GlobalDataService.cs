using System.Collections.ObjectModel;
using System.Windows.Shell;
using VM.IPlugin.Models.VarModels;
using VM.Start.Models;
using VM.Start.Models.Projects.Nodes;

namespace VM.Start.Services
{
    /// <summary>
    /// 全局变量管理服务  变量显示  多个模块 多种类型
    /// </summary>
    public class GlobalDataService:BindableBase
    {

        public  List<ModuleVarList> GlobalVarList = new();

        private  ObservableCollection<ModuleVarList> _displayVarList=new();

        public ObservableCollection<ModuleVarList> DisplayVarList
        {
            get { return _displayVarList; }
            set { _displayVarList = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 根据传入的筛选条件 刷新显示的变量列表 刷新🔗地址
        /// </summary>
        /// <param name="Fiter"></param>
        public void RefreshDisplayVarList(Func<IDataPort, bool> Fiter,IProcessNode node =null)
        {
            DisplayVarList.Clear();
       
            //遍历当前工程模块的变量输出
            foreach (var item in SysConfigProvider.Ins.CurrentProject.DisplayProcessNodes)
            {
                if (item.SortId >=  node.SortId ||
                    item.ViewModel.ModuleData.VarOut == null ||
                    item.ViewModel.ModuleData.VarOut.Count == 0) continue;
                ModuleVarList tempModuleList = new ModuleVarList();
                foreach (var data in item.ViewModel.ModuleData.VarOut.Where(Fiter))
                {
                    data.DisplayName = $"{item.Name}{item.SortId - 1}";
                    tempModuleList.VarModels.Add(data);
                }
                if (tempModuleList.VarModels.Count !=0){
                    tempModuleList.DisplayName = $"{item.Name}-{item.SortId - 1}";
                    DisplayVarList.Add(tempModuleList);
                }
               
            }
            //查找全局变量列表
            foreach (ModuleVarList list in GlobalVarList)
            {
                ModuleVarList tempList = list.Clone() as ModuleVarList;
                if (tempList == null) continue;
                tempList.VarModels.Clear();
                foreach (var item in list.VarModels.Where(Fiter))
                {
                    tempList.VarModels.Add(item);
                }
                if(tempList.VarModels.Count != 0)
                {
                    DisplayVarList.Add(tempList);
                }
            }
        }
    }
}
