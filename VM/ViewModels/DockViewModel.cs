

using VM.Start.Views;

namespace VM.Start.ViewModels
{
   public  class DockViewModel:BindableBase
    {
        public LogView logView { get; set; } = new();
    }
}
