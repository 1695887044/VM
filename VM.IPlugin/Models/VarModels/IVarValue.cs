
using System.Collections.Specialized;
using System.ComponentModel;

namespace VM.IPlugin.Models.VarModels
{
    public interface IVarValue
    {

        string LinkPath { get;  }
        long Index { get; set; }

        string DataType { set; get; }

        public string Name { get; set; }

        public string Expression { get; set; }


        public string Note { get; set; }

    }
    public class VarValue<T> : BindableBase,IVarValue
    {
        public string LinkPath { get;  set; }
        private T _value;


        public T Value
        {
            get { return _value; }
            set { _value = value; RaisePropertyChanged();  }
        }
        public long Index { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Note { get; set; }

       
    }
}
