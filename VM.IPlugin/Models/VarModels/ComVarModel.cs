
namespace VM.IPlugin.Models.VarModels
{
    public class ComVarModel<T>: VarValue<T>
    {
        public long Index { get; set; }
        public string DataType { get; set; } = typeof(T).ToString();
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Note { get; set; }
        public T Value { get; set; }
    
    }
}
