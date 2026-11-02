// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Geleitetes Kata: Mars Rover</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
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
// #### GRÜN:
//
// - Wir erstellen die `Rover`-Klasse und die benötigten Typen `Point` und `Direction`, um den Test zu bestehen.

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
// #### GRÜN:
//
// - Wir implementieren eine `ExecuteCommands`-Methode mit einfacher Logik.

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
// #### GRÜN:
// - Wir erweitern die `ExecuteCommands`-Methode, um die Logik für das Drehen
//   nach rechts, wenn der Rover bereits nach Osten ausgerichtet ist, zu
//   implementieren.

// %% [markdown]
//
// #### REFACTOR (1):
//
// - Wir ersetzen die ad-hoc Logik in der `ExecuteCommands`-Methode durch eine
//   Schleife, die jeden Befehl einzeln verarbeitet.

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
// ### 4. Zyklus: Verhalten hinzufügen - Mehrfach Drehen nach rechts
//
// #### ROT:
//
// - Wir erweitern den parametrischen Test, um zu überprüfen, ob die Befehle
//   `RRR` und `RRRR` den Rover korrekt ausrichten.
// - Der Test wird fehlschlagen.

// %% [markdown]
//
// #### GRÜN & REFACTOR:
//
// - Wir erweitern die `TurnRight`-Methode, um alle vier Richtungen zu
//   unterstützen.
// - Der parametrisierte Test ist jetzt grün.

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
// #### GRÜN & REFACTOR:
//
// - Wir implementieren die `TurnLeft`-Methode und erweitern die
//   `ExecuteCommands`-Methode, um den `L`-Befehl zu unterstützen.
// - Der parametrisierte Test ist jetzt grün.

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
// #### GRÜN & REFACTOR:
//
// - Wir implementieren die Bewegungslogik für die `N`-Richtung.
// - Der Test ist jetzt grün.

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
// #### GRÜN & REFACTOR:
//
// - Wir erweitern die `Move`-Methode, um alle vier Richtungen zu unterstützen.
// - Der parametrisierte Test ist jetzt grün.

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
// - Der Test ist jetzt grün.
// - Da die Grid-Instanz nicht modifiziert wird, können wir eine Fixture
//   einführen, die sie für alle Tests bereitstellt.
// - In diesem Fall ist das nicht notwendig, da die Grid-Instanz sehr billig zu
//   erstellen ist, aber für komplexere Objekte ist es eine gute Praxis.

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
