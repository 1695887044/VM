using System.Diagnostics;
using VM.IPlugin;
using VM.IPlugin.Enums;
using VM.IPlugin.Models;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.Delay.ViewModels
{
    [Serializable]
    public class DelayViewModel : ModuleViewModelBase
    {
       
        Stopwatch stopwatch { get; set; } =new Stopwatch();
        private LinkVarModel _delayTime = new LinkVarModel { Text = "100" };
         VarValue<int> delayTime;
        public DelegateCommand LinkViewCommand { get; private set; }


        public LinkVarModel DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        public DelayViewModel()
        {
            LinkViewCommand = new DelegateCommand(OpenLink);
        }
        /// <summary>
        /// 打开变量窗口 加入筛选条件
        /// </summary>
        private void OpenLink()
        {
            OpenLinkargs openLinkargs = new OpenLinkargs();
            openLinkargs.guid = Paramer.ModuleGuid;
            openLinkargs.name = "DelayTime";
            openLinkargs.Fiter = (s => s.DataType == "int");
            OpenVarLinkView(openLinkargs);
        }

        public override bool Execute()
        {
            int _time = delayTime == null ? Convert.ToInt32(DelayTime.Value) : delayTime.Value;
            stopwatch.Restart();
            try
            {
                OnModuleStateChanged(StateEvent.Running);
                stopwatch.Start();
                while (stopwatch.ElapsedMilliseconds <= _time ||  !stopwatch.IsRunning)
                {
                    DisplayTime = stopwatch.ElapsedMilliseconds.ToString();
                    Thread.Sleep(2);
                }
                stopwatch.Stop();
                OnModuleStateChanged(StateEvent.Complete);
                return stopwatch.ElapsedMilliseconds >= _time;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                OnModuleStateChanged(StateEvent.Error);
                return false;
            }
        }
        /// <summary>
        /// 链接变量发生改变
        /// </summary>
        /// <param name="changedEvent"></param>
        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
            if(changedEvent.varValue is VarValue<int> model)
            {
                DelayTime.Text = $"{model.LinkPath}&{model.Name}";
                delayTime = model;
            }
           
        }
        public override bool Confirm()
        {         
            
            return true;
        }

        public override bool Cancel()
        {
            stopwatch.Stop();
            OnModuleStateChanged(StateEvent.Paused);
            return true;
        }
        
       
    
    }
}
