namespace AdventureV6.Actions;

public class QuitAction : IAction
{
    public string Description => "Quit the game.";
    public ActionTags Tags => ActionTags.Quit;

    public void Perform(Player instigator)
    {
        throw new QuitGameException();
    }
}

public class QuitGameException : Exception
{
}
