using System.Windows.Input;
using System.Drawing;
using MathNet.Spatial.Euclidean;
using GFX.Tutorial.Mathematics.Extensions;

namespace GFX.Tutorial.Inputs
{
    /// <inheritdoc />
    public class InputWpf :
        Input
    {
        #region // storage

        private System.Windows.FrameworkElement Control { get; set; }

        public override Size Size => new Size((int)Control.ActualWidth, (int)Control.ActualHeight);

        public override event SizeEventHandler SizeChanged;

        public override event MouseEventHandler MouseMove;

        public override event MouseEventHandler MouseDown;

        public override event MouseEventHandler MouseUp;

        public override event MouseEventHandler MouseWheel;

        public override event KeyEventHandler KeyDown;

        public override event KeyEventHandler KeyUp;

        #endregion

        #region // constructor

        /// <inheritdoc />
        public InputWpf(System.Windows.FrameworkElement control)
        {
            Control = control;

            Control.SizeChanged += ControlOnSizeChanged;
            Control.MouseMove += ControlOnMouseMove;
            Control.MouseDown += ControlOnMouseDown;
            Control.MouseUp += ControlOnMouseUp;
            Control.MouseWheel += ControlOnMouseWheel;
            Control.KeyDown += ControlOnKeyDown;
            Control.KeyUp += ControlOnKeyUp;
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            Control.SizeChanged -= ControlOnSizeChanged;
            Control.MouseMove -= ControlOnMouseMove;
            Control.MouseDown -= ControlOnMouseDown;
            Control.MouseUp -= ControlOnMouseUp;
            Control.MouseWheel -= ControlOnMouseWheel;
            Control.KeyDown -= ControlOnKeyDown;
            Control.KeyUp -= ControlOnKeyUp;

            Control = default;

            base.Dispose();
        }

        #endregion

        #region // handlers

        /// <inheritdoc cref="SizeChanged" />
        private void ControlOnSizeChanged(object sender, System.Windows.SizeChangedEventArgs args) => SizeChanged?.Invoke(sender, new SizeEventArgs(Size));

        /// <inheritdoc cref="MouseMove" />
        private void ControlOnMouseMove(object sender, System.Windows.Input.MouseEventArgs args) => MouseMove?.Invoke(sender, new MouseEventArgs(args, GetPosition(args), 0));

        /// <inheritdoc cref="MouseDown" />
        private void ControlOnMouseDown(object sender, MouseButtonEventArgs args) => MouseDown?.Invoke(sender, new MouseEventArgs(args, GetPosition(args), 0));

        /// <inheritdoc cref="MouseUp" />
        private void ControlOnMouseUp(object sender, MouseButtonEventArgs args) => MouseUp?.Invoke(sender, new MouseEventArgs(args, GetPosition(args), 0));

        /// <inheritdoc cref="MouseWheel" />
        private void ControlOnMouseWheel(object sender, MouseWheelEventArgs args) => MouseWheel?.Invoke(sender, new MouseEventArgs(args, GetPosition(args)));

        /// <inheritdoc cref="KeyDown" />
        private void ControlOnKeyDown(object sender, System.Windows.Input.KeyEventArgs args) => KeyDown?.Invoke(sender, new KeyEventArgs(args));

        /// <inheritdoc cref="KeyUp" />
        private void ControlOnKeyUp(object sender, System.Windows.Input.KeyEventArgs args) => KeyUp?.Invoke(sender, new KeyEventArgs(args));

        #endregion

        #region // routines

        private Point2D GetPosition(System.Windows.Input.MouseEventArgs args)
        {
            return args.GetPosition(Control).ToPoint2D();
        }

        #endregion
    }
}
