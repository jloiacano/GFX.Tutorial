using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

using MathNet.Spatial.Euclidean;

using GFX.Tutorial.Engine.Common;
using GFX.Tutorial.Engine.Render;
using GFX.Tutorial.Utilities;
using GFX.Tutorial.Mathematics;
using GFX.Tutorial.Mathematics.Extensions;

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

            //Double t = DateTime.UtcNow.Millisecond / 1000.0;
            //Color GetColor(int x, int y) => Color.FromArgb
            //    (
            //    byte.MaxValue,
            //    (byte)((double)x / BufferSize.Width * byte.MaxValue),
            //    (byte)((double)y / BufferSize.Height * byte.MaxValue),
            //    (byte)(Math.Sin(t * Math.PI) * byte.MaxValue)
            //    );

            Color GetColor(int x, int y) => Color.DimGray;

            Parallel.For(0, BackBuffer.Buffer.Length, index =>
            {
                BackBuffer.GetXY(index, out var x, out var y);
                BackBuffer.Buffer[index] = GetColor(x, y).ToArgb();
            });

            // screen space triangle
            DrawPolyline(new[]
            {
                new Point3D(100, 100, 0),
                new Point3D(100, 200, 0),
                new Point3D(300, 200, 0),
                new Point3D(100, 100, 0)
            }, Space.Screen, Pens.White);

            // view space triangle -- all things relative to viewport even after resize
            DrawPolyline(new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0, -0.9f, 0),
                new Point3D(0.9F, -0.9f, 0),
                new Point3D(0, 0, 0),
            }, Space.View, Pens.Black);

            TestTransformations();

            string renderTime = FramesPerSecondCounter.FramesPerSecondString;
            graphics.DrawString($"{renderTime}", FontConsolas12, Brushes.Lime, 0, 0);
            graphics.DrawString($"Buffer: {BufferSize.Width}, {BufferSize.Height}", FontConsolas12, Brushes.LightGreen, 0, 16);
            graphics.DrawString($"Viewport: {Viewport.Width}, {Viewport.Height}", FontConsolas12, Brushes.LightGreen, 0, 32);

            // flush and swap buffers
            BufferedGraphics.Graphics.DrawImage(BackBuffer.Bitmap, new RectangleF(Point.Empty, Viewport.Size), new RectangleF(new PointF(-0.5f, -0.5f), BufferSize), GraphicsUnit.Pixel);
            BufferedGraphics.Render(GraphicsHostDeviceContext);
        }

        private void DrawPolyline(IEnumerable<Point3D> points, Space space, Pen pen)
        {
            switch (space)
            {
                case Space.World:
                    throw new NotSupportedException();

                case Space.View:
                    DrawPolylineScreenSpace(MatrixExtensions.Viewport(Viewport).Transform(points), pen);
                    break;

                case Space.Screen:
                    DrawPolylineScreenSpace(points, pen);
                    break;

                default:
                    break;
            }
        }

        private void DrawPolylineScreenSpace(IEnumerable<Point3D> pointsScreen, Pen pen)
        {
            var from = default(Point3D?);
            foreach (var pointScreen in pointsScreen)
            {
                if (from.HasValue)
                {
                    BackBuffer.Graphics.DrawLine(pen, (float)from.Value.X, (float)from.Value.Y, (float)pointScreen.X, (float)pointScreen.Y);
                }
                from = pointScreen;
            }
        }

        private void TestTransformations()
        {
            Point3D[] pointsArrowScreen = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(40, 0, 0),
                new Point3D(35, 10, 0),
                new Point3D(50, 0, 0),
                new Point3D(35, -10, 0),
                new Point3D(40, 0, 0),
            };

            Point3D[] pointsArrowView = new[]
            {
                new Point3D(0, 0, 0),
                new Point3D(0.08, 0, 0),
                new Point3D(0.07, 0.02, 0),
                new Point3D(0.1, 0, 0),
                new Point3D(0.07, -0.02, 0),
                new Point3D(0.08, 0, 0),
            };

            DrawPolyline(pointsArrowScreen, Space.Screen, Pens.Yellow);
            DrawPolyline(pointsArrowView, Space.Screen, Pens.Purple);

            // get animation parameters
            TimeSpan periodDuration = new TimeSpan(0, 0, 0, 5, 0);
            DateTime utcNow = DateTime.UtcNow;
            double t = (utcNow.Second * 1000 + utcNow.Millisecond) % periodDuration.TotalMilliseconds / periodDuration.TotalMilliseconds;
            var sinT = Math.Sin(t * Math.PI * 2);

            // translate
            DrawPolyline((MatrixExtensions.Translate(sinT * 40, 0, 0) * MatrixExtensions.Translate(50, 100, 0)).Transform(pointsArrowScreen), Space.Screen, Pens.Orange);
            DrawPolyline((MatrixExtensions.Translate(sinT * 0.1, 0, 0) * MatrixExtensions.Translate(-0.8, 0, 0)).Transform(pointsArrowView), Space.View, Pens.Chartreuse);

            // scale
            DrawPolyline((MatrixExtensions.Scale(t * 2, t * 2, 1) * MatrixExtensions.Translate(150, 100, 0)).Transform(pointsArrowScreen), Space.Screen, Pens.Orange);
            DrawPolyline((MatrixExtensions.Scale(t * 2, t * 2, 1) * MatrixExtensions.Translate(-0.6, 0, 0)).Transform(pointsArrowView), Space.View, Pens.Chartreuse);

            // rotate  
            DrawPolyline((MatrixExtensions.Rotate(new Vector3D(0, 0, 1), t * Math.PI * 2) * MatrixExtensions.Translate(300, 100, 0)).Transform(pointsArrowScreen), Space.Screen, Pens.Orange);
            DrawPolyline((MatrixExtensions.Rotate(new Vector3D(0, 0, 1), t * Math.PI * 2) * MatrixExtensions.Translate(-0.2, 0, 0)).Transform(pointsArrowView), Space.View, Pens.Chartreuse);

            // rotate * translate
            DrawPolyline((
                MatrixExtensions.Rotate(new Vector3D(0, 0, 1), t * Math.PI * 2) *
                MatrixExtensions.Translate(0, sinT * 40, 0) *
                MatrixExtensions.Translate(400, 100, 0)
                ).Transform(pointsArrowScreen), Space.Screen, Pens.White);

            DrawPolyline((
                MatrixExtensions.Rotate(new Vector3D(0, 0, 1), t * Math.PI * 2) *
                MatrixExtensions.Translate(0, sinT * 0.2, 0) *
                MatrixExtensions.Translate(0, 0, 0)
                ).Transform(pointsArrowView), Space.View, Pens.Yellow);

            // translate * rotate
            DrawPolyline((
                MatrixExtensions.Translate(0, sinT * 40, 0) *
                MatrixExtensions.Rotate(new Vector3D(0, 0, 1), t * Math.PI * 2) *
                MatrixExtensions.Translate(500, 100, 0)
                ).Transform(pointsArrowScreen), Space.Screen, Pens.White);


            DrawPolyline((
                MatrixExtensions.Translate(0, sinT * 0.2, 0) *
                MatrixExtensions.Rotate(new Vector3D(0, 0, 1), t * Math.PI * 2) *
                MatrixExtensions.Translate(0.4, 0, 0)
                ).Transform(pointsArrowView), Space.View, Pens.Yellow);
        }

        #endregion
    }
}
