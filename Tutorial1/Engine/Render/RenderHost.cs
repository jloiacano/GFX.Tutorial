using System;

namespace GFX.Tutorial.Engine.Render
{
    public abstract class RenderHost : 
        IRenderHost
    {

        #region storage

        public IntPtr HostHandle { get; private set; }

        public FramesPerSecondCounter FramesPerSecondCounter { get; private set; }

        #endregion

        #region ctor

        /// <summary>
        /// Takes hostHandle from window and saves it
        /// </summary>
        protected RenderHost(IntPtr hostHandle)
        {
            HostHandle = hostHandle;

            FramesPerSecondCounter = new FramesPerSecondCounter(new TimeSpan(0, 0, 0, 0, 1000));
        }

        public virtual void Dispose()
        {
            FramesPerSecondCounter?.Dispose();
            FramesPerSecondCounter = default;
            // default keyword: set reference type to null object (in this case as an object of type IntPtr
            // the default is: IntPtr.Zero which is an empty static readonly struct)
            HostHandle = default;
        }
        
        #endregion

        #region // render

        public void Render()
        {
            FramesPerSecondCounter.StartFrame();

            RenderInternal();

            FramesPerSecondCounter.StopFrame();
        }

        protected abstract void RenderInternal();
        
        #endregion

    }
}
