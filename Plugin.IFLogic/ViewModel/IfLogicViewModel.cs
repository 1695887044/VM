using Plugin.IFLogic.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.IPlugin;
using VM.IPlugin.Models;
using VM.Shard.Attritubess;
using VM.Shard.Resources;

namespace Plugin.IFLogic.ViewModel
{
    [Serializable]
    [PluginInfo(
        DisplayName = "IF 条件",
        PluginName = "IfLogic",
        View = typeof(IfLogicView), // 记得在项目里建一个 IfLogicView.xaml
        ViewModel = typeof(IfLogicViewModel),
        Category = "逻辑控制",
        Icon =Icons.Icon_If
    )]
    public class IfLogicViewModel : ModuleViewModelBase,IControlFlow
    {
        public bool ShouldExecuteChildren()
        {
            return true;
        }

        protected override bool Execute()
        {
            return true;
        }
    }
}
