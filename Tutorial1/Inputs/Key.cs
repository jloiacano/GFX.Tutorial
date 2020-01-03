﻿namespace GFX.Tutorial.Inputs
{
    /// <summary>
    /// Specifies the possible key values on a keyboard.
    /// </summary>
    public enum Key
    {
        None = 0,
        Cancel = 1,
        Back = 2,
        Tab = 3,
        LineFeed = 4,
        Clear = 5,
        Enter = 6,
        Return = 6,
        Pause = 7,
        Capital = 8,
        CapsLock = 8,
        HangulMode = 9,
        KanaMode = 9,
        JunjaMode = 10, // 0x0000000A
        FinalMode = 11, // 0x0000000B
        HanjaMode = 12, // 0x0000000C
        KanjiMode = 12, // 0x0000000C
        Escape = 13, // 0x0000000D
        ImeConvert = 14, // 0x0000000E
        ImeNonConvert = 15, // 0x0000000F
        ImeAccept = 16, // 0x00000010
        ImeModeChange = 17, // 0x00000011
        Space = 18, // 0x00000012
        PageUp = 19, // 0x00000013
        Prior = 19, // 0x00000013
        Next = 20, // 0x00000014
        PageDown = 20, // 0x00000014
        End = 21, // 0x00000015
        Home = 22, // 0x00000016
        Left = 23, // 0x00000017
        Up = 24, // 0x00000018
        Right = 25, // 0x00000019
        Down = 26, // 0x0000001A
        Select = 27, // 0x0000001B
        Print = 28, // 0x0000001C
        Execute = 29, // 0x0000001D
        PrintScreen = 30, // 0x0000001E
        Snapshot = 30, // 0x0000001E
        Insert = 31, // 0x0000001F
        Delete = 32, // 0x00000020
        Help = 33, // 0x00000021
        D0 = 34, // 0x00000022
        D1 = 35, // 0x00000023
        D2 = 36, // 0x00000024
        D3 = 37, // 0x00000025
        D4 = 38, // 0x00000026
        D5 = 39, // 0x00000027
        D6 = 40, // 0x00000028
        D7 = 41, // 0x00000029
        D8 = 42, // 0x0000002A
        D9 = 43, // 0x0000002B
        A = 44, // 0x0000002C
        B = 45, // 0x0000002D
        C = 46, // 0x0000002E
        D = 47, // 0x0000002F
        E = 48, // 0x00000030
        F = 49, // 0x00000031
        G = 50, // 0x00000032
        H = 51, // 0x00000033
        I = 52, // 0x00000034
        J = 53, // 0x00000035
        K = 54, // 0x00000036
        L = 55, // 0x00000037
        M = 56, // 0x00000038
        N = 57, // 0x00000039
        O = 58, // 0x0000003A
        P = 59, // 0x0000003B
        Q = 60, // 0x0000003C
        R = 61, // 0x0000003D
        S = 62, // 0x0000003E
        T = 63, // 0x0000003F
        U = 64, // 0x00000040
        V = 65, // 0x00000041
        W = 66, // 0x00000042
        X = 67, // 0x00000043
        Y = 68, // 0x00000044
        Z = 69, // 0x00000045
        LWin = 70, // 0x00000046
        RWin = 71, // 0x00000047
        Apps = 72, // 0x00000048
        Sleep = 73, // 0x00000049
        NumPad0 = 74, // 0x0000004A
        NumPad1 = 75, // 0x0000004B
        NumPad2 = 76, // 0x0000004C
        NumPad3 = 77, // 0x0000004D
        NumPad4 = 78, // 0x0000004E
        NumPad5 = 79, // 0x0000004F
        NumPad6 = 80, // 0x00000050
        NumPad7 = 81, // 0x00000051
        NumPad8 = 82, // 0x00000052
        NumPad9 = 83, // 0x00000053
        Multiply = 84, // 0x00000054
        Add = 85, // 0x00000055
        Separator = 86, // 0x00000056
        Subtract = 87, // 0x00000057
        Decimal = 88, // 0x00000058
        Divide = 89, // 0x00000059
        F1 = 90, // 0x0000005A
        F2 = 91, // 0x0000005B
        F3 = 92, // 0x0000005C
        F4 = 93, // 0x0000005D
        F5 = 94, // 0x0000005E
        F6 = 95, // 0x0000005F
        F7 = 96, // 0x00000060
        F8 = 97, // 0x00000061
        F9 = 98, // 0x00000062
        F10 = 99, // 0x00000063
        F11 = 100, // 0x00000064
        F12 = 101, // 0x00000065
        F13 = 102, // 0x00000066
        F14 = 103, // 0x00000067
        F15 = 104, // 0x00000068
        F16 = 105, // 0x00000069
        F17 = 106, // 0x0000006A
        F18 = 107, // 0x0000006B
        F19 = 108, // 0x0000006C
        F20 = 109, // 0x0000006D
        F21 = 110, // 0x0000006E
        F22 = 111, // 0x0000006F
        F23 = 112, // 0x00000070
        F24 = 113, // 0x00000071
        NumLock = 114, // 0x00000072
        Scroll = 115, // 0x00000073
        LeftShift = 116, // 0x00000074
        RightShift = 117, // 0x00000075
        LeftCtrl = 118, // 0x00000076
        RightCtrl = 119, // 0x00000077
        LeftAlt = 120, // 0x00000078
        RightAlt = 121, // 0x00000079
        BrowserBack = 122, // 0x0000007A
        BrowserForward = 123, // 0x0000007B
        BrowserRefresh = 124, // 0x0000007C
        BrowserStop = 125, // 0x0000007D
        BrowserSearch = 126, // 0x0000007E
        BrowserFavorites = 127, // 0x0000007F
        BrowserHome = 128, // 0x00000080
        VolumeMute = 129, // 0x00000081
        VolumeDown = 130, // 0x00000082
        VolumeUp = 131, // 0x00000083
        MediaNextTrack = 132, // 0x00000084
        MediaPreviousTrack = 133, // 0x00000085
        MediaStop = 134, // 0x00000086
        MediaPlayPause = 135, // 0x00000087
        LaunchMail = 136, // 0x00000088
        SelectMedia = 137, // 0x00000089
        LaunchApplication1 = 138, // 0x0000008A
        LaunchApplication2 = 139, // 0x0000008B
        Oem1 = 140, // 0x0000008C
        OemSemicolon = 140, // 0x0000008C
        OemPlus = 141, // 0x0000008D
        OemComma = 142, // 0x0000008E
        OemMinus = 143, // 0x0000008F
        OemPeriod = 144, // 0x00000090
        Oem2 = 145, // 0x00000091
        OemQuestion = 145, // 0x00000091
        Oem3 = 146, // 0x00000092
        OemTilde = 146, // 0x00000092
        AbntC1 = 147, // 0x00000093
        AbntC2 = 148, // 0x00000094
        Oem4 = 149, // 0x00000095
        OemOpenBrackets = 149, // 0x00000095
        Oem5 = 150, // 0x00000096
        OemPipe = 150, // 0x00000096
        Oem6 = 151, // 0x00000097
        OemCloseBrackets = 151, // 0x00000097
        Oem7 = 152, // 0x00000098
        OemQuotes = 152, // 0x00000098
        Oem8 = 153, // 0x00000099
        Oem102 = 154, // 0x0000009A
        OemBackslash = 154, // 0x0000009A
        ImeProcessed = 155, // 0x0000009B
        System = 156, // 0x0000009C
        DbeAlphanumeric = 157, // 0x0000009D
        OemAttn = 157, // 0x0000009D
        DbeKatakana = 158, // 0x0000009E
        OemFinish = 158, // 0x0000009E
        DbeHiragana = 159, // 0x0000009F
        OemCopy = 159, // 0x0000009F
        DbeSbcsChar = 160, // 0x000000A0
        OemAuto = 160, // 0x000000A0
        DbeDbcsChar = 161, // 0x000000A1
        OemEnlw = 161, // 0x000000A1
        DbeRoman = 162, // 0x000000A2
        OemBackTab = 162, // 0x000000A2
        Attn = 163, // 0x000000A3
        DbeNoRoman = 163, // 0x000000A3
        CrSel = 164, // 0x000000A4
        DbeEnterWordRegisterMode = 164, // 0x000000A4
        DbeEnterImeConfigureMode = 165, // 0x000000A5
        ExSel = 165, // 0x000000A5
        DbeFlushString = 166, // 0x000000A6
        EraseEof = 166, // 0x000000A6
        DbeCodeInput = 167, // 0x000000A7
        Play = 167, // 0x000000A7
        DbeNoCodeInput = 168, // 0x000000A8
        Zoom = 168, // 0x000000A8
        DbeDetermineString = 169, // 0x000000A9
        NoName = 169, // 0x000000A9
        DbeEnterDialogConversionMode = 170, // 0x000000AA
        Pa1 = 170, // 0x000000AA
        OemClear = 171, // 0x000000AB
        DeadCharProcessed = 172, // 0x000000AC
    }
}
