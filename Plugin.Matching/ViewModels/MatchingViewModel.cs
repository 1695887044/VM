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
        Category = "检测识别",
        Icon = "M613.88 650.88h-484.5c-58.93 0-104.76-48.01-104.76-104.76V188.2c0-58.93 48.01-104.76 104.76-104.76H611.7c58.93 0 104.76 48.01 104.76 104.76v357.92c2.18 56.74-45.83 104.76-102.58 104.76zM129.39 127.1c-34.92 0-61.11 28.37-61.11 61.11v357.92c0 34.92 28.37 61.11 61.11 61.11H611.7c34.92 0 61.11-28.37 61.11-61.11V188.2c0-34.92-28.37-61.11-61.11-61.11H129.39z M613.88 656.88h-484.5c-61.07 0-110.76-49.69-110.76-110.76V188.2c0-61.07 49.69-110.76 110.76-110.76H611.7c61.07 0 110.76 49.69 110.76 110.76v357.81c1.06 28.47-9.58 55.82-29.98 77.02-20.7 21.51-49.35 33.85-78.6 33.85zM129.39 89.45c-54.45 0-98.76 44.3-98.76 98.76v357.92c0 54.46 44.3 98.76 98.76 98.76h484.5c26 0 51.5-11 69.95-30.17 18.15-18.86 27.6-43.13 26.63-68.36V188.2c0-54.45-44.3-98.76-98.76-98.76H129.39zM611.7 613.23H129.39c-37 0-67.11-30.1-67.11-67.11V188.2c0-37 30.1-67.11 67.11-67.11H611.7c37 0 67.11 30.1 67.11 67.11v357.92c0 37-30.1 67.11-67.11 67.11zM129.39 133.1c-30.39 0-55.11 24.72-55.11 55.11v357.92c0 30.39 24.72 55.11 55.11 55.11H611.7c30.39 0 55.11-24.72 55.11-55.11V188.2c0-30.39-24.72-55.11-55.11-55.11H129.39z M908.04 928.29h-484.5c-58.93 0-104.76-48.01-104.76-104.76V465.62c0-58.93 48.01-104.76 104.76-104.76h482.32c58.93 0 104.76 48.01 104.76 104.76v357.92c2.18 58.92-45.83 104.75-102.58 104.75zM423.55 404.51c-34.92 0-61.11 28.37-61.11 61.11v357.92c0 34.92 28.37 61.11 61.11 61.11h482.32c34.92 0 61.11-28.37 61.11-61.11V465.62c0-34.92-28.37-61.11-61.11-61.11H423.55z M908.04 934.29h-484.5c-61.07 0-110.76-49.68-110.76-110.76V465.62c0-61.07 49.69-110.76 110.76-110.76h482.31c61.07 0 110.76 49.69 110.76 110.76v357.81c1.05 29.06-9.44 56.54-29.55 77.4-20.48 21.27-49.29 33.46-79.02 33.46zM423.55 366.86c-54.45 0-98.76 44.3-98.76 98.76v357.92c0 54.45 44.3 98.76 98.76 98.76h484.5c26.49 0 52.14-10.86 70.39-29.79 17.85-18.52 27.15-42.93 26.19-68.74V465.62c0-54.45-44.3-98.76-98.76-98.76H423.55z m482.31 523.79H423.55c-37 0-67.11-30.1-67.11-67.11V465.62c0-37 30.1-67.11 67.11-67.11h482.31c37 0 67.11 30.1 67.11 67.11v357.92c0 37-30.11 67.11-67.11 67.11zM423.55 410.51c-30.39 0-55.11 24.72-55.11 55.11v357.92c0 30.39 24.72 55.11 55.11 55.11h482.31c30.39 0 55.11-24.72 55.11-55.11V465.62c0-30.39-24.72-55.11-55.11-55.11H423.55z M849.75 834.83l-80.28-15.71-26.53-77.38 53.75-61.67 80.28 15.71 26.53 77.38-53.75 61.67z m-68.9-28.77l63.28 12.39 42.37-48.61-20.91-61-63.28-12.39-42.37 48.61 20.91 61zM581.27 839.25l-70.84-40.9v-81.8l70.84-40.9 70.84 40.9v81.8l-70.84 40.9z m-55.84-49.56l55.84 32.24 55.84-32.24V725.2l-55.84-32.24-55.84 32.24v64.49zM816.31 597.3L775 565.47l6.91-51.69 48.22-19.86 41.31 31.83-6.91 51.69-48.22 19.86z m-28.49-37.11l30.33 23.37 35.4-14.58 5.07-37.95-30.33-23.37-35.4 14.58-5.07 37.95z M221.88 323.22l-64.8-49.92 10.83-81.08 75.64-31.16 64.8 49.93-10.83 81.08-75.64 31.15z m-48.79-56.52l51.08 39.35 59.62-24.56 8.54-63.92-51.08-39.35-59.62 24.56-8.54 63.92z M549.82 544.87l-2.41 2.41-18.11-18.11c9.71-10.85 15.68-25.14 15.68-40.86 0-33.88-27.47-61.35-61.35-61.35s-61.35 27.47-61.35 61.35 27.47 61.35 61.35 61.35c15.72 0 29.99-5.97 40.86-15.68l18.11 18.11-2.41 2.41 31.45 31.45 9.64-9.64-31.46-31.44zM429.08 488.3c0-30.06 24.47-54.53 54.53-54.53s54.53 24.47 54.53 54.53c0 30.06-24.47 54.53-54.53 54.53-30.07 0.02-54.53-24.45-54.53-54.53z M571.63 591.62l-37.11-37.1 2.42-2.42-12.69-12.69c-11.58 9.22-25.86 14.25-40.63 14.25-36.03 0-65.35-29.32-65.35-65.35 0-36.03 29.32-65.35 65.35-65.35s65.35 29.32 65.35 65.35c0 14.78-5.04 29.07-14.25 40.63l12.68 12.68 2.42-2.42 37.1 37.11-15.29 15.31z m-25.79-37.1l25.79 25.79 3.98-3.98-25.79-25.79-3.98 3.98z m-62.23-116.75c-27.86 0-50.53 22.67-50.53 50.53 0 13.48 5.26 26.17 14.82 35.72 9.55 9.55 22.22 14.81 35.68 14.81h0.03c27.87 0 50.53-22.67 50.53-50.53s-22.67-50.53-50.53-50.53z"
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
                    RoiReigon.HTuples[0] + item.Row,
                    RoiReigon.HTuples[1] + item.Column,
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
