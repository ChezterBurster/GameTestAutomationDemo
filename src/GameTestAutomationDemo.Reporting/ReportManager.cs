public static class ReportManager
{
    private static readonly string BasePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");

    private static string CurrentTestPath;

    public static void StartTest(string testName)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        CurrentTestPath = Path.Combine(BasePath, $"{testName}_{timestamp}");

        Directory.CreateDirectory(CurrentTestPath);

        Log($"START TEST: {testName}");
    }

    public static void Log(string message)
    {
        var logFile = Path.Combine(CurrentTestPath, "log.txt");
        var line = $"[{DateTime.Now:HH:mm:ss}] {message}";

        Console.WriteLine(line);
        File.AppendAllText(logFile, line + Environment.NewLine);
    }

    public static void SaveScreenshot(string name, byte[] imageBytes)
    {
        var path = Path.Combine(CurrentTestPath, $"{name}.png");
        File.WriteAllBytes(path, imageBytes);
    }

    public static void EndTest(bool success)
    {
        Log(success ? "RESULT: PASS" : "RESULT: FAIL");

        var summaryPath = Path.Combine(CurrentTestPath, "summary.txt");
        File.WriteAllText(summaryPath,
            $"Result: {(success ? "PASS" : "FAIL")}\n" +
            $"Timestamp: {DateTime.Now}");
    }

    public static string GetCurrentTestPath()
    {
        return CurrentTestPath;
    }
}
