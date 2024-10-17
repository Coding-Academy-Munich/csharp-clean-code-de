namespace AdventureV4a;

public class Pawn(string name, Location location)
{
    public void Move(string direction)
    {
        if (!Location.ConnectedLocations.TryGetValue(direction, out Location? newLocation))
        {
            throw new ArgumentException($"No such direction: {direction}");
        }

        Location = newLocation;
    }

    public void MoveIfPossible(string direction)
    {
        try
        {
            Move(direction);
        }
        catch (ArgumentException)
        {
            // ignored
        }
    }

    public string Name { get;  } = name;
    public Location Location { get; private set; } = location;
}
