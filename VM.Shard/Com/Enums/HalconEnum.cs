using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VM.Shard.Com.Enums
{
    internal class HalconEnum
    {
    }
    public enum Metric
    {
        [Description("使用极性")]
        use_polarity,

        [Description("忽略全局极性")]
        ignore_global_polarity,

        [Description("忽略局部极性")]
        ignore_local_polarity,

        [Description("忽略颜色极性")]
        ignore_color_polarity
    }
    public enum Optimization
    {
        [Description("自动")]
        auto,

        [Description("无优化")]
        none,

        [Description("低点数缩减")]
        point_reduction_low,

        [Description("中等点数缩减")]
        point_reduction_medium,

        [Description("高点数缩减")]
        point_reduction_high,

        [Description("预生成")]
        pregeneration,

        [Description("不预生成")]
        no_pregeneration
    }
}
