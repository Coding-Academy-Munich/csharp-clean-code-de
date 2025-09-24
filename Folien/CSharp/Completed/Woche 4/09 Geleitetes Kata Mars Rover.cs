// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Geleitetes Kata: Mars Rover</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Geleitetes Kata: Mars Rover
//
// Wir werden einen Rover programmieren, der sich auf einem Grid bewegen kann.
// - Der Rover hat
//   - eine Position `(x, y)` und
//   - eine Richtung (North, East, South, West).
// - Er kann eine Folge von Befehlen empfangen:
//   - `L` (nach links 90 Grad drehen),
//   - `R` (nach rechts 90 Grad drehen) und
//   - `M` (ein Gitterfeld nach vorne bewegen).

// %% [markdown]
//
// ### 1. Zyklus: Erzeugen des Rovers
//
// #### ROT:
// - Wir schreiben einen Test, der überprüft, dass ein neu erzeugtes
//   Rover-Objekt an der Position `(0, 0)` und in Richtung `N` (Norden)
//   orientiert ist.
// - Dieser Test wird fehlschlagen, da die Rover-Klasse noch nicht existiert.

// %% [markdown]
//
// ```csharp
// public class MarsRoverTests
// {
//     [Fact]
//     public void Rover_Initializes_To_Zero_Zero_Facing_North()
//     {
//         var rover = new Rover(); // Fails: Rover does not exist
//
//         Assert.Equal(new Point(0, 0), rover.Position);
//         Assert.Equal(Direction.N, rover.Direction);
//     }
// }
// ```

// %% [markdown]
//
// #### GRÜN:
//
// - Wir erstellen die `Rover`-Klasse und die benötigten Typen `Point` und `Direction`, um den Test zu bestehen.

// %% [markdown]
//
// ```csharp
// namespace MarsRover;
//
// public record struct Point(int X, int Y);
//
// public enum Direction { N, E, S, W }
//
// public class Rover
// {
//     public Point Position { get; private set; }
//     public Direction Direction { get; private set; }
//
//     public Rover()
//     {
//         Position = new Point(0, 0);
//         Direction = Direction.N;
//     }
// }
// ```

// %% [markdown]
//
// ### 2. Zyklus: Verhalten hinzufügen - Drehen nach rechts
//
// #### ROT:
// - Wir schreiben einen Test, um zu überprüfen, ob das Senden des Befehls `R`
//   an einen nach `N` ausgerichteten Rover ihn nach `E` (Osten) ausrichtet.
// - Der Test wird fehlschlagen, da es noch keine Methode zur Befehlsausführung
//   gibt.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Fact]
// public void Rover_Turning_Right_Once_Changes_Direction_To_East()
// {
//     var rover = new Rover();
//     rover.ExecuteCommands("R"); // Fails: ExecuteCommands does not exist
//     Assert.Equal(Direction.E, rover.Direction);
// }
// ```

// %% [markdown]
//
// #### GRÜN:
//
// - Wir implementieren eine `ExecuteCommands`-Methode mit einfacher Logik.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// public void ExecuteCommands(string commands)
// {
//     if (command == "R")
//     {
//         Direction = Direction.E;
//     }
// }
// ```

// %% [markdown]
//
// ### 3. Zyklus: Verhalten hinzufügen - Zweimal Drehen nach rechts
//
// #### ROT:
// - Wir schreiben einen Test, um zu überprüfen, ob das Senden von `RR` an einen
//   Rover, der nach `N` ausgerichtet ist, ihn nach `S` (Süden) ausrichtet.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Fact]
// public void Rover_Turning_Right_Twice_Changes_Direction_To_South()
// {
//     var rover = new Rover();
//     rover.ExecuteCommands("RR");
//     Assert.Equal(Direction.S, rover.Direction);
// }
// ```

// %% [markdown]
//
// #### GRÜN:
// - Wir erweitern die `ExecuteCommands`-Methode, um die Logik für das Drehen
//   nach rechts, wenn der Rover bereits nach Osten ausgerichtet ist, zu
//   implementieren.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// public void ExecuteCommands(string commands)
// {
//     if (command == "R")
//     {
//         Direction = Direction.E;
//     }
//     else if (command == "RR")
//     {
//         Direction = Direction.S;
//     }
// }
// ```

