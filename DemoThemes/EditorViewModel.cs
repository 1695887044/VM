using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DemoThemes
{
    public class EditorViewModel
    {
        public ICollectionView GroupedProperties { get; set; }

        public EditorViewModel(object targetModel)
        {
            var list = new ObservableCollection<PropertyItemVM>();
            var properties = targetModel.GetType().GetProperties();

            foreach (var prop in properties)
            {
                // 1. 获取 Display 特性
                var displayAttr = prop.GetCustomAttribute<DisplayAttribute>();
                if (displayAttr == null) continue; // 没有特性的属性不显示

                // 2. 获取 ReadOnly 特性
                var readOnlyAttr = prop.GetCustomAttribute<ReadOnlyAttribute>();
                bool isReadOnly = readOnlyAttr != null && readOnlyAttr.IsReadOnly;

                list.Add(new PropertyItemVM(targetModel, prop, displayAttr, isReadOnly));
            }

            // 3. 启用 WPF 的分组功能
            GroupedProperties = CollectionViewSource.GetDefaultView(list);
            GroupedProperties.GroupDescriptions.Add(new PropertyGroupDescription("GroupName"));
        }
    }
}