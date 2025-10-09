using VM.Shard.Com.Enums;

namespace VM.Start.Models.Projects
{
    public class ProjectInfo:ModelBase
    {

        public int ProjectId { get; set; }

        public int FolderId { get; set; }

        public string  ProjectName { get; set; }

        public string Remarks { get; set; }

        public bool IsEncypt { get; set; }

        public eProjectAutoRunMode ProjectRunMode {  get; set; }

        public eProjectType ProjectType { get; set; }

        private bool _refreshUI;

        public bool  RefreshUI 
        {
            get { return _refreshUI; }
            set { _refreshUI = value;  RaisePropertyChanged(); }
        }

    }
}
