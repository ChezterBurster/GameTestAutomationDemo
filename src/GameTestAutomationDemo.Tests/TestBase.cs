public abstract class TestBase : IDisposable
{
    protected Brawlhalla Game { get; private set; }
    protected MainMenu MainMenu { get; private set; }
    protected GamepadController Pad { get; private set; }

    protected TestBase()
    {
        Game = new Brawlhalla();
        Pad = new GamepadController();
        Setup();
        MainMenu ??= new MainMenu();
    }

    private void Setup()
    {
        Logger.Step("Starting game...");
        ResetMouse();
        Game.Start();

        Game.WaitForLoadingScreen();
        MainMenu = Game.WaitForMainMenu();
    }

    public void Dispose()
    {
        try
        {
            Logger.Step("Closing game...");
            Game.Close();
        }
        catch (Exception ex)
        {
            Logger.Error("Error during teardown", ex);
        }
    }

    private void ResetMouse()
    {
        MouseController.Click(0, 0);
        Thread.Sleep(100);
    }
}
