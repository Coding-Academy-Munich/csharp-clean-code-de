namespace MarsRoverSkTest;

using MarsRoverSk;

public class MarsRoverSkTests
{
    [Fact]
    public void Test_Message()
    {
        Assert.Equal("Mars Rover Starter Kit", MarsRoverSk.Message);
    }
}
