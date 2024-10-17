namespace AdventureV4c;

public record World(Dictionary<string, Location> Locations, string InitialLocationName)
{
    public Location GetLocationByName(string name)
    {
        return Locations[name];
    }

    public override string ToString()
    {
        string locationLines = string.Join("\n", Locations.Values.Select(location => $"    {location}"));
        return $"World{{\n  Initial location name: '{InitialLocationName}'\n  Locations:\n{locationLines}\n}}";
    }
}
