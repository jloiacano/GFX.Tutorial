using System.Drawing;

namespace GFX.Tutorial.Windows
{
    public struct POINT
    {
        public int X, Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public POINT(Point point) : this(point.X, point.Y)
        {
        }

        public static implicit operator Point(POINT point) => new Point(point.X, point.Y);

        public static implicit operator POINT(Point point) => new POINT(point.X, point.Y);
    }
}
