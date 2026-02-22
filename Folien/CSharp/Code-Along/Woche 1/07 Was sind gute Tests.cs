// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Was sind gute Tests?</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// <img src="img/velocity-tests-03.png"
//      alt="Velocity vs. Tests 3"
//      style="width: 75%; margin-left: auto; margin-right: auto;"/>

// %% [markdown]
//
// ## Welche Eigenschaften sollte ein Test haben?
//
// <ul>
// <li class="fragment">Zeigt viele Fehler/Regressionen im Code auf</li>
// <li class="fragment">Gibt schnelle Rückmeldung</li>
// <li class="fragment">Ist deterministisch</li>
// <li class="fragment">Ist leicht zu warten und verstehen</li>
// <li class="fragment"><b>Unempfindlich gegenüber Refactorings</b></li>
// </ul>
//
// <p class="fragment">
//   Leider stehen diese Eigenschaften oft im Konflikt zueinander!
// </p>

// %% [markdown]
//
// ## Aufzeigen von Fehlern/Regressionen
//
// ### Einflüsse
//
// <ul>
//   <li class="fragment">Menge des abgedeckten Codes</li>
//   <li class="fragment">Komplexität des abgedeckten Codes</li>
//   <li class="fragment">Interaktion mit externen Systemen</li>
//   <li class="fragment">Signifikanz des abgedeckten Codes für die Domäne/das
//   System</li>
// </ul>

// %%
#r "nuget: xunit, *"

// %%
using Xunit;
using System.Globalization;

// %%
public class Item
{
    public Item(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; init; }

    public decimal Price {
        get { return price; }
        set { value = (value < 0m) ? -value : value; price = value; }
    }

    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "Item({0}, {1:F2})", Name, Price);
    }

    private decimal price; // always positive
}

// %%
using System;
using System.Collections.Generic;
using System.Globalization;

// %%
public class Order
{
    private List<Item> items;

    public Order(List<Item> itemList)
    {
        items = new List<Item>(itemList);
    }

    public List<Item> Items { get { return items; } }

    public decimal Total
    {
        get
        {
            decimal total = 0m;
            foreach (Item item in items)
            {
                total += item.Price;
            }
            return total;
        }
    }

    public override string ToString()
    {
        string itemsString = string.Join(", ", items.Select(item => item.ToString()));
        return string.Format(CultureInfo.InvariantCulture, "Order([{0}]), total = {1:F2}", itemsString, Total);
    }
}

// %%

// %%

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ## Schnelle Rückmeldung
//
// ### Einflüsse
//
// - Menge des abgedeckten Codes
// - Komplexität/Anzahl Iterationen des abgedeckten Codes
// - Interaktion mit externen Systemen

// %% [markdown]
//
// ## Deterministisch
//
// <ul>
//   <li class="fragment">Gleicher Code führt immer zum gleichen Ergebnis</li>
//   <li class="fragment">Gründe für Nichtdeterminismus
//     <ul>
//       <li class="fragment">Zufallszahlen</li>
//       <li class="fragment">Zeit/Datum</li>
//       <li class="fragment">Interaktion mit externen Systemen</li>
//       <li class="fragment">Nicht initialisierte Variablen</li>
//       <li class="fragment">Kommunikation zwischen Tests</li>
//     </ul>
//   </li>
//   <li class="fragment">
//      Tests, die falsche Warnungen erzeugen sind nicht
//      hilfreich sondern schädlich!
//   </li>
// <ul>

// %%
using System;

// %%
void TestRandomBad()
{
    Random random = new Random();
    int roll = random.Next(1, 3);

    Assert.Equal(2, roll);
}

// %%

// %%
void TestRandomBetter()
{
    Random random = new Random();
    int roll = random.Next(1, 3);

    Assert.Equal(2, roll);
}

// %%

// %%
void TestDateBad()
{
    DateTime now = DateTime.Now;

    Assert.Equal(2024, now.Year);
    Assert.Equal(0, now.Second % 2);
}

// %%

