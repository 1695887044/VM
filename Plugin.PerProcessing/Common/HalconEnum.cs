

namespace Plugin.PerProcessing.Common
{
    public enum eTransImageType
    {
        通用比例转换,
        RGB,
        HSV,
        HSI,
        YUV,
    }
    public enum eTransImageChannel
    {
        第一通道,
        第二通道,
        第三通道,
    }
    public enum eMirrorImageType
    {
        水平镜像,
        垂直镜像,
        对角镜像,
    }
    public enum eVarThresholdType
    {
        大于等于,
        小于等于,
        等于,
        不等于,
    }
    public enum eRotateImageAngle
    {
        _90,
        _180,
        _270,
    }
    public enum eOperatorType
    {
        彩色转灰,
        图像镜像,
        图像旋转,
        修改图像尺寸,

        均值滤波,
        中值滤波,
        高斯滤波,

        灰度膨胀,
        灰度腐蚀,

        锐化,
        对比度,
        亮度调节,
        灰度开运算,
        灰度闭运算,
        反色,

        二值化,
        均值二值化,
    }
}
