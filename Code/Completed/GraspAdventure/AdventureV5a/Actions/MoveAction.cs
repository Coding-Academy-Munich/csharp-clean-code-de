namespace AdventureV5a.Actions;

public class MoveAction(string direction) : IAction
{
    public string Direction { get; } = direction;
    public string Description => $"Move the player {Direction}.";


    public void Perform(Player instigator)
    {
        instigator.Pawn.MoveToLocation(instigator.Location.GetConnectedLocation(Direction));
    }
}
