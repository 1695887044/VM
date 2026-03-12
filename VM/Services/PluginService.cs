using System.IO;
using System.Reflection;
using VM.IPlugin;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;

namespace VM.Start.Services
{
    public record PluginInfo : IPlutionInfo
    {
        public string PluginName { get; set; }

        public string PluginIcon { get; set; }

        public string Category { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public Type ViewType { get; set; }

        public Type ViewModelType { get; set; }

        public int Code { get; set; }
    }
    public class PluginService
    {
        private readonly PrismProvider prism;

        /// <summary>
        /// 模块插件字典
        /// </summary>
        public static Dictionary<string, IPlutionInfo> PluginDic_Module = new();

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

        public void InitPlugin()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string PlugInsDir;
            PlugInsDir = Path.Combine(baseDir, "Modules");
        #if DEBUG
            string relativePath = @"..\..\..\..\Modules";
            PlugInsDir = Path.GetFullPath(Path.Combine(baseDir, relativePath));
        #endif
            if (!Directory.Exists(PlugInsDir))
                return; //判断是否存在
            foreach (var dllPath in Directory.GetFiles(PlugInsDir))
            {
                //检查是不是dll
                FileInfo fi = new FileInfo(dllPath);
                if (!fi.Name.StartsWith("Plugin") || !fi.Name.EndsWith(".dll"))
                    continue;
                Assembly assemPlugIn = Assembly.LoadFile(dllPath);
                 
         // 该方法会占用文件 但可以调试
                var types = assemPlugIn
                    .GetTypes()
                    .Where(t =>( !t.IsAbstract && typeof(ModuleViewModelBase).IsAssignableFrom(t)));
                foreach (var type in types)
                {
                    var att = type.GetCustomAttribute<PluginInfoAttribute>();
                    if (att == null)
                    {
                        continue;
                    }
                    PluginInfo _plugin = new PluginInfo()
                    {
                         Category = att.Category,
                        Description = att.Description,
                        DisplayName = att.DisplayName,
                        PluginIcon = att.Icon,
                        PluginName = att.PluginName,
                        ViewModelType = att.ViewModel,
                        ViewType = att.View
                    };
                    //IOC注册
                    prism.Container.Register(_plugin.ViewType);
                    prism.Container.Register(_plugin.ViewModelType);
                    if (att.Category == "相机")
                    {
                        PluginDic_Camera.Add(att.PluginName, _plugin);
                    }
                    else if (att.Category == "激光")
                    {
                        PluginDic_Laser.Add(att.PluginName, _plugin);
                    }
                    else if (att.Category == "运动控制")
                    {
                        PluginDic_Motion.Add(att.PluginName, _plugin);
                    }
                    else
                    {
                        PluginDic_Module.Add(att.PluginName, _plugin);
                    }
                }
            }
        }
    }
}
