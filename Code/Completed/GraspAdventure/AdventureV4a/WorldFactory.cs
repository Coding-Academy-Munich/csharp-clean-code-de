using AdventureData;

namespace AdventureV4a;

public static class WorldFactory
{
    public static World FromJsonFile(string fileName)
    {
        List<Dictionary<string, object?>> locationData = JsonLoader.LoadData(fileName);
        return FromLocationData(locationData);
    }

    public static World FromLocationData(List<Dictionary<string, object?>> locationData)
    {
        Dictionary<string, Location> locations = locationData.Select(Location.FromData).ToDictionary(l => l.Name);
        var initialLocationName = (string) locationData[0]["name"]!;

        foreach (Dictionary<string, object?> fromLocationData in locationData)
        {
            var fromName = (string) fromLocationData["name"]!;
            object? targets = fromLocationData["connections"];
            if (targets is not Dictionary<string, object> targetsMap)
                throw new ArgumentException($"Invalid type for connections: {targets?.GetType()}");

            Location fromLocation = locations[fromName];
            foreach ((string direction, object toName) in targetsMap)
            {
                Location toLocation = locations[(string) toName];
                fromLocation.SetConnectedLocation(direction, toLocation);
            }
        }

        return new World(locations, initialLocationName);
    }
}
