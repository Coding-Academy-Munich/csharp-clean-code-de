using AdventureV5a.Actions;

namespace AdventureV5a.Tests;

public class PlayerV5ATests
{
    private readonly Location _room1;
    private readonly Location _room2;
    private readonly Player _unit;

    public PlayerV5ATests()
    {
        _room1 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 1"});
        _room2 = Location.FromData(new Dictionary<string, object?> {["name"] = "Room 2"});
        _room1.SetConnectedLocation("north", _room2);
        _unit = new Player("Player", _room1);
    }

    [Fact]
    public void TakeTurn_ShouldTakeOnlyPossibleMoveAction()
    {
        _unit.TakeTurn();
        Assert.Equal(_room2, _unit.Location);
    }

    [Fact]
    public void GetPossibleActions_ShouldReturnMoveAction()
    {
        var actions = _unit.GetPossibleActions();
        Assert.Contains(actions, a => a is MoveAction);
    }

    [Fact]
    public void SelectAction_ShouldReturnFirstAction()
    {
        List<IAction> actions = [new MoveAction("north"), new SkipTurnAction(), new MoveAction("south")];
        IAction action = Player.SelectAction(actions);
        Assert.Same(actions[0], action);
    }


    [Fact]
    public void SelectAction_ShouldReturnSkipTurnActionAsDefault()
    {
        List<IAction> actions = [];
        IAction action = Player.SelectAction(actions);
        Assert.IsType<SkipTurnAction>(action);
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
