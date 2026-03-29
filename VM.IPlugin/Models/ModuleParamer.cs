using System.Runtime.CompilerServices;
using VM.IPlugin.Models.VarModels;

namespace VM.IPlugin
{
    public class ModuleParamer
    {
        public List<IDataPort> VarIn { get; set; } = new List<IDataPort>();
        public List<IDataPort> VarOut { get; set; } = new List<IDataPort>();

        public List<IDataPort> VarCache { get; set; } = new List<IDataPort>();
    }

    public static class ModuleParamerExt
    {
        public static DataPort<T> SetVarValue<T>(
            this List<IDataPort> datas,
            T Value,
            [CallerMemberName] string srcName = null
        )
        {
            if (string.IsNullOrEmpty(srcName))
                return null;
            var data = datas.Find(s => s.Name == srcName);
            if (data is DataPort<T> target)
            {
                target.Value = Value;
                return target;
            }
            if (data != null)
            {
                throw new InvalidCastException($"端口 {srcName} 的类型是 {data.GetType().Name}，无法转换为 {typeof(T).Name}");
            }
            var newPort = new DataPort<T> { Name = srcName, Value = Value };
            datas.Add(newPort);
            return newPort;
        }

        

        public static T GetVarValue<T>(this List<IDataPort> datas, [CallerMemberName] string portName = null)
        {
            if (string.IsNullOrEmpty(portName) || datas == null)
                return default(T);

            var data = datas.Find(s => s.Name == portName);

            if (data is DataPort<T> target)
            {
                return target.Value;
            }

            return default(T); 
        }
        public static DataPort<T> GetPort<T>(this List<IDataPort> datas, [CallerMemberName] string portName = null)
        {
            if (string.IsNullOrEmpty(portName) || datas == null)
                return null;

            return datas.Find(s => s.Name == portName) as DataPort<T>;
        }
    }
}
