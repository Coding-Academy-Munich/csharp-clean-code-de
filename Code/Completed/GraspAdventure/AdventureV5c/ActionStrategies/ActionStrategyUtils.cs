using AdventureV5c.Actions;

namespace AdventureV5c.ActionStrategies;

public static class ActionStrategyUtils
{
    public static IAction SelectActionWithTags(ActionTags tags, List<IAction> actions)
    {
        if (actions.Count == 0)
        {
            return new SkipTurnAction();
        }

        // Try to find an action that has all tags
        foreach (IAction action in actions)
        {
            if ((action.Tags & tags) == tags)
            {
                return action;
            }
        }
        // Find an action that matches at least some of the tags
        foreach (IAction action in actions)
        {
            if ((action.Tags & tags) != 0)
            {
                return action;
            }
        }
        // No action that matches any tags, return the first one.
        return actions[0];
    }
}
