using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media;

namespace W.UI.Core.Helper
{
    public enum BackdropType { None = 0, Mica = 2, Acrylic = 3, Tabbed = 4 }

    public static class WindowHelper
    {
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        public static void ApplyMica(Window window)
        {
            if (Environment.OSVersion.Version.Major < 10) return;

            var source = PresentationSource.FromVisual(window) as HwndSource;
            if (source == null) return;

            // 设为透明背景以透出 Mica
            window.Background = Brushes.Transparent;

            // DWMWA_SYSTEMBACKDROP_TYPE: 2 = Mica, 3 = Acrylic
            int backdropType = 2;
            int prop = 38;

            DwmSetWindowAttribute(source.Handle, prop, ref backdropType, sizeof(int));

            // 强制刷新一下非客户区颜色
            int trueValue = 1;
            DwmSetWindowAttribute(source.Handle, 20, ref trueValue, sizeof(int));
        }
    }
}
