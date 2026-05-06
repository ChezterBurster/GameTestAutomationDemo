using System.Runtime.InteropServices;

public enum ScanCode : ushort
{
    W = 0x11,
    A = 0x1E,
    S = 0x1F,
    D = 0x20,
    C = 0x2E,
    Space = 0x39,
}

public static class KeyboardController
{
    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public int type;
        public InputUnion U;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct InputUnion
    {
        [FieldOffset(0)]
        public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;       // ⚠️ NO se usa (dejar en 0)
        public ushort wScan;     // ✅ ScanCode
        public int dwFlags;
        public int time;
        public IntPtr dwExtraInfo;
    }

    private const int INPUT_KEYBOARD = 1;
    private const int KEYEVENTF_SCANCODE = 0x0008;
    private const int KEYEVENTF_KEYUP = 0x0002;
    private const int KEYEVENTF_EXTENDEDKEY = 0x0001;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    // =========================
    // PUBLIC API
    // =========================

    public static void PressKey(ScanCode key, int durationMs = 50)
    {
        KeyDown(key);
        Thread.Sleep(durationMs);
        KeyUp(key);
    }

    public static void KeyDown(ScanCode key)
    {
        Send(key, false);
    }

    public static void KeyUp(ScanCode key)
    {
        Send(key, true);
    }

    public static void SendKey(ScanCode key)
    {
        var inputs = new INPUT[]
        {
            CreateInput(key, false),
            CreateInput(key, true)
        };

        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    // =========================
    // INTERNAL
    // =========================

    private static void Send(ScanCode key, bool keyUp)
    {
        var input = CreateInput(key, keyUp);

        SendInput(1, new[] { input }, Marshal.SizeOf(typeof(INPUT)));
    }

    private static INPUT CreateInput(ScanCode key, bool keyUp)
    {
        int flags = KEYEVENTF_SCANCODE;

        if (keyUp)
            flags |= KEYEVENTF_KEYUP;

        if (IsExtendedKey(key))
            flags |= KEYEVENTF_EXTENDEDKEY;

        return new INPUT
        {
            type = INPUT_KEYBOARD,
            U = new InputUnion
            {
                ki = new KEYBDINPUT
                {
                    wVk = 0, // IMPORTANTE
                    wScan = (ushort)key,
                    dwFlags = flags,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            }
        };
    }

    private static bool IsExtendedKey(ScanCode key)
    {
        // Para futuro (flechas, ctrl derecho, etc.)
        return false;
    }
}
