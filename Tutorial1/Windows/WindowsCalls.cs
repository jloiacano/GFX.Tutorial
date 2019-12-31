using System;
using System.Drawing;
using System.Text;

namespace GFX.Tutorial.Windows
{
    public static class WindowsCalls
    {
        #region // constants

        public const int WM_GETTEXTLENGTH = 0x000E;
        public const int WM_GETTEXT = 0x000D;

        #endregion

        public static Rectangle GetClientRectangle(IntPtr windowHandle)
        {
            User32.ClientToScreen(windowHandle, out var point);
            User32.GetClientRect(windowHandle, out var rectangle);
            int rectangleWidth = rectangle.Right - rectangle.Left;
            int rectangleHeight = rectangle.Bottom - rectangle.Top;
            return new Rectangle(point.X, point.Y, rectangleWidth, rectangleHeight);
        }

        public static string GetWindowsCaption(IntPtr windowHandle)
        {
            StringBuilder windowTextStringBuilder = new StringBuilder(byte.MaxValue);
            User32.GetWindowText(windowHandle, windowTextStringBuilder, windowTextStringBuilder.Capacity + 1);
            return windowTextStringBuilder.ToString();
        }

        public static string GetWindowClassName(IntPtr windowHandle)
        {
            StringBuilder stringBuilder = new StringBuilder(byte.MaxValue);
            int success = User32.GetClassName(windowHandle, stringBuilder, stringBuilder.Capacity);
            if (success != 0)
            {
                return stringBuilder.ToString();
            }
            return null;
        }

        public static Rectangle GetWindowRectangle(IntPtr windowHandle)
        {
            User32.GetWindowRect(windowHandle, out var rectangle);
            int rectangleWidth = rectangle.Right - rectangle.Left;
            int rectangleHeight = rectangle.Bottom - rectangle.Top;
            Rectangle rectangleToReturn = new Rectangle(rectangle.Left, rectangle.Top, rectangleWidth, rectangleHeight);
            return rectangleToReturn;
        }

        public static string GetWindowTextRaw(IntPtr windowHandle)
        {
            // get correct string length
            int length = User32.SendMessage(windowHandle, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero).ToInt32();
            StringBuilder stringBuilder = new StringBuilder(length + 1);

            // get actual string
            User32.SendMessage(windowHandle, WM_GETTEXT, new IntPtr(stringBuilder.Capacity), stringBuilder);
            return stringBuilder.ToString();
        }
    }
}
