using AdventureV6.Actions;
using AdventureV6.ActionStrategies;

namespace AdventureV6;

public class Player(string name, Location location, ISelectActionStrategy actionStrategy)
{
    public Player(string name, Location location) : this(name, location, new SelectFirstActionStrategy()) { }

    public string Name { get; } = name;
    public Location Location { get; private set; } = location;
    public ISelectActionStrategy ActionStrategy { get; set; } = actionStrategy;
    public bool IsDebugModeActive { get; set; } = false;
    public bool IsInteractive => ActionStrategy.IsInteractive();

    public void MoveToLocation(Location location) => Location = location;

    public void TakeTurn()
    {
        List<IAction> possibleActions = GetPossibleActions();
        IAction selectedAction = SelectAction(possibleActions);
        PerformIfPossible(selectedAction);
    }

    public List<IAction> GetPossibleActions()
    {
        List<IAction> actions = Location.ConnectedDirections
            .Select(direction => new MoveAction(direction))
            .ToList<IAction>();
        actions.Add(new InvestigateAction());
        actions.Add(new SkipTurnAction());

        if (!IsInteractive) return actions;

        actions.Add(new QuitAction());
        if (IsDebugModeActive)
        {
            actions.Add(new ErrorAction());
        }
        return actions;
    }

    public IAction SelectAction(List<IAction> possibleActions)
    {
        return ActionStrategy.SelectAction(this, possibleActions);
    }

    public void Perform(IAction action)
    {
        action.Perform(this);
    }

    public void PerformIfPossible(IAction action)
    {
        try
        {
            Perform(action);
        }
        catch (ArgumentException)
        {
            // ignored
        }
    }

    public override string? ToString() => $"Player {Name} ({Location})";
}
