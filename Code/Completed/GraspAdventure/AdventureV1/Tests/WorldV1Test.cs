namespace AdventureV1;

public class WorldV1Test
{
    [Fact]
    public void ToString_ContainsInitialLocation()
    {
        World unit = new(new Dictionary<string, Location>(), "Test Location");
        Assert.Contains("Test Location", unit.ToString());
    }
}
