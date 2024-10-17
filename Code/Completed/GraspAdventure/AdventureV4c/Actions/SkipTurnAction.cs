namespace AdventureV4c.Actions;

public class SkipTurnAction : IAction
{
    public string GetDescription()
    {
        return "Skip this turn.";
    }

    public void Perform(Pawn instigator)
    {
        // Do nothing
    }
}
