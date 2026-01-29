using System.Windows;
using System.Windows.Controls;

namespace VM.IPlugin
{
    public interface IModuleViewBase
    {
       bool IsLoaded { get; }

       void ShowView();

       void CancelView();

        void InitView(ModuleViewModelBase model=null);


    }
}
