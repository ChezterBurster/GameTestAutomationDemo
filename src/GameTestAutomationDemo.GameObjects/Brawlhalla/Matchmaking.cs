public class Matchmaking
{
    public Match WaitForMatchStart()
    {
        Bot.WaitAndAssert("matchmaking_label", 45);
        return new Match();
    }
}
