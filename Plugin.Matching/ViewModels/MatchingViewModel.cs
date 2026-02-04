using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using HalconDotNet;
using Microsoft.Win32;
using Plugin.Matching.Models;
using Plugin.Matching.Services;
using VM.Halcon.Extensions;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;

namespace Plugin.Matching.ViewModels
{
    enum MatchMode
    {
        [Description("形状模板匹配")]
        ShapeModel,

        [Description("灰度模板匹配")]
        NccModel,

        [Description("局部形变匹配")]
        LocalDeformable,
    }

    enum RegionMode
    {
        [Description("矩形")]
        Rectgion1,

        [Description("矩形-带方向")]
        Rectgion2,
    }

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

        private VarValue<HImage> curHImage;

        public VarValue<HImage> CurHImage
        {
            get { return curHImage; }
            set
            {
                curHImage = value;
                RaisePropertyChanged();
            }
        }
        private VarValue<DrawingObjectInfo> curRoi;

        public VarValue<DrawingObjectInfo> CurRoi
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

        private VarValue<DrawingObjectInfo> _currentRegion;

        public VarValue<DrawingObjectInfo> CurrentRegion
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
            ViewTopText = "请在图像上绘制模板区域";
            if (CurrentHImage == null)
                return;
            IsDrawing = true;
            var t = CurrentHImage.CopyImage();
            CurrentHImage = null;
            CurrentHImage = t;
            await Task.Run(() =>
            {
                HObject drawObj;
                HOperatorSet.GenEmptyObj(out drawObj);
                HOperatorSet.SetColor(HWindow, "blue");
                var hTuples = new HTuple[5];
                HOperatorSet.DrawRectangle2(
                    HWindow,
                    out hTuples[0],
                    out hTuples[1],
                    out hTuples[2],
                    out hTuples[3],
                    out hTuples[4]
                );
                drawObj = hTuples.GenRectangle2();
                ViewTopText = string.Empty;
                RoiReigon = new DrawingObjectInfo(
                    VM.Halcon.Enums.DrawShapeType.Rectangle,
                    drawObj,
                    hTuples
                );
            });
            IsDrawing = false;

            //使用模板
            MathchingService.Roi = RoiReigon;
            await MathchingService.CreateTemplate(CurrentHImage, RoiReigon.Hobject);
            MathchingService.Run(CurrentHImage);
            //仿射区域
            TemplateImage = CurrentHImage.ReduceDomain(RoiReigon.Hobject).CropDomain().ToHimage();
            TemplateHWindow.SetDraw("margin");
            TemplateHWindow.SetColor("green");
            foreach (var item in MathchingService.MatchResults)
            {
                HOperatorSet.VectorAngleToRigid(
                    0,
                    0,
                    0,
                    item.Row,
                    item.Column,
                    item.Angle,
                    out var tempMat2D
                );
                HOperatorSet.AffineTransContourXld(
                    item.Contours,
                    out HObject transformedContours,
                    tempMat2D
                );
                TemplateHWindow.DispObj(transformedContours);
                HOperatorSet.VectorAngleToRigid(
                    0,
                    0,
                    0,
                    item.Row + RoiReigon.HTuples[0],
                    item.Column + RoiReigon.HTuples[1],
                    item.Angle + RoiReigon.HTuples[2],
                    out var tempMat2D1
                );
                HOperatorSet.AffineTransContourXld(
                    item.Contours,
                    out HObject transformedContours1,
                    tempMat2D1
                );
                HWindow.DispObj(transformedContours1);
                HWindow.DispCross(
                    item.Row + RoiReigon.HTuples[0],
                    item.Column + RoiReigon.HTuples[1],
                    30,
                    0
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
                // HOperatorSet.VectorAngleToRigid(0, 0, 0, item.Row + RoiReigon.HTuples[0], item.Column + RoiReigon.HTuples[1], item.Angle + RoiReigon.HTuples[2], out var tempMat2D1);
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
