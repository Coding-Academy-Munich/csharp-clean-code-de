using AdventureData;

namespace AdventureV3a.Tests;

public class WorldV3ATests
{
    private readonly List<Dictionary<string, object?>> _locationData;
    private readonly World _unit;

    public WorldV3ATests()
    {
        _locationData =
        [
            new Dictionary<string, object?>
            {
                ["name"] = "Room 1",
                ["description"] = "This is a room",
                ["connections"] = new Dictionary<string, object> {["north"] = "Room 2"}
            },

            new Dictionary<string, object?>
            {
                ["name"] = "Room 2",
                ["description"] = "This is another room",
                ["connections"] = new Dictionary<string, object> {["south"] = "Room 1"}
            }
        ];
        _unit = World.FromLocationData(_locationData);
    }

    [Fact]
    public void FromLocationData_ShouldCreateWorldCorrectly()
    {
        var world = World.FromLocationData(_locationData);

        Assert.Equal(2, world.Locations.Count);
        Assert.Equal("Room 1", world.InitialLocationName);
    }

    [Fact]
    public void FromLocationData_ForComplexLocation_ShouldCreateWorldCorrectly()
    {
        var data = JsonLoader.LoadData("dungeon-locations.json");
        var world = World.FromLocationData(data);

        Assert.Equal(5, world.Locations.Count);
        Assert.Equal(8, world.Connections.Count);
    }

    [Fact]
    public void GetLocationByName_ShouldReturnCorrectLocation()
    {
        Assert.Equal("Room 1", _unit.GetLocationByName("Room 1").Name);
        Assert.Equal("This is a room", _unit.GetLocationByName("Room 1").Description);
        Assert.Equal("Room 2", _unit.GetLocationByName("Room 2").Name);
        Assert.Equal("This is another room", _unit.GetLocationByName("Room 2").Description);
    }

    [Fact]
    public void Locations_ShouldContainAllLocations()
    {
        Assert.Equal(2, _unit.Locations.Count);
        Assert.True(_unit.Locations.ContainsKey("Room 1"));
        Assert.True(_unit.Locations.ContainsKey("Room 2"));
    }

    [Fact]
    public void InitialLocationName_ShouldBeCorrect()
    {
        Assert.Equal("Room 1", _unit.InitialLocationName);
    }

    [Fact]
    public void GetConnectedLocation_IfTargetExists_ShouldReturnCorrectLocation()
    {
        var fromLoc = _unit.GetLocationByName("Room 1");
        var toLoc = _unit.GetConnectedLocation(fromLoc, "north");

        Assert.Equal(_unit.GetLocationByName("Room 2"), toLoc);
    }

    [Fact]
    public void GetConnectedLocation_IfTargetDoesNotExist_ShouldThrowException()
    {
        var fromLoc = _unit.GetLocationByName("Room 1");

        Assert.Throws<ArgumentException>(() => _unit.GetConnectedLocation(fromLoc, "nowhere"));
    }

    [Fact]
    public void Connections_ForSimpleWorld_ShouldBeCorrect()
    {
        var connections = _unit.Connections;
        Assert.Equal(2, connections.Count);

        var room1 = _unit.GetLocationByName("Room 1");
        var room2 = _unit.GetLocationByName("Room 2");
        Assert.Contains(connections, c => c.From == room1 && c.Direction == "north" && c.To == room2);
        Assert.Contains(connections, c => c.From == room2 && c.Direction == "south" && c.To == room1);
    }

    [Fact]
    public void ToString_ShouldContainAllLocationInfo()
    {
        var result = _unit.ToString();
        Assert.Contains("Room 1", result);
        Assert.Contains("This is a room", result);
        Assert.Contains("Room 2", result);
        Assert.Contains("This is another room", result);
    }
}
