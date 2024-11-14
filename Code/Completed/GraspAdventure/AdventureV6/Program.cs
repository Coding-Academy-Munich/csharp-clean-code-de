using AdventureData;
using AdventureV6;

Game myGame = Game.FromJson("dungeon-locations.json", "Joe Cool");
myGame.Run();