// %% [markdown]
//
// #### REFACTOR (1):
//
// - Wir ersetzen die ad-hoc Logik in der `ExecuteCommands`-Methode durch eine
//   Schleife, die jeden Befehl einzeln verarbeitet.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// public void ExecuteCommands(string commands)
// {
//     foreach (char command in commands)
//     {
//         if (command == 'R')
//         {
//             Direction = Direction switch
//             {
//                 Direction.N => Direction.E,
//                 Direction.E => Direction.S,
//                 _ => Direction
//             };
//         }
//     }
// }
// ```

// %% [markdown]
//
// #### REFACTOR (2):
//
// - Wir refaktorisieren die Drehrichtung in eine separate Methode `TurnRight`
// - Wir erzeugen den Rover im Konstruktor der Test-Klasse, und speichern ihn in
//   einer Member-Variable, um Wiederholungen zu vermeiden.
// - Wir ersetzen die zwei Tests für das Drehen  nach rechts durch einen
//   parametrisierten Test.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// private void TurnRight()
// {
//     Direction = Direction switch
//     {
//         Direction.N => Direction.E,
//         Direction.E => Direction.S,
//         _ => Direction
//     };
// }
//
// public void ExecuteCommands(string commands)
// {
//     foreach (char command in commands)
//     {
//         if (command == 'R')
//         {
//             TurnRight();
//         }
//     }
// }
// ```
//
// ```csharp
// // In MarsRoverTests.cs
// public class MarsRoverTests
// {
//     private readonly Rover _rover;
//
//     public MarsRoverTests()
//     {
//         _rover = new Rover(new Point(0, 0), Direction.N);
//     }
//
//     // ...
//
//     [Theory]
//     [InlineData("R", Direction.E)]
//     [InlineData("RR", Direction.S)]
//     public void Rover_Turning_Right_Changes_Direction_Correctly(string commands, Direction expectedDirection)
//     {
//         _rover.ExecuteCommands(commands);
//         Assert.Equal(expectedDirection, _rover.Direction);
//     }
// }
// ```

// %% [markdown]
//
// ### 4. Zyklus: Verhalten hinzufügen - Mehrfach Drehen nach rechts
//
// #### ROT:
//
// - Wir erweitern den parametrischen Test, um zu überprüfen, ob die Befehle
//   `RRR` und `RRRR` den Rover korrekt ausrichten.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Theory]
// [InlineData("R", Direction.E)]
// [InlineData("RR", Direction.S)]
// [InlineData("RRR", Direction.W)]
// [InlineData("RRRR", Direction.N)]
// public void Rover_Turning_Right_Changes_Direction_Correctly(string commands, Direction expectedDirection)
// {
//     _rover.ExecuteCommands(commands);
//     Assert.Equal(expectedDirection, _rover.Direction);
// }
// ```

// %% [markdown]
//
// #### GRÜN & REFACTOR:
//
// - Wir erweitern die `TurnRight`-Methode, um alle vier Richtungen zu
//   unterstützen.
// - Der parametrisierte Test ist jetzt grün.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// private void TurnRight()
// {
//     Direction = Direction switch
//     {
//         Direction.N => Direction.E,
//         Direction.E => Direction.S,
//         Direction.S => Direction.W,
//         Direction.W => Direction.N,
//         _ => Direction
//     };
// }
// ```

// %% [markdown]
//
// ### 5. Zyklus: Verhalten hinzufügen - Drehen nach links
//
// #### ROT:
//
// - Wir erweitern den parametrischen Test um ein- bis viermaliges Drehen nach
//   links.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Theory]
// [InlineData("R", Direction.E)]
// [InlineData("RR", Direction.S)]
// [InlineData("RRR", Direction.W)]
// [InlineData("RRRR", Direction.N)]
// [InlineData("L", Direction.W)]
// [InlineData("LL", Direction.S)]
// [InlineData("LLL", Direction.E)]
// [InlineData("LLLL", Direction.N)]
// public void Rover_Turning_Changes_Direction_Correctly(string commands, Direction expectedDirection)
// {
//     _rover.ExecuteCommands(commands);
//     Assert.Equal(expectedDirection, _rover.Direction);
// }
// ```

// %% [markdown]
//
// #### GRÜN & REFACTOR:
//
// - Wir implementieren die `TurnLeft`-Methode und erweitern die
//   `ExecuteCommands`-Methode, um den `L`-Befehl zu unterstützen.
// - Der parametrisierte Test ist jetzt grün.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// private void TurnLeft()
// {
//     Direction = Direction switch
//     {
//         Direction.N => Direction.W,
//         Direction.W => Direction.S,
//         Direction.S => Direction.E,
//         Direction.E => Direction.N,
//         _ => Direction
//     };
// }
//
// public void ExecuteCommands(string commands)
// {
//     foreach (char command in commands)
//     {
//         if (command == 'R')
//         {
//             TurnRight();
//         }
//         else if (command == 'L')
//         {
//             TurnLeft();
//         }
//     }
// }
// ```

