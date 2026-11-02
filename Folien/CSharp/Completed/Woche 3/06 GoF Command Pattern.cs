// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF: Command Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Zweck
//
// - Kapselung eines Requests als Objekt, um so die Parametrisierung von Clients
//   mit verschiedenen Requests, Warteschlangen- oder Logging-Operationen sowie
//   das Rückgängigmachen von Operationen zu ermöglichen

// %% [markdown]
//
// ### Auch bekannt als
//
// Aktion, Transaktion

// %% [markdown]
//
// ### Motivation
//
// Kommandos in graphischen Benutzeroberflächen
//
// - Können auf mehrere Arten aktiviert werden (Menü, Tastatur, Maus)
// - Sollten oft Undo/Redo unterstützen
// - Sollten geloggt werden können
// - Sollten in Warteschlangen abgelegt werden können
// - Sollten für Macros verwendet werden können

// %% [markdown]
//
// Mögliche Lösung: Implementierung als Objekt
//
// - Jedes Kommando wird als Objekt implementiert
// - Das Kommando-Objekt kapselt die Operation und ihre Parameter
// - Das Kommando-Objekt bietet eine Methode `Execute()`, die die Operation
//   ausführt

// %% [markdown]
//
// <img src="img/command_example.png"
//      style="display:block;margin:auto;width:70%"/>

// %%
class Document(string text)
{
    private string _text = text;

    public string Text => _text;

    public void Modify(string newText)
    {
        Console.WriteLine($"  Document::Modify(\"{newText}\")");
        _text = newText + _text;
    }

    public void Append(string text)
    {
        Console.WriteLine($"  Document::Append(\"{text}\")");
        _text += text;
    }

    public string State => _text;

    public void Restore(string state)
    {
        Console.WriteLine($"  Document::Restore(\"{state}\")");
        _text = state;
    }
}

// %%
interface ICommand
{
    void Execute();
    void Undo();
}

// %%
static int commandCounter = 0;

// %%
class ModifyCommand(Document doc, string change) : ICommand
{
    private readonly Document _doc = doc;
    private readonly string _change = change;
    private string _state;
    private readonly int _counter = ++commandCounter;

    public void Execute()
    {
        Console.WriteLine("ModifyCommand::Execute()");
        _state = _doc.State;
        _doc.Modify("!" + _change + "_" + _counter);
    }

    public void Undo()
    {
        Console.WriteLine("ModifyCommand::Undo()");
        _doc.Restore(_state);
    }
}

// %%
class AppendCommand(Document doc, string change) : ICommand
{
    private readonly Document _doc = doc;
    private readonly string _change = change;
    private string _state;
    private readonly int _counter = ++commandCounter;

    public void Execute()
        {
        Console.WriteLine("AppendCommand::Execute()");
        _state = _doc.State;
        _doc.Append(_change + "_" + _counter + "!");
        }

    public void Undo()
        {
        Console.WriteLine("AppendCommand::Undo()");
        _doc.Restore(_state);
        }
    }

// %%
static List<ICommand> history = new List<ICommand>();

// %%
class Menu(Document doc)
{
    private readonly Document _doc = doc;

    public void ModifyDocument()
    {
        var command = new ModifyCommand(_doc, "menu_mod");
        command.Execute();
        history.Add(command);
    }

    public void AppendDocument()
    {
        var command = new AppendCommand(_doc, "menu_app");
        command.Execute();
        history.Add(command);
    }

    public void Undo()
    {
        if (history.Count == 0) return;

        var last = history[history.Count - 1];
        last.Undo();
        history.RemoveAt(history.Count - 1);
    }
}

// %%
class KeyboardShortcuts(Document doc)
{
    private readonly Document _doc = doc;

    public void ModifyDocument()
    {
        var save = new ModifyCommand(_doc, "key_mod");
        save.Execute();
        history.Add(save);
    }

    public void AppendDocument()
    {
        var save = new AppendCommand(_doc, "key_app");
        save.Execute();
        history.Add(save);
    }

