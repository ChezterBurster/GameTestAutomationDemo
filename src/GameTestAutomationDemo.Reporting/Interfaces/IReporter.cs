public interface IReporter
{
    void StartTest(string testName);

    void Log(
        LogLevel level,
        string message,
        Exception? ex = null,
        string? screenshotPath = null);

    void SaveScreenshot(string name, byte[] imageBytes);

    void EndTest(bool success);
}
