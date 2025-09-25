

namespace VM.Start.Models.Recipes
{
    [Serializable]
    public class SystemConfigModel:ModelBase
    {
        #region prop
        private string _License;
        /// <summary>
        /// 永久秘钥
        /// </summary>
        public string License
        {
            get { return _License; }
            set { _License = value; this.RaisePropertyChanged(); }
        }
        private string _ProbationLicense;
        /// <summary>
        /// 试用期秘钥
        /// </summary>
        public string ProbationLicense
        {
            get { return _ProbationLicense; }
            set { _ProbationLicense = value; this.RaisePropertyChanged(); }
        }
        private int _ProbationNum = 0;
        /// <summary>
        /// 试用期次数
        /// </summary>
        public int ProbationNum
        {
            get { return _ProbationNum; }
            set { _ProbationNum = value; this.RaisePropertyChanged(); }
        }
        private int _ProbationTime = 0;
        /// <summary>
        /// 试用期时间
        /// </summary>
        public int ProbationTime
        {
            get { return _ProbationTime; }
            set { _ProbationTime = value; this.RaisePropertyChanged(); }
        }
        private string _CurrentCultureName ;
        /// <summary>
        /// 当前语言
        /// </summary>
        public string CurrentCultureName
        {
            get { return _CurrentCultureName; }
            set { _CurrentCultureName = value; this.RaisePropertyChanged(); }
        }
        private int _SoftwareVersion;

        public int SoftwareVersion
        {
            get { return _SoftwareVersion; }
            set { _SoftwareVersion = value; this.RaisePropertyChanged(); }
        }
        private bool _AutoLoadLayout;
        /// <summary>
        /// 自动加载布局
        /// </summary>
        public bool AutoLoadLayout
        {
            get { return _AutoLoadLayout; }
            set { _AutoLoadLayout = value; this.RaisePropertyChanged(); }
        }

        /// <summary>
        /// 方案是否自动加载
        /// </summary>
        private bool _SolutionAutoLoad;
        public bool SolutionAutoLoad
        {
            get { return _SolutionAutoLoad; }
            set { _SolutionAutoLoad = value; this.RaisePropertyChanged(); }
        }
        /// <summary>
        /// 方案是否自动运行
        /// </summary>
        private bool _SolutionAutoRun;
        public bool SolutionAutoRun
        {
            get { return _SolutionAutoRun; }
            set { _SolutionAutoRun = value; this.RaisePropertyChanged(); }
        }
        /// <summary>
        /// 方案链接路径
        /// </summary>
        private string _SolutionPathText;
        public string SolutionPathText
        {
            get { return _SolutionPathText; }
            set { _SolutionPathText = value; this.RaisePropertyChanged(); }
        }
        /// <summary>
        /// 软件是否自动打开
        /// </summary>
        private bool _SoftwareAutoStartup;
        public bool SoftwareAutoStartup
        {
            get { return _SoftwareAutoStartup; }
            set { _SoftwareAutoStartup = value; this.RaisePropertyChanged(); }
        }

        private int _CurrentRecipeIndex;
        /// <summary>
        /// 当前配方序号
        /// </summary>
        public int CurrentRecipeIndex
        {
            get { return _CurrentRecipeIndex; }
            set { _CurrentRecipeIndex = value; this.RaisePropertyChanged(); }
        }
        private string _CurrentRecipe = "PersistentVar.rep";
        /// <summary>
        /// 当前配方名称
        /// </summary>
        public string CurrentRecipe
        {
            get { return _CurrentRecipe; }
            set { _CurrentRecipe = value; this.RaisePropertyChanged(); }
        }
        private string _CurrentVersion;
        /// <summary>
        /// 当前软件版本
        /// </summary>

        public string CurrentVersion
        {
            get { return _CurrentVersion; }
            set { _CurrentVersion = value; this.RaisePropertyChanged(); }
        }

        private string _CompanyName = "威克锐光电科技（苏州）有限公司";
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; this.RaisePropertyChanged(); }
        }
        private string _ProjectName = "准同步设备";
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName
        {
            get { return _ProjectName; }
            set { _ProjectName = value; this.RaisePropertyChanged(); }
        }
        private string _ComputerName = "威克锐光电科技（苏州）有限公司";
        /// <summary>
        /// 电脑名
        /// </summary>
        public string ComputerName
        {
            get { return _ComputerName; }
            set { _ComputerName = value; this.RaisePropertyChanged(); }
        }

        private string _SoftwareIcoPath ;
        /// <summary>
        /// 软件图标
        /// </summary>
        public string SoftwareIcoPath
        {
            get { return _SoftwareIcoPath; }
            set { _SoftwareIcoPath = value; this.RaisePropertyChanged(); }
        }
        private int _TotalNum;

        public int TotalNum
        {
            get { return _TotalNum; }
            set { _TotalNum = value; this.RaisePropertyChanged(); }
        }
        private int _OKNum;

        public int OKNum
        {
            get { return _OKNum; }
            set { _OKNum = value; this.RaisePropertyChanged(); }
        }
        private int _NGNum;

        public int NGNum
        {
            get { return _NGNum; }
            set { _NGNum = value; this.RaisePropertyChanged(); }
        }
        private double _Yield;
        /// <summary>
        /// 良率
        /// </summary>
        public double Yield
        {
            get { return _Yield; }
            set { _Yield = value; this.RaisePropertyChanged(); }
        }
        private bool _IsShieldBuzzer = false;
        #endregion
    }
}
