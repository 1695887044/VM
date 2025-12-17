
using Plugin.PerProcessing.Common;
using Plugin.PerProcessing.Model;

namespace Plugin.PerProcessing.Services
{
    public static class ToolFactory
    {
        /// <summary>
        /// 字符串 → 枚举 → 工具实例（壳+默认参数）
        /// </summary>
        public static IToolData? CreateTool(string toolText)
        {
            // 1. 先转枚举
            var op = ComMethods.MatchEnum<eOperatorType>(toolText);
            if (op == null) return null;

            // 2. 枚举 → 实例
            return op.Value switch
            {
                // 滤波
                eOperatorType.均值滤波 or
                eOperatorType.中值滤波 or
                eOperatorType.高斯滤波 => new FilterImageTool
                {
                    SubType = op.Value,
                     ToolParamer = new () { SubType = op.Value }
                },

                // 阈值
                eOperatorType.二值化 => new ThresholdTool
                {
                    SubType = op.Value,
                    ToolParamer = new ()
                },
                eOperatorType.均值二值化 => new VarThresholdTool
                {
                    SubType = op.Value,
                    ToolParamer = new ()
                },

                // 形态学
                eOperatorType.灰度膨胀 or
                eOperatorType.灰度腐蚀 or
                eOperatorType.灰度开运算 or
                eOperatorType.灰度闭运算 => new MorphologyTool
                {
                    SubType = op.Value,
                    ToolParamer = new () { SubType = op.Value }
                },

                // 增强
                eOperatorType.锐化 or
                eOperatorType.对比度 or
                eOperatorType.亮度调节 => new EnhanceTool
                {
                    SubType = op.Value,
                    ToolParamer = new EnhanceParam { SubType = op.Value }
                },

                // 图像调整
                eOperatorType.彩色转灰 => new TransImageTool
                {
                    SubType = op.Value,
                    ToolParamer = new TransImageParam()
                },
                eOperatorType.图像镜像 => new MirrorImageTool
                {
                    SubType = op.Value,
                    ToolParamer = new MirrorImageParam()
                },
                eOperatorType.图像旋转 => new RotateImageTool
                {
                    SubType = op.Value,
                    ToolParamer = new RotateImageParam()
                },
                eOperatorType.修改图像尺寸 => new ChangeImageTool
                {
                    SubType = op.Value,
                    ToolParamer =new ChangeImageParam()
                },
                eOperatorType.反色 => new InvertImageTool
                {
                    SubType = op.Value,
                    ToolParamer = new InvertImageParam()
                },

                _ => null
            };
        }
    }
}
