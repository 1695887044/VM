using VM.IPlugin.Enums;

namespace VM.IPlugin.ModuleEvent
{
    public class ModuleEventArgs: EventArgs
    {

        public object? Parameter { get; set; }

        public string Message { get; set; }

        public StateEvent OldState { get; set; }

        private StateEvent actState;

        public StateEvent ActState
        {
            get { return actState; }
            set
            {
                if (actState == value) return;
                OldState = ActState; actState = value;
            }
        }
        public object? Tag { get; set; }

    }
}
