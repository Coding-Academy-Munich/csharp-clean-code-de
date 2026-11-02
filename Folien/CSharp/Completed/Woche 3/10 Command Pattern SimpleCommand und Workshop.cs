// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Command Pattern: SimpleCommand und Workshop</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

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
static List<ICommand> history = new List<ICommand>();

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
public class OnlineBankingSystem
{
    private readonly Account _account;

    public OnlineBankingSystem(string accountNumber, double initialBalance)
    {
        _account = new Account(accountNumber, initialBalance);
    }

    public Account GetAccount() => _account;

    // TODO: Implement Transfer, SetupStandingOrder, and Undo operations using the Command Pattern.
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
