// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Arbeiten mit Notebooks</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Starten der Notebooks
//
// - Empfohlen: Polyglot Notebooks in VS Code (siehe Video "Polyglot
//   Notebooks in VS Code")
// - Alternativ: Docker (siehe Video "Docker für Notebooks")


// %% [markdown]
//
// # Notebooks
//
// - Notebooks erlauben interaktives Arbeiten mit Code und Text
// - Diese Art zu Arbeiten ist bei C# nicht so verbreitet
// - Für Schulungen ist sie aber hervorragend geeignet

// %% [markdown]
//
// ## Arbeiten mit Notebooks
//
// - Der Notebook-Server zeigt uns eine Liste der Dateien im aktuellen
//   Verzeichnis
// - Durch Anklicken einer Notebook-Datei können wir sie öffnen
// - Notebooks sind in Zellen aufgeteilt
// - Zellen können entweder Text oder C# Code enthalten
// - Im Gegensatz zu C# Programmen, können Zellen auch Ausdrücke enthalten
// - Wenn die Zelle ausgeführt wird, werden die Ausdrücke ausgewertet und das
//   Ergebnis angezeigt

// %%
123

// %%
17 + 4

// %% [markdown]
//
// - Zellen können auch Anweisungen enthalten, z.B. Funktionsdefinitionen
// - Beim Ausführen einer Zelle, in der eine Funktion definiert wird, wird die
//   Funktion für den Rest des Notebooks verfügbar gemacht

// %%
using System;

void SayHello(string name)
{
    Console.WriteLine($"Hello {name}!");
}

// %%
SayHello("World");

// %%
SayHello("you");

// %% [markdown]
//
// - Das gleiche gilt für Structs und Klassen:

// %%
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

// %%
var p = new Point { X = 1, Y = 2 };

// %%
p.X

// %%
p.Y

// %% [markdown]
//
// - Es gibt zwei Modi: Kommando- und Edit-Modus (`Esc` / `Enter`)
// - Einige Tastaturkürzel:
//   - `Strg`-`Enter`
//   - `Shift`-`Enter`
//   - (`Tab`)
//   - (`Shift-Tab`)

// %%
123

// %%
17 + 4

// %%
int answer = 42;

// %%
answer
