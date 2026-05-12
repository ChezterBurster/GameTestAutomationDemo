using System.Diagnostics;

public class Brawlhalla
{
    public void Start()
    {
        Logger.Step("Launching Brawlhalla via Steam...");
        _ = Process.Start(new ProcessStartInfo
        {
            FileName = "steam://rungameid/291550",
            UseShellExecute = true
        }) ?? throw new Exception("Failed to launch Brawlhalla via Steam");

        TestHelper.TakeScreenshot("Steam_Command_Sent");
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
        TestHelper.TakeScreenshot("Game_Closed");
    }

    public MainMenu WaitForMainMenu()
    {
        Bot.WaitAndAssert("main_menu_logo", 60);
        return new MainMenu();
    }

    public void WaitForLoadingScreen()
    {
        Bot.WaitAndAssert("loading_screen", 30);
    }
}
