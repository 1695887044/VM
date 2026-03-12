using System.Collections.ObjectModel;
using VM.IPlugin;
using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects.Nodes
{
    /// <summary>
    /// 单个执行模块
    /// </summary>
    public class ProcessNode : ModelBase, IProcessNode
    {
        public int SortId { get; set; }
        public ModuleViewModelBase ViewModel { get; set; }
        public string CostTime { get; set; } = "0";
        public bool IsRuning { get; set; }
        public int State { get; set; }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;RaisePropertyChanged(); }
        }

        public IModuleViewBase View { get; init; }
        public eProjectType NodeType { get; set; }

        public INode Parent { get; set; }

        public ObservableCollection<INode> Children { get; set; } = new();

        public string IconText { get; set; }

        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; RaisePropertyChanged(); }
        }


        public string Tag { get; set; } = string.Empty;


    }
}
