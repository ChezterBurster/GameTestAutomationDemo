public class CharacterSelection
{
    public void ConfirmCharacter(GamepadController pad)
    {
        //Bot.Click("confirm_character_button");
        Bot.WaitAndAssert("ready_banner");
        Logger.Info("Confirming character selection...");
        Bot.PressContinue(pad);
    }

    public void SelectCharacter(string name)
    {
        Logger.Step($"Selecting '{name}' character...");
        Bot.Click($"{name}_icon");
    }

    public Matchmaking WaitForMatchmaking()
    {
        Bot.WaitAndAssert("searching_indicator");
        Logger.Info("Searching match...");
        return new Matchmaking();
    }
}