// %%
void TestDateBetter()
{
    DateTime now = DateTime.Now;

    Assert.Equal(2024, now.Year);
    Assert.Equal(0, now.Second % 2);
}

// %%

// %% [markdown]
//
// ## Leicht zu warten
//
// <ul>
//   <li>Einfache, standardisierte Struktur<br>&nbsp;<br>
//     <table style="display:inline;margin:20px 20px;">
//     <tr><td style="text-align:left;width:60px;padding-left:15px;">Arrange</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid black;">Given</td>
//         <td style="text-align:left;width:600px;padding-left:15px;border-left:1px solid black;">
//           Bereite das Test-Environment vor</td></tr>
//     <tr><td style="text-align:left;padding-left:15px;">Act</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid black;">
//            When</td>
//         <td style="text-align:left;width:600px;padding-left:15px;border-left:1px solid black;">
//            Führe die getestete Aktion aus (falls vorhanden)</td></tr>
//     <tr><td style="text-align:left;padding-left:15px;">Assert</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid black;">
//            Then</td>
//         <td style="text-align:left;width:600px;padding-left:15px;border-left:1px solid black;">
//            Überprüfe die Ergebnisse</td></tr>
//     </table>
//     <br>&nbsp;<br>
//   </li>
//   <li>Wenig Code
//     <ul>
//       <li>Wenig Boilerplate</li>
//       <li>Factories, etc. für Tests</li>
//     </ul>
//   </li>
// </ul>

// %% [markdown]
//
// ## Unempfindlich gegenüber Refactorings
//
// - Möglichst wenige falsche Positive!
// - Typischerweise vorhanden oder nicht, wenig Zwischenstufen
//
// ### Einflüsse
//
// - Bezug zu Domäne/System
// - Zugriff auf interne Strukturen

// %%
#pragma warning disable CS0414
public class VeryPrivate
{
    private int secret = 42;
}
#pragma warning restore CS0414

// %%
using System.Reflection;

// %%
public void TestVeryPrivate()
{
    VeryPrivate unit = new VeryPrivate();

    Type veryPrivateType = unit.GetType();
    FieldInfo secretField = veryPrivateType.GetField("secret", BindingFlags.NonPublic | BindingFlags.Instance);
    int secretValue = (int)secretField.GetValue(unit);

    Assert.Equal(42, secretValue);
}

// %%

// %% [markdown]
//
// Die folgenden Einflüsse stehen im Konflikt zueinander:
//
// - Erkennen von Fehlern/Regressionen
// - Schnelle Rückmeldung
// - Unempfindlich gegenüber Refactorings
//
// Die Qualität eines Tests hängt vom *Produkt* dieser Faktoren ab!

// %% [markdown]
//
// ## Wie finden wir den Trade-Off?
//
// - Unempfindlich gegenüber Refactorings kann *nie* geopfert werden
// - Wir müssen also einen Kompromiss finden zwischen
//   - Erkennen von Fehlern/Regressionen
//   - Schnelle Rückmeldung
//
// ### Typischerweise
//
// - Schnelles Feedback für die meisten Tests (Unit-Tests)
// - Gründliche Fehlererkennung für wenige Tests (Integrationstests)

// %% [markdown]
//
// ## Workshop: Gute und schlechte Tests
//
// In den folgenden Aufgaben arbeiten Sie mit der Klasse `Scoreboard`, die eine
// Highscore-Liste verwaltet. Sie werden Tests analysieren, die verschiedene
// Probleme aufweisen und danach bessere Tests schreiben.

// %%
using System.Linq;

// %%
public record ScoreEntry(string PlayerName, int Score);

// %%
public class Scoreboard
{
    private readonly List<ScoreEntry> _entries = new();
    private readonly DateTime _createdAt = DateTime.Now;

    public int Count => _entries.Count;

    public void AddScore(string playerName, int score)
    {
        if (score < 0) score = 0;
        _entries.Add(new ScoreEntry(playerName, score));
        _entries.Sort((a, b) => b.Score.CompareTo(a.Score));
    }

