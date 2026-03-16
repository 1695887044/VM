using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VM.IPlugin.Models.VarModels
{
    public static class VarValueExtension
    {
        /// <summary>
        /// 反射调用
        /// </summary>
        public static DataPort<T> AppendOutVar<T>(
            this ModuleParamer module,
            string name,
            string display,
            string Datatype,
            T value
        )
        {
            var existPort = module.VarOut.Find(s => s.Name == name) as DataPort<T>;
            if (existPort != null)
            {
                existPort.Value = value;
                return existPort;
            }
            DataPort<T> _Data = new DataPort<T>()
            {
                Name = name,
                DisPlayName = display,
                Value = value,
                DisplayName = module.ModuleGuid.ToString(),
            };
            module.VarOut.Add(_Data);
            return _Data;
        }

        public static DataPort<T>? GetVarValue<T>(this ModuleParamer module, string name)
        {
            return module?.VarOut?.Find(s => s.Name == name) as DataPort<T>;
        }

        public static DataPort<T> SetVarValue<T>(
            this ModuleParamer module,
            T src,
            T Value,
            [CallerArgumentExpression("src")] string srcName = null
        )
        {
            if (module?.VarOut == null || string.IsNullOrEmpty(srcName))
                return null;
            if (src != null)
                src = Value;
            // 2. 查找匹配项
            var data = module.VarOut.Find(s => s.Name == srcName);

            // 3. 类型与值处理
            if (data is DataPort<T> target)
            {
                target.Value = Value;
                return target;
            }
            return null;
        }

        public static DataPort<T>? SetVarValue<T>(this ModuleParamer module, string name, T Value)
        {
            var data = module?.VarOut?.Find(s => s.Name == name) as DataPort<T>;
            if (data != null)
            {
                data.Value = Value; 
                return data;
            }
            return null;
        }

        public static void RemoveVarValue(this ModuleParamer module, IDataPort data)
        {
            try
            {
                module.VarOut?.Remove(data);
            }
            catch (Exception ex) { }
        }

        public static void RemoveVarValue(this ModuleParamer module, string name)
        {
            try
            {
                IDataPort v = module.VarOut?.Find(s => s.Name == name);
                if (v != null)
                {
                    module.VarOut?.Remove(v);
                }
            }
            catch (Exception ex) { }
        }
    }
}
