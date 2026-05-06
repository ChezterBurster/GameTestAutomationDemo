using System.Diagnostics;

public class Brawlhalla
{
    private Process _process;

    public void Start()
    {
        Logger.Step("Launching Brawlhalla via Steam...");

        _process = Process.Start(new ProcessStartInfo
        {
            FileName = "steam://rungameid/291550",
            UseShellExecute = true
        });

        if (_process == null)
            throw new Exception("Failed to launch Brawlhalla via Steam");

        Logger.Step("Steam launch command sent");
    }

    public void Close()
    {
        foreach (var p in Process.GetProcessesByName("Brawlhalla"))
        {
            if (!p.HasExited)
            {
                Logger.Step("Closing Brawlhalla...");
                p.Kill();
            }
        }
    }

    public MainMenu WaitForMainMenu()
    {
        Bot.WaitAndAssert("main_menu_logo", 60);
        Logger.Step("Waiting for main menu...");
        return new MainMenu();
    }

    public void WaitForLoadingScreen()
    {
        Logger.Step("Waiting for loading screen...");
        Bot.WaitAndAssert("loading_screen", 30);
    }
}
