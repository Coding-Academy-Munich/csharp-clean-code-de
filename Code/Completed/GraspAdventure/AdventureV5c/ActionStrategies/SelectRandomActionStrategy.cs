using AdventureV5c.Actions;

namespace AdventureV5c.ActionStrategies;

public class SelectRandomActionStrategy : ISelectActionStrategy
{
    public IAction SelectAction(Player player, List<IAction> actions)
    {
        if (actions.Count == 0)
        {
            return new SkipTurnAction();
        }
        var random = new Random();
        return actions[random.Next(actions.Count)];
    }

    public bool IsInteractive()
    {
        return false;
    }
}
