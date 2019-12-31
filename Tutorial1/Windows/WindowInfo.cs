using System;
using System.Collections.Generic;
using System.Drawing;

namespace GFX.Tutorial.Windows
{
    public class WindowInfo
    {
        #region // storage

        public IntPtr Handle { get; }
        public WindowInfo Parent { get; }
        public IntPtr ParentHandle { get; }
        public string ClassName { get; }
        public string Caption { get; }
        public string RawText { get; }
        public Rectangle RectangleWindow { get; }
        public Rectangle RectangleClientLocal { get; }
        public Rectangle RectangleClientAbsolute { get; }
        public bool IsWindowVisible { get; }
        public List<WindowInfo> Children { get; }

        #endregion

        #region // constructor

        public WindowInfo(IntPtr windowHandle, WindowInfo parent, bool getChildren)
        {
            // store
            Children = new List<WindowInfo>();
            Handle = windowHandle;
            Parent = parent;

            // get properties
            ParentHandle = User32.GetParent(Handle);
            ClassName = WindowsCalls.GetWindowClassName(Handle);
            Caption = WindowsCalls.GetWindowsCaption(Handle);
            RawText = WindowsCalls.GetWindowTextRaw(Handle);
            RectangleWindow = WindowsCalls.GetWindowRectangle(Handle);
            RectangleClientAbsolute = WindowsCalls.GetClientRectangle(Handle);
            RectangleClientLocal = new Rectangle
                (
                    RectangleClientAbsolute.X - RectangleWindow.X,
                    RectangleClientAbsolute.Y - RectangleWindow.Y,
                    RectangleClientAbsolute.Width,
                    RectangleClientAbsolute.Height
                );
            IsWindowVisible = User32.IsWindowVisible(Handle);

            // recursively collect children
            if (getChildren)
            {
                CollectChildren();
            }
        }

        // overload constructors for different situations
        public WindowInfo(IntPtr windowHandle) : this(windowHandle, null, true) { }

        public WindowInfo(IntPtr windowHandle, WindowInfo parent) : this(windowHandle, parent, true) { }

        public WindowInfo(IntPtr windowHandle, bool getChildren) : this(windowHandle, null, getChildren) { }

        #endregion

        #region // routines

        public override string ToString()
        {
            return $"{$"{Handle.ToInt32():X16}".ToUpperInvariant()} '{ClassName}' '{Caption}' '{RawText}' " +
                $"[{RectangleWindow.X}, {RectangleWindow.Y}; {RectangleWindow.Width} x {RectangleWindow.Height}]" +
                $"[{RectangleClientAbsolute.X}, {RectangleClientAbsolute.Y}; {RectangleClientAbsolute.Width} x {RectangleClientAbsolute.Height}]" +
                $"[{RectangleClientLocal.X}, {RectangleClientLocal.Y}; {RectangleClientLocal.Width} x {RectangleClientLocal.Height}]";
        }

        private void CollectChildren()
        {
            IntPtr child = IntPtr.Zero;

            while (true)
            {
                child = User32.FindWindowEx(Handle, child, null, null);
                if (child != IntPtr.Zero)
                {
                    Children.Add(new WindowInfo(child, this, true));
                }
                else
                {
                    break;
                }
            }
        }

        public WindowInfo GetRoot()
        {
            var root = this;
            while (root.ParentHandle != IntPtr.Zero)
            {
                root = new WindowInfo(root.ParentHandle);
            }
            return root;
        }

        #endregion
    }
}
