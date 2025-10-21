using System.Diagnostics;
using VM.IPlugin;
using VM.IPlugin.Enums;
using VM.IPlugin.Models;

namespace Plugin.Delay.ViewModels
{
    [Serializable]
    public class DelayViewModel : ModuleViewModelBase
    {
        Stopwatch stopwatch { get; set; } =new Stopwatch();
        private LinkVarModel _delayTime = new LinkVarModel { Text = "100" };

        public LinkVarModel DelayTime
        {
            get { return _delayTime; }
            set { _delayTime = value; }
        }

        public override bool Execute()
        {
            int _time = Convert.ToInt32(DelayTime.Value);
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
