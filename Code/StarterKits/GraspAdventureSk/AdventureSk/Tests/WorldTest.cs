using AdventureData;

namespace AdventureSk.Tests;

public class WorldTest
{
    [Fact]
    public void JsonFilesCanBeRead()
    {
        List<Dictionary<string, object?>> data = JsonLoader.LoadData("dungeon-locations.json");
        Assert.NotNull(data);
        Assert.NotEmpty(data);
        Assert.Equal(5, data.Count);
    }
}