    public void Undo()
    {
        if (history.Count == 0) return;

        var last = history[history.Count - 1];
        last.Undo();
        history.RemoveAt(history.Count - 1);
    }
}

// %%
commandCounter = 0;
Document doc = new Document("<<doc>>");
Menu menu = new Menu(doc);
KeyboardShortcuts shortcuts = new KeyboardShortcuts(doc);
Console.WriteLine(doc.Text);

// %%
menu.ModifyDocument();
Console.WriteLine($"  {doc.State}");

// %%
shortcuts.ModifyDocument();
Console.WriteLine($"  {doc.State}");

// %%
menu.AppendDocument();
Console.WriteLine($"  {doc.State}");

// %%
Console.WriteLine($"  {doc.State}");
shortcuts.ModifyDocument();
Console.WriteLine($"  {doc.State}");

// %%
shortcuts.AppendDocument();
Console.WriteLine($"  {doc.State}");

// %%
menu.Undo();
Console.WriteLine($"  {doc.State}");

// %%
menu.Undo();
Console.WriteLine($"  {doc.State}");

// %%
shortcuts.Undo();
Console.WriteLine($"  {doc.State}");

// %%
Console.WriteLine($"  {doc.State}");
shortcuts.ModifyDocument();
Console.WriteLine($"  {doc.State}");

// %%
shortcuts.AppendDocument();
Console.WriteLine($"  {doc.State}");

// %%
menu.Undo();
Console.WriteLine($"  {doc.State}");

// %%
shortcuts.Undo();
Console.WriteLine($"  {doc.State}");

// %% [markdown]
//
// ### Anwendbarkeit
//
// - Parametrisierung von Objekten mit einer ausführbaren Operation
// - Erzeugung und Ausführung von Operationen zu unterschied Zeitpunkten
// - Warteschlangen von Operationen
// - Undo/Redo
// - Logging von Operationen
// - Unterstützung von Transaktionen

// %% [markdown]
//
// ### Struktur
//
// <img src="img/pat_command.png"
//      style="display:block;margin:auto;width:70%"/>

// %% [markdown]
//
// ### Teilnehmer
//
// - **Command**
//   - Deklariert ein Interface für das Ausführen einer Operation
// - **ConcreteCommand**
//   - Implementiert `Execute()` durch Aufruf der entsprechenden Operation(en)
//     an `Receiver`
//   - Definiert einen Link zwischen `Receiver` und `Action`
// - **Client**
//   - Erstellt ein `ConcreteCommand`-Objekt und setzt dessen `Receiver`
// - **Invoker**
//   - Ruft `Execute()` auf `ConcreteCommand` auf um die Anfrage auszuführen

// %% [markdown]
//
// ### Sequenzendiagramm
//
// <img src="img/pat_command_seq.png"
//      style="display:block;margin:auto;width:70%"/>

// %% [markdown]
//
// ### Interaktionen
//
// - Ein Client erstellt ein `ConcreteCommand`-Objekt und setzt dessen `Receiver`
// - Ein `Invoker`-Objekt speichert das `ConcreteCommand`-Objekt
// - Der `Invoker` ruft `Execute()` auf dem `ConcreteCommand`-Objekt auf.
//   Falls Kommandos rückgängig gemacht werden können, speichert das
//   `ConcreteCommand`-Objekt den Zustand, der benötigt wird, um die Operation
//   rückgängig zu machen.
// - Das `ConcreteCommand`-Objekt ruft eine oder mehrere Operationen am
//   `Receiver` auf, um die Anfrage auszuführen

// %% [markdown]
//
// ### Konsequenzen
//
// - Einfache Erweiterung des Systems durch neue `ConcreteCommand`-Klassen
// - Einfache Implementierung von Undo/Redo und anderen erweiterten Funktionen
// - Entkopplung von `Client`, `Invoker` und `Receiver` möglich
// - Zusammenfassung von Operationen in `MacroCommands` möglich
