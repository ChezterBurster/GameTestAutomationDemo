using Nefarius.ViGEm.Client.Targets.Xbox360;
using OpenCvSharp;

public class Bot
{
    public static void Click(
        string element,
        int timeoutSeconds = 5,
        double threshold = 0.8)
    {
        var point = WaitAndAssert(element, timeoutSeconds, threshold);
        Logger.Step($"Clicking on '{element}'...");
        MouseController.Click(point.Value.X, point.Value.Y);
        TestHelper.TakeScreenshot($"AfterClicking_{element}");
        Thread.Sleep(300);
    }

    public static bool Exists(string element, double threshold = 0.8)
    {
        if (string.IsNullOrWhiteSpace(element))
            return false;
        return Waiter.WaitFor(element, timeoutSeconds: 1, pollIntervalMs: 100, threshold) != null;
    }

    public static Point? WaitAndAssert(string element, int timeoutSeconds = 5, double threshold = 0.8)
    {
        TestHelper.TakeScreenshot($"Before_{element}");
        Logger.Step($"Waiting for '{element}'...");
        var point = Waiter.WaitFor(element, timeoutSeconds, 200, threshold);
        TestHelper.AssertElemment(point, element, timeoutSeconds);
        TestHelper.TakeScreenshot($"After_{element}");
        return point;
    }

    public static void PressContinue(GamepadController pad)
    {
        Logger.Step("Pressing continue button...");
        pad.PressButton(Xbox360Button.A, 200);
        TestHelper.TakeScreenshot("After_continuebutton");
    }
}
