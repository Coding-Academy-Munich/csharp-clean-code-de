using AdventureV5c;
using AdventureV5c.Actions;
using AdventureV5c.ActionStrategies;

World myWorld = WorldFactory.FromJsonFile("dungeon-locations.json");
var myPlayer = new Player(
    "John Doe",
    myWorld.GetLocationByName(myWorld.InitialLocationName),
    new SelectActionInteractively());

Console.WriteLine($"World = {myWorld}");
Console.WriteLine($"Player = {myPlayer}");
Console.WriteLine("Starting game...");

try
{
    for (var i = 0; i < 10; i++)
    {
        myPlayer.TakeTurn();
        Console.WriteLine(myPlayer.Location.Name);
        Console.Write("    ");
        Console.WriteLine(myPlayer.Location.Description);
    }
    Console.WriteLine("No more turns left...");
}
catch (QuitGameException)
{
    Console.WriteLine("Quitting the game...");
}
