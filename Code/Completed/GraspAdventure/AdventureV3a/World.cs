using AdventureData;

namespace AdventureV3a;
using System.Text;

public record World(Dictionary<string, Location> Locations, string InitialLocationName, List<Connection> Connections)
{
    public Location GetLocationByName(string name)
    {
        return Locations[name];
    }

    public static World FromJsonFile(string fileName)
    {
        var locationData = JsonLoader.LoadData(fileName);
        return FromLocationData(locationData);
    }

    public static World FromLocationData(List<Dictionary<string, object?>> locationData)
    {
        var locations = locationData.Select(Location.FromData).ToDictionary(l => l.Name);
        var initialLocationName = ExtractName(locationData[0]);
        var connections = new List<Connection>();

        foreach (var fromLocationData in locationData)
        {
            string fromName = ExtractName(fromLocationData);
            var targets = fromLocationData["connections"];
            if (!(targets is Dictionary<string, object> targetsMap))
                throw new ArgumentException($"Invalid type for connections: {targets?.GetType()}");

            var fromLocation = locations[fromName];
            foreach (var toData in targetsMap)
            {
                string direction = toData.Key;
                string toName = (string)toData.Value;
                var toLocation = locations[toName];
                connections.Add(new Connection(fromLocation, direction, toLocation));
            }
        }

        return new World(locations, initialLocationName, connections);
    }

    private static string ExtractName(Dictionary<string, object?> locationData)
    {
        return (string)(locationData["name"] ?? "???");
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"World{{");
        sb.AppendLine($"  Initial location name: '{InitialLocationName}'");
        sb.AppendLine("  Locations:");
        foreach (var location in Locations.Values)
        {
            sb.AppendLine($"    {location}");
        }
        sb.Append('}');
        return sb.ToString();
    }

    public Location GetConnectedLocation(Location loc, string direction)
    {
        var connection = Connections.FirstOrDefault(c => c.From == loc && c.Direction == direction);
        if (connection == null)
            throw new ArgumentException($"No connected location for {loc} in direction {direction}");
        return connection.To;
    }
}
