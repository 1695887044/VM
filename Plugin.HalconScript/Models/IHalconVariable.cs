using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.IPlugin.Models.VarModels;

namespace Plugin.HalconScript.Models
{
    public interface IHalconVariable 
    {
        string Name { get; }
        IDataPort Data { get; set; }
        uint TypeId { get; }
    }
}
