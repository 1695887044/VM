using System.Collections.ObjectModel;
using System.Windows;

namespace VM.Start.ViewModels
{
    public class ProcessViewModel:BindableBase
    {
        public ObservableCollection<string> Datas { get; set; } = new();
        public DelegateCommand<DragEventArgs> DropCommand { get; set; }

        public ProcessViewModel()
        {
           Datas = new ObservableCollection<string> { "saasa", "sassassa" };
            DropCommand = new DelegateCommand<DragEventArgs>( _dropExecute);
        }

        private void _dropExecute(DragEventArgs  args)
        {
            if (args.AllowedEffects != DragDropEffects.Copy) return;
            object node = args.Data.GetData("VM.Start.Models.Projects.Nodes.MethodNode");
        }
    }
}
