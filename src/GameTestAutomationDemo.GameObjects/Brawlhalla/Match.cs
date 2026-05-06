public class Match
{
    public void AssertHudVisible()
    {
        Bot.WaitAndAssert("match_hud", 30, 0.7);
    }
}
