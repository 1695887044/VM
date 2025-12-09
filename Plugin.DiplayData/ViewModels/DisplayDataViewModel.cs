using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace Plugin.DiplayData.ViewModels
{
    internal class DisplayDataViewModel : ModuleViewModelBase
    {
        public override bool Cancel()
        {
           return true;
        }

        public override bool Confirm()
        {
            return true;
        }

        public override bool Execute()
        {
            return true;
        }
        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
            throw new NotImplementedException();
        }
    }
}
