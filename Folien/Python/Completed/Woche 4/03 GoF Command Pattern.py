// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF: Command Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Zweck
//
// Kapselung​ eines Requests als Objekt, um so die Parametrisierung von Clients
// mit verschiedenen Requests, Warteschlangen- oder Logging-Operationen sowie
// das Rückgängigmachen von Operationen zu ermöglichen.

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

// %% [markdown]
//
// ### Implementierung
//
// - Für einfache Szenarien kann eine `SimpleCommand`-Klasse, die einen Zeiger
//   auf eine Memberfunktion speichert verwendet werden
// - ...

// %%
class SimpleCommand : ICommand
{
    private readonly Document _doc;
    private readonly Action<Document, string> _action;
    private readonly string _text;
    private string _state;
    private readonly int _counter = ++commandCounter;

    public SimpleCommand(Document doc, Action<Document, string> action, string text)
    {
        _doc = doc;
        _action = action;
        _text = text;
    }

    public void Execute()
    {
        Console.WriteLine("SimpleCommand::Execute()");
        _state = _doc.State;
        _action(_doc, "!" + _text + "_" + _counter + "!");
    }

    public void Undo()
    {
        Console.WriteLine("SimpleCommand::Undo()");
        _doc.Restore(_state);
    }
}

// %%
void RunSimpleAction(Document doc, Action<Document, string> action, string text)
{
    var command = new SimpleCommand(doc, action, text);
    command.Execute();
    history.Add(command);
}

// %%
void Undo()
{
    if (history.Count == 0)
    {
        return;
    }

    var last = history[history.Count - 1];
    last.Undo();
    history.RemoveAt(history.Count - 1);
}

// %%
history.Clear();
commandCounter = 0;
Document doc = new Document("<<doc>>");
Console.WriteLine($"  {doc.State}");

// %%
RunSimpleAction(doc, (doc, text) => doc.Modify(text), "mod");
Console.WriteLine($"  {doc.State}");

// %%
RunSimpleAction(doc, (d, t) => d.Append(t), "app");
Console.WriteLine($"  {doc.State}");

// %%
RunSimpleAction(doc, (d, t) => d.Modify(t), "mod");
Console.WriteLine($"  {doc.State}");

// %%
Undo();
Console.WriteLine($"  {doc.State}");

// %%
Undo();
Console.WriteLine($"  {doc.State}");

// %%
RunSimpleAction(doc, (d, t) => d.Append(t), "app");
Console.WriteLine($"  {doc.State}");

// %%
Undo();
Console.WriteLine($"  {doc.State}");

// %%
Undo();
Console.WriteLine($"  {doc.State}");

// %%
Undo();
Console.WriteLine($"  {doc.State}");

// %% [markdown]
//
// ## Workshop: Command Pattern
//
// ### Szenario
//
// Eine Bank möchte ihr Online-Banking-System verbessern. Das System soll verschiedene
// Funktionen anbieten, wie Überweisungen tätigen, Daueraufträge einrichten und
// Kontostände abfragen. Zusätzlich soll es möglich sein, die letzte Aktion rückgängig
// zu machen, falls ein Kunde einen Fehler gemacht hat.

// %% [markdown]
//
// ### Ziel
//
// Ihre Aufgabe ist es, die Online-Banking-Operationen mit dem Command Pattern zu
// implementieren. Dies soll die einfache Erweiterung des Systems in der Zukunft
// ermöglichen und eine Undo-Funktion für die letzte Transaktion bereitstellen.

// %% [markdown]
//
// ### Starter Code

// %%
public class StandingOrder
{
    public double Amount { get; }
    public string Recipient { get; }
    public string Frequency { get; }

    public StandingOrder(double amount, string recipient, string frequency)
    {
        Amount = amount;
        Recipient = recipient;
        Frequency = frequency;
    }
}

// %%
public class Account
{
    private readonly string _accountNumber;
    private double _balance;
    private readonly List<StandingOrder> _standingOrders = new List<StandingOrder>();

    public Account(string accountNumber, double initialBalance)
    {
        _accountNumber = accountNumber;
        _balance = initialBalance;
    }

    public void Transfer(double amount, string recipient)
    {
        if (amount <= _balance)
        {
            _balance -= amount;
            Console.WriteLine($"Transferred: ${amount} to {recipient}. New Balance: ${_balance}");
        }
        else
        {
            Console.WriteLine($"Insufficient funds. Current Balance: ${_balance}");
        }
    }

    public void SetupStandingOrder(double amount, string recipient, string frequency)
    {
        Console.WriteLine($"Set up standing order: ${amount} to {recipient} {frequency}");
        _standingOrders.Add(new StandingOrder(amount, recipient, frequency));
    }

