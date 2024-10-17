using AdventureV4c.Actions;

namespace AdventureV4c.Tests;

public class PawnV4cTests
{
    private readonly Location _room1;
    private readonly Location _room2;
    private readonly Pawn _unit;

    public PawnV4cTests()
    {
        _room1 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 1"});
        _room2 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 2"});
        _room1.SetConnectedLocation("north", _room2);
        _unit = new Pawn("Player", _room1);

    }

    [Fact]
    public void Perform_MoveAction_ShouldMoveToConnectedLocation()
    {
        _unit.Perform(new MoveAction("north"));
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void Perform_MoveAction_ShouldThrowForInvalidDirection()
    {
        Assert.Throws<ArgumentException>(() => _unit.Perform(new MoveAction("nowhere")));
    }

    [Fact]
    public void Perform_SkipTurnAction_ShouldDoNothing()
    {
        _unit.Perform(new SkipTurnAction());
        Assert.Equal(_room1, _unit.Location);
    }

    [Fact]
    public void PerformIfPossible_MoveAction_ShouldMoveToConnectedLocation()
    {
        _unit.PerformIfPossible(new MoveAction("north"));
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void PerformIfPossible_MoveAction_ShouldNotMoveForInvalidDirection()
    {
        _unit.PerformIfPossible(new MoveAction("nowhere"));
        Assert.Equal(_room1, _unit.Location);
    }

    [Fact]
    public void PerformIfPossible_SkipTurnAction_ShouldDoNothing()
    {
        _unit.PerformIfPossible(new SkipTurnAction());
        Assert.Equal(_room1, _unit.Location);
    }
}
