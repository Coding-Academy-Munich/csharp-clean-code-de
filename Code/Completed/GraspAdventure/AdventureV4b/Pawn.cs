namespace AdventureV4b;

public class Pawn(string name, Location location)
{
    public void Perform(Action action, string direction)
    {
        switch (action)
        {
            case Action.Move:
                Location = Location.GetConnectedLocation(direction);
                break;
            case Action.SkipTurn:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    public void PerformIfPossible(Action action, string direction)
    {
        try
        {
            Perform(action, direction);
        }
        catch (ArgumentException)
        {
            // ignored
        }
    }

    public string Name { get;  } = name;
    public Location Location { get; private set; } = location;
}
