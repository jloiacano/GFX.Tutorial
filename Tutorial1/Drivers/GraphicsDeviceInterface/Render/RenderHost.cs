using System;

namespace GFX.Tutorial.Drivers.GraphicsDeviceInterface.Render
{
    public class RenderHost :
        Engine.Render.RenderHost
    {
        public RenderHost(IntPtr hostHandle) : 
            base(hostHandle)
        {            
        }
    }
}
