namespace AdventureV6;

public interface ISelectActionStrategy
{
    IAction SelectAction(Player player, List<IAction> actions);
    bool IsInteractive();
}
