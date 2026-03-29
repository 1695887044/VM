using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Attritubess
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OutputPortAttribute : Attribute
    {
        public string PortName { get; }
        public OutputPortAttribute(string portName) { PortName = portName; }
    }

    // 2. 定义输入端口特性
    [AttributeUsage(AttributeTargets.Property)]
    public class InputPortAttribute : Attribute
    {
        public string PortName { get; }
        public InputPortAttribute(string portName) { PortName = portName; }
    }
}
