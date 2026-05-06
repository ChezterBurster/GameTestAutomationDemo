public class MainMenu
{
    public void AssertPlayButtonVisible()
    {
        Bot.WaitAndAssert("play_button");
    }

    public ModeSelection GoToOnlinePlay()
    {
        Bot.Click("play_button");
        return new ModeSelection();
    }
}
