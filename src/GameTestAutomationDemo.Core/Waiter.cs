using OpenCvSharp;
using System.Diagnostics;

public class Waiter
{
    public static Point? WaitFor(
        string element,
        int timeoutSeconds = 5,
        int pollIntervalMs = 200,
        double threshold = 0.8)
    {
        var stopwatch = Stopwatch.StartNew();

        while (stopwatch.Elapsed.TotalSeconds < timeoutSeconds)
        {
            var point = VisionEngine.Find(element, threshold);

            if (point != null)
                return point;

            Thread.Sleep(pollIntervalMs);
        }

        return null;
    }
}
