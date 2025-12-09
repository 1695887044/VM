

using System.Collections.ObjectModel;
using VM.IPlugin.Models.VarModels;

namespace VM.Start.Models
{
    public class ModuleVarList:BindableBase,ICloneable
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ModuleNo { get; set; }
        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; } = string.Empty;

        private ObservableCollection<IVarValue> varModels = new();

        public ObservableCollection<IVarValue> VarModels
        {
            get { return varModels; }
            set { varModels = value; RaisePropertyChanged(); }
        }

        public object Clone()
        {
            return new ModuleVarList()
            {
                DisplayName = this.DisplayName,
                ModuleNo = this.ModuleNo,
                Remarks = this.Remarks,
                VarModels = new ObservableCollection<IVarValue>()
            };
               
    }
    }
}
