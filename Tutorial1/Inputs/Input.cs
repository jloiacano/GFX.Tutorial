using System;

namespace GFX.Tutorial.Inputs
{
    public abstract class Input :
        IInput
    {
        #region // storage

        public abstract System.Drawing.Size Size { get; }

        public abstract event SizeEventHandler SizeChanged;

        public abstract event MouseEventHandler MouseMove;

        public abstract event MouseEventHandler MouseDown;

        public abstract event MouseEventHandler MouseUp;

        public abstract event MouseEventHandler MouseWheel;

        public abstract event KeyEventHandler KeyDown;

        public abstract event KeyEventHandler KeyUp;

        #endregion

        #region // constructor

        public Input()
        {
            Test.Subscribe(this);
        }

        public virtual void Dispose()
        {
            Test.Unsubscribe(this);
        }

        #endregion

        #region // test

        /// <summary>
        /// TODO: debug class to test input.
        /// </summary>
        private static class Test
        {
            public static void Subscribe(IInput input)
            {
                input.SizeChanged += InputOnSizeChanged;
                input.MouseMove += InputOnMouseMove;
                input.MouseDown += InputOnMouseDown;
                input.MouseUp += InputOnMouseUp;
                input.MouseWheel += InputOnMouseWheel;
                input.KeyDown += InputOneKeyDown;
                input.KeyUp += InputOneKeyUp;
            }

            public static void Unsubscribe(IInput input)
            {
                input.SizeChanged += InputOnSizeChanged;
                input.MouseMove += InputOnMouseMove;
                input.MouseDown += InputOnMouseDown;
                input.MouseUp += InputOnMouseUp;
                input.MouseWheel += InputOnMouseWheel;
                input.KeyDown += InputOneKeyDown;
                input.KeyUp += InputOneKeyUp;
            }

            private static void InputOnSizeChanged(object sender, ISizeEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.SizeChanged)} {args.UpdatedSize}");
            }
            private static void InputOnMouseMove(object sender, IMouseEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.MouseMove)} {args.Position}");
            }
            private static void InputOnMouseDown(object sender, IMouseEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.MouseDown)} {args.Position} {args.Mousebuttons}");
            }
            private static void InputOnMouseUp(object sender, IMouseEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.MouseUp)} {args.Position} {args.Mousebuttons}");
            }
            private static void InputOnMouseWheel(object sender, IMouseEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.MouseWheel)} {args.Position} {args.WheelDelta}");
            }
            private static void InputOneKeyDown(object sender, IKeyEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.KeyDown)} {args.Key} {args.KeyModifiers}");
            }
            private static void InputOneKeyUp(object sender, IKeyEventArgs args)
            {
                Console.WriteLine($"{nameof(IInput.KeyUp)} {args.Key} {args.KeyModifiers}");
            }
        }

        #endregion
    }
}
