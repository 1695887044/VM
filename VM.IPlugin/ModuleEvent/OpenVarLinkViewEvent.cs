using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.ModuleEvent
{
    public class OpenVarLinkViewEvent : PubSubEvent<OpenLinkargs> { }

    public class OpenVarLinkViewEvent<T> : PubSubEvent<OpenLinkargs<T>> { }

    public class OpenLinkargs : EventArgs
    {
        public Guid guid;

        public string name;

        public Object? Tag { get; set; }

        public Func<IVarValue, bool> Fiter { get; set; }

        public OpenLinkargs() { }

        public OpenLinkargs(string fiter, Action<IVarChangedEventParamModel> callBack)
        {
            Fiter = (s => s.DataType == "HImage");
            CallBack = callBack;
        }

        public OpenLinkargs(Guid g, string n)
        {
            guid = g;
            name = n;
        }

        public Action<IVarChangedEventParamModel> CallBack { get; set; }
    }

    public class OpenLinkargs<T> : EventArgs
    {
        public Guid guid;

        public string name;

        public Object? Tag { get; set; }

        public Func<IVarValue, bool> Fiter { get; set; }

        public OpenLinkargs() { }

        public OpenLinkargs(string fiter, Action<VarChangedEventParamModel<T>> callBack)
        {
            Fiter = (s => s.DataType == "HImage");
            CallBack = callBack;
        }

        public OpenLinkargs(Guid g, string n)
        {
            guid = g;
            name = n;
        }

        public Action<VarChangedEventParamModel<T>> CallBack { get; set; }
    }
}
