using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DemoThemes
{
    public class PropertyItemVM : INotifyPropertyChanged
    {
        private readonly object _targetObj;
        private readonly PropertyInfo _propInfo;

        public PropertyItemVM(object target, PropertyInfo prop, DisplayAttribute displayAttr, bool isReadOnly)
        {
            _targetObj = target;
            _propInfo = prop;
            DisplayName = displayAttr?.Name ?? prop.Name;
            GroupName = displayAttr?.GroupName ?? "默认分组";
            Description = displayAttr?.Description;
            IsReadOnly = isReadOnly;

            // 判断属性类型，用于界面选择模板
            if (prop.PropertyType == typeof(bool)) EditorType = "BoolEditor";
            else if (prop.PropertyType.IsEnum) EditorType = "EnumEditor";
            else EditorType = "TextEditor";
        }

        // 界面绑定的属性
        public string DisplayName { get; }
        public string GroupName { get; }
        public string Description { get; }
        public bool IsReadOnly { get; }
        public string EditorType { get; } // 用于模板选择器

        // 核心：值的双向绑定
        public object Value
        {
            get => _propInfo.GetValue(_targetObj);
            set
            {
                // 简单类型转换逻辑（工业级项目这里需要更严谨的Converter）
                try
                {
                    var convertedVal = Convert.ChangeType(value, _propInfo.PropertyType);
                    _propInfo.SetValue(_targetObj, convertedVal);
                    OnPropertyChanged("Value");
                }
                catch { /* 处理格式错误 */ }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
