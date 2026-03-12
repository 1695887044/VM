using VM.IPlugin.Consts;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Start.Services;

namespace VM.Start.Dialogs.ViewModels
{
    public class VarLinkViewModel : BindableBase, IDialogAware
    {
        public string Title { get;  } = "变量链接";
        public  GlobalDataService VarService { get; set; }

        public DelegateCommand<IDataPort> ConfirmCommand { get; init; }

        public DelegateCommand CloseCommand { get; init; }
        public VarLinkViewModel(GlobalDataService varService)
        {
            ConfirmCommand = new DelegateCommand<IDataPort>(ConfirmExecte);
            CloseCommand = new(() => {
                RequestClose.Invoke();
            });
            VarService = varService;
        }
        /// <summary>
        /// 用户确认变量链接
        /// </summary>
        /// <param name="obj"></param>
        private void ConfirmExecte(IDataPort obj)
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add(GlobalConst.LinkVarEventParamterKey, obj);
            RequestClose.Invoke(dialogParameters,ButtonResult.OK);
           
            OnDialogClosed();
        }

        public DialogCloseListener RequestClose { get; set; }

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
