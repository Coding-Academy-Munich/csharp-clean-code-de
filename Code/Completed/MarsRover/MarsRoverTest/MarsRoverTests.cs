// ReSharper disable RedundantArgumentDefaultValue

using JetBrains.Annotations;

namespace MarsRoverTest;

using MarsRover;

[UsedImplicitly]
public class MarsRoverTestFixture
{
    public Grid Grid { get; } = new(100, 100);
}

public class MarsRoverTests : IClassFixture<MarsRoverTestFixture>
{
    private readonly Grid _grid;
    private readonly Rover _rover;

    public MarsRoverTests(MarsRoverTestFixture fixture)
    {
        _grid = fixture.Grid;
        _rover = new Rover(_grid, new Point(0, 0), Direction.N);
    }

    // Test 1: Initial state
    [Fact]
    public void Rover_Initializes_To_Zero_Zero_Facing_North()
    {
        // For this specific test, we want to test the rover that is instantiated by default, so we create a fresh one.
        var newRover = new Rover(_grid);

        Assert.Equal(new Point(0, 0), newRover.Position);
        Assert.Equal(Direction.N, newRover.Direction);
    }

    // Test 2: Turning
    // Initial test for turning.
    [Fact]
    public void Rover_Turning_Right_Once_Changes_Direction_To_East()
    {
        _rover.ExecuteCommands("R");
        Assert.Equal(Direction.E, _rover.Direction);
    }

    // Second test for turning. The structure is almost similar to the first test for turning.
    [Fact]
    public void Rover_Turning_Right_Twice_Changes_Direction_To_South()
    {
        _rover.ExecuteCommands("R");
        _rover.ExecuteCommands("R");
        Assert.Equal(Direction.S, _rover.Direction);
    }

    // Don't write tests like this!
    [Fact]
    public void Rover_Turning_Right_Cycles_Through_Directions_Correctly()
    {
        _rover.ExecuteCommands("R");
        Assert.Equal(Direction.E, _rover.Direction);
        _rover.ExecuteCommands("R");
        Assert.Equal(Direction.S, _rover.Direction);
        _rover.ExecuteCommands("R");
        Assert.Equal(Direction.W, _rover.Direction);
        _rover.ExecuteCommands("R");
        Assert.Equal(Direction.N, _rover.Direction);
    }

    // If we have many similar tests, we can use a parametric test (Theory) to reduce the amount of boilerplate code
    // we have to write.
    // To turn this into a parametric test, we need to be able to run multiple commands from a string.
    [Theory]
    [InlineData("R", Direction.E)]
    [InlineData("RR", Direction.S)]
    [InlineData("RRR", Direction.W)]
    [InlineData("RRRR", Direction.N)]
    [InlineData("L", Direction.W)]
    [InlineData("LL", Direction.S)]
    [InlineData("LLL", Direction.E)]
    [InlineData("LLLL", Direction.N)]
    public void Rover_Turning_Commands_Result_In_Correct_Direction(string commands, Direction expectedDirection)
    {
        _rover.ExecuteCommands(commands);
        Assert.Equal(expectedDirection, _rover.Direction);
    }

    // Test 3: Moving
    // Initial test for moving.
    [Fact]
    public void Rover_Moves_Forward_Facing_North()
    {
        var rover = new Rover(_grid, new Point(10, 10), Direction.N);
        rover.ExecuteCommands("M");
        Assert.Equal(new Point(10, 11), rover.Position);
    }

    [Theory]
    [InlineData(Direction.N, 10, 11)]
    [InlineData(Direction.S, 10, 9)]
    [InlineData(Direction.E, 11, 10)]
    [InlineData(Direction.W, 9, 10)]
    public void Rover_Moves_Forward_One_Grid_Point_In_Correct_Direction(
        Direction startDirection, int expectedX, int expectedY)
    {
        // This test requires a specific starting position, so it instantiates its own Rover.
        var rover = new Rover(_grid, new Point(10, 10), startDirection);

        rover.ExecuteCommands("M");

        Assert.Equal(new Point(expectedX, expectedY), rover.Position);
    }

    // Test 4: Command Sequence
    // This test is unnecessary, since we already use command sequences in the test for turning.
    // It would, however, be a good test if we had not used command sequences previously.
    // It might still be useful to check that we can execute sequences containing a mix of commands.
    [Fact]
    public void Rover_Can_Execute_A_Sequence_Of_Commands()
    {
        // This test requires a specific starting position, so it instantiates its own Rover.
        var rover = new Rover(_grid, new Point(5, 5), Direction.N);

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
    public void Rover_Wraps_Around_The_Grid_Edges(
        int startX, int startY, Direction startDirection, int expectedX, int expectedY)
    {
        // For this test, a specific 10x10 grid is needed, which is different from the fixture's 100x100 grid.
        // Therefore, we still instantiate a new Grid and Rover here.
        var grid = new Grid(10, 10);
        var rover = new Rover(grid, new Point(startX, startY), startDirection);

        rover.ExecuteCommands("M");

        Assert.Equal(new Point(expectedX, expectedY), rover.Position);
    }
}
