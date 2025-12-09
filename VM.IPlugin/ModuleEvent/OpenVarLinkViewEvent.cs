

using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin.ModuleEvent
{
    public class OpenVarLinkViewEvent : PubSubEvent<OpenLinkargs>
    {
    }


    public class OpenLinkargs:EventArgs
    {
        public Guid guid;

        public string name;

        public Object? Tag { get; set; }

        public Func<IVarValue, bool> Fiter { get; set; }
        public OpenLinkargs()
        {
            
        }
        public OpenLinkargs(Guid g, string n)
        {
            guid = g;
            name = n;
        }
    }
}
