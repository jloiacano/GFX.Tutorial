﻿using System;
using MathNet.Spatial.Euclidean;
using GFX.Tutorial.Mathematics.Extensions;

namespace GFX.Tutorial.Inputs
{
    public class MouseEventArgs :
        EventArgs,
        IMouseEventArgs
    {
        #region // storage

        public Point2D Position { get; }

        public MouseButtons Mousebuttons { get; }

        public int WheelDelta { get; }

        public int ClickCount { get; }

        #endregion

        #region // constructor

        public MouseEventArgs(Point2D position, bool buttonLeft, bool buttonMiddle, bool buttonRight, bool buttonX1, bool buttonX2, int wheelDelta, int clickCount)
        {
            Position = position;
            Mousebuttons |= buttonLeft ? MouseButtons.Left : MouseButtons.None;
            Mousebuttons |= buttonMiddle ? MouseButtons.Middle : MouseButtons.None;
            Mousebuttons |= buttonRight ? MouseButtons.Right : MouseButtons.None;
            Mousebuttons |= buttonX1 ? MouseButtons.XButton1 : MouseButtons.None;
            Mousebuttons |= buttonX2 ? MouseButtons.XButton2 : MouseButtons.None;
            WheelDelta = wheelDelta;
            ClickCount = clickCount;
        }

        public MouseEventArgs(System.Windows.Forms.MouseEventArgs args) :
            this
            (
                args.Location.ToPoint2D(),
                (args.Button & System.Windows.Forms.MouseButtons.Left) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.Middle) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.Right) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.XButton1) != 0,
                (args.Button & System.Windows.Forms.MouseButtons.XButton2) != 0,
                args.Delta,
                args.Clicks
            )
        {
        }

        public MouseEventArgs(System.Windows.Input.MouseEventArgs args, Point2D position, int wheelDelta) :
            this
            (
                position,
                args.LeftButton == System.Windows.Input.MouseButtonState.Pressed,
                args.MiddleButton == System.Windows.Input.MouseButtonState.Pressed,
                args.RightButton == System.Windows.Input.MouseButtonState.Pressed,
                args.XButton1 == System.Windows.Input.MouseButtonState.Pressed,
                args.XButton2 == System.Windows.Input.MouseButtonState.Pressed,
                wheelDelta,
                0
            )
        {
        }

        public MouseEventArgs(System.Windows.Input.MouseWheelEventArgs args, Point2D position) :
            this (args, position, args.Delta)
        {
        }

        #endregion
    }
}
