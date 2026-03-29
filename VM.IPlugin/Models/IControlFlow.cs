using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.IPlugin.Models
{
    public interface  IControlFlow
    {
        /// <summary>
        /// 告诉调度引擎：是否允许进入并执行我的子节点？
        /// </summary>
        /// <returns>True表示进入子节点，False表示跳过所有子节点</returns>
        bool ShouldExecuteChildren();
    }
}
