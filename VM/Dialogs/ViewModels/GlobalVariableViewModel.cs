using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VM.Start.Services;

namespace VM.Start.Dialogs.ViewModels
{
    public class GlobalVariableViewModel : BindableBase,IDialogAware
    {
        public string Title => "全局变量";
        public GlobalVariableService GlobalDataService { get; }

        // --- 供 UI 下拉框绑定的类型列表 ---
        public ObservableCollection<Type> AvailableTypes { get; } = new ObservableCollection<Type>
        {
            typeof(int),
            typeof(double),
            typeof(string),
            typeof(bool),
            typeof(int[]),
            typeof(double[]),
            typeof(string[]),
            typeof(bool[])
        };

        // --- 新增表单的绑定字段 ---
        private Type _selectedNewType;
        public Type SelectedNewType
        {
            get => _selectedNewType;
            set => SetProperty(ref _selectedNewType, value);
        }


        private string _newUiName;
        public string NewUiName
        {
            get => _newUiName;
            set => SetProperty(ref _newUiName, value);
        }

        private string _newInitialValue;
        public string NewInitialValue
        {
            get => _newInitialValue;
            set => SetProperty(ref _newInitialValue, value);
        }

        // --- 命令 ---
        public DelegateCommand AddVariableCommand { get; }

        public DialogCloseListener RequestClose { get; }

        public GlobalVariableViewModel(GlobalVariableService globalDataService)
        {
            GlobalDataService = globalDataService;

            SelectedNewType = AvailableTypes[0];

            AddVariableCommand = new DelegateCommand(ExecuteAddVariable);
        }

        private void ExecuteAddVariable()
        {
            // 1. 简单的表单校验
            if (string.IsNullOrWhiteSpace(NewUiName))
            {
                MessageBox.Show("内部名称和显示名称不能为空！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2. 🏆 调用我们在上一轮写好的底层动态工厂方法！
                GlobalDataService.AddUserGlobalVariable( NewUiName, SelectedNewType, NewInitialValue);

                // 3. 添加成功后，清空输入框，提升体验

                NewUiName = string.Empty;
                NewInitialValue = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"添加失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    }
}
