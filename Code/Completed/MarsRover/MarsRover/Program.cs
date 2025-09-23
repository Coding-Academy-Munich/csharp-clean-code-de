using MarsRover;

Grid grid = new(10, 10);
Rover rover = new(grid);

Console.WriteLine($"{rover}");
rover.ExecuteCommands("LMMRMMR");
Console.WriteLine($"{rover}");
