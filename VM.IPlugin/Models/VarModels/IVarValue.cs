
using VM.IPlugin.ModuleEvent;

namespace VM.IPlugin.Models.VarModels
{
    public interface IVarValue
    {

        string LinkPath { get; set; }
        long Index { get; set; }

        string DataType { set; get; }

        public string Name { get; set; }

        public string Expression { get; set; }


        public string Note { get; set; }

    }
    public class VarValue<T> : BindableBase,IVarValue
    {

        public static VarValue<T> CreateVarValue(string name, string Datatype, T value)
        {
            VarValue<T> _Data = new VarValue<T>();
            _Data.Name = name;
            _Data.DataType = Datatype;
            _Data.Value = value;
            return _Data;
        }
        public string LinkPath { get;  set; }
        private T _value;


        public T Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); OnValueChanged?.Invoke(this, _value); }
        }
        public long Index { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Note { get; set; }

        public event EventHandler<T>? OnValueChanged;
       
    }
    public static class VarValueExtension
    {
        public static void AppendOutVar<T>(this ModuleParamer module , string name, string Datatype,  T value)
        {
            VarValue<T> _Data = new VarValue<T>();
            _Data.Name = name;
            _Data.DataType = Datatype;
            _Data.Value = value;
            _Data.LinkPath = module.ModuleGuid.ToString();
            module.VarOut.Add(_Data);
        }
        public static void AppendOutValueVar<T>(this ModuleParamer module, string name, string Datatype,ref T value)
        {
            VarValue<T> _Data = new VarValue<T>();
            _Data.Name = name;
            _Data.DataType = Datatype;
            _Data.Value = value;
            _Data.LinkPath = module.ModuleGuid.ToString();
            module.VarOut.Add(_Data);
        }
        public static IVarValue? GetVarValue<T>(this ModuleParamer module, string name)
        {
            try
            {
                return   module.VarOut?.Find(s=>s.Name == name );
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void SetVarValue<T>(this ModuleParamer module, string name,Action<VarValue<T>?> setter)
        {
            try
            {
                var data =  module.VarOut?.Find(s => s.Name == name);
                if(data != null && data is VarValue<T> d)
                {
                    setter(d);
                }
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