    public void CancelStandingOrder(string recipient)
    {
        var order = _standingOrders.Find(so => so.Recipient == recipient);
        if (order != null)
        {
            Console.WriteLine($"Cancelled standing order: ${order.Amount} to {order.Recipient} {order.Frequency}");
            _standingOrders.Remove(order);
        }
        else
        {
            Console.WriteLine($"No standing order found for recipient: {recipient}");
        }
    }

    public double GetBalance() => _balance;

    public string GetAccountNumber() => _accountNumber;

    public IReadOnlyList<StandingOrder> GetStandingOrders() => _standingOrders;

    public void DisplayAccount()
    {
        Console.Write($"Account: {GetAccountNumber()}, Balance: ${GetBalance()}");
        var standingOrders = GetStandingOrders();

        if (standingOrders.Count == 0)
        {
            Console.WriteLine(", No Standing Orders");
        }
        else
        {
            Console.WriteLine("\n  Standing Orders:");
            foreach (var so in standingOrders)
            {
                Console.WriteLine($"    Amount: ${so.Amount}, Recipient: {so.Recipient}, Frequency: {so.Frequency}");
            }
        }
    }
}
// %%

public abstract class Command
{
    public abstract void Execute();
    public abstract void Undo();
}

// %%

public class TransferCommand : Command
{
    private readonly Account _account;
    private readonly double _amount;
    private readonly string _recipient;

    public TransferCommand(Account account, double amount, string recipient)
    {
        _account = account;
        _amount = amount;
        _recipient = recipient;
    }

    public override void Execute() => _account.Transfer(_amount, _recipient);

    public override void Undo()
    {
        Console.WriteLine($"Reversing transfer of ${_amount} from {_recipient}.");
        _account.Transfer(-_amount, "Reversal");
    }
}

// %%
public class SetupStandingOrderCommand : Command
{
    private readonly Account _account;
    private readonly double _amount;
    private readonly string _recipient;
    private readonly string _frequency;

    public SetupStandingOrderCommand(Account account, double amount, string recipient, string frequency)
    {
        _account = account;
        _amount = amount;
        _recipient = recipient;
        _frequency = frequency;
    }

    public override void Execute()
    {
        _account.SetupStandingOrder(_amount, _recipient, _frequency);
    }

    public override void Undo() => _account.CancelStandingOrder(_recipient);
}

// %%

public class OnlineBankingSystem
{
    private readonly Account _account;
    private readonly List<Command> _commandHistory = new List<Command>();

    public OnlineBankingSystem(string accountNumber, double initialBalance)
    {
        _account = new Account(accountNumber, initialBalance);
    }

    public Account GetAccount() => _account;

    public void Transfer(double amount, string recipient)
    {
        var cmd = new TransferCommand(_account, amount, recipient);
        cmd.Execute();
        _commandHistory.Add(cmd);
    }

    public void SetupStandingOrder(double amount, string recipient, string frequency)
    {
        var cmd = new SetupStandingOrderCommand(_account, amount, recipient, frequency);
        cmd.Execute();
        _commandHistory.Add(cmd);
    }

    public void UndoLastTransaction()
    {
        if (_commandHistory.Count > 0)
        {
            var lastCommand = _commandHistory[_commandHistory.Count - 1];
            lastCommand.Undo();
            _commandHistory.RemoveAt(_commandHistory.Count - 1);
        }
        else
        {
            Console.WriteLine("No transaction to undo.");
        }
    }

    public double GetBalance() => _account.GetBalance();
}

// %%
var banking = new OnlineBankingSystem("123456789", 1000);
banking.GetAccount().DisplayAccount();

// %%
banking.Transfer(100, "John Doe");
banking.GetAccount().DisplayAccount();

// %%
banking.SetupStandingOrder(50, "Electricity Company", "Monthly");
banking.GetAccount().DisplayAccount();

// %%
banking.Transfer(200, "Jane Doe");

// %%
banking.GetAccount().DisplayAccount();

// %%
banking.UndoLastTransaction();
banking.GetAccount().DisplayAccount();

// %%
banking.UndoLastTransaction();
banking.GetAccount().DisplayAccount();

// %%
banking.UndoLastTransaction();
banking.GetAccount().DisplayAccount();

// %%
banking.SetupStandingOrder(75, "Water Company", "Monthly");
banking.GetAccount().DisplayAccount();

// %%
banking.UndoLastTransaction();
banking.GetAccount().DisplayAccount();

// %%
