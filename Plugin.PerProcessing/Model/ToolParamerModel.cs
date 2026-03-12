using Plugin.PerProcessing.Common;
using W.UI.Attributes;

namespace Plugin.PerProcessing.Model
{
    public interface IPerParam { }

    public class ThresholdParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.二值化;
        private int thresholdLow;

        [SuperDisplay(Name = "阈值-低", GroupPath = "参数组/阈值参数")]
        [NumericRange(0, 255, 5)]
        public int ThresholdLow
        {
            get { return thresholdLow; }
            set
            {
                thresholdLow = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }
        private int thresholdHight;

        [SuperDisplay(Name = "阈值-高", GroupPath = "参数组/阈值参数")]
        [NumericRange(0, 255, 5)]
        public int ThresholdHight
        {
            get { return thresholdHight; }
            set
            {
                thresholdHight = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private bool thresholdReverse;

        [SuperDisplay(Name = "阈值-反转", GroupPath = "参数组/阈值参数")]
        public bool ThresholdReverse
        {
            get { return thresholdReverse; }
            set
            {
                thresholdReverse = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() =>
            $"{ThresholdLow}:{ThresholdHight}---{ThresholdReverse}";
    }

    public class VarThresholdParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();

        public eOperatorType SubType { get; set; } = eOperatorType.均值二值化;

        private int varThresholdWidth = 5;

        [SuperDisplay(Name = "阈值-宽", GroupPath = "参数组/阈值参数")]
        [NumericRange(0, 255, 5)]
        public int VarThresholdWidth
        {
            get => varThresholdWidth;
            set
            {
                varThresholdWidth = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private int varThresholdHeight = 5;
        [SuperDisplay(Name = "阈值-高", GroupPath = "参数组/阈值参数")]
        [NumericRange(0, 255, 5)]
        public int VarThresholdHeight
        {
            get => varThresholdHeight;
            set
            {
                varThresholdHeight = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private int varThresholdSkew;
        [SuperDisplay(Name = "阈值-倾斜", GroupPath = "参数组/阈值参数")]
        [NumericRange(0, 255, 5)]
        public int VarThresholdSkew
        {
            get => varThresholdSkew;
            set
            {
                varThresholdSkew = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private eVarThresholdType varThresholdType;
        [SuperDisplay(Name = "阈值-模式", GroupPath = "参数组/阈值参数")]
        public eVarThresholdType VarThresholdType
        {
            get => varThresholdType;
            set
            {
                varThresholdType = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() =>
            $"{VarThresholdWidth}×{VarThresholdHeight} 偏移={VarThresholdSkew} {VarThresholdType}";
    }

    public class InvertImageParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.反色;
        private bool invertImageLogic;
        [SuperDisplay(Name = "图片-反色", GroupPath = "参数组/图片参数")]
        public bool InvertImageLogic
        {
            get { return invertImageLogic; }
            set
            {
                invertImageLogic = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() => InvertImageLogic ? "反色启用" : "反色关闭";
    }

    /// <summary>
    /// 形态学参数
    /// </summary>
    public class MorphologyParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.灰度闭运算;
        private int width;
        [SuperDisplay(Name = "灰度-宽", GroupPath = "参数组/形态学参数")]
        [NumericRange(0, 255, 2)]
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }
        private int height;
        [SuperDisplay(Name = "灰度-高", GroupPath = "参数组/形态学参数")]
        [NumericRange(0, 255, 2)]
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() => $"{SubType}{Width}×{Height}";
    }

    public class TransImageParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.彩色转灰;
        private eTransImageType transImageType = eTransImageType.通用比例转换;
        public eTransImageType TransImageType
        {
            get => transImageType;
            set
            {
                transImageType = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private eTransImageChannel transImageChannel;
        [SuperDisplay(Name = "转换模式", GroupPath = "参数组/图片转参数")]
        public eTransImageChannel TransImageChannel
        {
            get => transImageChannel;
            set
            {
                transImageChannel = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() => $"{TransImageType} | {TransImageChannel}";
    }

    public class RotateImageParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.图像旋转;
        private eRotateImageAngle rotateImageAngle;
        [SuperDisplay(Name = "图片旋转", GroupPath = "参数组/图片参数")]
        public eRotateImageAngle RotateImageAngle
        {
            get => rotateImageAngle;
            set
            {
                rotateImageAngle = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() => RotateImageAngle.ToString();
    }

    public class MirrorImageParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.图像镜像;
        private eMirrorImageType mirrorImageType;
        [SuperDisplay(Name = "图片镜像", GroupPath = "参数组/图片参数")] 
        public eMirrorImageType MirrorImageType
        {
            get => mirrorImageType;
            set
            {
                mirrorImageType = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() => MirrorImageType.ToString();
    }

    public class ChangeImageParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.修改图像尺寸;
        private int width;
        [SuperDisplay(Name = "图片-宽", GroupPath = "参数组/图像尺寸参数")]
        [NumericRange(0, 255, 2)]
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }
        private int height;
        [SuperDisplay(Name = "图片-高", GroupPath = "参数组/图像尺寸参数")]
        [NumericRange(0, 255, 2)]
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() => $"{Width}×{Height}";
    }

    public class FilterParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.中值滤波;
        private int width;
        [SuperDisplay(Name = "中值滤波-宽", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 255, 2)]
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }
        private int height;
        [SuperDisplay(Name = "中值滤波-高", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 255, 2)]
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }
        private int size;
        [SuperDisplay(Name = "中值滤波-大小", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 255, 2)]
        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() =>
            SubType switch
            {
                eOperatorType.高斯滤波 => $"Size={Size}",
                _ => $"{SubType.ToString()}-{Width}×{Height}",
            };
    }

    public class EnhanceParam : BindableBase, IPerParam
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.锐化;
        private int width = 3;
        [SuperDisplay(Name = "锐化-宽", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 255, 2)]
        public int Width
        {
            get => width;
            set
            {
                width = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private int height = 3;
        [SuperDisplay(Name = "锐化-高", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 255, 2)]
        public int Height
        {
            get => height;
            set
            {
                height = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        // 锐化
        private double emphaFactor = 0.3;
        [SuperDisplay(Name = "锐化-系数", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 1, 0.1)]
        public double EmphaFactor
        {
            get => emphaFactor;
            set
            {
                emphaFactor = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        // 对比度 (Illuminate)
        private double illuminateFactor = 0.7;
        [SuperDisplay(Name = "对比度-系数", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 1, 0.1)]
        public double IlluminateFactor
        {
            get => illuminateFactor;
            set
            {
                illuminateFactor = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        // 亮度调节 (ScaleImage)
        private double scaleMult = 0.1;
        [SuperDisplay(Name = "亮度-系数", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 1, 0.1)]
        public double ScaleMult
        {
            get => scaleMult;
            set
            {
                scaleMult = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        private int scaleAdd = 1;
        [SuperDisplay(Name = "比例-系数", GroupPath = "参数组/滤波参数")]
        [NumericRange(0, 1, 0.1)]
        public int ScaleAdd
        {
            get => scaleAdd;
            set
            {
                scaleAdd = value;
                RaisePropertyChanged(nameof(DisplayValue));
            }
        }

        public override string ToString() =>
            SubType switch
            {
                eOperatorType.锐化 => $"{Width}×{Height} 因子={EmphaFactor:F1}",
                eOperatorType.对比度 => $"{Width}×{Height} 因子={IlluminateFactor:F1}",
                eOperatorType.亮度调节 => $"Mult={ScaleMult:F1} Add={ScaleAdd}",
                _ => $"{Width}×{Height}",
            };
    }
}
