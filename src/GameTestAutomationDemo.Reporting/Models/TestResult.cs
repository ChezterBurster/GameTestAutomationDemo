public class TestResult
{
    public string TestName { get; set; } = string.Empty;

    public bool Success { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public double DurationSeconds =>
        (EndTime - StartTime).TotalSeconds;

    public int TotalLogs { get; set; }

    public int ErrorCount { get; set; }

    public int WarningCount { get; set; }

    public string ReportPath { get; set; } = string.Empty;
}
