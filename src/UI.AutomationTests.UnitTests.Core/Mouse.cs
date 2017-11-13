using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;

namespace UI.AutomationTests.UnitTests.Core
{
    public class DisplaySettings
    {
        public const uint DisplayWidthPx = 1920;
        public const uint DisplayHeightPx = 1080;
    }

    public static class Mouse
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
        private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        private const uint MOUSEEVENTF_MOVE = 0x01;
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        public static void Click(Point point)
        {
            MakeMouseEvent(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, point);
        }

        public static void RightClick(Point point)
        {
            MakeMouseEvent(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE | MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, point);
        }

        public static void MouseDoubleClick(Point point)
        {
            MakeMouseEvent(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP | MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, point);
        }

        public static void RightDoubleClick(Point point)
        {
            MakeMouseEvent(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE | MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP | MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, point);
        }

        private static void MakeMouseEvent(uint dwFlags, Point point)
        {
            if (point == null)
                throw new ArgumentNullException();

            uint dx = 65535 / DisplaySettings.DisplayWidthPx * (uint)point.X;
            uint dy = 65535 / DisplaySettings.DisplayHeightPx * (uint)point.Y;

            mouse_event(dwFlags, dx, dy, 0, UIntPtr.Zero);
        }
    }
}
