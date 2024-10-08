namespace AdventureData.Tests;

public class FileFinderTest
{

    [Fact]
    public void TestFileFinder()
    {
        string file = FileFinder.Find("simple-locations.json");
        Assert.NotNull(file);
    }
}
