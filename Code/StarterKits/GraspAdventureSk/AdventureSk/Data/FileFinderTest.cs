using AdventureSk.Data;

namespace AdventureSk.Data;

public class FileFinderTest
{

    [Fact]
    public void TestFileFinder()
    {
        string file = FileFinder.Find("simple-locations.json");
        Assert.NotNull(file);
    }
}
