

using HalconDotNet;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using VM.Halcon.Enums;
using VM.Halcon.Extensions;
using VM.Halcon.Models;

namespace VM.Halcon.Controls
{
    [TemplatePart(Name = "PART_Halcon", Type = typeof(HSmartWindowControlWPF))]
    public class ImageEdit : Control
    {
        #region //Ctl
        private HSmartWindowControlWPF hSmart;
        private HWindow hWindow;
        private HImage hImage;
        StringBuilder sb = new StringBuilder();
        public int HImageWidth, HImageHeight;
        #endregion

        #region //Dp
        public string TopText
        {
            get { return (string)GetValue(TopTextProperty); }
            set { SetValue(TopTextProperty, value); }
        }
        public static readonly DependencyProperty TopTextProperty =
            DependencyProperty.Register("TopText", typeof(string), typeof(ImageEdit), new PropertyMetadata(string.Empty));



        public bool DisplayCross
        {
            get { return (bool)GetValue(DisplayCrossProperty); }
            set { SetValue(DisplayCrossProperty, value); }
        }
        public static readonly DependencyProperty DisplayCrossProperty =
            DependencyProperty.Register("DisplayCross", typeof(bool), typeof(ImageEdit), new PropertyMetadata(false));

        public bool DisplayImageInfo
        {
            get { return (bool)GetValue(DisplayImageInfoProperty); }
            set { SetValue(DisplayImageInfoProperty, value); }
        }
        public static readonly DependencyProperty DisplayImageInfoProperty =
            DependencyProperty.Register("DisplayImageInfo", typeof(bool), typeof(ImageEdit), new PropertyMetadata(false));
        public bool ContextEnable
        {
            get { return (bool)GetValue(ContextEnableProperty); }
            set { SetValue(ContextEnableProperty, value); }
        }

        public static readonly DependencyProperty ContextEnableProperty =
            DependencyProperty.Register("ContextEnable", typeof(bool), typeof(ImageEdit), new PropertyMetadata(true));


        public string BottomText
        {
            get { return (string)GetValue(BottomTextProperty); }
            set { SetValue(BottomTextProperty, value); }
        }

        public static readonly DependencyProperty BottomTextProperty =
            DependencyProperty.Register("BottomText", typeof(string), typeof(ImageEdit), new PropertyMetadata(string.Empty));

        public HWindow HWindow
        {
            get { return (HWindow)GetValue(HWindowProperty); }
            set { SetValue(HWindowProperty, value); }
        }

        public static readonly DependencyProperty HWindowProperty =
            DependencyProperty.Register("HWindow", typeof(HWindow), typeof(ImageEdit), new PropertyMetadata(null));



        public HObject HImage
        {
            get { return (HObject)GetValue(HImageProperty); }
            set { SetValue(HImageProperty, value); }
        }
        public static readonly DependencyProperty HImageProperty =
            DependencyProperty.Register("HImage", typeof(HObject), typeof(ImageEdit), new PropertyMetadata(HImageChangedCallBack));


        public ObservableCollection<DrawingObjectInfo> DrawObjectList
        {
            get { return (ObservableCollection<DrawingObjectInfo>)GetValue(DrawObjectListProperty); }
            set { SetValue(DrawObjectListProperty, value); }
        }
        public static readonly DependencyProperty DrawObjectListProperty =
            DependencyProperty.Register("DrawObjectList", typeof(ObservableCollection<DrawingObjectInfo>), typeof(ImageEdit), new PropertyMetadata(new ObservableCollection<DrawingObjectInfo>()));





        #endregion

