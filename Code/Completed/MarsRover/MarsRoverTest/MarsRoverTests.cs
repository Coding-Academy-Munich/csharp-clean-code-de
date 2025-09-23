// ReSharper disable RedundantArgumentDefaultValue
namespace MarsRoverTest;

using MarsRover;
using Xunit;

public class MarsRoverTests
{
    // Test 1: Initial state
    [Fact]
    public void Rover_Initializes_To_Zero_Zero_Facing_North()
    {
        var grid = new Grid(100, 100);
        var rover = new Rover(grid);

        Assert.Equal(new Point(0, 0), rover.Position);
        Assert.Equal(Direction.N, rover.Direction);
    }

    // Test 2: Turning
    [Fact]
    public void Rover_Turning_Right_Cycles_Through_Directions_Correctly()
    {
        var grid = new Grid(100, 100);
        var rover = new Rover(grid, new Point(0, 0), Direction.N);

        rover.ExecuteCommands("R");
        Assert.Equal(Direction.E, rover.Direction);
        rover.ExecuteCommands("R");
        Assert.Equal(Direction.S, rover.Direction);
        rover.ExecuteCommands("R");
        Assert.Equal(Direction.W, rover.Direction);
        rover.ExecuteCommands("R");
        Assert.Equal(Direction.N, rover.Direction);
    }

    [Fact]
    public void Rover_Turning_Left_Cycles_Through_Directions_Correctly()
    {
        var grid = new Grid(100, 100);
        var rover = new Rover(grid, new Point(0, 0), Direction.N);

        rover.ExecuteCommands("L");
        Assert.Equal(Direction.W, rover.Direction);
        rover.ExecuteCommands("L");
        Assert.Equal(Direction.S, rover.Direction);
        rover.ExecuteCommands("L");
        Assert.Equal(Direction.E, rover.Direction);
        rover.ExecuteCommands("L");
        Assert.Equal(Direction.N, rover.Direction);
    }

    // Test 3: Moving
    [Theory]
    [InlineData(Direction.N, 10, 11)]
    [InlineData(Direction.S, 10, 9)]
    [InlineData(Direction.E, 11, 10)]
    [InlineData(Direction.W, 9, 10)]
    public void Rover_Moves_Forward_One_Grid_Point_In_Correct_Direction(Direction startDirection, int expectedX,
        int expectedY)
    {
        var grid = new Grid(100, 100);
        var rover = new Rover(grid, new Point(10, 10), startDirection);

        rover.ExecuteCommands("M");

        Assert.Equal(new Point(expectedX, expectedY), rover.Position);
    }

    // Test 4: Command Sequence
    [Fact]
    public void Rover_Can_Execute_A_Sequence_Of_Commands()
    {
        var grid = new Grid(100, 100);
        var rover = new Rover(grid, new Point(5, 5), Direction.N);

        rover.ExecuteCommands("RMMMLM");

        Assert.Equal(new Point(8, 6), rover.Position);
        Assert.Equal(Direction.N, rover.Direction);
    }

    // Test 5: The "Design Driver" test
    [Theory]
    [InlineData(5, 9, Direction.N, 5, 0)] // Wraps from North to South
    [InlineData(5, 0, Direction.S, 5, 9)] // Wraps from South to North
    [InlineData(9, 5, Direction.E, 0, 5)] // Wraps from East to West
    [InlineData(0, 5, Direction.W, 9, 5)] // Wraps from West to East
    public void Rover_Wraps_Around_The_Grid_Edges(int startX, int startY, Direction startDirection, int expectedX,
        int expectedY)
    {
        var grid = new Grid(10, 10);
        var rover = new Rover(grid, new Point(startX, startY), startDirection);

        rover.ExecuteCommands("M");

        Assert.Equal(new Point(expectedX, expectedY), rover.Position);
    }
}
