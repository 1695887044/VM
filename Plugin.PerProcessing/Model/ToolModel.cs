namespace Plugin.PerProcessing.Model
{
    public class ToolModel:BindableBase
    {
        private bool isEnabled=true;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; RaisePropertyChanged(); }
        }


        private string displayString;

        public string DisplayString
        {
            get { return displayString; }
            set { displayString = value; RaisePropertyChanged(); }
        }
        private string note;

        public string Note
        {
            get { return note; }
            set { note = value; RaisePropertyChanged(); }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }
        public string ToolName { get; set; }
    }
}
