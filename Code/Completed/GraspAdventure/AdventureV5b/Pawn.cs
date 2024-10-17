namespace AdventureV5b;

public class Pawn(string name, Location location)
{

    public string Name { get; } = name;
    public Location Location { get; private set; } = location;

    public void MoveToLocation(Location getConnectedLocation)
    {
        Location = getConnectedLocation;
    }
}
