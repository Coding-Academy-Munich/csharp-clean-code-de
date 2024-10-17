namespace AdventureV5a;

public interface IAction
{
    string Description { get; }

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
