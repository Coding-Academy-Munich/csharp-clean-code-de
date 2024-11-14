namespace AdventureV5c.ActionStrategies;

public class SelectAggressiveActionStrategy : ISelectActionStrategy
{
    public IAction SelectAction(Player player, List<IAction> actions)
    {
        return ActionStrategyUtils.SelectActionWithTags(ActionTags.Aggressive, actions);
    }

    public bool IsInteractive()
    {
        return false;
    }
}
