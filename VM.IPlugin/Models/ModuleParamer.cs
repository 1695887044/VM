
using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin
{
    public class ModuleParamer
    {
        public Guid ModuleGuid = Guid.NewGuid();

        public Object? Uid {  get; set; }
        public List<IVarValue> VarIn { get; set; } =new List<IVarValue>();
        public List<IVarValue> VarOut { get; set; } = new List<IVarValue>();

        public List<IVarValue> VarCache { get; set; } = new List<IVarValue>();
    }
}
