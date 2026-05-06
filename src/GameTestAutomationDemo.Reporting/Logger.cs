public static class Logger
{
    public static void Step(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [STEP] {message}");
    }

    public static void Error(string message, Exception ex)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERROR] {message}");
        Console.WriteLine(ex.Message);
    }

    public static void Passed(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [PASSED] {message}");
    }
}
