namespace AdventureV3a.Tests;

public class LocationV3ATests
{
    private readonly Dictionary<string, object?> _locationData = new()
    {
        ["name"] = "Room 1",
        ["description"] = "This is a room"
    };

    [Fact]
    public void FromData_ShouldCreateLocationCorrectly()
    {
        Location unit = Location.FromData(_locationData);

        Assert.Equal("Room 1", unit.Name);
        Assert.Equal("This is a room", unit.Description);
    }

    [Fact]
    public void ToString_ShouldContainNameAndDescription()
    {
        Location unit = Location.FromData(_locationData);

        Assert.Contains("Room 1", unit.ToString());
        Assert.Contains("This is a room", unit.ToString());
    }

    [Fact]
    public void Equals_ShouldBeEqualForSameData()
    {
        Location unit1 = Location.FromData(_locationData);
        Location unit2 = Location.FromData(_locationData);

        Assert.NotSame(unit1, unit2);
        Assert.Equal(unit1, unit2);
        Assert.Equal(unit1.GetHashCode(), unit2.GetHashCode());
    }
}
