namespace AdventureV5a.Actions;

public class SkipTurnAction : IAction
{
    public string Description => "Skip this turn.";

    public void Perform(Player instigator)
    {
        // Do nothing
    }
}
