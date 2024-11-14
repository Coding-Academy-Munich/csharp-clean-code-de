using AdventureV6.Actions;
using AdventureV6.ActionStrategies;

namespace AdventureV6;

public class Game(World world, Player player)
{
    public static Game FromJson(string json, string playerName)
    {
        World theWorld = WorldFactory.FromJsonFile(json);
        var thePlayer = new Player(
            playerName,
            theWorld.GetLocationByName(theWorld.InitialLocationName),
            new SelectActionInteractively());
        return new Game(theWorld, thePlayer);
    }

    public void Run()
    {
        try
        {
            for (var i = 0; i < 10; i++)
            {
                Player.TakeTurn();
                Console.WriteLine(Player.Location.Name);
                Console.Write("    ");
                Console.WriteLine(Player.Location.Description);
            }

            Console.WriteLine("No more turns left...");
        }
        catch (QuitGameException)
        {
            Console.WriteLine("Quitting the game...");
        }

    }

    public World World { get; } = world;
    public Player Player { get; } = player;
}
