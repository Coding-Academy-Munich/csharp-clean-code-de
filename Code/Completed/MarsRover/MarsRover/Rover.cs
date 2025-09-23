using System.Diagnostics.CodeAnalysis;

namespace MarsRover;

/// <summary>
/// The position of the Rover on the grid.
/// Note: Using a record struct for Point provides value equality by default.
/// </summary>
/// <param name="X"></param>
/// <param name="Y"></param>
public record struct Point(int X, int Y);

/// <summary>
/// Represents the direction of movement for the Rover.
/// </summary>
public enum Direction
{
    N,
    E,
    S,
    W
}

/// <summary>
/// Represents the grid on which the rover moves.
/// This class was created during the "Refactor" step of the TDD cycle
/// when a test for wrapping around the grid revealed that the Rover class
/// had too much responsibility (knowing the world's boundaries).
/// </summary>
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class Grid
{
    public int Width { get; }
    public int Height { get; }

    public Grid(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Grid dimensions must be positive.");
        }

        Width = width;
        Height = height;
    }

    // Calculates the next position, handling the "wrapping" logic at the edges.
    public Point CalculateNextPosition(Point current, Direction dir)
    {
        return dir switch
        {
            Direction.N => new Point(current.X, (current.Y + 1) % Height),
            Direction.S => new Point(current.X, (current.Y - 1 + Height) % Height),
            Direction.E => new Point((current.X + 1) % Width, current.Y),
            Direction.W => new Point((current.X - 1 + Width) % Width, current.Y),
            _ => current
        };
    }
}

/// <summary>
/// Represents a Mars Rover that navigates a grid.
/// The Rover's design evolved through TDD. It delegates movement
/// calculations to the Grid, resulting in a clean, decoupled design.
/// </summary>
public class Rover
{
    public Point Position { get; private set; }
    public Direction Direction { get; private set; }
    private readonly Grid _grid;

    public Rover(Grid grid, Point startPosition = new(), Direction startDirection = Direction.N)
    {
        _grid = grid ?? throw new ArgumentNullException(nameof(grid), "Rover must be placed on a valid grid.");
        Position = startPosition;
        Direction = startDirection;
    }

    public void ExecuteCommands(string commands)
    {
        foreach (char command in commands)
        {
            switch (command)
            {
                case 'R':
                    TurnRight();
                    break;
                case 'L':
                    TurnLeft();
                    break;
                case 'M':
                    Move();
                    break;
                // Invalid commands are ignored
            }
        }
    }

    private void TurnRight()
    {
        Direction = Direction switch
        {
            Direction.N => Direction.E,
            Direction.E => Direction.S,
            Direction.S => Direction.W,
            Direction.W => Direction.N,
            _ => Direction
        };
    }

    private void TurnLeft()
    {
        Direction = Direction switch
        {
            Direction.N => Direction.W,
            Direction.W => Direction.S,
            Direction.S => Direction.E,
            Direction.E => Direction.N,
            _ => Direction
        };
    }

    private void Move()
    {
        // The Rover delegates the responsibility of calculating the next position
        // to the Grid. This is one of the key design insights from TDD.
        Position = _grid.CalculateNextPosition(Position, Direction);
    }

    public override string ToString()
    {
        return $"Rover({Position.X}, {Position.Y})->{Direction}";
    }
}
