using AdventureV5b.Actions;

namespace AdventureV5b;

public class Player(Pawn pawn, PlayerStrategy strategy = PlayerStrategy.FirstAction)
{
    public Player(string name, Location location, PlayerStrategy strategy = PlayerStrategy.FirstAction) : this(
        new Pawn(name, location), strategy)
    {
    }

    public Pawn Pawn { get; } = pawn;
    public Location Location => Pawn.Location;
    public PlayerStrategy Strategy { get; set; } = strategy;
    public bool IsDebugModeActive { get; set; } = false;

    public bool IsInteractive => Strategy == PlayerStrategy.Interactive;

    public void TakeTurn()
    {
        List<IAction> possibleActions = GetPossibleActions();
        IAction selectedAction = SelectAction(possibleActions);
        PerformIfPossible(selectedAction);
    }

    internal List<IAction> GetPossibleActions()
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

    internal IAction SelectAction(List<IAction> possibleActions)
    {
        if (possibleActions.Count == 0)
        {
            return new SkipTurnAction();
        }

        switch (Strategy)
        {
            case PlayerStrategy.Interactive:
                return SelectActionInteractively(possibleActions);
            case PlayerStrategy.FirstAction:
                return possibleActions[0];
            case PlayerStrategy.RandomAction:
                return possibleActions[new Random().Next(possibleActions.Count)];
            case PlayerStrategy.Aggressive:
                return SelectActionWithTag(ActionTags.Aggressive, possibleActions);
            case PlayerStrategy.Helpful:
                return SelectActionWithTag(ActionTags.Helpful, possibleActions);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static IAction SelectActionInteractively(List<IAction> possibleActions)
    {
        while (true)
        {
            Console.WriteLine("Available actions:");
            for (var i = 0; i < possibleActions.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {possibleActions[i].Description}");
            }

            Console.WriteLine("Please select an action:");
            string? input = Console.ReadLine();
            if (input != null && int.TryParse(input, out int selectedActionIndex) && selectedActionIndex > 0 &&
                selectedActionIndex <= possibleActions.Count)
            {
                return possibleActions[selectedActionIndex - 1];
            }

            Console.WriteLine("Invalid input. Please try again.");
        }
    }

    private static IAction SelectActionWithTag(ActionTags tag, List<IAction> possibleActions)
    {
        if (possibleActions.Count == 0)
        {
            return new SkipTurnAction();
        }

        List<IAction> actionsWithTag = possibleActions.Where(a => a.Tags.HasFlag(tag)).ToList();
        return actionsWithTag.Count == 0 ? possibleActions[0] : actionsWithTag[0];
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