// %% [markdown]
//
// ### 6. Zyklus: Verhalten hinzufügen - Bewegen
//
// #### ROT:
// - Wir schreiben einen Test, um zu überprüfen, ob das Senden von `M` an einen
//   Rover bei `(10, 10)`, der nach `N` ausgerichtet ist, ihn nach `(10, 11)`
//   bewegt.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Fact]
// public void Rover_Moves_Forward_Facing_North()
// {
//     var rover = new Rover(new Point(10, 10), Direction.N);
//     rover.ExecuteCommands("M"); // Fails: 'M' logic does not exist
//     Assert.Equal(new Point(10, 11), rover.Position);
// }
// ```

// %% [markdown]
//
// #### GRÜN & REFACTOR:
//
// - Wir implementieren die Bewegungslogik für die `N`-Richtung.
// - Der Test ist jetzt grün.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// private void Move()
// {
//     Position = Direction switch
//     {
//         Direction.N => new Point(Position.X, Position.Y + 1),
//         _ => Position
//     };
// }
//
// public void ExecuteCommands(string commands)
// {
//     foreach (char command in commands)
//     {
//         if (command == 'R')
//         {
//             TurnRight();
//         }
//         else if (command == 'L')
//         {
//             TurnLeft();
//         }
//         else if (command == 'M')
//         {
//             Move();
//         }
//     }
// }
// ```

// %% [markdown]
//
// ### 7. Zyklus: Verhalten hinzufügen - Bewegen in alle Richtungen
//
// #### ROT:
// - Wir erweitern den Test, um das Bewegen in alle vier Richtungen zu
//   überprüfen.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Theory]
// [InlineData(Direction.N, 10, 11)]
// [InlineData(Direction.S, 10, 9)]
// [InlineData(Direction.E, 11, 10)]
// [InlineData(Direction.W, 9, 10)]
// public void Rover_Moves_Forward_One_Grid_Point_In_Correct_Direction(
//     Direction direction, int expectedX, int expectedY)
// {
//     // This test requires a specific starting position, so it instantiates its own Rover.
//     var rover = new Rover(new Point(10, 10), direction);
//     var expectedPosition = new Point(expectedX, expectedY);
//     rover.ExecuteCommands("M");
//     Assert.Equal(expectedPosition, rover.Position);
//     Assert.Equal(direction, rover.Direction);
// }
// ```

// %% [markdown]
//
// #### GRÜN & REFACTOR:
//
// - Wir erweitern die `Move`-Methode, um alle vier Richtungen zu unterstützen.
// - Der parametrisierte Test ist jetzt grün.

// %% [markdown]
//
// ```csharp
// // In Rover.cs
// private void Move()
// {
//     Position = Direction switch
//     {
//         Direction.N => new Point(Position.X, Position.Y + 1),
//         Direction.E => new Point(Position.X + 1, Position.Y),
//         Direction.S => new Point(Position.X, Position.Y - 1),
//         Direction.W => new Point(Position.X - 1, Position.Y),
//         _ => Position
//     };
// }
// ```


// %% [markdown]
//
// ### 8. Zyklus: Design durch eine neue Anforderung treiben
//
// #### ROT:
//
// - Wir führen das Konzept des "wrapping" ein.
//   - Ein Rover am Rand eines Grids soll auf der gegenüberliegenden Seite wieder erscheinen.
//   - Die Größe des Grids ist konfigurierbar.
// - Dieser Test wird fehlschlagen, weil der Rover kein Konzept von einem Grid hat.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Fact]
// public void Rover_Wraps_Around_The_Grid_Edge()
// {
//     // Rover should be on a 10x10 grid...
//     var rover = new Rover(new Point(5, 9), Direction.N);
//     rover.ExecuteCommands("M");
//     // Fails: Position will be (5, 10), not (5, 0).
//     Assert.Equal(new Point(5, 0), rover.Position);
//     // The Rover needs to know about the grid size (e.g., 10x10) to make this work.
// }
// ```

