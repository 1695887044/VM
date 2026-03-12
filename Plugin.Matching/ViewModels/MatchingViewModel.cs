using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using HalconDotNet;
using Microsoft.Win32;
using Plugin.Matching.Models;
using Plugin.Matching.Services;
using Plugin.Matching.Views;
using VM.Halcon.Extensions;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;

namespace Plugin.Matching.ViewModels
{
    [PluginInfo(
        DisplayName = "模板匹配",
        PluginName = "Matching",
        View = typeof(MatchingView),
        ViewModel = typeof(MatchingViewModel),
        Category = "检测识别"
    )]
    public class MatchingViewModel : ModuleViewModelBase
    {
        #region //View 关于Halcon的变量
        private bool isDrawing;

        public bool IsDrawing
        {
            get { return isDrawing; }
            set
            {
                isDrawing = value;
                RaisePropertyChanged();
            }
        }

        private string topText;

        public string ViewTopText
        {
            get { return topText; }
            set
            {
                topText = value;
                RaisePropertyChanged();
            }
        }
        private HImage templateImage;

        public HImage TemplateImage
        {
            get { return templateImage; }
            set
            {
                templateImage = value;
                RaisePropertyChanged();
            }
        }

        private HImage currentHImage;

        public HImage CurrentHImage
        {
            get { return currentHImage; }
            set
            {
                currentHImage = value;
                RaisePropertyChanged();
            }
        }
        public HWindow HWindow { get; set; }
        public HWindow TemplateHWindow { set; get; }
        public DrawingObjectInfo RoiReigon { get; set; }
        #endregion

        #region 命令
        public DelegateCommand<LinkPathParam> LabelLinkCommand { get; init; }
        public DelegateCommand<String> MathchOperatorCommand { get; init; }

        private DataPort<HImage> curHImage;

        public DataPort<HImage> CurHImage
        {
            get { return curHImage; }
            set
            {
                curHImage = value;
                RaisePropertyChanged();
            }
        }
        private DataPort<DrawingObjectInfo> curRoi;

        public DataPort<DrawingObjectInfo> CurRoi
        {
            get { return curRoi; }
            set
            {
                curRoi = value;
                RaisePropertyChanged();
            }
        }

        public bool UseRoi { get; set; }
        #endregion

        private string matchType = "1";

        public string MatchType
        {
            get { return matchType; }
            set
            {
                matchType = value;
                RaisePropertyChanged();
            }
        }

        private DataPort<DrawingObjectInfo> _currentRegion;

        public DataPort<DrawingObjectInfo> CurrentRegion
        {
            get { return _currentRegion; }
            set
            {
                _currentRegion = value;
                RaisePropertyChanged();
            }
        }

        public ITemplateMatchService MathchingService { get; set; }

        public MatchingViewModel()
        {
            LabelLinkCommand = new(LinkBindMethod);
            MathchOperatorCommand = new DelegateCommand<string>(MathchOperator);
            MathchingService = MatchingStrategyFactory.CreateStrtegy(MatchType);
        }

        void LinkBindMethod(LinkPathParam p1)
        {
            if (p1.PathType == VM.IPlugin.Enums.LinkPathType.Link)
            {
                switch (p1.Param)
                {
                    case "Img":
                        OpenVarLinkView<HImage>(s =>
                        {
                            CurrentHImage = s.varValue.Value;
                            CurHImage = s.varValue;
                        });
                        break;
                    case "Roi":
                        OpenVarLinkView<DrawingObjectInfo>(s => CurRoi = s.varValue);
                        break;
                }
            }
            else { }
        }

        /// <summary>
        /// 创建模板方法
        /// </summary>
        /// <param name="obj"></param>
        private async void MathchOperator(string obj)
        {
            switch (obj)
            {
                case "Create":
                    await createModel();
                    break;
                case "Save":
                    MathchingService.SaveTemplate();
                    break;
                case "Load":
                    MathchingService.LoadTemplate();
                    break;
            }
        }

        async Task createModel()
        {
            if (CurrentHImage == null)
                return;
            ViewTopText = "请在图像上绘制模板区域";
            IsDrawing = true;
            var t = CurrentHImage.CopyImage();
            CurrentHImage = null;
            CurrentHImage = t;
            RoiReigon = await HWindow.DrawShapeAsync();
            IsDrawing = false;
            ViewTopText = string.Empty;
            //使用模板
            MathchingService.Roi = RoiReigon;
            await MathchingService.CreateTemplate(CurrentHImage, RoiReigon.Hobject);
            MathchingService.Run(CurrentHImage);
            //仿射区域 有两个区域要显示 模板 ||  仿射匹配结果
            TemplateImage = CurrentHImage.ReduceDomain(RoiReigon.Hobject).CropDomain().ToHimage();
            TemplateHWindow.SetDraw("margin");
            TemplateHWindow.SetColor("green");
            foreach (var item in MathchingService.MatchResults)
            {
                TemplateHWindow.TransformAndDisplay(
                    item.Contours,
                    0,
                    0,
                    0,
                    item.Row,
                    item.Column,
                    item.Angle
                );
                //这边是从中心点开始算的
                //矩形计算中点
                var center = RoiReigon.GetDrawObjectCenter();
                HWindow.TransformAndDisplay(
                    item.Contours,
                    0,
                    0,
                    0,
                    RoiReigon.HTuples[0]+ item.Row,
                      RoiReigon.HTuples[1]+ item.Column,
                    item.Angle
                );
            }
        }

        protected override bool Execute()
        {
            if (MathchingService == null)
                return false;
            MathchingService.Roi = null;
            MathchingService?.Run(CurrentHImage);
            foreach (var item in MathchingService.MatchResults)
            {
                HOperatorSet.VectorAngleToRigid(
                    0,
                    0,
                    0,
                    item.Row,
                    item.Column,
                    item.Angle,
                    out var tempMat2D1
                );
                HOperatorSet.AffineTransContourXld(
                    item.Contours,
                    out HObject transformedContours1,
                    tempMat2D1
                );
                HWindow.DispObj(transformedContours1);
            }
            return true;
        }
    }
}
