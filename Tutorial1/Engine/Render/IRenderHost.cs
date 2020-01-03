using GFX.Tutorial.Inputs;
using System;

namespace GFX.Tutorial.Engine.Render
{
    /// <summary>
    /// Class that carries implements this interface will be the carrier for the 
    /// particular window and will deal with the highest levels of rendering
    /// </summary>
    public interface IRenderHost :
        IDisposable
    {
        // The pointer to the window
        IntPtr HostHandle { get; }

        // Input from host
        IInput HostInput { get; }

        FramesPerSecondCounter FramesPerSecondCounter { get; }

        void Render();
    }
}
