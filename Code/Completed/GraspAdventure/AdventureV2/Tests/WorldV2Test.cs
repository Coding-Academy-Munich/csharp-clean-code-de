namespace AdventureV2;

public class WorldV2Test
{
    [Fact]
    public void ToString_ContainsInitialLocation()
    {
        World unit = new(new Dictionary<string, Location>(), "Test Location");
        Assert.Contains("Test Location", unit.ToString());
    }
}
