public class LogEntry
{
    public DateTime Timestamp { get; set; }

    public LogLevel Level { get; set; }

    public string Message { get; set; } = string.Empty;

    public string? ScreenshotPath { get; set; }

    public string? Exception { get; set; }
}
