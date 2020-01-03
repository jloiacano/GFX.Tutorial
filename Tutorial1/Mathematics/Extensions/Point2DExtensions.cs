using MathNet.Spatial.Euclidean;

namespace GFX.Tutorial.Mathematics.Extensions
{
    public static class Point2DExtensions
    {

        public static Point2D ToPoint2D(this System.Drawing.Point point) => new Point2D(point.X, point.Y);
        public static Point2D ToPoint2D(this System.Windows.Point point) => new Point2D(point.X, point.Y);
    }
}
