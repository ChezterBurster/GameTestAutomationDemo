using System.Runtime.InteropServices;

public static class MouseController
{
    private const int INPUT_MOUSE = 0;

    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;

    [DllImport("user32.dll")]
    private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(int nIndex);

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
        public MOUSEINPUT mi;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    // =============================
    // PUBLIC API
    // =============================

    public static void Move(int x, int y)
    {
        var (nx, ny) = NormalizeCoordinates(x, y);

        var input = new INPUT
        {
            type = INPUT_MOUSE,
            U = new InputUnion
            {
                mi = new MOUSEINPUT
                {
                    dx = nx,
                    dy = ny,
                    dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE
                }
            }
        };

        Send(input);
    }

    public static void Click(int x, int y)
    {
        Move(x, y);
        Thread.Sleep(40);

        LeftDown();
        Thread.Sleep(20);
        LeftUp();
    }

    public static void LeftDown()
    {
        SendMouse(MOUSEEVENTF_LEFTDOWN);
    }

    public static void LeftUp()
    {
        SendMouse(MOUSEEVENTF_LEFTUP);
    }

    // =============================
    // INTERNAL HELPERS
    // =============================

    private static void SendMouse(uint flags)
    {
        var input = new INPUT
        {
            type = INPUT_MOUSE,
            U = new InputUnion
            {
                mi = new MOUSEINPUT
                {
                    dwFlags = flags
                }
            }
        };

        Send(input);
    }

    private static void Send(INPUT input)
    {
        INPUT[] inputs = [input];
        _ = SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    private static (int, int) NormalizeCoordinates(int x, int y)
    {
        int screenWidth = GetSystemMetrics(0);
        int screenHeight = GetSystemMetrics(1);

        int normalizedX = (int)(x * 65535.0 / screenWidth);
        int normalizedY = (int)(y * 65535.0 / screenHeight);

        return (normalizedX, normalizedY);
    }

    public static void Click(object x, object y)
    {
        throw new NotImplementedException();
    }
}