    public IReadOnlyList<ScoreEntry> GetTopScores(int count)
    {
        return _entries.Take(count).ToList().AsReadOnly();
    }

    public int? GetRank(string playerName)
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            if (_entries[i].PlayerName == playerName)
                return i + 1;
        }
        return null;
    }

    public ScoreEntry? HighestScore => _entries.Count > 0 ? _entries[0] : null;

    public override string ToString()
    {
        var lines = _entries.Select((e, i) =>
            $"{i + 1}. {e.PlayerName}: {e.Score}");
        return $"Scoreboard (created {_createdAt:yyyy-MM-dd})\n" +
               string.Join("\n", lines);
    }
}

// %% [markdown]
//
// ### Schlechter Test 1: Trivial
//
// Was ist das Problem mit diesem Test?

// %%
void TestScoreboardCount()
{
    var board = new Scoreboard();

    Assert.Equal(0, board.Count);
}

// %%

// %% [markdown]
// *Antwort:* 

// %% [markdown]
//
// ### Schlechter Test 2: Nichtdeterministisch
//
// Was ist das Problem mit diesem Test?

// %%
void TestScoreboardCreatedToday()
{
    var board = new Scoreboard();

    string output = board.ToString();

    string today = DateTime.Now.ToString("yyyy-MM-dd");
    Assert.Contains(today, output);
}

// %%

// %% [markdown]
// *Antwort:* 

// %% [markdown]
//
// ### Schlechter Test 3: Empfindlich gegenüber Refactoring (ToString)
//
// Was ist das Problem mit diesem Test?

// %%
void TestScoreboardOutput()
{
    var board = new Scoreboard();
    board.AddScore("Alice", 100);
    board.AddScore("Bob", 200);

    string output = board.ToString();

    Assert.Contains("1. Bob: 200", output);
    Assert.Contains("2. Alice: 100", output);
}

// %%

// %% [markdown]
// *Antwort:* 

// %% [markdown]
//
// ### Schlechter Test 4: Zugriff auf interne Strukturen
//
// Was ist das Problem mit diesem Test?

// %%
void TestScoreboardInternalOrder()
{
    var board = new Scoreboard();
    board.AddScore("Alice", 100);
    board.AddScore("Bob", 200);
    board.AddScore("Charlie", 150);

    var entriesField = typeof(Scoreboard).GetField(
        "_entries",
        BindingFlags.NonPublic | BindingFlags.Instance);
    var entries = (List<ScoreEntry>)entriesField!.GetValue(board)!;

    Assert.Equal("Bob", entries[0].PlayerName);
    Assert.Equal("Charlie", entries[1].PlayerName);
    Assert.Equal("Alice", entries[2].PlayerName);
}

// %%

// %% [markdown]
// *Antwort:* 

// %% [markdown]
//
// ### Schreiben Sie bessere Tests!
//
// Schreiben Sie Tests für die `Scoreboard`-Klasse, die nur die öffentliche
// API verwenden und die Kriterien für gute Tests erfüllen:
//
// - Testen Sie, dass Scores korrekt gerankt werden
// - Testen Sie, dass `GetTopScores` die richtige Anzahl zurückgibt
// - Testen Sie, dass `GetRank` korrekte Rankings zurückgibt
// - Testen Sie das Verhalten bei negativen Scores
// - Testen Sie das Verhalten bei unbekannten Spielern

// %%
void TestScoreboardAddAndRank()
{
    // Arrange: create a scoreboard and add some scores
    // Act: retrieve the top scores
    // Assert: verify the ranking is correct
}

// %%

// %%
void TestScoreboardGetTopScoresLimit()
{
    // Test that GetTopScores returns only the requested number
}

// %%

// %%
void TestScoreboardGetRank()
{
    // Test that GetRank returns correct rankings
}

// %%

// %%
void TestScoreboardUnknownPlayer()
{
    // Test the behavior for an unknown player
}

// %%

// %%
void TestScoreboardNegativeScore()
{
    // Test the behavior for negative scores
}

// %%
