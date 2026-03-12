
using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin
{
    public class ModuleParamer
    {
        public Guid ModuleGuid = Guid.NewGuid();

        public Object? Uid {  get; set; }
        public List<IDataPort> VarIn { get; set; } =new List<IDataPort>();
        public List<IDataPort> VarOut { get; set; } = new List<IDataPort>();

        public List<IDataPort> VarCache { get; set; } = new List<IDataPort>();
    }
}
