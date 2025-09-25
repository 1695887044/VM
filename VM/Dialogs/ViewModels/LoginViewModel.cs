using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VM.Start.Models;

namespace VM.Start.Dialogs.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public DelegateCommand<string> LoginByTagCommand { get; set; }



        private UserModel user;

        public UserModel User
        {
            get { return user; }
            set { user = value;RaisePropertyChanged(); }
        }
        public LoginViewModel()
        {
            User = new();
            User.UserName = "Admin";
            LoginByTagCommand = new DelegateCommand<string>(LoginByTag);
        }
        private  void LoginByTag(string obj)
        {
            OnDialogClosed();
           
        }
        #region 弹窗方法
        public DialogCloseListener RequestClose {  get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            IDialogParameters parameters = new DialogParameters();
            parameters.Add("user", User);
            RequestClose.Invoke(parameters,ButtonResult.Cancel);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
        #endregion
    }
}
