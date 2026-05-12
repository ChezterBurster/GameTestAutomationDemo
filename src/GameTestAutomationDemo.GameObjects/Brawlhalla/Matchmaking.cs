public class Matchmaking
{
    public Match WaitForMatchStart()
    {
        Bot.WaitAndAssert("matchmaking_label", 45);
        Logger.Info("Wainting for match to finish loading.");
        return new Match();
    }
}
