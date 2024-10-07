namespace LibrarySk;

public class DeleteMeTest
{
    [Fact]
    public void TestNothing()
    {
        DeleteMe deleteMe = new();
        Assert.Equal(deleteMe, deleteMe);
    }
}
