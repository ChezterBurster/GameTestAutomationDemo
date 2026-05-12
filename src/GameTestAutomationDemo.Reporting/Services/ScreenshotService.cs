public static class ScreenshotService
{
    public static string SaveScreenshot(
        string directory,
        string name,
        byte[] imageBytes)
    {
        Directory.CreateDirectory(directory);

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");

        var fileName = $"{timestamp}_{name}.png";

        var fullPath = Path.Combine(directory, fileName);

        File.WriteAllBytes(fullPath, imageBytes);

        return fullPath;
    }
}
