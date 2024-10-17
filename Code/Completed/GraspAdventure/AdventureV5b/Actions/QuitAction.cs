namespace AdventureV5b.Actions;

public class QuitAction : IAction
{
    public string Description => "Quit the game.";
    public ActionTags Tags => ActionTags.Quit;

    public void Perform(Player instigator)
    {
        // Quit the game
    }
}
