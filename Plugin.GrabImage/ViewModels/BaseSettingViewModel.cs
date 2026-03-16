using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.GrabImage.ViewModels
{
    public abstract class BaseSettingViewModel:BindableBase
    {
        public event Action<string> OnImageChanged;
        protected void RaiseImageChanged(string img)
        {
            OnImageChanged?.Invoke(img);
        }
    }
}
