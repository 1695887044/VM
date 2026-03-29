using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using VM.IPlugin.Enums;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;

namespace VM.IPlugin
{
    [Serializable]
    public abstract class ModuleViewModelBase : BindableBase
    {
        #region Properties
        Stopwatch stopwatch { get; set; } = new Stopwatch();


        [OutputPort("DisplayTime")]
        public int DisplayTime
        {
            get { return ModuleData.VarOut.GetVarValue<int>(); }
            set
            {
                ModuleData.VarOut.SetVarValue(value);
                RaisePropertyChanged();
            }
        }

        [OutputPort("State")]
        public StateEvent State
        {
            get { return ModuleData.VarOut.GetVarValue<StateEvent>(); }
            set
            {
                ModuleData.VarOut.SetVarValue(value);
                RaisePropertyChanged();
                ModuleStateChanged?.Invoke(this, value);
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

        protected void OpenVarLinkView(
            Func<IDataPort, bool> fiter,
            Action<IVarChangedEventParamModel> callBack
        )
        {
            var targs = new OpenLinkargs(fiter, callBack);
            OpenVarLinkViewEvent?.Invoke(this, targs);
        }

        #endregion

        /// <summary>
        /// 事件
        /// </summary>
        public event EventHandler<StateEvent>? ModuleStateChanged;
        public event EventHandler<IOpenLinkargs>? OpenVarLinkViewEvent;

        public void ExecuteModule()
        {
            stopwatch.Restart();
            stopwatch.Start();
            DisplayTime = 0;
            State = StateEvent.Running;
            try
            {
                State = Execute() ? StateEvent.Stop : StateEvent.Error;
            }
            catch (Exception ex)
            {
                State = StateEvent.Error;
            }
            finally
            {
                stopwatch.Stop();
                State = StateEvent.Stop;
                DisplayTime = (int)stopwatch.ElapsedMilliseconds;
                
            }
        }

        #region 创建模块时,初始化一些属性
        public virtual void ModuleInit()
        {
            var propertys = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => Attribute.IsDefined(p, typeof(OutputPortAttribute)));
            var baseMethod = typeof(VarValueExtension).GetMethod(
                "AppendOutVar",
                BindingFlags.Static | BindingFlags.Public
            );
            if (baseMethod == null)
                return;
            foreach (var prop in propertys)
            {
                var value = prop.GetValue(this);
                if (value == null)
                {
                    value = default;
                }
                var genericMethod = baseMethod.MakeGenericMethod(prop.PropertyType);
                var at = prop.GetCustomAttribute<OutputPortAttribute>();
                genericMethod.Invoke(
                    null,
                    new object[]
                    {
                        ModuleData,
                        prop.Name,
                        at.PortName,
                        prop.PropertyType.Name,
                        value,
                    }
                );
            }
        }
        #endregion
    }
}
