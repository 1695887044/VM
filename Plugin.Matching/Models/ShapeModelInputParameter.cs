using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plugin.Matching.Models
{
    public class BaseParameter : BindableBase
    {
        public virtual void ApplyDefaultParameter() { }
    }
    /// <summary>
    /// 形状匹配模板参数
    /// </summary>
    public class ShapeModelInputParameter : BaseParameter
    {
        private string numLevels;
        private double angleStart;
        private double angleExtent;
        private string angleStep;
        private string optimization;
        private string metric;
        private string contrast;
        private string minContrast;

        /// <summary>
        /// 金字塔层数
        /// </summary>
        public string NumLevels
        {
            get { return numLevels; }
            set { numLevels = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转起始角度
        /// </summary>
        public double AngleStart
        {
            get { return angleStart; }
            set { angleStart = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转角度范围
        /// </summary>
        public double AngleExtent
        {
            get { return angleExtent; }
            set { angleExtent = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 旋转角度步长
        /// </summary>
        public string AngleStep
        {
            get { return angleStep; }
            set { angleStep = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板优化方法
        /// </summary>
        public string Optimization
        {
            get { return optimization; }
            set { optimization = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 匹配方法
        /// </summary>
        public string Metric
        {
            get { return metric; }
            set { metric = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 对比度
        /// </summary>
        public string Contrast
        {
            get { return contrast; }
            set { contrast = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 最小对比度
        /// </summary>
        public string MinContrast
        {
            get { return minContrast; }
            set { minContrast = value; RaisePropertyChanged(); }
        }
        public override void ApplyDefaultParameter()
        {
            NumLevels = "auto";
            AngleStart = 0;
            AngleExtent = 360;
            AngleStep = "auto";
            Optimization = "auto";
            Metric = "use_polarity";
            Contrast = "auto";
            MinContrast = "auto";
        }
    }

    public class ShapeModelRunParameter : BaseParameter
    {
        private int numLevels;
        private double angleStart;
        private double angleExtent;
        private double minScore;
        private double greediness;
        private string subPixel;
        private double maxOverlap;
        private int numMatches;

        /// <summary>
        /// 匹配个数
        /// </summary>
        public int NumMatches
        {
            get { return numMatches; }
            set { numMatches = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 重叠率
        /// </summary>
        public double MaxOverlap
        {
            get { return maxOverlap; }
            set { maxOverlap = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 精度设置
        /// </summary>
        public string SubPixel
        {
            get { return subPixel; }
            set { subPixel = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 贪婪程度
        /// </summary>
        public double Greediness
        {
            get { return greediness; }
            set { greediness = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转起始角度
        /// </summary>
        public double MinScore
        {
            get { return minScore; }
            set { minScore = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转起始角度
        /// </summary>
        public double AngleStart
        {
            get { return angleStart; }
            set { angleStart = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 模板旋转角度范围
        /// </summary>
        public double AngleExtent
        {
            get { return angleExtent; }
            set { angleExtent = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 金字塔层数
        /// </summary>
        public int NumLevels
        {
            get { return numLevels; }
            set { numLevels = value; RaisePropertyChanged(); }
        }

        public  override void ApplyDefaultParameter()
        {
            AngleStart = 0;
            AngleExtent = 360;
            MinScore = 0.5;
            NumMatches = 1;
            MaxOverlap = 0.5;
            SubPixel = "least_squares";
            NumLevels = 0;
            Greediness = 0.9;
        }
    }
    public class TemplateMatchResult
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Row坐标
        /// </summary>
        public double Row { get; set; }

        /// <summary>
        /// Column坐标
        /// </summary>
        public double Column { get; set; }

        /// <summary>
        /// 角度
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// 匹配分数
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// 匹配结果
        /// </summary>
        public HObject Contours { get; set; }
    }
}
