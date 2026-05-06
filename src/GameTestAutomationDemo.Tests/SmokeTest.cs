public class SmokeTests : TestBase
{
    [Fact]
    public void Should_Launch_And_Navigate_To_Online_Play()
    {
        MainMenu.AssertPlayButtonVisible();

        var modeSelection = MainMenu.GoToOnlinePlay();
        modeSelection.AssertGameModesVisible();
    }
}
