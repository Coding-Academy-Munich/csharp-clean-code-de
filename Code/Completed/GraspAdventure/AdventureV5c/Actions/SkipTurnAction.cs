namespace AdventureV5c.Actions;

public class SkipTurnAction : IAction
{
    public string Description => "Skip this turn.";
    public ActionTags Tags => ActionTags.Rest;

    public void Perform(Player instigator)
    {
        // Do nothing
    }
}
