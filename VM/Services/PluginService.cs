using VM.IPlugin;
using VM.IPlugin.ModuleEvent;

namespace VM.Start.Services
{
    public class PluginService
    {
        private readonly PrismProvider prism;
        /// <summary>
        /// 模块插件字典
        /// </summary>
        public static  Dictionary<string, IPlutionInfo> PluginDic_Module = new();

        /// <summary>
        /// 相机插件字典
        /// </summary>
        public static Dictionary<string, IPlutionInfo> PluginDic_Camera = new();
        /// <summary>
        /// 激光插件字典
        /// </summary>
        public static Dictionary<string, IPlutionInfo> PluginDic_Laser = new();

        /// <summary>
        /// 轴卡插件字典
        /// </summary>
        public static Dictionary<string, IPlutionInfo> PluginDic_Motion = new();


        public PluginService(PrismProvider prism)
        {
            this.prism = prism;
        }
        public  void InitPlugin()
        {
            prism.EventAggregator.GetEvent<PluginEvent>().Subscribe((s) => {
               // s.ViewModelType = prism.Container.Resolve<ModuleViewModelBase>("ViewName");
                if (s.Code != 200) return;
                if (s.Category == "相机")
                {
                    PluginDic_Camera.Add(s.PluginName, s);
                }
                else if (s.Category == "激光")
                {
                    PluginDic_Laser.Add(s.PluginName, s);
                }
                else if (s.Category == "运动控制")
                {
                    PluginDic_Motion.Add(s.PluginName, s);
                }
                else
                {
                    PluginDic_Module.Add(s.PluginName, s);
                }

            });
        }
    }
}
