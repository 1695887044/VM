using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using NLog;
using VM.Shard.Services;
using VM.Start.Models.Logs;

namespace VM.Start.Services
{
    public class LogService : BindableBase, ILoggerService
    {
        private readonly SynchronizationContext? _synchronizationContext;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public const int MaxLogCount = 20000;

        private Log_Level _CrtType = Log_Level.Info;

        public Log_Level CrtType
        {
            get { return _CrtType; }
            set
            {
                _CrtType = value;
                RaisePropertyChanged();
                OnRefreshLog();
            }
        }

        private ObservableCollection<LogModel> _displayLogs = new();

        public ObservableCollection<LogModel> DisplayLogs
        {
            get { return _displayLogs; }
            set
            {
                _displayLogs = value;
                RaisePropertyChanged();
            }
        }
        public Dictionary<Log_Level, List<LogModel>> LogSource { get; private set; } = new();

        public LogService()
        {
            _synchronizationContext = SynchronizationContext.Current;
        }

        public void LogDebug(
            string message, bool IsDebug = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) => AssertPlus(Log_Level.Debug, IsDebug ? $"[调用者:{memberName}] [文件:{filePath}:{lineNumber}] | {message}" : message);

        public void LogError(
            string message, bool IsDebug = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) => AssertPlus(Log_Level.Error, IsDebug ? $"[调用者:{memberName}] [文件:{filePath}:{lineNumber}] | {message}" : message);

        public void LogFatal(
            string message, bool IsDebug = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) => AssertPlus(Log_Level.Fatal, IsDebug ? $"[调用者:{memberName}] [文件:{filePath}:{lineNumber}] | {message}" : message);

        public void LogInfo(
            string message, bool IsDebug = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) => AssertPlus(Log_Level.Info, IsDebug ? $"[调用者:{memberName}] [文件:{filePath}:{lineNumber}] | {message}" : message);

        public void LogWarn(
            string message, bool IsDebug = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0
        ) => AssertPlus(Log_Level.Warn, IsDebug ? $"[调用者:{memberName}] [文件:{filePath}:{lineNumber}] | {message}" : message);

        public void ShowLog() { }

        private void AssertPlus(Log_Level level, string Message)
        {
            if (!LogSource.ContainsKey(level))
            {
                LogSource[level] = new List<LogModel>();
            }
            if (LogSource[level].Count > MaxLogCount)
            {
                LogSource[level].RemoveAt(MaxLogCount - 1);
            }
            switch (level)
            {
                case Log_Level.Debug:
                    _logger.Debug(Message);
                    break;
                case Log_Level.Error:
                    _logger.Error(Message);
                    break;
                case Log_Level.Fatal:
                    _logger.Fatal(Message);
                    break;
                case Log_Level.Info:
                    _logger.Info(Message);
                    break;
                case Log_Level.Warn:
                    _logger.Warn(Message);
                    break;
            }
            LogSource[level].Insert(0, LogModel.CreateLogModel(Message, level));
            OnDisplogChanged(level);
            //刷新日志数量
        }

        private void OnDisplogChanged(Log_Level level)
        {
            if (level != CrtType)
                return;
            CallUiRefresh(() =>
            {
                DisplayLogs.Insert(0, LogSource[level].First());
            });
        }

        public void OnRefreshLog()
        {
            if (!LogSource.ContainsKey(CrtType))
            {
                DisplayLogs?.Clear();
                return;
            }
            var takenum =
                LogSource[CrtType].Count > MaxLogCount ? MaxLogCount : LogSource[CrtType].Count;
            CallUiRefresh(() => DisplayLogs = new(LogSource[CrtType].Take(takenum)));
        }

        public void CallUiRefresh(Action action)
        {
            if (
                _synchronizationContext != null
                && SynchronizationContext.Current == _synchronizationContext
            )
            {
                action();
                return;
            }
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}
