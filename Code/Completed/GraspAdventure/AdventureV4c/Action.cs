namespace AdventureV4c;

public interface IAction
{
    string GetDescription();
    void Perform(Pawn instigator);

    void PerformIfPossible(Pawn instigator)
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
