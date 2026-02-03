using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.IPlugin.Controls;

namespace VM.IPlugin.Models
{
    public interface ILinkable
    {
        public DelegateCommand<LinkPathParam> LinkPathCommand { get; init; }
    }
}
