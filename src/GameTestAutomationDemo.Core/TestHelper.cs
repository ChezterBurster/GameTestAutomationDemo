using OpenCvSharp;

public static class TestHelper
{
    public static void AssertElemment(Point? element, string name, int timeoutSeconds)
    {
        try
        {
            if (element == null)
                throw new Exception($"Element '{name}' not found after {timeoutSeconds}s");
        }
        catch (Exception ex)
        {
            TakeScreenshot("Failure");
            Logger.Error(ex.Message, ex);
            ReportManager.EndTest(false);
            throw;
        }
        Logger.Passed($"Assertion passed: '{element}' found");
    }

    public static void TakeScreenshot(string name)
    {
        Cv2.ImEncode(".png", VisionEngine.Capture(), out byte[] imagebytes);
        ReportManager.SaveScreenshot($"{name}", imagebytes);
    }
}
