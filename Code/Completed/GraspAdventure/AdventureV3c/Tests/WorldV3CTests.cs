using AdventureData;

namespace AdventureV3c.Tests;

public class WorldV3CTests
{
    private readonly List<Dictionary<string, object?>> _locationData;
    private readonly World _unit;

    public WorldV3CTests()
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
        _unit = WorldFactory.FromLocationData(_locationData);
    }

    [Fact]
    public void FromLocationData_ShouldCreateWorldCorrectly()
    {
        var world = WorldFactory.FromLocationData(_locationData);

        Assert.Equal(2, world.Locations.Count);
        Assert.Equal("Room 1", world.InitialLocationName);
    }

    [Fact]
    public void FromLocationData_ForComplexLocation_ShouldCreateWorldCorrectly()
    {
        List<Dictionary<string, object?>> data = JsonLoader.LoadData("dungeon-locations.json");
        World world = WorldFactory.FromLocationData(data);

        Assert.Equal(5, world.Locations.Count);
        Location vestibule = world.GetLocationByName("Vestibule");
        Location entranceHall = world.GetLocationByName("Entrance Hall");
        Location darkCorridor = world.GetLocationByName("Dark Corridor");
        Location brightlyLitCorridor = world.GetLocationByName("Brightly Lit Corridor");

        Assert.Equal(["north"], vestibule.GetConnectedDirections());
        Assert.Equal(["west", "east", "south"], new HashSet<string>(entranceHall.GetConnectedDirections()));
        Assert.Equal(entranceHall, vestibule.GetConnectedLocation("north"));
        Assert.Equal(darkCorridor, entranceHall.GetConnectedLocation("west"));
        Assert.Equal(brightlyLitCorridor, entranceHall.GetConnectedLocation("east"));
        Assert.Equal(vestibule, entranceHall.GetConnectedLocation("south"));
        Assert.Equal(entranceHall, darkCorridor.GetConnectedLocation("east"));
        Assert.Equal(entranceHall, brightlyLitCorridor.GetConnectedLocation("west"));
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
    public void WorldShouldSetUpConnections()
    {
        Location room1 = _unit.GetLocationByName("Room 1");
        Location toLoc = room1.GetConnectedLocation("north");

        Assert.Equal(_unit.GetLocationByName("Room 2"), toLoc);
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
