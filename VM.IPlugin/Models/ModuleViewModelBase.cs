using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using VM.IPlugin.Enums;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace VM.IPlugin
{
    [Serializable]
    public abstract class ModuleViewModelBase : BindableBase
    {
        #region Properties
        Stopwatch stopwatch { get; set; } = new Stopwatch();


        private int displayTime = 0;

        [Display(Name = "执行时间")]
        public int DisplayTime
        {
            get { return displayTime; }
            set
            {
                displayTime = value;
                RaisePropertyChanged();
            }
        }

        private StateEvent state;

        [Display(Name = "状态")]
        public StateEvent State
        {
            get { return state; }
            set
            {
                state = value;
                RaisePropertyChanged();
                ModuleStateChanged?.Invoke(this, state);
            }
        }

        private ModuleParamer _moduleData = new();

        public ModuleParamer ModuleData
        {
            get { return _moduleData; }
            set { _moduleData = value; }
        }
        #endregion
        #region Methods
        /// <summary>
        /// 模块执行
        /// </summary>
        /// <returns></returns>
        protected abstract bool Execute();

        /// <summary>
        /// 确定
        /// </summary>
        /// <returns></returns>
        public virtual bool Confirm() => true;

        /// <summary>
        /// 取消
        /// </summary>
        /// <returns></returns>
        public virtual bool Cancel() => true;


        /// <summary>
        /// 🔗链接变量发生改变
        /// </summary>
        public virtual void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent) { }

        /// <summary>
        /// 打开变量链接视图
        /// </summary>
        protected void OpenVarLinkView<T>(Action<VarChangedEventParamModel<T>> callBack)
        {
            var targs = new OpenLinkargs<T>(callBack);
            OpenVarLinkViewEvent?.Invoke(this, targs);
        }
        protected void OpenVarLinkView(Func<IDataPort, bool> fiter, Action<IVarChangedEventParamModel> callBack)
        {
            var targs = new OpenLinkargs(fiter, callBack);
            OpenVarLinkViewEvent?.Invoke(this, targs);
        }

        #endregion

        #region
        /// <summary>
        /// 事件
        /// </summary>
        public event EventHandler<StateEvent>? ModuleStateChanged;
        public event EventHandler<IOpenLinkargs>? OpenVarLinkViewEvent;

        public void ExecuteModule()
        {
            stopwatch.Restart();
            stopwatch.Start();
            DisplayTime = ModuleData.SetVarValue(DisplayTime, 0).Value;
            State = ModuleData.SetVarValue(State, StateEvent.Running).Value;
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                State = ModuleData.SetVarValue(State, StateEvent.Error).Value;
            }
            stopwatch.Stop();
            State=ModuleData.SetVarValue(State, StateEvent.Stop).Value;
            DisplayTime =ModuleData.SetVarValue(DisplayTime, (int)stopwatch.ElapsedMilliseconds).Value;
        }

        #region 创建模块时,初始化一些属性
        public virtual void ModuleInit()
        {
            //拿到所有标注Display特性的属性
            var propertys = this.GetType()
                .GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(DisplayAttribute)));
            //注册输出变量
            foreach (var prop in propertys)
            {
                var value = prop.GetValue(this);
                //Halcon 注册的时候就是Null
                // 获取 AppendOutVar 方法的 MethodInfo
                var method = typeof(VarValueExtension)
                    .GetMethods()
                    .First(p => p.Name.Equals("AppendOutVar"));
                // 构造泛型方法
                if (value == null)
                {
                    value = default;
                }
                var genericMethod = method.MakeGenericMethod(prop.PropertyType);
                var at = prop.GetCustomAttribute<DisplayAttribute>();
                // 调用泛型方法
                genericMethod.Invoke(
                    null,
                    new object[] { ModuleData, prop.Name, at.Name, prop.PropertyType.Name, value }
                );
            }
        }
        #endregion
        #endregion
    }
}
