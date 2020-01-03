using System;

namespace GFX.Tutorial.Inputs
{
    public interface IInput : 
        IDisposable
    {
        /// <summary>
        ///  The size of the component
        /// </summary>
        System.Drawing.Size Size { get; }

        /// <summary>
        /// Occurs when size of the component changes
        /// </summary>
        event SizeEventHandler SizeChanged;

        /// <summary>
        /// Occurs when the mose pointer is moved over the component
        /// </summary>
        event MouseEventHandler MouseMove;

        /// <summary>
        /// Occurs when mouse button is pressed
        /// </summary>
        event MouseEventHandler MouseDown;

        /// <summary>
        /// Occurs when mouse button is released
        /// </summary>
        event MouseEventHandler MouseUp;

        /// <summary>
        /// Occurs when mouse wheel moves while the component is focused
        /// </summary>
        event MouseEventHandler MouseWheel;

        /// <summary>
        /// Occurs when a key is pressed down while the component has focus
        /// </summary>
        event KeyEventHandler KeyDown;

        /// <summary>
        /// Occurs when a key is released down while the component has focus
        /// </summary>
        event KeyEventHandler KeyUp;
    }
}
