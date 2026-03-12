using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.ModuleEvent
{
    public class RefreshUIEvent<T> : PubSubEvent<DataPort<T>>
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

        public IDataPort varValue { get; set; }
        public string Note { get; set; }
    }
    public class VarChangedEventParamModel<T> : VarChangedEventParamModel, IVarChangedEventParam<T>
    {

        public DataPort<T> varValue { get; set; }
    }

    public interface IVarChangedEventParamModel
    {
        string SendName { get; set; }
        string LinkName { get; set; }

        IDataPort varValue { get; set; }
        string Name { get; set; }
        string DataType { get; set; }
        bool IsAdd { get; set; }

        string Note { get; set; }
    }
    public interface IVarChangedEventParam<T> : IVarChangedEventParamModel
    {
        public DataPort<T> varValue { get; set; }
    }
}
