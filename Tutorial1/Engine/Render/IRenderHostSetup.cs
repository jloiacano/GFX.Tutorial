using GFX.Tutorial.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFX.Tutorial.Engine.Render
{
    public interface IRenderHostSetup
    {
        /// <inheritdoc cref="IRenderHost.HostHandle" />
        IntPtr HostHandle { get; }
        /// <inheritdoc cref="IRenderHost.HostInput" />
        IInput HostInput { get; }
    }
}
