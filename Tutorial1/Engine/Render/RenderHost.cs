using GFX.Tutorial.Inputs;
using System;
using System.Drawing;

namespace GFX.Tutorial.Engine.Render
{
    public abstract class RenderHost : 
        IRenderHost
    {

        #region storage

        public IntPtr HostHandle { get; private set; }

        public IInput HostInput { get; private set; }

        /// <summary>
        /// Desired buffer size
        /// </summary>
        protected Size BufferSize { get; private set; }

        /// <summary>
        /// The size to which the buffer will be scaled
        /// </summary>
        protected Size ViewportSize { get; private set; }

        public FramesPerSecondCounter FramesPerSecondCounter { get; private set; }

        #endregion

        #region ctor

        /// <summary>
        /// Takes hostHandle from window and saves it
        /// </summary>
        protected RenderHost(IRenderHostSetup renderhHostSetup)
        {
            HostHandle = renderhHostSetup.HostHandle;

            HostInput = renderhHostSetup.HostInput;

            BufferSize = HostInput.Size;

            ViewportSize = HostInput.Size;

            FramesPerSecondCounter = new FramesPerSecondCounter(new TimeSpan(0, 0, 0, 0, 1000));

            HostInput.SizeChanged += HostInputOnSizeChanged;
        }

        public virtual void Dispose()
        {
            HostInput.SizeChanged -= HostInputOnSizeChanged;

            FramesPerSecondCounter.Dispose();
            FramesPerSecondCounter = default;

            BufferSize = default;
            ViewportSize = default;

            HostInput.Dispose();
            HostInput = default;

            HostHandle = default;
        }

        #endregion

        #region // routines

        private void HostInputOnSizeChanged(object sender, ISizeEventArgs args)
        {
            var size = args.UpdatedSize;

            // to avoid issues for drivers that do not allow an empty buffer
            if (size.Width < 1 || size.Height < 1)
            {
                size = new Size(1, 1);
            }

            //ResizeBuffers(new Size(size.Width / 10, size.Height / 10));
            ResizeBuffers(size);
            ResizeViewport(size);
        }

        protected virtual void ResizeBuffers(Size size)
        {
            BufferSize = size;
        }

        protected virtual void ResizeViewport(Size size)
        {
            ViewportSize = size;
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
