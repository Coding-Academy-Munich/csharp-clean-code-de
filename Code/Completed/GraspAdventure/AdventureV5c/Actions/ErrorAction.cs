namespace AdventureV5c.Actions;

public class ErrorAction : IAction
{
    public string Description => "Raise an error for testing purposes.";
    public ActionTags Tags => ActionTags.Error | ActionTags.DebugOnly;

    public void Perform(Player instigator)
    {
        throw new InvalidOperationException("This is a test error.");
    }
}
