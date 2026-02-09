using System.ComponentModel.DataAnnotations;
using System.Windows.Markup;
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

    public class VarValue<T> : BindableBase, IVarValue
    {
        private string _linkPath;

        public string LinkPath
        {
            get { return _linkPath; }
            set
            {
                _linkPath = value;
                RaisePropertyChanged();
            }
        }

        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
                OnValueChanged?.Invoke(this, _value);
            }
        }

        public string DisPlayName { get; set; }
        public long Index { get; set; }
        public string DataType { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Note { get; set; }

        public Type? Type { get; set; }
        public event EventHandler<T>? OnValueChanged;
    }


}
