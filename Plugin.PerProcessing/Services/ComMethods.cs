using HalconDotNet;
using Plugin.PerProcessing.Model;
namespace Plugin.PerProcessing.Services
{
    public static class ComMethods
    {
        public static Dictionary<string, Func<HImage, HImage>> Methods = new();
        public static PretreatHService MethodService = new();
        public static T? As<T>(this IToolData src) where T : class => src as T;
        public static TEnum? MatchEnum<TEnum>(string text) where TEnum : struct, Enum
        {
            return Enum.TryParse<TEnum>(text, true, out var val) ? val : null;
        }
    }
}
