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

        /// <summary>
        /// Functional Constructor
        /// </summary>
        private void Ctor()
        {
            RenderHosts = WindowFactory.SeedWindows();

            while (!Dispatcher.HasShutdownStarted)
            {
                Render(RenderHosts);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public void Dispose()
        {
            // dispose all renderhosts that we have
            RenderHosts.ForEach(renderHost => renderHost.Dispose());
            RenderHosts = default;
        }

        #endregion

        #region // render

        private static void Render(IEnumerable<IRenderHost> renderHosts)
        {
            if (renderHosts != null)
            {
                renderHosts.ForEach(renderHost => renderHost.Render());
            }
        }

        #endregion
    }
}
