using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using VM.IPlugin.Enums;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace VM.IPlugin
{

    public abstract class ModuleViewModelBase:BindableBase
    {

        Stopwatch stopwatch { get; set; } = new Stopwatch();

        public ModuleEventArgs Args { get; set; } = new();
       
        private string displayTime ="0";

        [Display(Name ="执行时间")]
        public string DisplayTime
        {
            get { return displayTime; }
            set { displayTime = value;RaisePropertyChanged(); }
        }

        private StateEvent state;
        [Display(Name = "状态")]
        public StateEvent State
        {
            get { return state; }
            set { state = value; RaisePropertyChanged(); }
        }

        private ModuleParamer _moduleData = new();

        public ModuleParamer ModuleData
        {
            get { return _moduleData; }
            set { _moduleData = value; }
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
        protected void OpenVarLinkView(OpenLinkargs args =null,Action Fiter =null, Action callback =null)
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
        protected void OpenVarLinkView( Func<IVarValue,bool> Fiter, Action<IVarChangedEventParamModel> callback)
        {
             var args = new OpenLinkargs();
              args.Fiter = Fiter;
              args.CallBack = callback;
            OpenVarLinkViewEvent?.Invoke(this, args);
        }
        protected void OpenVarLinkView(OpenLinkargs args = null)
        {
            if (args == null)
            {
                args = new OpenLinkargs();
            }
            if (args.Fiter == null)
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


        public void ExecuteModule()
        {
            stopwatch.Restart();
            stopwatch.Start();
            OnModuleStateChanged(StateEvent.Running);
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                Args.Message = ex.Message;
                OnModuleStateChanged(StateEvent.Error);
            }
            stopwatch.Stop();
            OnModuleStateChanged(StateEvent.Stop);
            DisplayTime = stopwatch.ElapsedMilliseconds.ToString();
        }

        #region 创建模块时,初始化一些属性
        public virtual void ModuleInit()
        {
            //拿到所有标注Display特性的属性
            var propertys = this.GetType().GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(DisplayAttribute)));
            //注册输出变量
            foreach (var prop in propertys)
            {
                var value = prop.GetValue(this);
                //Halcon 注册的时候就是Null
                // 获取 AppendOutVar 方法的 MethodInfo
                var method = typeof(VarValueExtension).GetMethods().First(p => p.Name.Equals("AppendOutVar"));
                // 构造泛型方法
                if(value == null)
                {
                    value = default;
                }
                var genericMethod = method.MakeGenericMethod(prop.PropertyType);
                // 调用泛型方法
                genericMethod.Invoke(null, new object[] { ModuleData, prop.Name, prop.PropertyType.Name, value });
            }
        }
        public virtual void RegisterOut()
        {
           // ModuleData.AppendOutVar("状态", "StateEvent", StateEvent.Initializing);
            //ModuleData.AppendOutVar("时间", "int",0 );
        }
        public virtual void RegisterIn()
        {
            
        }
        /// <summary>
        /// 注册属性变更通知
        /// </summary>
        private bool RegisterSubScrip<T>(IVarValue varValue, Delegate handler)
        {
            if(varValue == null) return false;
            if(varValue  is VarValue<T> _var)
            {
                
            }
            return false;
        }
        #endregion
        #endregion
    }
}
