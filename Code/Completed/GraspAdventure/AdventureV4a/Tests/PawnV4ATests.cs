namespace AdventureV4a.Tests;

public class PawnV4ATests
{
    private readonly Location _room1;
    private readonly Location _room2;
    private readonly Pawn _unit;

    public PawnV4ATests()
    {
        _room1 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 1"});
        _room2 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 2"});
        _room1.SetConnectedLocation("north", _room2);
        _unit = new Pawn("Player", _room1);

    }

    [Fact]
    public void Move_ShouldMoveToConnectedLocation()
    {
        _unit.Move("north");
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void Move_ShouldThrowForInvalidDirection()
    {
        Assert.Throws<ArgumentException>(() => _unit.Move("nowhere"));
    }

    [Fact]
    public void MoveIfPossible_ShouldMoveToConnectedLocation()
    {
        _unit.MoveIfPossible("north");
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void MoveIfPossible_ShouldNotMoveForInvalidDirection()
    {
        _unit.MoveIfPossible("nowhere");
        Assert.Equal(_room1, _unit.Location);
    }
}
