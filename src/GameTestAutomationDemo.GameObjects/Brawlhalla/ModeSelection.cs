public class ModeSelection
{
    public void AssertGameModesVisible()
    {
        Bot.WaitAndAssert("mode_selection_title");
        Bot.WaitAndAssert("1v1_strikeout_button");
        Bot.WaitAndAssert("free_for_all_button");
        Bot.WaitAndAssert("friendly_2v2_button");
        Bot.WaitAndAssert("unranked_1v1_button");
    }

    public CharacterSelection SelectFreeForAll()
    {
        Bot.Click("free_for_all_button");
        return new CharacterSelection();
    }
}
