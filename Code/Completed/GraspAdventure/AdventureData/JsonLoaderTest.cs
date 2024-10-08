using Xunit;
using System.IO;
using System.Text;

namespace AdventureData.Tests;

public class JsonLoaderTests
{
    private const string SimpleJsonFile = @"
    [
        {
            ""name"": ""Room 1"",
            ""description"": ""A small room"",
            ""connections"": {""north"": ""Room 2""}
        },
        {
            ""name"": ""Room 2"",
            ""description"": ""A large room"",
            ""connections"": {""south"": ""Room 1""}
        }
    ]";

    private static void AssertSimpleJsonWasCorrectlyParsed(List<Dictionary<string, object>?> result)
    {
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        var room1 = result[0];
        Assert.Equal("Room 1", room1["name"]);
        Assert.Equal("A small room", room1["description"]);
        Assert.IsType<Dictionary<string, object>>(room1["connections"]);
        var room1Connections = (Dictionary<string, object>)room1["connections"];
        Assert.Equal("Room 2", room1Connections["north"]);

        var room2 = result[1];
        Assert.Equal("Room 2", room2["name"]);
        Assert.Equal("A large room", room2["description"]);
        Assert.IsType<Dictionary<string, object>>(room2["connections"]);
        var room2Connections = (Dictionary<string, object>)room2["connections"];
        Assert.Equal("Room 1", room2Connections["south"]);
    }

    [Fact]
    public void ParseSimpleJson()
    {
        var result = JsonLoader.ParseJson(SimpleJsonFile);

        AssertSimpleJsonWasCorrectlyParsed(result);
    }

    [Fact]
    public void ParseSimpleJsonFromStream()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(SimpleJsonFile));
        var result = JsonLoader.ParseJson(stream);

        AssertSimpleJsonWasCorrectlyParsed(result);
    }

    [Fact]
    public void ParseEmptyJson()
    {
        var result = JsonLoader.ParseJson("[]");
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ParseInvalidJson()
    {
        var result = JsonLoader.ParseJson("invalid json");
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}
