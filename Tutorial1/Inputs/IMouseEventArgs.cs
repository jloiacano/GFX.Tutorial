using MathNet.Spatial.Euclidean;
namespace GFX.Tutorial.Inputs
{
    public interface IMouseEventArgs
    {
        /// <summary>
        /// Position of the mouse during the mouse event
        /// </summary>
        Point2D Position { get; }

        /// <inheritdoc cref="MouseButtons" />
        MouseButtons Mousebuttons { get; }

        /// <summary>
        /// Signed count of the number of detents the mouse wheel has rotated.
        /// <definition> detents: the little start/stop motion of the mouse wheel when you rotate it. </definition>
        /// </summary>
        int WheelDelta { get; }

        /// <summary>
        /// Number of times the mouse button was pressed and released
        /// </summary>
        int ClickCount { get; }
    }
}
