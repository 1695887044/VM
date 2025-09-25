

namespace VM.Start.Core.IOC
{
    public enum Lifetime
    {
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,
        /// <summary>
        /// 多例
        /// </summary>
        Transient,

    }

    /// <summary>
    /// 标注类型的生命周期、是否自动初始化
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExposedServiceAttribute : Attribute
    {
        public Lifetime Lifetime { get; set; }

        public bool AutoInitialize { get; set; }

        public Type[] Types { get; set; }

        public ExposedServiceAttribute(Lifetime lifetime = Lifetime.Transient, params Type[] types)
        {
            Lifetime = lifetime;
            Types = types;
        }
    }
}
