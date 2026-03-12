
using System.Runtime.CompilerServices;

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
        void LogDebug(string message, bool IsDebug = false,[CallerMemberName] string memberName = "",
                  [CallerFilePath] string filePath = "",
                  [CallerLineNumber] int lineNumber = 0);
        void LogInfo(string message, bool IsDebug = false, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string filePath = "",
                  [CallerLineNumber] int lineNumber = 0);
        void LogWarn(string message, bool IsDebug = false, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string filePath = "",
                  [CallerLineNumber] int lineNumber = 0);
        void LogError(string message, bool IsDebug = false, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string filePath = "",
                  [CallerLineNumber] int lineNumber = 0);
        void LogFatal(string message, bool IsDebug = false, [CallerMemberName] string memberName = "",
                  [CallerFilePath] string filePath = "",
                  [CallerLineNumber] int lineNumber = 0);

        void ShowLog();
    }
}
