public class CharacterSelection
{
    public void ConfirmCharacter(GamepadController pad)
    {
        //Bot.Click("confirm_character_button");
        Bot.WaitAndAssert("ready_banner");
        Bot.PressContinue(pad);
    }

    public void SelectCharacter(string name)
    {
        Bot.Click($"{name}_icon");
    }

    public Matchmaking WaitForMatchmaking()
    {
        Bot.WaitAndAssert("searching_indicator");
        return new Matchmaking();
    }
}
