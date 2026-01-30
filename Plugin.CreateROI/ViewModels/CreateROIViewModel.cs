using HalconDotNet;
using System.Collections.ObjectModel;
using VM.Halcon.Enums;
using VM.Halcon.Extensions;
using VM.Halcon.Models;
using VM.IPlugin;
using VM.IPlugin.Models.VarModels;
using VM.IPlugin.ModuleEvent;
using VM.IPlugin.Services;

namespace Plugin.CreateROI.ViewModels
{
    public class CreateROIViewModel : ModuleViewModelBase
    {
        private bool isDrawing;

        public bool IsDrawing
        {
            get { return isDrawing; } 
            set { isDrawing = value;RaisePropertyChanged(); }
        }


        private string topText;

        public string ViewTopText
        {
            get { return topText; }
            set { topText = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<DrawingObjectInfo> roiData = new();

        public ObservableCollection<DrawingObjectInfo> RoiData
        {
            get { return roiData; }
            set { roiData = value; RaisePropertyChanged(); }
        }



        private string linkPath;

        public string LinkPath
        {
            get { return linkPath; }
            set { linkPath = value; RaisePropertyChanged(); }
        }
        private HImage currentHImage;

        public HImage CurrentHImage
        {
            get { return currentHImage; }
            set { currentHImage = value; RaisePropertyChanged(); }
        }
        public HWindow HWindow { get; set; }

         Dictionary<VarValue<DrawingObjectInfo>, VarValue<HImage>> roiDict = new();
        private DrawingObjectInfo selectRoi;

        public DrawingObjectInfo SelectRoi
        {
            get { return selectRoi; }
            set { selectRoi = value; RaisePropertyChanged(); OnSelectRoiChagned(); }
        }
        private    void OnSelectRoiChagned()
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
        public DelegateCommand<String> RoiItemOperatorCommand {  get; init; }
        public DelegateCommand<string> LinkPathCommand { get; init; }
        public CreateROIViewModel(IMessageService messageService)
        {
            LinkPathCommand = new DelegateCommand<string>(LinkMethod);
            RoiItemOperatorCommand = new DelegateCommand<string>(roiItemOperator);
            RoiData.CollectionChanged += RoiData_CollectionChanged;
            this.messageService = messageService;
        }

        private void roiItemOperator(string obj)
        {
            if(obj == "A")
            {
               var str =  messageService.InputShow("ROI区域重命名",SelectRoi.RoiName);
                SelectRoi.RoiName = string.IsNullOrEmpty(str) ? SelectRoi.RoiName : str;

            }
            if (obj == "C")
            {
                var data =  roiDict.Keys.First(s => s.Value == SelectRoi);
                if(data != null)
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
                            HOperatorSet.DrawRectangle1Mod(HWindow, SelectRoi.HTuples[0], SelectRoi.HTuples[1], SelectRoi.HTuples[2], SelectRoi.HTuples[3],
                                out SelectRoi.HTuples[0], out SelectRoi.HTuples[1], out SelectRoi.HTuples[2], out SelectRoi.HTuples[3]);
                            drawObj = SelectRoi.HTuples.GenRectangle();
                            break;
                        }
                    case DrawShapeType.Ellipse:
                        {
                            HOperatorSet.DrawEllipseMod(HWindow, SelectRoi.HTuples[0], SelectRoi.HTuples[1], SelectRoi.HTuples[2], SelectRoi.HTuples[3], SelectRoi.HTuples[4],
                                out SelectRoi.HTuples[0], out SelectRoi.HTuples[1], out SelectRoi.HTuples[2], out SelectRoi.HTuples[3], out SelectRoi.HTuples[4]);
                            drawObj = SelectRoi.HTuples.GenEllipse();
                            break;
                        }
                    case DrawShapeType.Circle:
                        {
                            HOperatorSet.DrawCircleMod(HWindow, SelectRoi.HTuples[0], SelectRoi.HTuples[1], SelectRoi.HTuples[2],
                                out SelectRoi.HTuples[0], out SelectRoi.HTuples[1], out SelectRoi.HTuples[2]);
                            drawObj = SelectRoi.HTuples.GenCircle();
                            break;
                        }
                }
                if (drawObj == null) return;
                SelectRoi.Hobject = drawObj;
                HOperatorSet.GenContourRegionXld(drawObj, out HObject contours, "border"); //获取绘制对象的轮廓
                HOperatorSet.DispObj(contours, HWindow);
            });
            IsDrawing = false;
        }

        private void RoiData_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DrawingObjectInfo? _roi ;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    _roi = e?.NewItems[0] as DrawingObjectInfo;
                    if (_roi == null) return;
                    //roiDict[ModuleData.AppendOutVar("Roi", "HRegion", _roi)] = ModuleData.AppendOutVar("Roi", "HImage", CurrentHImage.ReduceDomain(_roi.Hobject).CropDomain().ToHimage());
                    break;
            }
        }

        VarValue<HImage> _linkvar;
        private readonly IMessageService messageService;

        private void LinkMethod(string obj)
        {
            if (obj == "Link")
            {
                OpenLinkargs openLinkargs = new OpenLinkargs();
                openLinkargs.guid = ModuleData.ModuleGuid;
                openLinkargs.name = "GrabImage";
                openLinkargs.Fiter = (s => s.DataType == "HImage");
                OpenVarLinkView(openLinkargs);
                return;
            }
            if (_linkvar != null)
            {
                _linkvar.OnValueChanged -= Linkvar_OnValueChanged;
                _linkvar = null;
                LinkPath = string.Empty;
            }
        }
        private void Linkvar_OnValueChanged(object? sender, HImage e)
        {
            CurrentHImage = e;
        }
        public override bool Cancel()
        {
            return true;
        }

        public override bool Confirm()
        {
            return true;
        }

        public override bool Execute()
        {
            return true;
        }

        public override void OnLinkVarPathChanged(VarChangedEventParamModel changedEvent)
        {
            if (changedEvent.varValue is VarValue<HImage> linkvar)
            {
                _linkvar = linkvar;
                _linkvar.OnValueChanged += Linkvar_OnValueChanged;
                LinkPath = $"{_linkvar.LinkPath}&&{_linkvar.Name}";
                CurrentHImage = _linkvar.Value;
            }
        }
    }
}
