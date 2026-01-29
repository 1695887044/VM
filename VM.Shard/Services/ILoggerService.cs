
namespace VM.Shard.Services
{
    public enum Log_Level
    {
        Debug,
        Info,
        Warn, 
        Error,
        Fatal
    }
    public interface ILoggerService
    {
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
        void LogFatal(string message);

        void ShowLog();
    }
}
