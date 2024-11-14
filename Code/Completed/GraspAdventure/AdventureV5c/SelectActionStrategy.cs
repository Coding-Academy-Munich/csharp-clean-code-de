namespace AdventureV5c;

public interface ISelectActionStrategy
{
    IAction SelectAction(Player player, List<IAction> actions);
    bool IsInteractive();
}
