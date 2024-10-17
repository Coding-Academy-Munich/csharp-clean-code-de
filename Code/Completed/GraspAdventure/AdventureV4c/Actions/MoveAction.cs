namespace AdventureV4c.Actions;

public class MoveAction(string direction) : IAction
{
    public string GetDescription()
    {
        return $"Move the player {Direction}.";
    }

    public void Perform(Pawn instigator)
    {
        instigator.MoveToLocation(instigator.Location.GetConnectedLocation(Direction));
    }

    public string Direction { get; } = direction;
}
