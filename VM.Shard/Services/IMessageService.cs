using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Services
{
    public interface IMessageService
    {
        /// <summary>
        /// 显示消息对话框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        void ShowMessage(string message, bool ReadOnlay = false, Log_Level level = Log_Level.Info, string title = "提示");

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        /// <returns>用户是否确认</returns>
        bool ShowConfirmation(string message, bool ReadOnlay = false, Log_Level level = Log_Level.Info, string title = "提示");

        /// <summary>
        /// 显示确认对话框
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="title">标题</param>
        /// <returns>用户是否确认</returns>
        T  ShowPropertyView<T>( T data, bool ReadOnlay = false, Log_Level level = Log_Level.Info, string title = "提示");

        /// <summary>
        /// 异步显示模态对话框
        /// </summary>
        /// <typeparam name="TViewModel">对话框的ViewModel类型</typeparam>
        /// <typeparam name="TResult">返回结果的类型</typeparam>
        /// <param name="viewModel">对话框的ViewModel实例</param>
        /// <returns>对话框返回的结果</returns>
        Task<TResult> ShowDialogAsync<TViewModel, TResult>(TViewModel viewModel) where TViewModel : class;
    }
}
