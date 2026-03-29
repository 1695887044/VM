using System.Collections.ObjectModel;
using VM.IPlugin.Models.VarModels;

namespace VM.Start.Models
{
    public class DataPortGroup:BindableBase,ICloneable
    {
        public int ModuleNo { get; set; }
        public string DisplayName { get; set; }
        public string Remarks { get; set; } = string.Empty;

        private ObservableCollection<IDataPort> _ports = new();

        public ObservableCollection<IDataPort> Ports
        {
            get { return _ports; }
            set { _ports = value; RaisePropertyChanged(); }
        }

        public object Clone()
        {
            return new DataPortGroup()
            {
                DisplayName = this.DisplayName,
                ModuleNo = this.ModuleNo,
                Remarks = this.Remarks,
                Ports = new ObservableCollection<IDataPort>()
            };
               
        }
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}
