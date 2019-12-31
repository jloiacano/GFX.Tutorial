using GFX.Tutorial.Engine.Render;
using GFX.Tutorial.Utilities;
using System;
using System.Collections.Generic;

namespace GFX.Tutorial.Client
{
    internal class Program : 
        System.Windows.Application,
        IDisposable
    {
        #region // storage

        private IReadOnlyList<IRenderHost> RenderHosts { get; set; }

        #endregion

        #region // ctor

        public Program()
        {
            //Startup : part of PresentationFramwework: Occurs when the System.Windows.Application.Run method of the System.Windows.Application
            //                                          object is called.
            Startup += (sender, args) => Ctor();

            //Exit : part of PresentationFramework: Occurs just before an application shuts down, and cannot be canceled.
            Exit += (sender, args) => Dispose();
        }

        private void Ctor()
        {
            var readOnlyList = WindowFactory.SeedWindows();
        }

        public void Dispose()
        {
            RenderHosts.ForEach(renderHost => renderHost.Dispose());
            RenderHosts = default;
        }

        #endregion
    }
}
