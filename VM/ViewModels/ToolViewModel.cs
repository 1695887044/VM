using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using VM.Start.Core.Interfaces;
using VM.Start.Models.Nodes;
using VM.Start.Services;

namespace VM.Start.ViewModels
{
   public  class ToolViewModel:BindableBase
    {
        #region Prop
        public DelegateCommand<string> ProjectCommand { get; init; }
        public DelegateCommand<MouseEventArgs> ToolBarMouseDownCommand { get; init; }

        /// <summary>
        /// 底端工具栏数据源
        /// </summary>
        public List<INode> ToolBarSource { get; set; } = new();
        /// <summary>
        /// 流程树变化命令
        /// </summary>
        public DelegateCommand<INode> TreeViewChangeCommand { get; init; }
        public ISolutionManager SolutionManager { get; init; }
        public DelegateCommand<string> TreeViewContextMenuCommand {  get; init; }
        private ObservableCollection<string> _moduleList;

        public ObservableCollection<string> ModuleList
        {
            get { return _moduleList; }
            set { _moduleList = value; RaisePropertyChanged(); }
        }


        #endregion
        #region Ctor

        public ToolViewModel(PrismProvider prismProvider,ISolutionManager solutionManager)
        {
            ModuleList= new ObservableCollection<string>();
            ProjectCommand = new DelegateCommand<string>(OnProjectExecute);
            TreeViewChangeCommand = new DelegateCommand<INode>(treeViewChanged);
            TreeViewContextMenuCommand = new DelegateCommand<string>(TreeViewContextMenuExecute);
            SolutionManager = solutionManager;
            loadToolBarSource();
        }
        #endregion

        #region 底部工作栏事件
        /// <summary>
        /// 插件信息加载到工作栏
        /// </summary>
        private void loadToolBarSource()
        {
            PluginService.PluginDic_Module.GroupBy(t => t.Value.Category).ToList().ForEach(g =>
            {
                FolderNode folder = new FolderNode() { Name = g.Key };
                g.ToList().ForEach(p =>
                {
                    ToolNode method = new ToolNode() { Name = p.Value.DisplayName, Remark = p.Value.Description, IconText = p.Value.PluginIcon, Tag = p.Value.PluginName };
                    folder.Children.Add(method);
                });
                ToolBarSource.Add(folder);
            });

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
            switch (obj)
            {
                case "Create_A":
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
