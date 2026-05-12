public static class ReportManager
{
    private static readonly object LockObject = new();

    private static readonly string BasePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reports");

    private static readonly AsyncLocal<ReportSession> CurrentSession = new();

    public static void StartTest(string testName)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");

        var testPath = Path.Combine(BasePath, $"{testName}_{timestamp}");

        Directory.CreateDirectory(testPath);

        CurrentSession.Value = new ReportSession
        {
            TestName = testName,
            TestPath = testPath,
            StartTime = DateTime.Now
        };

        Log(LogLevel.Info, $"START TEST: {testName}");
    }

    public static void Log(
        LogLevel level,
        string message,
        Exception? ex = null,
        string? screenshotPath = null)
    {
        var session = CurrentSession.Value;

        if (session == null)
            throw new Exception("No active reporting session");

        var entry = new LogEntry
        {
            Timestamp = DateTime.Now,
            Level = level,
            Message = message,
            Exception = ex?.ToString(),
            ScreenshotPath = screenshotPath
        };

        session.Logs.Add(entry);

        var line =
            $"[{entry.Timestamp:HH:mm:ss}] [{level.ToString().ToUpper()}] {message}";

        lock (LockObject)
        {
            Console.WriteLine(line);

            File.AppendAllText(
                Path.Combine(session.TestPath, "log.txt"),
                line + Environment.NewLine);
        }

        if (ex != null)
        {
            Console.WriteLine(ex);
        }
    }

    public static void SaveScreenshot(string name, byte[] imageBytes)
    {
        var session = CurrentSession.Value;

        if (session == null)
            return;

        var screenshotsFolder =
            Path.Combine(session.TestPath, "screenshots");

        var path = ScreenshotService.SaveScreenshot(
            screenshotsFolder,
            name,
            imageBytes);

        Log(
            LogLevel.Info,
            $"Screenshot saved: {name}.",
            screenshotPath: path);
    }

    public static void EndTest(bool success)
    {
        var session = CurrentSession.Value;

        if (session == null)
            return;

        session.Success = success;
        session.EndTime = DateTime.Now;

        Log(
            success
                ? LogLevel.Passed
                : LogLevel.Error,
            success
                ? "TEST PASSED"
                : "TEST FAILED");

        HtmlReportExporter.Export(session);
    }

    public static string GetCurrentTestPath()
    {
        return CurrentSession.Value?.TestPath ?? string.Empty;
    }
}
