using Plugin.PerProcessing.Common;

namespace Plugin.PerProcessing.Model
{
    public interface IToolData
    {
        public bool IsEnabled { get; set; }
        public string Name { get; set; }

        public string Note { get; set; }
        public eOperatorType SubType { get; set; }


    }
    public abstract class ToolDataBase<T>:BindableBase, IToolData where T:IPerParam, new()
    {
        public eOperatorType SubType { get; set; }
        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; RaisePropertyChanged(); }
        }
        private bool isEnabled = true;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; RaisePropertyChanged(); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }


        private T toolParamer;

        public T ToolParamer
        {
            get { return toolParamer; }
            set { toolParamer = value; RaisePropertyChanged();}
        }
    }

}
