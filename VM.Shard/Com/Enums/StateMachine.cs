
namespace VM.Shard.Com.Enums
{
    public class StateMachine
    {
        public enum MachineState
        {
            Idle,           // 空闲
            Initializing,   // 初始化
            Ready,          // 准备就绪
            Running,        // 运行中
            Paused,         // 暂停
            Stopping,       // 停止中
            Error,          // 错误
            Maintenance     // 维护
        }

        /// <summary>
            /// 状态转换事件枚举
            /// </summary>
        public enum StateEvent
        {
            Initialize,     // 初始化
            Start,          // 启动
            Pause,          // 暂停
            Resume,         // 恢复
            Stop,           // 停止
            Reset,          // 重置
            Error,          // 错误
            Maintain,       // 维护
            Complete        // 完成
        }
    
    }
}
