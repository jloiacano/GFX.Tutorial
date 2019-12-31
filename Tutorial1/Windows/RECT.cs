using System.Drawing;

namespace GFX.Tutorial.Windows
{
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle rectangle) : this(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
        {
        }

        public int X
        {
            get => Left;
            set
            {
                Right -= Left - value;
                Left = value;
            }
        }

        public int Y
        {
            get => Top;
            set
            {
                Bottom -= Top - value;
                Top = value;
            }
        }

        public int Height
        {
            get => Bottom - Top;
            set => Bottom = value + Top;
        }

        public int Width
        {
            get => Right - Left;
            set => Right = value + Left;
        }

        public Point Location
        {
            get => new Point(Left, Top);
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Size Size
        {
            get => new Size(Width, Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public static implicit operator Rectangle(RECT rectangle) =>
            new Rectangle(rectangle.Left, rectangle.Top, rectangle.Width, rectangle.Height);

        public static implicit operator RECT(Rectangle rectangle) =>
            new RECT(rectangle);

        public static bool operator ==(RECT firstRectangle, RECT secondRectangle) =>
            firstRectangle.Equals(secondRectangle);

        public static bool operator !=(RECT firstRectangle, RECT secondRectangle) =>
            !firstRectangle.Equals(secondRectangle);

        public bool Equals(RECT rectangle) =>
            rectangle.Left == Left &&
            rectangle.Top == Top &&
            rectangle.Right == Right &&
            rectangle.Bottom == Bottom;

        public override bool Equals(object currentObject)
        {
            switch (currentObject)
            {
                case RECT rectangle:
                    return Equals(rectangle);

                case Rectangle rectangle:
                    return Equals(new RECT(rectangle));

                default:
                    return false;
            }
        }

        public override int GetHashCode() => ((Rectangle)this).GetHashCode();

        public override string ToString() => $"{{Left = {Left}, Top = {Top}, Right = {Right}, Bottom = {Bottom}}}";
    }
}
