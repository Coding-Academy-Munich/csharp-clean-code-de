namespace AdventureV5b.Actions;

public class HealAction : IAction
{
    public string Description => "Heal all characters.";
    public ActionTags Tags => ActionTags.Helpful;

    public void Perform(Player instigator)
    {
        // Heal all characters
    }
}
