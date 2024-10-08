namespace AdventureV1;

public class LocationV1Test
{
    private readonly Dictionary<string, object?> _data = new()
    {
        ["name"] = "Test Location",
        ["description"] = "This is a test location."
    };

    [Fact]
    public void FromData_ValidInput()
    {
        Location unit = Location.FromData(_data);
        Assert.Equal("Test Location", unit.Name);
        Assert.Equal("This is a test location.", unit.Description);
    }

    [Fact]
    public void FromData_MissingName()
    {
        _data.Remove("name");
        Assert.Throws<KeyNotFoundException>(() => Location.FromData(_data));
    }

    [Fact]
    public void FromData_MissingDescription()
    {
        _data.Remove("description");
        Location unit = Location.FromData(_data);
        Assert.Equal("", unit.Description);
    }
}
