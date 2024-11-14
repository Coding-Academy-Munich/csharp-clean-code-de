namespace AdventureV6;

public record Location(string Name, string Description, Dictionary<string, Location> ConnectedLocations)
{
    public static Location FromData(Dictionary<string, object?> data)
    {
        var name = (string) data["name"]!;
        string description = data.TryGetValue("description", out object? desc) ? (string) (desc ?? "") : "";
        return new Location(name, description, new Dictionary<string, Location>());
    }

    public Location GetConnectedLocation(string name)
    {
        if (!ConnectedLocations.TryGetValue(name, out Location? result))
        {
            throw new ArgumentException($"No such direction: {name}");
        }

        return result;
    }

    public void SetConnectedLocation(string name, Location location)
    {
        ConnectedLocations[name] = location;
    }

    public List<string> ConnectedDirections => ConnectedLocations.Keys.ToList();

    public override string ToString()
    {
        string directions = string.Join(", ", ConnectedDirections.Select(s => $"'{s}'"));
        return $"Location{{name='{Name}', description='{Description}', directions=[{directions}]}}";
    }
}
