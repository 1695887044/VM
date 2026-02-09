using Plugin.PerProcessing.Common;

namespace Plugin.PerProcessing.Model
{
    public class ThresholdParam:BindableBase
    {
        public string DisplayValue=> this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.二值化;
        private int thresholdLow;

        
        public int ThresholdLow
        {
            get { return thresholdLow; }
            set { thresholdLow = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        private int thresholdHight;

        public int ThresholdHight
        {
            get { return thresholdHight; }
            set { thresholdHight = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private bool thresholdReverse;

        public bool ThresholdReverse
        {
            get { return thresholdReverse; }
            set { thresholdReverse = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        public override string ToString()=> $"{ThresholdLow}:{ThresholdHight}---{ThresholdReverse}";
    }

    public class VarThresholdParam : BindableBase
    {
        public string DisplayValue => this.ToString();

        public eOperatorType SubType { get; set; } = eOperatorType.均值二值化;

        private int varThresholdWidth = 5;
        public int VarThresholdWidth
        {
            get => varThresholdWidth;
            set { varThresholdWidth = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private int varThresholdHeight = 5;
        public int VarThresholdHeight
        {
            get => varThresholdHeight;
            set { varThresholdHeight = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private int varThresholdSkew;
        public int VarThresholdSkew
        {
            get => varThresholdSkew;
            set { varThresholdSkew = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private eVarThresholdType varThresholdType;
        public eVarThresholdType VarThresholdType
        {
            get => varThresholdType;
            set { varThresholdType = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        public override string ToString() =>
            $"{VarThresholdWidth}×{VarThresholdHeight} 偏移={VarThresholdSkew} {VarThresholdType}";
    }

    public class InvertImageParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.反色;
        private bool invertImageLogic;

        public bool InvertImageLogic
        {
            get { return invertImageLogic; }
            set { invertImageLogic = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        public override string ToString() => InvertImageLogic ? "反色启用" : "反色关闭";

    }
    /// <summary>
    /// 形态学参数
    /// </summary>
    public class MorphologyParam: BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.灰度闭运算;
        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() => $"{SubType}{Width}×{Height}";

    }
    public class TransImageParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.彩色转灰;
        private eTransImageType transImageType = eTransImageType.通用比例转换;
        public eTransImageType TransImageType
        {
            get => transImageType;
            set { transImageType = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private eTransImageChannel transImageChannel;
        public eTransImageChannel TransImageChannel
        {
            get => transImageChannel;
            set { transImageChannel = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() => $"{TransImageType} | {TransImageChannel}";

    }
    public class RotateImageParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.图像旋转;
        private eRotateImageAngle rotateImageAngle;
        public eRotateImageAngle RotateImageAngle
        {
            get => rotateImageAngle;
            set { rotateImageAngle = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() => RotateImageAngle.ToString();

    }
    public class MirrorImageParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.图像镜像;
        private eMirrorImageType mirrorImageType;
        public eMirrorImageType MirrorImageType
        {
            get => mirrorImageType;
            set { mirrorImageType = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() => MirrorImageType.ToString();

    }
    public class ChangeImageParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.修改图像尺寸;
        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() => $"{Width}×{Height}";

    }
    public class FilterParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.中值滤波;
        private int width;

        public int Width
        {
            get { return width; }
            set { width = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() =>
            SubType switch
            {
                eOperatorType.高斯滤波 => $"Size={Size}",
                _ => $"{SubType.ToString()}-{Width}×{Height}"
            };
    }
    public class EnhanceParam:BindableBase
    {
        public string DisplayValue => this.ToString();
        public eOperatorType SubType { get; set; } = eOperatorType.中值滤波;
        private int width = 3;
        public int Width
        {
            get => width;
            set { width = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private int height = 3;
        public int Height
        {
            get => height;
            set { height = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        // 锐化
        private double emphaFactor = 0.3;
        public double EmphaFactor
        {
            get => emphaFactor;
            set { emphaFactor = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        // 对比度 (Illuminate)
        private double illuminateFactor = 0.7;
        public double IlluminateFactor
        {
            get => illuminateFactor;
            set { illuminateFactor = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        // 亮度调节 (ScaleImage)
        private double scaleMult = 0.1;
        public double ScaleMult
        {
            get => scaleMult;
            set { scaleMult = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }

        private int scaleAdd = 1;
        public int ScaleAdd
        {
            get => scaleAdd;
            set { scaleAdd = value; RaisePropertyChanged(nameof(DisplayValue)); }
        }
        public override string ToString() =>
           SubType switch
           {
               eOperatorType.锐化 => $"{Width}×{Height} 因子={EmphaFactor:F1}",
               eOperatorType.对比度 => $"{Width}×{Height} 因子={IlluminateFactor:F1}",
               eOperatorType.亮度调节 => $"Mult={ScaleMult:F1} Add={ScaleAdd}",
               _ => $"{Width}×{Height}"
           };
    }
}
