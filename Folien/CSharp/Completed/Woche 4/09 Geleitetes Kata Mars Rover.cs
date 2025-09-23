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
//   Rover-Objekt an der Position `{0, 0}` und in Richtung `N` (Norden)
//   orientiert ist.
// - Dieser Test wird fehlschlagen, da die Rover-Klasse noch nicht existiert.

// %% [markdown]
//
// ```csharp
// using Xunit;
//
// namespace MarsRover.Tests;
//
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
// ### 2. Zyklus: Verhalten hinzufügen - Drehen
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
// public void Rover_Can_Turn_Right()
// {
//     var rover = new Rover();
//     rover.ExecuteCommands("R"); // Fails: ExecuteCommands does not exist
//     Assert.Equal(Direction.E, rover.Direction);
// }
// ```

// %% [markdown]
//
// #### GRÜN & REFACTOR:
// - Wir implementieren eine `ExecuteCommands`-Methode mit einfacher Logik.
// - Wir fügen Tests für Links- und Rechtsdrehungen hinzu und refaktorisieren die Logik.

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
//                 Direction.S => Direction.W,
//                 Direction.W => Direction.N,
//                 _ => Direction
//             };
//         }
//         // Logic for 'L' would be added here next
//     }
// }
// ```

// %% [markdown]
//
// ### 3. Zyklus: Verhalten hinzufügen - Bewegen
//
// #### ROT:
// - Wir schreiben einen Test, um zu überprüfen, ob das Senden von `M` an einen
//   Rover bei `{10, 10}`, der nach `N` ausgerichtet ist, ihn nach `{10, 11}`
//   bewegt.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Fact]
// public void Rover_Moves_Forward_Facing_North()
// {
//     var rover = new Rover { Position = new Point(10, 10), Direction = Direction.N };
//     rover.ExecuteCommands("M"); // Fails: 'M' logic does not exist
//     Assert.Equal(new Point(10, 11), rover.Position);
// }
// ```

// %% [markdown]
//
// ### 4. Zyklus: Design durch eine neue Anforderung treiben
//
// #### ROT:
// - Wir führen das Konzept des "wrapping" ein. Ein Rover am Rand eines
//   10x10-Gitters soll auf der gegenüberliegenden Seite wieder erscheinen.
// - Dieser Test wird fehlschlagen, weil der Rover kein Konzept von einem Gitter
//   hat.

// %% [markdown]
//
// ```csharp
// // In MarsRoverTests.cs
// [Fact]
// public void Rover_Wraps_Around_The_Grid_Edge()
// {
//     var rover = new Rover { Position = new Point(5, 9), Direction = Direction.N };
//     rover.ExecuteCommands("M");
//     // Fails: Position will be (5, 10), not (5, 0).
//     // The Rover needs to know about the grid size (e.g., 10x10).
//     Assert.Equal(new Point(5, 0), rover.Position);
// }
// ```

// %% [markdown]
//
// ### Der "Aha!"-Moment: Refactoring zu einem besseren Design
//
// #### GRÜN & REFACTOR:
// - Der Test hat einen Designfehler aufgedeckt: Der Rover weiß zu viel über die Regeln der Welt.
// - Dies führt zu einer **bedeutenden Designverbesserung**: Wir erstellen eine `Grid`-Klasse.
// - Der Rover delegiert die Bewegungsberechnung an das `Grid`.
// - Das Ergebnis ist ein entkoppeltes Design, das direkt aus der
//   Notwendigkeit geboren wurde, einen Test zu bestehen.

// %% [markdown]
//
// ```csharp
// // Final Grid class
// public class Grid
// {
//     public int Width { get; }
//     public int Height { get; }
//
//     public Grid(int width, int height) { /* ... */ }
//
//     public Point CalculateNextPosition(Point current, Direction dir)
//     {
//         return dir switch
//         {
//             Direction.N => new Point(current.X, (current.Y + 1) % Height),
//             // ... other directions with wrapping logic
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
//     public Rover(Grid grid, /*...*/) {
//         _grid = grid;
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
