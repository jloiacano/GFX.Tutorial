using System;
using System.Drawing;
using GFX.Tutorial.Windows;

namespace GFX.Tutorial.Drivers.GraphicsDeviceInterface.Render
{
    public class RenderHost :
        Engine.Render.RenderHost
    {
        #region // storage

        /// <summary>
        /// Graphics retrieved from <see cref="IRenderHost.HostHandle"/>
        /// </summary>
        private Graphics GraphicsHost { get; set; }

        /// <summary>
        /// Double buffer wrapper
        /// </summary>
        private BufferedGraphics BufferedGraphics { get; set; }

        /// <summary>
        /// Font for drawing text with <see cref="System.Drawing"/>
        /// </summary>
        private Font FontConsolas12 { get; set; }

        #endregion

        #region // constructor

        public RenderHost(IntPtr hostHandle) : 
            base(hostHandle)
        {      
            GraphicsHost = Graphics.FromHwnd(HostHandle);
            Rectangle rectangleForBufferedGraphics = new Rectangle(Point.Empty, WindowsCalls.GetClientRectangle(HostHandle).Size);
            BufferedGraphics = BufferedGraphicsManager.Current.Allocate(GraphicsHost, rectangleForBufferedGraphics);
            FontConsolas12 = new Font("Consolas", 12);
        }

        public override void Dispose()
        {
            DisposeGraphicsHost();
            DisposeBufferedGraphics();
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

        public void DisposeBufferedGraphics()
        {
            if (BufferedGraphics == null)
            {
                throw new NullReferenceException("BufferedGraphics in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }
            BufferedGraphics.Dispose();
            BufferedGraphics = default;
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
            BufferedGraphics.Graphics.Clear(Color.Gray);
            string renderTime = FramesPerSecondCounter.FramesPerSecondString;
            BufferedGraphics.Graphics.DrawString($"{renderTime}", FontConsolas12, Brushes.Blue, 0, 0);

            BufferedGraphics.Render();
        }

        #endregion
    }
}
