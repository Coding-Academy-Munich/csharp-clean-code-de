namespace AdventureV5b.Actions;

public class InvestigateAction : IAction
{
    public string Description => "Investigate the area for clues.";
    public ActionTags Tags => ActionTags.Investigate | ActionTags.Aggressive;

    public void Perform(Player instigator)
    {
        // Investigate the area for clues
    }
}
