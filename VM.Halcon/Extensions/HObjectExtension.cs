using HalconDotNet;

namespace VM.Halcon.Extensions
{
    public static class HObjectExtension
    {
        public static int[] GetImageSize(this HObject image)
        {
            int width, height;
            HImage img = new HImage();
            HobjectToHimage(image, ref img);
            img.GetImageSize(out width, out height);
            return new int[] { width, height };

            static void HobjectToHimage(HObject hobject, ref HImage image)
            {
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HTuple p, t, w, h;
                    HOperatorSet.GetImagePointer1(hobject, out p, out t, out w, out h);
                    image.GenImage1(t, w, h, p);
                }
            }
        }
        public static int[] GetImageSize(this HImage image)
        {
            int width, height;
            image.GetImageSize(out width, out height);
            return new int[] { width, height };
        }
        public static HImage ToHimage(this HObject hobject)
        {
            HImage img = new HImage();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HTuple p, t, w, h;
                HOperatorSet.GetImagePointer1(hobject, out p, out t, out w, out h);
                img.GenImage1(t, w, h, p);
            }
            return img;
        }
    }
}
