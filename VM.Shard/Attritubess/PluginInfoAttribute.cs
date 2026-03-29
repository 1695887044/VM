using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Attritubess
{
   
    public class PluginInfoAttribute:Attribute
    {
        const string DefaultIcon = Shard.Resources.Icons.Icon_Info1;
        /// <summary>
        /// 展示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get; set; }
        /// <summary>
        /// 分组
        /// </summary>
        public string Category { get; set; }

        public Type View { get; set; }


        public Type ViewModel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; } = "注释";
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; } = DefaultIcon;
        /// <summary>
        /// 版本
        /// </summary>
        public Double Version { get; set; } = 1.0;
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

    }
}
