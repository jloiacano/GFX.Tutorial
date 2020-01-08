using System;
using System.Drawing;
using System.Threading.Tasks;
using GFX.Tutorial.Engine.Render;
using GFX.Tutorial.Utilities;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;
using GFX.Tutorial.Engine.Common;

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
        /// Device context of <see cref="GraphicsHost" />
        /// </summary>
        private IntPtr GraphicsHostDeviceContext { get; set; }

        /// <summary>
        /// Double buffer wrapper
        /// </summary>
        private BufferedGraphics BufferedGraphics { get; set; }

        /// <summary>
        /// Back buffer
        /// </summary>
        private DirectBitmap BackBuffer { get; set; }

        /// <summary>
        /// Font for drawing text with <see cref="System.Drawing"/>
        /// </summary>
        private Font FontConsolas12 { get; set; }

        #endregion

        #region // constructor

        public RenderHost(IRenderHostSetup renderHostSetup) : 
            base(renderHostSetup)
        {      
            GraphicsHost = Graphics.FromHwnd(HostHandle);
            GraphicsHostDeviceContext = GraphicsHost.GetHdc();
            //BackBuffer = new Bitmap();
            CreateBuffers(BufferSize);
            CreateSurface(HostInput.Size);
            FontConsolas12 = new Font("Consolas", 12);
        }

        public override void Dispose()
        {
            DisposeFontConsolas12();
            DisposeBuffers();
            DisposeSurface();
            DisposeGraphicsHostDeviceContext();
            DisposeGraphicsHost();

            base.Dispose();
        }

        #region // Disposal Helpers

        private void DisposeFontConsolas12()
        {
            if (FontConsolas12 == null)
            {
                throw new NullReferenceException("FontConsolas12 in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }
            FontConsolas12.Dispose();
            FontConsolas12 = default;
        }

        private void DisposeGraphicsHostDeviceContext()
        {
            if (GraphicsHostDeviceContext == null || GraphicsHost == null)
            {
                throw new NullReferenceException("GraphicsHostDeviceContext or GraphicsHost in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }

            GraphicsHost.ReleaseHdc(GraphicsHostDeviceContext);
            GraphicsHostDeviceContext = default;
        }

        private void DisposeBufferedGraphics()
        {
            if (BufferedGraphics == null)
            {
                throw new NullReferenceException("BufferedGraphics in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }
            BufferedGraphics.Dispose();
            BufferedGraphics = default;
        }

        private void DisposeGraphicsHost()
        {
            if (GraphicsHost == null)
            {
                throw new NullReferenceException("GrahpicsHost in Drivers\\GraphicsDeviceInterface\\Render\\RenderHost is NULL");
            }
            GraphicsHost.Dispose();
            GraphicsHost = default;
        }

        #endregion

        #endregion

        #region // routines

        protected override void ResizeHost(Size size)
        {
            base.ResizeHost(size);

            DisposeSurface();
            CreateSurface(size);
        }

        protected override void ResizeBuffers(Size size)
        {
            base.ResizeBuffers(size);

            DisposeBuffers();
            CreateBuffers(size);
        }

        private void CreateBuffers(Size size)
        {
            BackBuffer = new DirectBitmap(size);
        }

        private void DisposeBuffers()
        {
            BackBuffer.Dispose();
            BackBuffer = default;
        }

        private void CreateSurface(Size size)
        {
            Rectangle rectangleForBufferedGraphics = new Rectangle(Point.Empty, size);
            BufferedGraphics = BufferedGraphicsManager.Current.Allocate(GraphicsHostDeviceContext, rectangleForBufferedGraphics);
            BufferedGraphics.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

        private void DisposeSurface()
        {
            DisposeBufferedGraphics();
        }

        #endregion

        #region // render

        protected override void RenderInternal()
        {
            Graphics graphics = BackBuffer.Graphics;

            Double t = DateTime.UtcNow.Millisecond / 1000.0;
            Color GetColor(int x, int y) => Color.FromArgb
                (
                byte.MaxValue,
                (byte)((double)x / BufferSize.Width * byte.MaxValue),
                (byte)((double)y / BufferSize.Height * byte.MaxValue),
                (byte)(Math.Sin(t * Math.PI) * byte.MaxValue)
                );

            Parallel.For(0, BackBuffer.Buffer.Length, index =>
            {
                BackBuffer.GetXY(index, out var x, out var y);
                BackBuffer.Buffer[index] = GetColor(x, y).ToArgb();
            });

            // screen space triangle
            DrawLineScreenSpace(graphics, Pens.White, new Point3D(100, 100, 0), new Point3D(100, 200, 0));
            DrawLineScreenSpace(graphics, Pens.White, new Point3D(100, 200, 0), new Point3D(300, 200, 0));
            DrawLineScreenSpace(graphics, Pens.White, new Point3D(300, 200, 0), new Point3D(100, 100, 0));

            // view space triangle -- all things relative to viewport even after resize
            DrawLineViewSpace(graphics, Pens.Black, new Point3D(0, 0, 0), new Point3D(0, -0.9f, 0));
            DrawLineViewSpace(graphics, Pens.Black, new Point3D(0, -0.9f, 0), new Point3D(0.9F, -0.9f, 0));
            DrawLineViewSpace(graphics, Pens.Black, new Point3D(0.9F, -0.9f, 0), new Point3D(0, 0, 0));

            string renderTime = FramesPerSecondCounter.FramesPerSecondString;
            graphics.DrawString($"{renderTime}", FontConsolas12, Brushes.Lime, 0, 0);
            graphics.DrawString($"Buffer: {BufferSize.Width}, {BufferSize.Height}", FontConsolas12, Brushes.LightGreen, 0, 16);
            graphics.DrawString($"Viewport: {Viewport.Width}, {Viewport.Height}", FontConsolas12, Brushes.LightGreen, 0, 32);

            // flush and swap buffers
            BufferedGraphics.Graphics.DrawImage(BackBuffer.Bitmap, new RectangleF(Point.Empty, Viewport.Size), new RectangleF(new PointF(-0.5f, -0.5f), BufferSize), GraphicsUnit.Pixel);
            BufferedGraphics.Render(GraphicsHostDeviceContext);
        }

        private void DrawLineScreenSpace(Graphics graphics, Pen pen, Point3D startScreen, Point3D endScreen)
        {
            graphics.DrawLine(pen, (float)startScreen.X, (float)startScreen.Y, (float)endScreen.X, (float)endScreen.Y);
        }

        private static Point3D TransformFromViewSpaceToScreenSpace(Viewport viewport, Point3D point)
        {
            return new Point3D
                (
                    (point.X + 1) * 0.5 * viewport.Width + viewport.X,
                    (1 - point.Y) * 0.5 * viewport.Height + viewport.Y,
                    0
                );
        }

        private void DrawLineViewSpace(Graphics graphics, Pen pen, Point3D startView, Point3D endView)
        {
            Point3D startScreen = TransformFromViewSpaceToScreenSpace(Viewport, startView);
            Point3D endScreen = TransformFromViewSpaceToScreenSpace(Viewport, endView);
            DrawLineScreenSpace(graphics, pen, startScreen, endScreen);
        }

        #endregion
    }
}