        #region //Methods
        private static void HImageChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ImageEdit view && e.NewValue != null)
            {
                view.Display((HObject)e.NewValue);
                view.HImageWidth = view.HImage.GetImageSize()[0];
                view.HImageHeight = view.HImage.GetImageSize()[1];
                view.hImage = view.HImage.ToHimage();
            }
        }

        private void Display(HObject hObject)
        {
            HWindow.DispObj(hObject);

            HWindow.SetPart(0, 0, -2, -2);
        }

        #endregion
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.GetTemplateChild("PART_Halcon") is HSmartWindowControlWPF obj1)
            {
                hSmart = (HSmartWindowControlWPF)obj1;
                this.hSmart.Loaded += HalconWpfView_Loaded;
            }
            RegisterMouseMethods();

        }
        /// <summary>
        /// 鼠标右键方法注册
        /// </summary>
        private void RegisterMouseMethods()
        {
            this.ContextMenu = new ContextMenu();
            MenuItem RoiMenu = new MenuItem();
            RoiMenu.Header = "区域";
            //添加ROI
            RoiMenu.Items.Add(CreateMenu("绘制矩形",
                (s, e) => { DrawShape(DrawShapeType.Rectangle, new HTuple(), new HTuple(), new HTuple(), new HTuple()); }));
            RoiMenu.Items.Add(CreateMenu("绘制椭圆",
                (s, e) => { DrawShape(DrawShapeType.Ellipse, new HTuple(), new HTuple(), new HTuple(), new HTuple(), new HTuple()); }));
            RoiMenu.Items.Add(CreateMenu("绘制圆形",
              (s, e) => { DrawShape(DrawShapeType.Circle, new HTuple(), new HTuple(), new HTuple()); }));
            RoiMenu.Items.Add(CreateMenu("绘制区域",
                    (s, e) => { DrawShape(DrawShapeType.Region); }));
            RoiMenu.Items.Add(CreateMenu("绘制屏蔽区",
                 (s, e) => { DrawShape(DrawShapeType.Region); }));
            //信息展示按钮
            MenuItem DisplayMenu = new MenuItem();
            DisplayMenu.Header = "显示";
            DisplayMenu.Items.Add(CreateMenu("适应窗口", (s, e) => {

            }));
            _ = DisplayMenu.Items.Add(CreateMenu("显示/隐藏十字", (s, e) =>
            {
                if (DisplayCross)
                {
                    RePaint();
                }
                else
                {
                    PaintCross();
                }
                DisplayCross = !DisplayCross;
            }));
            _ = DisplayMenu.Items.Add(CreateMenu("显示/隐藏信息", (s, e) =>
            {
                DisplayImageInfo = !DisplayImageInfo;
                if (DisplayImageInfo)
                {
                    hSmart.HMouseMove += HSmart_HMouseMove;
                }
                else
                {
                    hSmart.HMouseMove -= HSmart_HMouseMove;
                }
            }));
            //添加
            this.ContextMenu.Items.Add(RoiMenu);
            this.ContextMenu.Items.Add(DisplayMenu);
        }

        #region //鼠标右键菜单
        private async void DrawShape(DrawShapeType shapeType, params HTuple[] hTuples)
        {
            TopText = "按鼠标左键绘制，右键结束。";
            HObject drawObj;
            HOperatorSet.GenEmptyObj(out drawObj);
            HOperatorSet.SetColor(hWindow, "blue");

            hSmart.HZoomContent = HSmartWindowControlWPF.ZoomContent.Off;
            if (this.HImage == null) return;
            await Task.Run(() =>
            {
                switch (shapeType)
                {
                    case DrawShapeType.Rectangle:
                        {
                            HOperatorSet.DrawRectangle1(hWindow, out hTuples[0], out hTuples[1], out hTuples[2], out hTuples[3]);
                            drawObj = hTuples.GenRectangle();
                            break;
                        }
                    case DrawShapeType.Ellipse:
                        {
                            HOperatorSet.DrawEllipse(hWindow, out hTuples[0], out hTuples[1], out hTuples[2], out hTuples[3], out hTuples[4]);
                            drawObj = hTuples.GenEllipse();
                            break;
                        }
                    case DrawShapeType.Circle:
                        {
                            HOperatorSet.DrawCircle(hWindow, out hTuples[0], out hTuples[1], out hTuples[2]);
                            drawObj = hTuples.GenCircle();
                            break;
                        }
                    case DrawShapeType.Mask:
                    case DrawShapeType.Region:
                        {
                            //绘制自定义区域 
                            HOperatorSet.DrawRegion(out drawObj, hWindow);
                            break;
                        }
                }
                if (drawObj == null) return;
            });
            DrawObjectList.Add(new DrawingObjectInfo(shapeType, drawObj, hTuples));
            HOperatorSet.GenContourRegionXld(drawObj, out HObject contours, "border"); //获取绘制对象的轮廓
            HOperatorSet.DispObj(contours, hWindow);
            hSmart.HZoomContent = HSmartWindowControlWPF.ZoomContent.WheelForwardZoomsIn;
        }


        #endregion
        private void RePaint()
        {
            this.hWindow.SetDraw("margin");
            HSystem.SetSystem("flush_graphic", "false");
            this.hWindow.ClearWindow();
            this.hWindow.DispObj(HImage);
            HSystem.SetSystem("flush_graphic", "true");
            hWindow.SetColor("black");
            hWindow.DispLine(-100.0, -100, -101, -101);
        }
        /// <summary>
        /// 绘制十字
        /// </summary>
        private void PaintCross()
        {
            //显示十字线
            HXLDCont xldCross = new HXLDCont();
            HImageWidth = HImage.GetImageSize()[0];
            HImageHeight = HImage.GetImageSize()[1];
            this.hWindow.SetColor("green");
            HRegion hRegion = new HRegion(0, 0, (HTuple)HImageWidth, (HTuple)HImageHeight);
            HOperatorSet.AreaCenter(
                hRegion,
                out HTuple _Area,
                out HTuple _ROW,
                out HTuple _COL
            );
            _ROW = HImageHeight / 2;
            _COL = HImageWidth / 2;
            //小十字
            this.hWindow.DispLine(_ROW - 5, _COL, _ROW + 5, _COL);
            this.hWindow.DispLine(_ROW, _COL - 5, _ROW, _COL + 5);
            //中心圆
            //mCtrl_HWindow.HalconWindow.DispCircle(_ROW, _COL, 35);
            //大十字-横
            this.hWindow.DispLine(
                (double)_ROW,
                (double)_COL + 50,
                (double)_ROW,
                (double)_COL * 2
            );
            this.hWindow.DispLine(
                (double)_ROW,
                0,
                (double)_ROW,
                (double)_COL - 50
            );
            //大十字-竖
            this.hWindow.DispLine(
                0,
                (double)_COL,
                (double)_ROW - 50,
                (double)_COL
            );
            this.hWindow.DispLine(
                (double)_ROW + 50,
                (double)_COL,
                (double)_ROW * 2,
                (double)_COL
            );
        }
        private MenuItem CreateMenu(string name, RoutedEventHandler click)
        {
            MenuItem menu = new MenuItem();
            menu.Header = name;
            menu.Click += click;
            return menu;
        }
        private void HalconWpfView_Loaded(object sender, RoutedEventArgs e)
        {

            hWindow = hSmart.HalconWindow;
            HWindow = hWindow;
        }
        /// <summary>
        /// 鼠标在空间窗体里滑动,显示鼠标所在位置的灰度值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HSmart_HMouseMove(object sender, HSmartWindowControlWPF.HMouseEventArgsWPF e)
        {
            sb.Clear();
            if (HImage == null) return;
            try
            {

                HOperatorSet.CountChannels(HImage, out HTuple channel_count);
                hWindow.GetMpositionSubPix(out var positionY, out var positionX, out var button_state);
                sb.Append($"X : {positionX:F2} , Y :{positionY:F2}");
                if (positionX < 0 || positionX >= HImageWidth) return;
                if (positionY < 0 || positionY >= HImageHeight) return;
                //区分通道  通道1
                if (channel_count == 1)
                {
                    var grayVal = hImage.GetGrayval(positionY, positionX);
                    sb.Append($"Gray: {grayVal}");
                }
                else if (channel_count == 3)
                {
                    HImage _RedChannel = hImage.AccessChannel(1);
                    HImage _GreenChannel = hImage.AccessChannel(2);
                    HImage _BlueChannel = hImage.AccessChannel(3);
                    var grayValRed = _RedChannel.GetGrayval(positionY, positionX);
                    var grayValGreen = _GreenChannel.GetGrayval(positionY, positionX);
                    var grayValBlue = _BlueChannel.GetGrayval(positionY, positionX);
                    sb.Append($" | R : {grayValRed:F2} , G : {grayValGreen:F2} , B : {grayValBlue:F2}");
                }
            }
            catch (Exception ex)
            {
                BottomText = ex.Message;
            }
            BottomText = sb.ToString();
        }


    }
}
