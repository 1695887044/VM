using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Com.Enums
{
    public class GenericEnum
    {
    }
    public enum eMsgType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 消息
        /// </summary>
        Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warn,
        /// <summary>
        /// 报错(不置位报警标志，设备可以继续运行)
        /// </summary>
        Error,
        /// <summary>
        /// 报警(置位报警标志，设备不能继续运行)
        /// </summary>
        Alarm,
    }
}
