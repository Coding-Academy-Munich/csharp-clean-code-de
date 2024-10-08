namespace AdventureV1;

public record Location(string Name, string Description)
{
    public static Location FromData(Dictionary<string, object?> data)
    {
        string name = data?["name"]?.ToString() ?? "???";
        string description = data?.GetValueOrDefault("description")?.ToString() ?? "";
        return new Location(name, description);
    }

}
