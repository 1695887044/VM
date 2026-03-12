using Plugin.Blob.Views;
using VM.IPlugin;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;

namespace Plugin.Blob.ViewModels
{
    [Serializable]
    [PluginInfo(
    DisplayName = "图像处理",
    PluginName = "Blob",
    View = typeof(BlobView),
    ViewModel = typeof(BlobViewModel),
    Category = "常用工具"
)]
    public class BlobViewModel : ModuleViewModelBase
    {

        protected override bool Execute()
        {
            return true;
        }

    }
}
