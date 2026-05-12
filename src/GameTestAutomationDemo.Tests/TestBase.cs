public abstract class TestBase : IDisposable
{
    protected Brawlhalla Game { get; private set; }
    protected MainMenu MainMenu { get; private set; }
    protected GamepadController Pad { get; private set; }
    private bool _success = false;

    protected TestBase()
    {
        //Inizilize TestReporter
        TestHelper.TakeScreenshot("TestStart");
        var testName = GetType().Name;
        ReportManager.StartTest(testName);

        //Initialize Test Environment
        Game = new Brawlhalla();
        Pad = new GamepadController();
        Setup();
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
            TestHelper.TakeScreenshot("Teardown_ERROR");
            Logger.Error("Error during teardown", ex);
        }
        ReportManager.EndTest(_success);
    }

    private void ResetMouse()
    {
        MouseController.Click(0, 0);
        Thread.Sleep(100);
    }

    protected void MarkTestAsPassed()
    {
        TestHelper.TakeScreenshot("End_of_Test");
        _success = true;
    }
}
