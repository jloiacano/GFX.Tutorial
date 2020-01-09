using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFX.Tutorial.Engine.Common
{
    public struct Viewport
    {
        #region // storage

        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
        public double MinimumZ { get; }
        public double MaximumZ { get; }

        #endregion

        #region // queries

        public System.Drawing.Point Location => new System.Drawing.Point(X, Y);
        public System.Drawing.Size Size => new System.Drawing.Size(Width, Height);
        public double AspectRatio => (double)Width / Height;

        #endregion

        #region // constructor

        public Viewport(int x, int y, int width, int height, double minimumZ, double maximumZ)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            MinimumZ = minimumZ;
            MaximumZ = maximumZ;
        }

        public Viewport(System.Drawing.Point location, System.Drawing.Size size, double minimumZ, double maximumZ) :
            this(location.X, location.Y, size.Width, size.Height, minimumZ, maximumZ)
        {
        }

        #endregion
    }
}
