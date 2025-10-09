

using System.IO;
using System.Windows;

namespace VM.Start.Models
{
    public class SystemInfo:ModelBase
    {
        private string _actTime;
        private string projectPath;

        public Timer DateTimer { get; init; }
       

        public string ProjectPath
        {
            get { return projectPath; }
            set { projectPath = value; }
        }

        public string ActTime
        {
            get { return _actTime; }
            set { _actTime = value; RaisePropertyChanged(); }
        }

        
        public SystemInfo()
        {
            DateTimer = new Timer((s) => { ActTime = DateTime.Now.ToString(); }, null, 0, 100);
            ProjectPath = AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
