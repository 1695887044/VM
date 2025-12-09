using VM.IPlugin.Consts;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Start.Services;

namespace VM.Start.Dialogs.ViewModels
{
    public class VarLinkViewModel : BindableBase, IDialogAware
    {
        public string Title { get;  } = "变量链接";
        public  GlobalVarService VarService { get; set; }

        public DelegateCommand<IVarValue> ConfirmCommand { get; init; }
        public VarLinkViewModel(GlobalVarService varService)
        {
            ConfirmCommand = new DelegateCommand<IVarValue>(ConfirmExecte);
            VarService = varService;
        }
        /// <summary>
        /// 用户确认变量链接
        /// </summary>
        /// <param name="obj"></param>
        private void ConfirmExecte(IVarValue obj)
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
