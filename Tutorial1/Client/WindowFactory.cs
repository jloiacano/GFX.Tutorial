using GFX.Tutorial.Engine.Render;
using GFX.Tutorial.Utilities;
using GFX.Tutorial.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GFX.Tutorial.Client
{
    public static class WindowFactory
    {
        // using IReadOnlyList so that it is unchangeable once created and it is indexable.
        public static IReadOnlyList<IRenderHost> SeedWindows()
        {
            var size = new Size(720, 480);

            var renderHosts = new[]
            {
                CreateWindowForm(size, "Forms Gdi", handle => new Drivers.GraphicsDeviceInterface.Render.RenderHost(handle)),
                CreateWindowWpf(size, "WPF Gdi", handle => new Drivers.GraphicsDeviceInterface.Render.RenderHost(handle)),

            };

            SortWindows(renderHosts);

            return renderHosts;
        }

        /// <summary>
        /// Create <see cref="System.Windows.Forms"/> API host control
        /// </summary>
        /// <returns></returns>
        private static System.Windows.Forms.Control CreateHostControl()
        {
            System.Windows.Forms.Control hostControl = new System.Windows.Forms.Panel()
            {
                Dock = System.Windows.Forms.DockStyle.Fill,
                BackColor = Color.Aqua,
                ForeColor = Color.Transparent,
            };

            void EnsureFocus(System.Windows.Forms.Control control)
            {
                if (!control.Focused)
                {
                    control.Focus();
                }
            }

            // focus control, so that we can capture mousewheel events
            hostControl.MouseEnter += (sender, args) => EnsureFocus(hostControl);
            hostControl.MouseClick += (sender, args) => EnsureFocus(hostControl);

            return hostControl;
        }

        private static IRenderHost CreateWindowForm(Size size, string title, Func<IntPtr, IRenderHost> constructorForRenderHost)
        {
            var window = new System.Windows.Forms.Form()
            {
                Size = size,
                Text = title
            };

            var hostControl = CreateHostControl();

            window.Controls.Add(hostControl);

            // handle panel mouseover for scroll-wheel actions when this is not the active panel
            hostControl.MouseEnter += (sender, args) =>
            {
                if (System.Windows.Forms.Form.ActiveForm != window)
                {
                    window.Activate();
                }

                if (!hostControl.Focused)
                {
                    hostControl.Focus();
                }
            };

            window.Closed += (sender, args) => System.Windows.Application.Current.Shutdown();

            window.Show();

            var renderHostToReturn = constructorForRenderHost(hostControl.Handle());

            return renderHostToReturn;
        }

        private static IRenderHost CreateWindowWpf(Size size, string title, Func<IntPtr, IRenderHost> constructorForRenderHost)
        {
            var window = new System.Windows.Window()
            {
                Width = size.Width,
                Height = size.Height,
                Title = title
            };

            var hostControl = CreateHostControl();

            // create forms host which is basically a wrapper for the WPF to function properly
            var windowsFormsHost = new System.Windows.Forms.Integration.WindowsFormsHost()
            {
                Child = hostControl,

                // Here, if need be, you can add necessary properties for the WPF
                //Width = 300,
                //Height = 200,
                //HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                //VerticalAlignment = System.Windows.VerticalAlignment.Top,
                //Margin = new System.Windows.Thickness(200, 100, 0, 0)
            };

            window.Content = windowsFormsHost;

            window.Closed += (sender, args) => System.Windows.Application.Current.Shutdown();

            window.Show();

            var renderHostToReturn = constructorForRenderHost(hostControl.Handle());

            return renderHostToReturn;
        }

        private static void SortWindows(IEnumerable<IRenderHost> renderHosts)
        {
            WindowInfo[] windowInfoArray = renderHosts.Select(renderHost => new WindowInfo(renderHost.HostHandle).GetRoot()).ToArray();

            Size maxSize = new Size(
                windowInfoArray.Max(window => window.RectangleWindow.Width), 
                windowInfoArray.Max(window => window.RectangleWindow.Height)
                );

            int maxColumns = (int)Math.Ceiling(Math.Sqrt(windowInfoArray.Length));
            int maxRows = (int)Math.Ceiling((double)windowInfoArray.Length / maxColumns);

            var primaryScreen = System.Windows.Forms.Screen.PrimaryScreen;
            int left = primaryScreen.WorkingArea.Width / 2 - maxColumns * maxSize.Width / 2;
            int top = primaryScreen.WorkingArea.Height / 2 - maxRows * maxSize.Height / 2;

            for (int row = 0; row < maxRows; row++)
            {
                for (int column = 0; column < maxColumns; column++)
                {
                    int index = row * maxColumns + column;

                    // if the index of the window is out of range (ie 3x3 grid of windows for only 8 windows; on index 9, return)
                    if (index >= windowInfoArray.Length)
                    {
                        return;
                    }

                    WindowInfo currentWindowInfo = windowInfoArray[index];

                    int x = column * maxSize.Width + left;
                    int y = row + maxSize.Height + top;
                    int currentWindowWidth = currentWindowInfo.RectangleWindow.Width;
                    int currentWindowHeight = currentWindowInfo.RectangleWindow.Height;

                    User32.MoveWindow(currentWindowInfo.Handle, x, y, currentWindowWidth, currentWindowHeight, false);
                }
            }
        }
    }
}
