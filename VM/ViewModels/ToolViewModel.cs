


using VM.Start.Models.Projects;
using VM.Start.Models.Projects.Nodes;

namespace VM.Start.ViewModels
{
   public  class ToolViewModel:BindableBase
    {
        #region Prop
        public DelegateCommand<string> ProjectCommand { get; init; }
        public DelegateCommand<INode> TreeViewChangeCommand { get; init; }

        public DelegateCommand<string> TreeViewContextMenuCommand {  get; init; }
        private Object selectProcess;

        public Object SelectProcess
        {
            get { return selectProcess; }
            set { selectProcess = value; RaisePropertyChanged(); }
        }


        private Project currentProject;

        public Project CurrentProject
        {
            get { return currentProject; }
            set { currentProject = value; RaisePropertyChanged(); }
        }
        #endregion
        #region Ctor
        public ToolViewModel()
        {
            ProjectCommand = new DelegateCommand<string>(OnProjectExecute);
            TreeViewChangeCommand = new DelegateCommand<INode>(treeViewChanged);
            TreeViewContextMenuCommand = new DelegateCommand<string>(TreeViewContextMenuExecute);
        }
        #endregion
        private void TreeViewContextMenuExecute(string obj)
        {
            
        }

        private void treeViewChanged(INode obj)
        {
            
        }

        private void OnProjectExecute(string obj)
        {
            var sas = SelectProcess;
            switch (obj)
            {
                case "Create_A":
                    Project project = new Project();
                    CurrentProject = project;
                    project.ProcessList.Children.Add(new ActionNode());
                    project.ProcessList.Children[0].Children.Add(new MethodNode() {  Name="子流程A"});
                    project.ProcessList.Children[0].Children[0].Children.Add(new FolderNode() { Name = "子流程Aaa" });
                    project.ProcessList.Children.Add(new ActionNode());
                    project.ProcessList.Children.Add(new ActionNode());
                    project.ProcessList.Children.Add(new ActionNode());
                    project.ProcessList.Children.Add(new ActionNode());
                    project.ProcessList.Children.Add(new ActionNode());
                    break;
                case "Delete_A":
                   
                    break;
                case "Create_M":
                    break;
                case "Set_M":
                    break;
                default:
                    break;
                }
            }
    }
}
