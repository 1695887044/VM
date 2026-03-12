using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.IPlugin.Models.VarModels
{
    /// <summary>
    /// 泛型数据端口基类，承载具体的业务数据，支持 MVVM 数据绑定机制
    /// </summary>
    /// <typeparam name="T">端口承载的数据类型 (如 HObject, double, string)</typeparam>
    public class DataPort<T> : BindableBase, IDataPort
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set
            {
                _displayName = value;
                RaisePropertyChanged(); // UI 联动更新
            }
        }

        private T _value;
        /// <summary>
        /// 端口实际承载的数据值
        /// </summary>
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged(); // 通知 WPF 界面 (如 TextBlock, 属性网格) 更新
                OnValueChanged?.Invoke(this, _value); // 触发 C# 后台事件
            }
        }

        // --- 基础属性 ---
        public string Tag { get; set; }
        public string DisPlayName { get; set; }
        public string Name { get; set; }
        public string Expression { get; set; }
        public string Note { get; set; }
        public uint Category { get; set; } = 255;

        /// <summary>
        /// 物理绑定的源端口引用 (内存级直连)。执行时，将从这个对象拉取 Value
        /// </summary>
        public DataPort<T> LinkedPort { get; set; }

        /// <summary>
        /// 获取当前泛型的具体类型
        /// </summary>
        public Type Type => typeof(T);

        /// <summary>
        /// 值变更事件，可用于订阅特殊的数据流联动逻辑
        /// </summary>
        public event EventHandler<T>? OnValueChanged;
        /// <summary>
        /// 数据清空
        /// </summary>
        public void Disconnect()
        {
            this.LinkedPort = null;
            this.DisplayName = string.Empty;
        }

    }
}