// %% [markdown]
//
// - Wir müssen unser Design überdenken, um das neue "wrapping"-Verhalten zu
//   unterstützen.
// - Dazu können wir mit verschiedenen Ansätzen experimentieren, indem wir den
//   Test auf verschiedene Arten schreiben:
//   - Angeben der Grid-Größe bei der Ausführung von Befehlen:
//     ```csharp
//     rover.ExecuteCommands("M", gridWidth: 10, gridHeight: 10);
//     ```
//   - Übergabe der Grid-Größe im Konstruktor des Rovers:
//     ```csharp
//     var rover = new Rover(new Point(5, 9), Direction.N, gridWidth: 10, gridHeight: 10);
//     ```
//   - Einführung einer `Grid`-Klasse, die für die "Topologie" der Welt
//     verantwortlich ist:
//     ```csharp
//     var grid = new Grid(width: 10, height: 10);
//     var rover = new Rover(grid, new Point(5, 9), Direction.N);
//     ```

// %% [markdown]
//
// #### GRÜN & REFACTOR:
//
// - Der Test hat ein Problem mit unserem bisherigen Design aufgedeckt:
//   - Der Rover weiß viel über die Regeln der Welt.
//   - Es wäre besser, die Verantwortlichkeit für die "Grid-Logik" auszulagern.
// - Designverbesserung:
//   - Wir erstellen eine `Grid`-Klasse.
//   - Der Rover delegiert die Bewegungsberechnung an das `Grid`.
// - Das Ergebnis ist ein entkoppeltes Design, das direkt aus der Notwendigkeit
//   geboren wurde, einen Test zu bestehen.

// %% [markdown]
//
// ```csharp
// // Grid class
// public class Grid
// {
//     public int Width { get; }
//     public int Height { get; }
//
//     public Grid(int width, int height)
//     {
//         if (width <= 0 || height <= 0)
//         {
//             throw new ArgumentException("Grid dimensions must be positive.");
//         }
//         Width = width;
//         Height = height;
//     }
//
//     public Point CalculateNextPosition(Point current, Direction dir)
//     {
//         return dir switch
//         {
//             Direction.N => new Point(current.X, (current.Y + 1) % Height),
//             Direction.E => new Point((current.X + 1) % Width, current.Y),
//             Direction.S => new Point(current.X, (current.Y - 1 + Height) % Height),
//             Direction.W => new Point((current.X - 1 + Width) % Width, current.Y),
//             _ => current
//         };
//     }
// }
// ```

// %% [markdown]
//
// ```csharp
// // Updated Rover class
// public class Rover
// {
//     private readonly Grid _grid;
//
//     public Rover(Grid grid, Point startPosition = new(), Direction startDirection = Direction.N)
//     {
//         _grid = grid;
//         Position = startPosition;
//         Direction = startDirection;
//     }
//
//     private void Move()
//     {
//         Position = _grid.CalculateNextPosition(Position, Direction);
//     }
//     // ... rest of the Rover code
// }
// ```

// %% [markdown]
//
// - Der Test ist jetzt grün.
// - Da die Grid-Instanz nicht modifiziert wird, können wir eine Fixture
//   einführen, die sie für alle Tests bereitstellt.
// - In diesem Fall ist das nicht notwendig, da die Grid-Instanz sehr billig zu
//   erstellen ist, aber für komplexere Objekte ist es eine gute Praxis.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// public class MarsRoverTestFixture
// {
//     public Grid Grid { get; } = new(100, 100);
// }
//
// public class MarsRoverTests : IClassFixture<MarsRoverTestFixture>
// {
//     private readonly Grid _grid;
//     private readonly Rover _rover;
//
//     public MarsRoverTests(MarsRoverTestFixture fixture)
//     {
//         _grid = fixture.Grid;
//         _rover = new Rover(_grid, new Point(0, 0), Direction.N);
//     }
//     // ... rest of the tests
// }
// ```

// %% [markdown]
//
// ## Kata: FizzBuzz
//
// Schreiben Sie eine Funktion
// ```csharp
// void PrintFizzBuzz(int n);
// ```
// die die Zahlen von 1 bis `n` auf dem Bildschirm ausgibt aber dabei
//
// - jede Zahl, die durch 3 teilbar ist, durch `Fizz` ersetzt
// - jede Zahl, die durch 5 teilbar ist, durch `Buzz` ersetzt
// - jede Zahl, die durch 3 und 5 teilbar ist, durch `FizzBuzz` ersetzt

