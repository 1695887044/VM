using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.IPlugin.Models.VarModels;

namespace Plugin.HalconScript.Models
{
    public class HalconVariable : BindableBase, IHalconVariable
    {
        private string _name;
        private IDataPort _data;
        private uint _typeId;

        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        public IDataPort Data
        {
            get => _data;
            set => SetProperty(ref _data, value);
        }

        public uint TypeId
        {
            get => _typeId;
            private set => SetProperty(ref _typeId, value);
        }

        public HalconVariable(string name, IDataPort data)
        {
            Name = name;
            Data = data;
        }

        public HalconVariable(string name, uint typeId)
        {
            Name = name;
            TypeId = typeId;
        }
    }
}
