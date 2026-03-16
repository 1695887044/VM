using System.Collections.ObjectModel;
using HalconDotNet;
using Plugin.CreateROI.Views;
using VM.Halcon.Enums;
using VM.Halcon.Extensions;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Controls;
using VM.IPlugin.Models;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.Shard.Attritubess;
using VM.Shard.Services;

namespace Plugin.CreateROI.ViewModels
{
    [Serializable]
    [PluginInfo(
        DisplayName = "ROI",
        PluginName = "ROI",
        View = typeof(CreateROIView),
        ViewModel = typeof(CreateROIViewModel),
        Category = "检测识别",
        Icon = "M85.333333 128a42.666667 42.666667 0 0 1 42.666667-42.666667h96a42.666667 42.666667 0 0 1 0 85.333334H170.666667v53.333333a42.666667 42.666667 0 0 1-85.333334 0V128z m288 0a42.666667 42.666667 0 0 1 42.666667-42.666667h192a42.666667 42.666667 0 1 1 0 85.333334h-192a42.666667 42.666667 0 0 1-42.666667-42.666667z m384 0a42.666667 42.666667 0 0 1 42.666667-42.666667H896a42.666667 42.666667 0 0 1 42.666667 42.666667v96a42.666667 42.666667 0 1 1-85.333334 0V170.666667h-53.333333a42.666667 42.666667 0 0 1-42.666667-42.666667zM128 373.333333a42.666667 42.666667 0 0 1 42.666667 42.666667v192a42.666667 42.666667 0 1 1-85.333334 0v-192a42.666667 42.666667 0 0 1 42.666667-42.666667z m768 0a42.666667 42.666667 0 0 1 42.666667 42.666667v192a42.666667 42.666667 0 1 1-85.333334 0v-192a42.666667 42.666667 0 0 1 42.666667-42.666667z m-768 384a42.666667 42.666667 0 0 1 42.666667 42.666667V853.333333h53.333333a42.666667 42.666667 0 1 1 0 85.333334H128a42.666667 42.666667 0 0 1-42.666667-42.666667v-96a42.666667 42.666667 0 0 1 42.666667-42.666667z m768 0a42.666667 42.666667 0 0 1 42.666667 42.666667V896a42.666667 42.666667 0 0 1-42.666667 42.666667h-96a42.666667 42.666667 0 1 1 0-85.333334H853.333333v-53.333333a42.666667 42.666667 0 0 1 42.666667-42.666667zM373.333333 896a42.666667 42.666667 0 0 1 42.666667-42.666667h192a42.666667 42.666667 0 1 1 0 85.333334h-192a42.666667 42.666667 0 0 1-42.666667-42.666667z"

    )]
    public class CreateROIViewModel : ModuleViewModelBase, ILinkable
    {
        private readonly IMessageService messageService;
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

        private ObservableCollection<DrawingObjectInfo> roiData = new();

        public ObservableCollection<DrawingObjectInfo> RoiData
        {
            get { return roiData; }
            set
            {
                roiData = value;
                RaisePropertyChanged();
            }
        }
        private DataPort<HImage> _srcImage;