// %% [markdown]
//
// Zum Beispiel soll `fizz_buzz(16)` die folgende Ausgabe erzeugen:
//
// ```plaintext
// 1
// 2
// Fizz
// 4
// Buzz
// Fizz
// 7
// 8
// Fizz
// Buzz
// 11
// Fizz
// 13
// 14
// FizzBuzz
// 16
// ```

// %%
using System.Collections.Generic;


// %%
#r "nuget: xunit, *"
using Xunit;

// %%
#load "XunitTestRunner.cs"
using static XunitTestRunner;

// %%
public class FizzBuzzSimple {
    public static void PrintFizzBuzz(int n) {
        for (int i = 1; i <= n; i++) {
            if (i % 3 == 0 && i % 5 == 0) {
                Console.WriteLine("FizzBuzz");
            } else if (i % 3 == 0) {
                Console.WriteLine("Fizz");
            } else if (i % 5 == 0) {
                Console.WriteLine("Buzz");
            } else {
                Console.WriteLine(i);
            }
        }
    }

    // Main method (not named Main in notebook...)
    public static void Run(string[] args) {
        // Example usage
        PrintFizzBuzz(16);
    }
}

// %%
FizzBuzzSimple.Run([]);

// %%
using System.IO;
using System.Collections.Generic;

// %%
public class FizzBuzz {
    public static List<string> GenerateFizzBuzz(int n) {
        if (n < 0)
            throw new ArgumentException("Input must be non-negative");

        List<string> result = new List<string>();
        for (int i = 1; i <= n; i++) {
            if (i % 3 == 0 && i % 5 == 0)
                result.Add("FizzBuzz");
            else if (i % 3 == 0)
                result.Add("Fizz");
            else if (i % 5 == 0)
                result.Add("Buzz");
            else
                result.Add(i.ToString());
        }
        return result;
    }

    public static void PrintFizzBuzz(int n, TextWriter output) {
        List<string> fizzBuzzList = GenerateFizzBuzz(n);
        foreach (string item in fizzBuzzList) {
            output.WriteLine(item);
        }
    }

    // Main method (not named Main in notebook...)
    public static void Run(string[] args) {
        // Example usage
        PrintFizzBuzz(16, Console.Out);
    }
}

// %%
FizzBuzz.Run([]);

// %%
using Xunit;
using System.IO;
using System.Collections.Generic;
using System.Linq;

// %%
public class FizzBuzzTest {
    [Fact]
    public void GenerateFizzBuzz_ReturnsCorrectSequenceFor15() {
        List<string> expected = new List<string> {
            "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz", "11", "Fizz", "13", "14", "FizzBuzz"
        };
        List<string> result = FizzBuzz.GenerateFizzBuzz(15);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    public void GenerateFizzBuzz_ReturnsCorrectValueForSpecificNumbers(int number, string expected) {
        List<string> result = FizzBuzz.GenerateFizzBuzz(number);
        Assert.Equal(expected, result.Last());
    }

    [Fact]
    public void GenerateFizzBuzz_ReturnsEmptyListForZero() {
        List<string> result = FizzBuzz.GenerateFizzBuzz(0);
        Assert.Empty(result);
    }

    [Fact]
    public void GenerateFizzBuzz_ThrowsArgumentExceptionForNegativeNumber() {
        Assert.Throws<ArgumentException>(() => FizzBuzz.GenerateFizzBuzz(-1));
    }

    [Fact]
    public void GenerateFizzBuzz_ReturnsCorrectSequenceFor100() {
        List<string> result = FizzBuzz.GenerateFizzBuzz(100);
        Assert.Equal(100, result.Count);
        Assert.Equal("1", result[0]);
        Assert.Equal("2", result[1]);
        Assert.Equal("Fizz", result[2]);
        Assert.Equal("4", result[3]);
        Assert.Equal("Buzz", result[4]);
        Assert.Equal("Fizz", result[5]);
        Assert.Equal("FizzBuzz", result[14]);
        Assert.Equal("FizzBuzz", result[29]);
        Assert.Equal("FizzBuzz", result[44]);
        Assert.Equal("Buzz", result[99]);
    }

    [Fact]
    public void PrintFizzBuzz_WritesToProvidedTextWriter() {
        StringWriter stringWriter = new StringWriter();
        FizzBuzz.PrintFizzBuzz(5, stringWriter);
        string[] result = stringWriter.ToString().Trim().Split(Environment.NewLine);
        Assert.Equal(new string[] { "1", "2", "Fizz", "4", "Buzz" }, result);
    }
}

// %%
XunitTestRunner.RunTests<FizzBuzzTest>();
