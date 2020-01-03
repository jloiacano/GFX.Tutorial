﻿namespace GFX.Tutorial.Inputs
{
    [System.Flags]
    public enum KeyModifiers
    {
        None =      0b_0000_0000,
        Control =   0b_0000_0001,
        Alt =       0b_0000_0010,
        Shift =     0b_0000_0100,
        Windows =   0b_0000_1000,
    }
}
