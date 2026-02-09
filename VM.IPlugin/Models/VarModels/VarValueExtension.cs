using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.IPlugin.Models.VarModels
{
    public static class VarValueExtension
    {
        /// <summary>
        /// 反射调用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <param name="display"></param>
        /// <param name="Datatype"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static VarValue<T> AppendOutVar<T>(
            this ModuleParamer module,
            string name,
            string display,
            string Datatype,
            T value
        )
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
                return module.VarOut?.Find(s => s.Name == name);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static VarValue<T> SetVarValue<T>(this ModuleParamer module, T src, T Value)
        {
            try
            {
                var data = module.VarOut?.Find(s => s.Name == nameof(src));
                if (data != null && data is VarValue<T> d)
                {
                    d.Value = Value;
                    return d;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static VarValue<T> SetVarValue<T>(this ModuleParamer module, string name, T Value)
        {
            try
            {
                var data = module.VarOut?.Find(s => s.Name == name);
                if (data != null && data is VarValue<T> d)
                {
                    d.Value = Value;
                    return d;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void RemoveVarValue(this ModuleParamer module, IVarValue data)
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
                IVarValue v = module.VarOut?.Find(s => s.Name == name);
                if (v != null)
                {
                    module.VarOut?.Remove(v);
                }
            }
            catch (Exception ex) { }
        }
    }
}
