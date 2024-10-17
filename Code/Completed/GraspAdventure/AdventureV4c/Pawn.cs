namespace AdventureV4c;

public class Pawn(string name, Location location)
{

    public string Name { get; } = name;
    public Location Location { get; private set; } = location;

    public void Perform(IAction action)
    {
        action.Perform(this);
    }

    public void PerformIfPossible(IAction action)
    {
        try
        {
            Perform(action);
        }
        catch (ArgumentException)
        {
            // ignored
        }
    }

    public void MoveToLocation(Location getConnectedLocation)
    {
        Location = getConnectedLocation;
    }
}
