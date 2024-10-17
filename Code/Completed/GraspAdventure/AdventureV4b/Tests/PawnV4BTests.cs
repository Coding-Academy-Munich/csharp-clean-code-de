namespace AdventureV4b.Tests;

public class PawnV4BTests
{
    private readonly Location _room1;
    private readonly Location _room2;
    private readonly Pawn _unit;

    public PawnV4BTests()
    {
        _room1 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 1"});
        _room2 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 2"});
        _room1.SetConnectedLocation("north", _room2);
        _unit = new Pawn("Player", _room1);

    }

    [Fact]
    public void Perform_MoveAction_ShouldMoveToConnectedLocation()
    {
        _unit.Perform(Action.Move, "north");
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void Perform_MoveAction_ShouldThrowForInvalidDirection()
    {
        Assert.Throws<ArgumentException>(() => _unit.Perform(Action.Move, "nowhere"));
    }

    [Fact]
    public void Perform_SkipTurnAction_ShouldDoNothing()
    {
        _unit.Perform(Action.SkipTurn, "");
        Assert.Equal(_room1, _unit.Location);
    }

    [Fact]
    public void PerformIfPossible_MoveAction_ShouldMoveToConnectedLocation()
    {
        _unit.PerformIfPossible(Action.Move, "north");
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void PerformIfPossible_MoveAction_ShouldNotMoveForInvalidDirection()
    {
        _unit.PerformIfPossible(Action.Move, "nowhere");
        Assert.Equal(_room1, _unit.Location);
    }

    [Fact]
    public void PerformIfPossible_SkipTurnAction_ShouldDoNothing()
    {
        _unit.PerformIfPossible(Action.SkipTurn, "");
        Assert.Equal(_room1, _unit.Location);
    }
}
