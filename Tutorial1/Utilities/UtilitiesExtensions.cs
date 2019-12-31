using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media;

namespace GFX.Tutorial.Utilities
{
    public static class UtilitiesExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection != null)
            {
                foreach (var item in collection)
                {
                    action(item);
                }
            }
        }

        public static IntPtr Handle(this Control window)
        {
            //return window.IsDisposed ? default : Handle((System.Windows.Forms.IWin32Window)window);

            if (window.IsDisposed)
            {
                return default;
            }
            else
            {
                return Handle((IWin32Window)window);
            }
        }

        public static IntPtr Handle(this IWin32Window window)
        {
            //return window?.Handle ?? default;
            if (window != null)
            {
                return window.Handle;
            }
            else
            {
                return default;
            }
        }

        public static IntPtr Handle(this Visual window)
        {
            return window.HandleSource()?.Handle ?? default;
        }

        /// <summary>
        /// Object Lifetime:
        /// 
        /// An HwndSource is a regulat common language runtime(CLR) object, and its lifetime is managed by the garbage collector.
        /// Because the HwndSource represents an unmanaged resource, HwndSource implements IDisposable.
        /// Syncronously calling Dispose immediately destroys the Win32 window if called from the owner thread.
        /// If called from another thread, the Win32 window is destroyed asyncronously.
        /// Calling Disopse explicitly from the interoperating code might be necessary for certain interoperation scenarios.
        /// </summary>
        /// <param name="window"></param>
        /// <returns> HwndSource </returns>
        public static System.Windows.Interop.HwndSource HandleSource(this Visual window)
        {
            return System.Windows.PresentationSource.FromVisual(window) as System.Windows.Interop.HwndSource;
        }
    }
}
