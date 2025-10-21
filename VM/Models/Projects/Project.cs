using VM.Start.Models.Projects.Nodes;

namespace VM.Start.Models.Projects
{
    public class Project:ModelBase
    {
        private ProjectInfo _info;

        public ProjectInfo Info
        {
            get { return _info != null ? _info : _info = new ProjectInfo(); }
            set { _info = value; }
        }

        private INode  processList;

        public INode ProcessList
        {
            get { return processList != null ? processList : processList = new ActionNode(); }
            set { processList = value;  RaisePropertyChanged(); }
        }



    }
}
