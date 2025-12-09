using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace Plugin.Blob.ViewModels
{
    internal class BlobViewModel : ModuleViewModelBase
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
