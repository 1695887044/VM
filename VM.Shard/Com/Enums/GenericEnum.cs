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
    public enum DialogResult
    {
        Cancel = 0,     
        Close = 1,     
        OK = 10,   
        Yes = 20,   
        No = 30,   
        Save = 40,
        SaveAll = 41,
        DontSave = 42,
        Retry = 50,
        Ignore = 60,
        Abort = 70
    }
    public enum eProjectAutoRunMode
    {
        主动执行 = 0,
        调用执行 = 1,
    }
    public enum eProjectType
    {
        /// <summary>
        /// 流程
        /// </summary>
        Process,
        /// <summary>
        /// 方法
        /// </summary>
        Method,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,
    }
}
