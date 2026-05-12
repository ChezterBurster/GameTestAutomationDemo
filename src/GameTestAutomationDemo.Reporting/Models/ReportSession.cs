public class ReportSession
{
    public string TestName { get; set; } = string.Empty;

    public string TestPath { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public bool Success { get; set; }

    public List<LogEntry> Logs { get; set; } = [];
}
