using HalconDotNet;
using Microsoft.Win32;
using Plugin.Matching.Models;
using Plugin.Matching.Services;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using VM.Halcon.Extensions;
using VM.Halcon.Models;
using VM.IPlugin;
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
        LocalDeformable
    }
    enum RegionMode
    {
        [Description("矩形")]
        Rectgion1,
        [Description("矩形-带方向")]
        Rectgion2
    }
    public class MatchingViewModel : ModuleViewModelBase
    {
        #region //View 关于Halcon的变量
        private bool isDrawing;

        public bool IsDrawing
        {
            get { return isDrawing; }
            set { isDrawing = value; RaisePropertyChanged(); }
        }


        private string topText;

        public string ViewTopText
        {
            get { return topText; }
            set { topText = value; RaisePropertyChanged(); }
        }
        private HImage templateImage;

        public HImage TemplateImage
        {
            get { return templateImage; }
            set { templateImage = value; RaisePropertyChanged(); }
        }

        private HImage currentHImage;

        public HImage CurrentHImage
        {
            get { return currentHImage; }
            set { currentHImage = value; RaisePropertyChanged(); }
        }
        public HWindow HWindow { get; set; }
        public HWindow TemplateHWindow { set; get; }
        public DrawingObjectInfo RoiReigon { get; set; }
        #endregion

        #region 命令
        public DelegateCommand<string> SelectOperatorCommand { get; init; }
        public DelegateCommand<Tuple<string,string>> LabelLinkCommand { get; init; }
        public DelegateCommand<String> MathchOperatorCommand { get; init; }

        private VarValue<HImage> curHImage;

        public VarValue<HImage> CurHImage
        {
            get { return curHImage; }
            set { curHImage = value; RaisePropertyChanged(); }
        }
        private VarValue<DrawingObjectInfo> curRoi;

        public VarValue<DrawingObjectInfo> CurRoi
        {
            get { return curRoi; }
            set { curRoi = value; RaisePropertyChanged(); }
        }

        public bool UseRoi { get; set; }
        #endregion

        private string matchType="1";

        public string MatchType
        {
            get { return matchType; }
            set { matchType = value; RaisePropertyChanged(); }
        }

        private VarValue<DrawingObjectInfo> _currentRegion;

        public VarValue<DrawingObjectInfo> CurrentRegion
        {
            get { return _currentRegion; }
            set { _currentRegion = value; RaisePropertyChanged(); }
        }

        public ITemplateMatchService MathchingService { get; set; }
        public MatchingViewModel()
        {
            SelectOperatorCommand =new DelegateCommand<string>(SelectOperator);
            LabelLinkCommand = new (LinkBindMethod);
            MathchOperatorCommand =new DelegateCommand<string>(MathchOperator);
            MathchingService = MatchingStrategyFactory.CreateStrtegy(MatchType);
        }

         void LinkBindMethod(Tuple<string, string> tuple)
        {
            if (tuple.Item1.Equals("Link"))
            {
                var openLinkargs = new OpenLinkargs();
                switch (tuple.Item2)
                {
                    case "Img":
                        openLinkargs.Fiter = (s => s.DataType == "HImage");
                        openLinkargs.CallBack = s => {

                            if (s.varValue is VarValue<HImage> d)
                            {
                                CurHImage = d;
                                CurrentHImage = d.Value;
                            }
                        };
                        break;
                    case "Roi":
                        openLinkargs.Fiter = (s => s.DataType == "HRegion");
                        openLinkargs.CallBack = s => {

                            if (s.varValue is VarValue<DrawingObjectInfo> d)
                            {
                                CurRoi = d;
                            }
                        };
                        break;
                }
                OpenVarLinkView(openLinkargs);
            }
            else
            {

            }
          
           
            
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
                   await  createModel(); break;
                case "Save":
                     MathchingService.SaveTemplate(); break;
                case "Load":
                     MathchingService.LoadTemplate(); break;
            }
          
        }


       

        async  Task createModel()
        {

            ViewTopText = "请在图像上绘制模板区域";
            if (CurrentHImage == null) return;
            IsDrawing = true;
            var t = CurrentHImage.CopyImage();
            CurrentHImage = null;
            CurrentHImage = t;
            await Task.Run(() =>
            {
                HObject drawObj;
                HOperatorSet.GenEmptyObj(out drawObj);
                HOperatorSet.SetColor(HWindow, "blue");
                var hTuples = new HTuple[4];
                HOperatorSet.DrawRectangle1(HWindow, out hTuples[0], out hTuples[1], out hTuples[2], out hTuples[3]);
                drawObj = hTuples.GenRectangle();
                ViewTopText = string.Empty;
                RoiReigon = new DrawingObjectInfo(VM.Halcon.Enums.DrawShapeType.Rectangle, drawObj, hTuples);
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
                HOperatorSet.VectorAngleToRigid(0, 0, 0, item.Row, item.Column, item.Angle, out var tempMat2D);
                HOperatorSet.AffineTransContourXld(item.Contours, out HObject transformedContours, tempMat2D);
                TemplateHWindow.DispObj(transformedContours);
                HOperatorSet.VectorAngleToRigid(0, 0, 0, item.Row + RoiReigon.HTuples[0], item.Column + RoiReigon.HTuples[1], item.Angle, out var tempMat2D1);
                HOperatorSet.AffineTransContourXld(item.Contours, out HObject transformedContours1, tempMat2D1);
                HWindow.DispObj(transformedContours1);
                HWindow.DispCross(item.Row + RoiReigon.HTuples[0], item.Column + RoiReigon.HTuples[1], 30, 0);
            }
        }

        /// <summary>
        /// 切换区域选项
        /// </summary>
        /// <param name="obj"></param>
        private void SelectOperator(string obj)
        {
            //
            switch (obj)
            {
                case "RegionLink":
                    OpenVarLinkView((s) => s.DataType == "Region", (s) => {
                        if (s.varValue is VarValue<DrawingObjectInfo> d)
                        {
                            CurrentRegion = d;
                        }

                    });
                    break;
            }
        }

        public override bool Cancel()
        {
            return true;
        }

        public override bool Confirm()
        {
            return true;
        }

        public  override bool Execute()
        {
             if (MathchingService == null) return false;
             MathchingService?.Run(CurrentHImage);
            foreach (var item in MathchingService.MatchResults)
            {
                HOperatorSet.VectorAngleToRigid(0, 0, 0, item.Row + RoiReigon.HTuples[0], item.Column + RoiReigon.HTuples[1], item.Angle, out var tempMat2D1);
                HOperatorSet.AffineTransContourXld(item.Contours, out HObject transformedContours1, tempMat2D1);
                HWindow.DispObj(transformedContours1);
                HWindow.DispCross(item.Row + RoiReigon.HTuples[0], item.Column + RoiReigon.HTuples[1], 30, 0);
            }
            return true;
        }

        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
           
        }
    }
}
