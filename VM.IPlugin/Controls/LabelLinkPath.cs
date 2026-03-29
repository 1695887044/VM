using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.Enums;

namespace VM.IPlugin.Controls
{
    [TemplatePart(Name = "PART_LinkButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ClearButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_ContentBox", Type = typeof(TextBox))]
    public class LabelLinkPath : Control
    {
        private Button? _linkButton;
        private Button? _clearButton;
        private TextBox? _contentBox;

        #region Dependency Properties

        private static readonly DependencyPropertyKey IsLinkedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsLinked", typeof(bool), typeof(LabelLinkPath), new PropertyMetadata(false));
        public static readonly DependencyProperty IsLinkedProperty = IsLinkedPropertyKey.DependencyProperty;

        public bool IsLinked
        {
            get { return (bool)GetValue(IsLinkedProperty); }
            private set { SetValue(IsLinkedPropertyKey, value); }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(LabelLinkPath), new PropertyMetadata("路径"));

        public object LinkParam
        {
            get { return (object)GetValue(LinkParamProperty); }
            set { SetValue(LinkParamProperty, value); }
        }
        public static readonly DependencyProperty LinkParamProperty =
            DependencyProperty.Register("LinkParam", typeof(object), typeof(LabelLinkPath), new PropertyMetadata(null));

        public ICommand OperatorCommand
        {
            get { return (ICommand)GetValue(OperatorCommandProperty); }
            set { SetValue(OperatorCommandProperty, value); }
        }
        public static readonly DependencyProperty OperatorCommandProperty =
            DependencyProperty.Register("OperatorCommand", typeof(ICommand), typeof(LabelLinkPath));

        public Object Value
        {
            get { return (Object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Object), typeof(LabelLinkPath), new PropertyMetadata(null, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LabelLinkPath ctl)
            {
                // 卸载旧对象的属性监听
                if (e.OldValue is INotifyPropertyChanged oldPc)
                    oldPc.PropertyChanged -= ctl.OnPortPropertyChanged;

                // 装载新对象的属性监听
                if (e.NewValue is INotifyPropertyChanged newPc)
                    newPc.PropertyChanged += ctl.OnPortPropertyChanged;

                ctl.RefreshUIState();
            }
        }

        #endregion

        // 监听绑定的后端对象变化 (当弹窗修改了端口路径时触发)
        private void OnPortPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DisplayName" || e.PropertyName == "Expression")
            {
                RefreshUIState();
            }
        }

        #region 极简 UI 状态路由

        private void RefreshUIState()
        {
            if (_contentBox == null) return;

            if (Value is IDataPort port)
            {
                // 判断：只要有路径名或表达式，就算是被绑定了
                bool hasLink = !string.IsNullOrEmpty(port.SourcePath) || !string.IsNullOrEmpty(port.Expression);

                // 给依赖属性赋值，XAML里的触发器会自动根据这个变成蓝色胶囊！
                this.IsLinked = hasLink;

                if (hasLink)
                {
                    // 状态 A：已链接
                    _contentBox.Text = $"{port.DisPlayName} / {port.SourcePath}";
                    _contentBox.FontStyle = FontStyles.Normal;
                }
                else
                {
                    // 状态 B：未链接
                    _contentBox.Text = "待绑定变量..."; // 灰色占位提示
                    _contentBox.FontStyle = FontStyles.Italic;
                }
            }
            else
            {
                this.IsLinked = false;
                _contentBox.Text = "无效的绑定对象";
            }
        }

        #endregion

        #region 控件生命周期与事件挂载

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _linkButton = GetTemplateChild("PART_LinkButton") as Button;
            _clearButton = GetTemplateChild("PART_ClearButton") as Button;
            _contentBox = GetTemplateChild("PART_ContentBox") as TextBox;

            if (_linkButton != null)
                _linkButton.Click += (s, e) => OperatorCommand?.Execute(new LinkPathParam(LinkPathType.Link, this.LinkParam));

            if (_clearButton != null)
                _clearButton.Click += (s, e) => OperatorCommand?.Execute(new LinkPathParam(LinkPathType.Clear, this.LinkParam));

            RefreshUIState();
        }

        #endregion
    }

    public record class LinkPathParam
    {
        public LinkPathType PathType { get; set; }
        public Object Param { get; set; }
        public LinkPathParam(LinkPathType pathType, Object param)
        {
            PathType = pathType;
            Param = param;
        }
    }
}