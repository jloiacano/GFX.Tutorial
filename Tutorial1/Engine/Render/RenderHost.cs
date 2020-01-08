using GFX.Tutorial.Engine.Common;
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
        /// Desired surface size
        /// </summary>
        protected Size HostSize { get; private set; }

        /// <summary>
        /// Desired buffer size
        /// </summary>
        protected Size BufferSize { get; private set; }

        /// <summary>
        /// The size to which the buffer will be scaled
        /// </summary>
        protected Viewport Viewport { get; private set; }

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

            HostSize = HostInput.Size;

            BufferSize = HostInput.Size;

            Viewport = new Viewport(Point.Empty, HostSize, 0, 1);

            FramesPerSecondCounter = new FramesPerSecondCounter(new TimeSpan(0, 0, 0, 0, 1000));

            HostInput.SizeChanged += HostInputOnSizeChanged;
        }

        public virtual void Dispose()
        {
            HostInput.SizeChanged -= HostInputOnSizeChanged;

            FramesPerSecondCounter.Dispose();
            FramesPerSecondCounter = default;

            Viewport = default;
            BufferSize = default;
            HostSize = default;

            HostInput.Dispose();
            HostInput = default;

            HostHandle = default;
        }

        #endregion

        #region // routines

        private void HostInputOnSizeChanged(object sender, ISizeEventArgs args)
        {
            Size Sanitize(Size size)
            {
                // to avoid issues for drivers that do not allow an empty buffer
                if (size.Width < 1 || size.Height < 1)
                {
                    size = new Size(1, 1);
                }
                return size;
            }

            Size updatedHostSize = Sanitize(HostInput.Size);
            if (HostSize != updatedHostSize)
            {
                ResizeHost(updatedHostSize);
            }

            Size updatedBufferSize = Sanitize(args.UpdatedSize);
            if (BufferSize != updatedBufferSize)
            {
                ResizeBuffers(updatedBufferSize);
            }
        }

        protected virtual void ResizeHost(Size size)
        {
            HostSize = size;
            Viewport = new Viewport(Point.Empty, size, 0, 1);
        }

        protected virtual void ResizeBuffers(Size size)
        {
            BufferSize = size;
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
