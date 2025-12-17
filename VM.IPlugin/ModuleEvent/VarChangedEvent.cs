using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.ModuleEvent
{
    public class VarChangedEvent : PubSubEvent<IVarChangedEventParamModel> { }

    public class VarChangedEvent<T> : PubSubEvent<VarChangedEventParamModel<T>> { }

    public class RefreshUIEvent<T> : PubSubEvent<VarValue<T>>
    {

    }
    public class VarChangedEventParamModel : IVarChangedEventParamModel
    {
        /// <summary>
        /// 定义插件发送名
        /// </summary>
        public string SendName { get; set; }
        public string LinkName { get; set; }

        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsAdd { get; set; }

        public IVarValue varValue { get; set; }
        public string Note { get; set; }
    }
    public class VarChangedEventParamModel<T> : VarChangedEventParamModel, IVarChangedEventParam<T>
    {

        public T varValue { get; set; }
    }

    public interface IVarChangedEventParamModel
    {
        public string SendName { get; set; }
        public string LinkName { get; set; }


        public string Name { get; set; }
        public string DataType { get; set; }
        public bool IsAdd { get; set; }

        public string Note { get; set; }
    }
    public interface IVarChangedEventParam<T> : IVarChangedEventParamModel
    {
        public T varValue { get; set; }
    }
}
