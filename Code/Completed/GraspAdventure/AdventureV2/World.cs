namespace AdventureV2;

public record World(Dictionary<string, Location> Locations, string InitialLocationName)
{
    public override string ToString()
    {
        return $"World(Initial location = '{InitialLocationName}', {Locations.Count} locations)";
    }
}
