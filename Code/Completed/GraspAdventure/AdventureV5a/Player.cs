using AdventureV5a.Actions;

namespace AdventureV5a;

public class Player(Pawn pawn)
{
    public Player(string name, Location location) : this(new Pawn(name, location))
    {
    }

    public Pawn Pawn { get; } = pawn;
    public Location Location => Pawn.Location;

    public void TakeTurn()
    {
        List<IAction> possibleActions = GetPossibleActions();
        IAction selectedAction = SelectAction(possibleActions);
        PerformIfPossible(selectedAction);
    }

    internal List<IAction> GetPossibleActions()
    {
        return Location.ConnectedDirections
            .Select(direction => new MoveAction(direction))
            .ToList<IAction>();
    }

    internal static IAction SelectAction(List<IAction> possibleActions)
    {
        return possibleActions.Count == 0 ? new SkipTurnAction() : possibleActions[0];
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
}
