using System.Collections.ObjectModel;
using VM.IPlugin.Models.VarModels;
using VM.Start.Models.Projects.Nodes;

namespace VM.Start.Models.Projects
{
    public class Project:ModelBase
    {
        private ProjectInfo _info;

        public ProjectInfo Info
        {
            get { return _info ?? new ProjectInfo(); }
            set { _info = value; }
        }
        public INode Nodes { get; set; }=new FolderNode();
      

        public ObservableCollection<IProcessNode> DisplayProcessNodes {  get; set; } =new ObservableCollection<IProcessNode>();
       
    }
}
