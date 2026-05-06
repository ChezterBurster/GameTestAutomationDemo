using Nefarius.ViGEm.Client.Targets.Xbox360;

public class Bot
{
    public static void Click(
        string element,
        int timeoutSeconds = 5,
        double threshold = 0.8)
    {
        var point = Waiter.WaitFor(element, timeoutSeconds, 200, threshold);

        if (point == null)
            throw new Exception($"Element '{element}' not found after {timeoutSeconds}s");

        MouseController.Click(point.Value.X, point.Value.Y);
        Thread.Sleep(50);
    }

    public static bool Exists(string element, double threshold = 0.8)
    {
        if (string.IsNullOrWhiteSpace(element))
            return false;
        return Waiter.WaitFor(element, timeoutSeconds: 1, pollIntervalMs: 100, threshold) != null;
    }

    public static void WaitAndAssert(string element, int timeoutSeconds = 5, double threshold = 0.8)
    {
        var point = Waiter.WaitFor(element, timeoutSeconds, 200, threshold);

        if (point == null)
            throw new Exception($"Assertion failed: '{element}' not found");

        Logger.Passed($"Assertion passed: '{element}' found");
    }

    public static void PressContinue(GamepadController pad)
    {
        pad.PressButton(Xbox360Button.A, 200);
    }
}