        public DataPort<HImage> SrcImage
        {
            get { return _srcImage; }
            set { _srcImage = value; RaisePropertyChanged(); }
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

        Dictionary<DataPort<DrawingObjectInfo>, DataPort<HImage>> roiDict = new();
        private DrawingObjectInfo selectRoi;

        public DrawingObjectInfo SelectRoi
        {
            get { return selectRoi; }
            set
            {
                selectRoi = value;
                RaisePropertyChanged();
                OnSelectRoiChagned();
            }
        }

        private void OnSelectRoiChagned()
        {
            if (SelectRoi == null)
            {
                return;
            }
            var img = CurrentHImage;
            CurrentHImage = null;
            CurrentHImage = img;
            HObject drawObj;
            HOperatorSet.SetColor(HWindow, "red");
            HOperatorSet.GenContourRegionXld(SelectRoi.Hobject, out HObject contours, "border"); //获取绘制对象的轮廓
            HOperatorSet.DispObj(contours, HWindow);
        }

        public DelegateCommand<String> RoiItemOperatorCommand { get; init; }
        public DelegateCommand<LinkPathParam> LinkPathCommand { get; init; }

        public CreateROIViewModel(IMessageService messageService)
        {
            LinkPathCommand = new(LinkMethods);
            RoiItemOperatorCommand = new DelegateCommand<string>(roiItemOperator);
            RoiData.CollectionChanged += RoiData_CollectionChanged;
            this.messageService = messageService;
        }

        private void roiItemOperator(string obj)
        {
            if (obj == "A")
            {
                var str = messageService.ShowPropertyView(SelectRoi.RoiName, true);
                SelectRoi.RoiName = string.IsNullOrEmpty(str) ? SelectRoi.RoiName : str;
            }
            if (obj == "C")
            {
                var data = roiDict.Keys.First(s => s.Value == SelectRoi);
                if (data != null)
                {
                    ModuleData.RemoveVarValue(data);
                    ModuleData.RemoveVarValue(roiDict[data]);
                    roiDict.Remove(data);
                    roiData.Remove(SelectRoi);
                }
            }
            if (obj == "B")
            {
                drawingRoi();
            }
        }

        private async void drawingRoi()
        {
            var img = CurrentHImage;
            HWindow?.ClearWindow();
            CurrentHImage = null;
            CurrentHImage = img;
            if (SelectRoi == null)
            {
                ViewTopText = string.Empty;
                return;
            }

            IsDrawing = true;
            HObject drawObj;
            HOperatorSet.GenEmptyObj(out drawObj);
            HOperatorSet.SetColor(HWindow, "blue");

            await Task.Run(() =>
            {
                switch (SelectRoi.ShapeType)
                {
                    case DrawShapeType.Rectangle:
                    {
                        HOperatorSet.DrawRectangle1Mod(
                            HWindow,
                            SelectRoi.HTuples[0],
                            SelectRoi.HTuples[1],
                            SelectRoi.HTuples[2],
                            SelectRoi.HTuples[3],
                            out SelectRoi.HTuples[0],
                            out SelectRoi.HTuples[1],
                            out SelectRoi.HTuples[2],
                            out SelectRoi.HTuples[3]
                        );
                        drawObj = SelectRoi.HTuples.GenRectangle();
                        break;
                    }
                    case DrawShapeType.Ellipse:
                    {
                        HOperatorSet.DrawEllipseMod(
                            HWindow,
                            SelectRoi.HTuples[0],
                            SelectRoi.HTuples[1],
                            SelectRoi.HTuples[2],
                            SelectRoi.HTuples[3],
                            SelectRoi.HTuples[4],
                            out SelectRoi.HTuples[0],
                            out SelectRoi.HTuples[1],
                            out SelectRoi.HTuples[2],
                            out SelectRoi.HTuples[3],
                            out SelectRoi.HTuples[4]
                        );
                        drawObj = SelectRoi.HTuples.GenEllipse();
                        break;
                    }
                    case DrawShapeType.Circle:
                    {
                        HOperatorSet.DrawCircleMod(
                            HWindow,
                            SelectRoi.HTuples[0],
                            SelectRoi.HTuples[1],
                            SelectRoi.HTuples[2],
                            out SelectRoi.HTuples[0],
                            out SelectRoi.HTuples[1],
                            out SelectRoi.HTuples[2]
                        );
                        drawObj = SelectRoi.HTuples.GenCircle();
                        break;
                    }
                }
                if (drawObj == null)
                    return;
                SelectRoi.Hobject = drawObj;
                HOperatorSet.GenContourRegionXld(drawObj, out HObject contours, "border"); //获取绘制对象的轮廓
                HOperatorSet.DispObj(contours, HWindow);
            });
            IsDrawing = false;
        }

        private void RoiData_CollectionChanged(
            object? sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e
        )
        {
            DrawingObjectInfo? _roi;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    _roi = e?.NewItems[0] as DrawingObjectInfo;
                    if (_roi == null)
                        return;
                    //roiDict[ModuleData.AppendOutVar("Roi", "HRegion", _roi)] = ModuleData.AppendOutVar("Roi", "HImage", CurrentHImage.ReduceDomain(_roi.Hobject).CropDomain().ToHimage());
                    break;
            }
        }

        private void LinkMethods(LinkPathParam param)
        {
            if (param.PathType == VM.IPlugin.Enums.LinkPathType.Link)
            {
                OpenVarLinkView<HImage>(s =>
                {
                    CurrentHImage = s.varValue.Value;
                    SrcImage = s.varValue;
                });
            }
        }

        protected override bool Execute()
        {
            return true;
        }
    }
}
