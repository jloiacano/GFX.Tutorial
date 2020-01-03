using GFX.Tutorial.Inputs;
using System;

namespace GFX.Tutorial.Engine.Render
{
    public class RenderHostSetup :
        IRenderHostSetup
    {

        #region // storage
        public IntPtr HostHandle { get; }

        public IInput HostInput { get; }

        #endregion

        #region // constructor

        public RenderHostSetup(IntPtr  hostHandle, IInput hostInput)
        {
            HostHandle = hostHandle;
            HostInput = hostInput;
        }
        #endregion
    }
}
