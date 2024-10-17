namespace AdventureV5a.Tests;

public class PawnV5ATests
{
    private readonly Location _room1;
    private readonly Location _room2;
    private readonly Pawn _unit;

    public PawnV5ATests()
    {
        _room1 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 1"});
        _room2 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 2"});
        _room1.SetConnectedLocation("north", _room2);
        _unit = new Pawn("Player", _room1);
    }

    [Fact]
    public void MoveToLocation_ShouldMoveToConnectedLocation()
    {
        _unit.MoveToLocation(_room2);
        Assert.Equal(_room2, _unit.Location);
    }
}
