namespace AdventureV5c.ActionStrategies;

public class SelectHelpfulActionStrategy : ISelectActionStrategy
{
    public IAction SelectAction(Player player, List<IAction> actions)
    {
        return ActionStrategyUtils.SelectActionWithTags(ActionTags.Helpful, actions);
    }

    public bool IsInteractive()
    {
        return false;
    }
}
