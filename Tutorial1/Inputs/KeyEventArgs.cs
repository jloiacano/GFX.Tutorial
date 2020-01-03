using System;
using System.Windows.Forms;

namespace GFX.Tutorial.Inputs
{
    public class KeyEventArgs :
        EventArgs,
        IKeyEventArgs
    {
        #region // storage

        public Key Key { get; }

        public KeyModifiers KeyModifiers { get; }

        #endregion

        #region // constructor

        public KeyEventArgs(Key key, bool modifierControl, bool modifierAlt, bool modifierShift, bool modifierWindows)
        {
            Key = (Key)key;
            KeyModifiers |= modifierControl ? KeyModifiers.Control : KeyModifiers.None;
            KeyModifiers |= modifierAlt ? KeyModifiers.Alt : KeyModifiers.None;
            KeyModifiers |= modifierShift ? KeyModifiers.Shift : KeyModifiers.None;
            KeyModifiers |= modifierWindows ? KeyModifiers.Windows : KeyModifiers.None;

        }
        public KeyEventArgs(System.Windows.Forms.KeyEventArgs args) :
            this
            (
                (Key)System.Windows.Input.KeyInterop.KeyFromVirtualKey((int)args.KeyCode),
                ((args.Modifiers & Keys.Control) | (args.Modifiers & Keys.LControlKey) | (Keys.RControlKey)) != 0,
                (args.Modifiers & Keys.Alt) != 0,
                ((args.Modifiers & Keys.Shift) | (args.Modifiers & Keys.ShiftKey) | (args.Modifiers & Keys.LShiftKey) | (args.Modifiers & Keys.RShiftKey)) != 0,
                ((args.Modifiers & Keys.LWin) | (args.Modifiers & Keys.RWin)) != 0

            )
        {
        }
        public KeyEventArgs(System.Windows.Input.KeyEventArgs args) :
            this
            (
                (Key)args.Key,
                args.KeyboardDevice.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Control),
                args.KeyboardDevice.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Alt),
                args.KeyboardDevice.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Shift),
                args.KeyboardDevice.Modifiers.HasFlag(System.Windows.Input.ModifierKeys.Windows)

            )
        {
        }

        #endregion
    }
}
