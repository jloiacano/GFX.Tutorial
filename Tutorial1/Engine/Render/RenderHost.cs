using GFX.Tutorial.Inputs;
using System;

namespace GFX.Tutorial.Engine.Render
{
    public abstract class RenderHost : 
        IRenderHost
    {

        #region storage

        public IntPtr HostHandle { get; private set; }

        public IInput HostInput { get; private set; }

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

            FramesPerSecondCounter = new FramesPerSecondCounter(new TimeSpan(0, 0, 0, 0, 1000));
        }

        public virtual void Dispose()
        {
            FramesPerSecondCounter.Dispose();
            FramesPerSecondCounter = default;

            HostInput.Dispose();
            HostInput = default;

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
