using System;

namespace GFX.Tutorial.Engine.Render
{
    public abstract class RenderHost : 
        IRenderHost
    {

        #region storage

        public IntPtr HostHandle { get; private set; }

        #endregion

        #region ctor

        /// <summary>
        /// Takes hostHandle from window and saves it
        /// </summary>
        protected RenderHost(IntPtr hostHandle)
        {
            HostHandle = hostHandle;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // default keyword: set reference type to null object (in this case as an object of type IntPtr
                // the default is: IntPtr.Zero which is an empty static readonly struct)
                HostHandle = default;
            }
        }

        #endregion

    }
}
