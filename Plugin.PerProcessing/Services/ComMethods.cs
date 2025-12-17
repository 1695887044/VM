using HalconDotNet;
namespace Plugin.PerProcessing.Services
{
    public static class ComMethods
    {
        public static Dictionary<string, Func<HImage, HImage>> Methods = new();
        public static PretreatHService MethodService = new();

    }
}
