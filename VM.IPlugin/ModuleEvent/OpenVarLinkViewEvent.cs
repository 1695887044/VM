using System.Reflection;
using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.ModuleEvent
{
    public interface IOpenLinkargs
    {
        public string Name { get; }

        public Object? Tag { get; set; }

        public Func<IDataPort, bool> Fiter { get; set; }

        public Action<IVarChangedEventParamModel> CallBack { get; }
    }

    public class OpenLinkargs : IOpenLinkargs
    {
        public string Name { get; private set; }

        public Object? Tag { get; set; }

        public Func<IDataPort, bool> Fiter { get; set; }

        public OpenLinkargs() { }

        public OpenLinkargs(Func<IDataPort, bool> fiter , Action<IVarChangedEventParamModel> callBack )
        {
            Fiter = fiter;
            CallBack = callBack;
        }

        public Action<IVarChangedEventParamModel> CallBack { get; set; }
    }

    public class OpenLinkargs<T> : IOpenLinkargs
    {
        public string Name { get; private set; }

        public Object? Tag { get; set; }

        public Func<IDataPort, bool> Fiter { get; set; }

        public OpenLinkargs() { }

        public OpenLinkargs(Action<VarChangedEventParamModel<T>> callBack)
        {
            Fiter = (s => s.Type == typeof(T));
            CallBack = callBack;
        }

        public Action<VarChangedEventParamModel<T>> CallBack { get; set; }

        Action<IVarChangedEventParamModel> IOpenLinkargs.CallBack =>
            (obj) =>
            {
                //拿到数据类型  T的参数模型  对应委托应该传入的是 数据类型 不是经过二次包装的
                if (obj == null) return;
                var runtimeType = obj.varValue.GetType().GetProperty("Value")?.PropertyType;
                if (runtimeType == null) return;
                //使用反射创建泛型类型的实例
                Type unboundGenericType = typeof(VarChangedEventParamModel<>);
                Type boundGenericType = unboundGenericType.MakeGenericType(runtimeType);
                object genericInstance = Activator.CreateInstance(boundGenericType)!;
                //设置属性值
                if (genericInstance is VarChangedEventParamModel<T> d)
                {
                    if(obj.varValue is DataPort<T> _d)
                    {
                        d.varValue = _d;
                        CallBack?.Invoke(d);
                    }
                }
            };
    }
}
