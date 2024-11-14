namespace AdventureV6;

public interface IAction
{
    string Description { get; }
    ActionTags Tags { get; }

    void Perform(Player instigator);

    void PerformIfPossible(Player instigator)
    {
        try
        {
            Perform(instigator);
        }
        catch (ArgumentException)
        {
            // Ignore invalid actions
        }
    }
}
