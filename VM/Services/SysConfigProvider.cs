

using System.Collections.ObjectModel;
using VM.Start.Models.Recipes;

namespace VM.Start.Services
{
    public  class SysConfigProvider
    {
        #region Singleton

        private static readonly SysConfigProvider _instance =
            new SysConfigProvider();

        private SysConfigProvider() { }

        public static SysConfigProvider Ins
        {
            get { return _instance; }
        }
        #endregion
        public SystemConfigModel SystemConfig;

    }
}
