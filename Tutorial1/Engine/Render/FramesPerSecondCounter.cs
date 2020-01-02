using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFX.Tutorial.Engine.Render
{
    public class FramesPerSecondCounter : IDisposable
    {
        #region // storage

        public TimeSpan UpdateRate { get; }
        private Stopwatch StopwatchUpdate { get; set; }
        private Stopwatch StopwatchFrame { get; set; }
        private TimeSpan Elapsed { get; set; }
        private int FrameCount { get; set; }
        public double FramesPerSecondForRender { get; private set; }
        public double FramesPerSecondForGlobal { get; private set; }
        public string FramesPerSecondString => $"FPS: {FramesPerSecondForRender:0.00} ({FramesPerSecondForGlobal:0.00})";

        #endregion

        #region // constructor

        public FramesPerSecondCounter(TimeSpan updateRate)
        {
            UpdateRate = updateRate;

            StopwatchUpdate = new Stopwatch();
            StopwatchFrame = new Stopwatch();

            StopwatchUpdate.Start();

            Elapsed = TimeSpan.Zero;
        }

        public void Dispose()
        {
            DisposeStopwatchUpdate();
            DisposeStopwatchFrame();
        }

        #region // Disposal Helpers

        public void DisposeStopwatchUpdate()
        {
            if (StopwatchUpdate == null)
            {
                throw new NullReferenceException("StopwatchUpdate in Engine\\Render\\FramesPerSecondCounter is NULL");
            }
            StopwatchUpdate.Stop();
            StopwatchUpdate = default;
        }

        public void DisposeStopwatchFrame()
        {
            if (StopwatchFrame == null)
            {
                throw new NullReferenceException("StopwatchFrame in Engine\\Render\\FramesPerSecondCounter is NULL");
            }
            StopwatchFrame.Stop();
            StopwatchFrame = default;
        }

        #endregion

        #endregion

        #region // routines

        public void StartFrame()
        {
            StopwatchFrame.Restart();
        }

        public void StopFrame()
        {
            StopwatchFrame.Stop();
            Elapsed += StopwatchFrame.Elapsed;
            FrameCount++;

            var updateElapsed = StopwatchUpdate.Elapsed;

            if (updateElapsed >= UpdateRate)
            {
                FramesPerSecondForRender = FrameCount / Elapsed.TotalSeconds;
                FramesPerSecondForGlobal = FrameCount / updateElapsed.TotalSeconds;

                StopwatchUpdate.Restart();
                Elapsed = TimeSpan.Zero;
                FrameCount = 0;
            }
        }

        #endregion
    }
}
