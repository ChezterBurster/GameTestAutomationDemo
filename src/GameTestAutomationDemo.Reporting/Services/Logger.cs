public static class Logger
{
    public static void Info(string message)
    {
        ReportManager.Log(LogLevel.Info, message);
    }

    public static void Step(string message)
    {
        ReportManager.Log(LogLevel.Step, message);
    }

    public static void Passed(string message)
    {
        ReportManager.Log(LogLevel.Passed, message);
    }

    public static void Warning(string message)
    {
        ReportManager.Log(LogLevel.Warning, message);
    }

    public static void Error(string message, Exception ex)
    {
        ReportManager.Log(LogLevel.Error, message, ex);
    }

    public static void Debug(string message)
    {
        ReportManager.Log(LogLevel.Debug, message);
    }
}
