

namespace Plugin.Matching.Models
{
    public class MatchParamer : BindableBase
    {
        private double _MinScore = 0.5;
        /// <summary>
        /// 最小分数
        /// </summary>
        public double MinScore
        {
            get { return _MinScore; }
            set { _MinScore = value; RaisePropertyChanged(); }
        }
        private double _MaxOverlap;

        public double MaxOverlap
        {
            get { return _MaxOverlap; }
            set { _MaxOverlap = value; RaisePropertyChanged(); }
        }
    }
}
