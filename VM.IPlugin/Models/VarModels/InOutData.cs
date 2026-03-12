using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.IPlugin.Models.VarModels
{
    // 输出端口 (只允许本插件自己修改值)
    public class OutputPort<T> : DataPort<T>
    {
        // 本插件运算完毕后调用
        public void UpdateValue(T newValue)
        {
            this.Value = newValue;
        }
    }

    // 输入端口 (只允许从外部拉取值，内部绝对只读)
    public class InputPort<T> : DataPort<T>
    {
        // 隐藏基类的 set 方法，下游无法再写 Input.Value = xxx;
        public new T Value
        {
            get
            {
                // 如果连了线，返回上游的值；如果没连线，返回自己可能填写的默认值
                return LinkedPort != null ? LinkedPort.Value : base.Value;
            }
        }
    }
}
