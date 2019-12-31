using System;
using System.Drawing;

namespace GFX.Tutorial.Drivers.GraphicsDeviceInterface.Render
{
    public class RenderHost :
        Engine.Render.RenderHost
    {
        #region // storage

        private Graphics GraphicsHost { get; set; }

        private Font FontConsolas12 { get; set; }

        #endregion

        #region // constructor

        public RenderHost(IntPtr hostHandle) : 
            base(hostHandle)
        {      
            GraphicsHost = Graphics.FromHwnd(HostHandle);
            FontConsolas12 = new Font("Consolas", 12);
        }

        public override void Dispose()
        {
            DisposeGraphicsHost();
            DisposeFontConsolas12();

            base.Dispose();
        }

        #region // Disposal Helpers

        public void DisposeGraphicsHost()
        {
            if (GraphicsHost == null)
            {
                throw new NullReferenceException("GrahpicsHost in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }
            GraphicsHost.Dispose();
            GraphicsHost = default;
        }

        public void DisposeFontConsolas12()
        {
            if (FontConsolas12 == null)
            {
                throw new NullReferenceException("FontConsolas12 in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }
            FontConsolas12.Dispose();
            FontConsolas12 = default;
        }

        #endregion

        #endregion

        #region // render

        protected override void RenderInternal()
        {
            GraphicsHost.Clear(Color.Gray);
            string renderTime = FramesPerSecondCounter.FramesPerSecondString;
            GraphicsHost.DrawString($"{renderTime}", FontConsolas12, Brushes.Red, 0, 0);
        }

        #endregion
    }
}
