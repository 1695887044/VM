using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   
    public class SuperDisplayAttribute:Attribute
    {
        const string Str1 = "默认分组";
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly { get; set; }
        /// <summary>
        /// 分组路径  "系统设置/通信参数"
        /// </summary>
        public string GroupPath { get; set; } = Str1;
        /// <summary>
        /// 序号
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 分组排序 ""
        /// </summary>
        public string GroupOrder { get; set; }
        /// <summary>
        /// 展示名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 提示
        /// </summary>

        public string Description { get; set; }
    }
}
