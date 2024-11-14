using AdventureV6.Actions;

namespace AdventureV6.ActionStrategies;

public class SelectFirstActionStrategy : ISelectActionStrategy
{
    public IAction SelectAction(Player player, List<IAction> actions)
    {
        return actions.Count == 0 ? new SkipTurnAction() : actions[0];
    }

    public bool IsInteractive()
    {
        return false;
    }
}
