using VM.IPlugin.Enums;
using VM.IPlugin.ModuleEvent;

namespace VM.IPlugin
{

    public abstract class ModuleViewModelBase:BindableBase
    {

        public ModuleEventArgs Args { get; set; } = new();
       
        private string displayTime ="0";

        public string DisplayTime
        {
            get { return displayTime; }
            set { displayTime = value;RaisePropertyChanged(); }
        }

        private StateEvent state;

        public StateEvent State
        {
            get { return state; }
            set { state = value; RaisePropertyChanged(); }
        }

        private ModuleParamer _Paramer = new();

        public ModuleParamer Paramer
        {
            get { return _Paramer; }
            set { _Paramer = value; }
        }

        #region Methods
        /// <summary>
        /// 模块执行
        /// </summary>
        /// <returns></returns>
        public abstract bool Execute();
        /// <summary>
        /// 确定
        /// </summary>
        /// <returns></returns>
        public abstract bool Confirm();
        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        public abstract bool Cancel();
        /// <summary>
        /// 模块状态发生改变  
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnModuleStateChanged(StateEvent state)
        {
            Args.ActState = state;
            ModuleStateChanged?.Invoke(this, Args);
        }
        /// <summary>
        /// 🔗链接变量发生改变
        /// </summary>
        public abstract void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent);
       
        /// <summary>
        /// 打开变量链接视图
        /// </summary>
        protected void OpenVarLinkView(OpenLinkargs args =null)
        {
            if(args == null)
            {
                args = new OpenLinkargs();
            }
            if(args.Fiter == null)
            {
                args.Fiter = (s => true);
            }
            OpenVarLinkViewEvent?.Invoke(this, args);
        }
        #endregion

        #region 
        /// <summary>
        /// 事件
        /// </summary>
        public event EventHandler<ModuleEventArgs>? ModuleStateChanged ;
        public event EventHandler<OpenLinkargs>? OpenVarLinkViewEvent;
        #endregion
    }
}
