
using System.ComponentModel.DataAnnotations;
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

        public string DisPlayName { get; set; }

        public string Note { get; set; }

        Type? Type { get; }

    }
    public class VarValue<T> : BindableBase,IVarValue
    {

        public static VarValue<T> CreateVarValue(string name, string Datatype, T value)
        {
            VarValue<T> _Data = new VarValue<T>();
            _Data.Name = name;
            _Data.DataType = Datatype;
            _Data.Value = value;
            _Data.Type = typeof(T);
            return _Data;
        }
        public static VarValue<T> CreateVarValue(string name,string display, string Datatype, T value)
        {
            VarValue<T> _Data = new VarValue<T>();
            _Data.Name = name;
            _Data.DisPlayName = display;
            _Data.DataType = Datatype;
            _Data.Value = value;
            _Data.Type = typeof(T);
            return _Data;
        }
        private string _linkPath;

        public string LinkPath
        {
            get { return _linkPath; }
            set { _linkPath = value; RaisePropertyChanged(); }
        }

        private T _value;


        public T Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged(); OnValueChanged?.Invoke(this, _value); }
        }

        public  string DisPlayName { get; set; }
        public long Index { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Note { get; set; }

        public Type? Type { get; set; }
        public event EventHandler<T>? OnValueChanged;
       
    }
    public static class VarValueExtension
    {
        public static VarValue<T> AppendOutVar<T>(this ModuleParamer module, string name, string display, string Datatype, T value)
        {
            VarValue<T> _Data = new VarValue<T>();
            _Data.Name = name;
            _Data.DataType = Datatype;
            _Data.Type = typeof(T);
            _Data.DisPlayName = display;
            _Data.Value = value;
            _Data.LinkPath = module.ModuleGuid.ToString();
            module.VarOut.Add(_Data);
            return _Data;
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
        public static void RemoveVarValue(this ModuleParamer module, IVarValue data) 
        {
            try
            {
                module.VarOut?.Remove(data);
            }
            catch (Exception ex)
            {
                
            }
        }
        public static void RemoveVarValue(this ModuleParamer module, string name)
        {
            try
            {
               IVarValue  v =   module.VarOut?.Find(s => s.Name == name);
                if (v != null) { 
                 module.VarOut?.Remove(v);               
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
