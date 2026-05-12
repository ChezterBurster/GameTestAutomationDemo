public class MatchmakingE2ETest : TestBase
{
    [Fact]
    public void Should_Start_Free4All_Match()
    {
        var modeSelection = MainMenu.GoToOnlinePlay();
        var characterSelection = modeSelection.SelectFreeForAll();

        characterSelection.SelectCharacter("random");
        characterSelection.ConfirmCharacter(Pad);

        var matchmaking = characterSelection.WaitForMatchmaking();
        var match = matchmaking.WaitForMatchStart();

        match.AssertHudVisible();
        MarkTestAsPassed();
    }
}
