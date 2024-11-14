using AdventureV6.Actions;

namespace AdventureV6.ActionStrategies;

public class SelectActionInteractively : ISelectActionStrategy
{
    public IAction SelectAction(Player player, List<IAction> actions)
    {
        while (true)
        {
            Console.WriteLine("Available actions:");
            for (var i = 0; i < actions.Count; i++)
            {
                Console.WriteLine($"{i + 1,2}. {actions[i].Description}");
            }
            Console.WriteLine("Select an action:");
            try
            {
                int selectedActionIndex = int.Parse(Console.ReadLine() ?? "invalid");
                if (selectedActionIndex >= 1 && selectedActionIndex <= actions.Count)
                    return actions[selectedActionIndex - 1];
                Console.WriteLine("Please enter a valid action number.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }

    public bool IsInteractive()
    {
        return true;
    }
}
