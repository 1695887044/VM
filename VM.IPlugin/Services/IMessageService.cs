

namespace VM.IPlugin.Services
{
    public interface IMessageService
    {
        void Show(string msg);

        string InputShow(string title, string msg);
    }
}
