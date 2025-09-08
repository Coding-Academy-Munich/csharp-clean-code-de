// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF: Observer Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Beispiel: Aktienkurse
//
// - Aktienkurse ändern sich ständig
// - Viele verschiedene Anwendungen wollen über Änderungen informiert werden
// - Anwendungen sollen unabhängig voneinander sein
// - Die Anwendung soll nicht über konkrete Applikationen Bescheid wissen

// %% [markdown]
//
// ## Observer
//
// ### Zweck
//
// - 1:n Beziehung zwischen Objekten
// - Automatische Benachrichtigung aller abhängigen Objekte bei Zustandsänderung
//
// ### Motivation
//
// - Konsistenz zwischen zusammenhängenden Objekten erhalten
// - Dabei aber lose Kopplung erhalten
// - Ein *Subject* kann beliebig viele *Observers* haben
// - *Observer* werden automatisch über Änderungen am *Subject* benachrichtigt

// %% [markdown]
//
// ### Klassendiagramm
//
// <img src="img/stock_example.png"
//      style="display:block;margin:auto;width:90%"/>

// %%
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

// %%
class Stock
{
    public Stock(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; }
    public double Price { get; set; }
}

// %%
interface IStockObserver
{
    void NoteUpdatedPrices(List<Stock> stocks);
}

// %%
class StockMarket
{
    private readonly Dictionary<string, Stock> _stocks = new();
    private readonly List<WeakReference<IStockObserver>> _observers = new();
    private readonly Random _random = new();

    public void AddStock(Stock stock)
    {
        _stocks[stock.Name] = stock;
    }

    public void AttachObserver(IStockObserver observer)
    {
        _observers.Add(new WeakReference<IStockObserver>(observer));
    }

    public void UpdatePrices()
    {
        var stocks = SelectStocksToUpdate();
        UpdatePricesFor(stocks);
        NotifyObservers(stocks);
    }

    private int GetNumStocksToSelect()
    {
        return (int)Math.Floor(_random.NextDouble() * 0.8 * _stocks.Count);
    }

    private List<Stock> SelectStocksToUpdate()
    {
        var allStocks = _stocks.Values.ToArray();
        _random.Shuffle(allStocks);
        var stocksToUpdate = allStocks.Take(GetNumStocksToSelect()).ToList();
        return stocksToUpdate;
    }

    private void UpdatePricesFor(List<Stock> stocks)
    {
        foreach (var stock in stocks)
        {
            stock.Price += stock.Price * (_random.NextDouble() * 0.2 - 0.1);
        }
    }

    private void NotifyObservers(List<Stock> stocks)
    {
        foreach (var observer in _observers)
        {
            if (observer.TryGetTarget(out var observerTarget))
            {
                observerTarget.NoteUpdatedPrices(stocks);
            }
        }
    }
}

// %%
class PrintingStockObserver : IStockObserver
{
    private readonly string _name;

    public PrintingStockObserver(string name)
    {
        _name = name;
    }

    public void NoteUpdatedPrices(List<Stock> stocks)
    {
        Console.WriteLine($"PrintingStockObserver {_name} received update:");
        foreach (var stock in stocks)
        {
            Console.WriteLine($"  {stock.Name}: {stock.Price}");
        }
    }
}

// %%
class RisingStockObserver : IStockObserver
{
    private readonly string _name;
    private readonly Dictionary<string, double> _oldPrices = new();

    public RisingStockObserver(string name)
    {
        _name = name;
    }

    public void NoteUpdatedPrices(List<Stock> stocks)
    {
        Console.WriteLine($"RisingStockObserver {_name} received update:");
        foreach (var stock in stocks)
        {
            if (_oldPrices.ContainsKey(stock.Name) && stock.Price > _oldPrices[stock.Name])
            {
                Console.WriteLine($"  {stock.Name}: {_oldPrices[stock.Name]} -> {stock.Price}");
            }
            _oldPrices[stock.Name] = stock.Price;
        }
    }
}

// %%
var market = new StockMarket();

// %%
var printingObserver = new PrintingStockObserver("PrintingObserver");
var risingObserver = new RisingStockObserver("RisingObserver");

// %%
market.AttachObserver(printingObserver);
market.AttachObserver(risingObserver);

// %%
market.AddStock(new Stock("Banana", 100.0));
market.AddStock(new Stock("Billionz", 200.0));
market.AddStock(new Stock("Macrosoft", 300.0));
market.AddStock(new Stock("BCD", 400.0));

// %%
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"============= Update {i + 1} =============");
    market.UpdatePrices();
}

// %%
printingObserver = null;

// %%
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"============= Update {i + 1} =============");
    market.UpdatePrices();
}

// %%
GC.Collect()

// %%
for (int i = 0; i < 5; i++)
{
    Console.WriteLine($"============= Update {i + 1} =============");
    market.UpdatePrices();
}

// %% [markdown]
//
// ### Anwendbarkeit
//
// - Ein Objekt muss andere Objekte benachrichtigen, ohne Details zu kennen
// - Eine Änderung in einem Objekt führt zu Änderungen in (beliebig vielen)
//   anderen Objekten
// - Eine Abstraktion hat zwei Aspekte, wobei einer vom anderen abhängt

// %% [markdown]
//
// ### Struktur: Pull Observer
//
// <img src="img/pat_observer_pull.png"
//      style="display:block;margin:auto;width:100%"/>


// %% [markdown]
//
// ## Observer (Behavioral Pattern)
//
// ### Teilnehmer
//
// - `Subject`
//   - kennt seine Observer. Jede Anzahl von Observern kann ein Subject
//     beobachten
//   - stellt eine Schnittstelle zum Hinzufügen und Entfernen von Observern
//     bereit
// - `Observer`
//   - definiert eine Aktualisierungs-Schnittstelle für Objekte, die über
//     Änderungen eines Subjects informiert werden sollen

// %% [markdown]
//
// - `ConcreteSubject`
//   - Speichert den Zustand, der für `ConcreteObserver`-Objekte von Interesse
//     ist
//   - Sendet eine Benachrichtigung an seine Observer, wenn sich sein Zustand
//     ändert
// - `ConcreteObserver`
//   - Kann eine Referenz auf ein `ConcreteSubject`-Objekt halten
//   - Speichert Zustand, der mit dem des Subjects konsistent bleiben soll
//   - Implementiert die `Observer`-Aktualisierungs-Schnittstelle, um seinen
//     Zustand mit dem des Subjects konsistent zu halten

// %% [markdown]
//
// ### Interaktionen: Pull Observer
//
// <img src="img/pat_observer_pull_seq.png"
//      style="display:block;margin:auto;width:65%"/>


// %% [markdown]
//
// ### Interaktionen
//
// - `ConcreteSubject` benachrichtigt seine Observer über Änderungen in seinem
//   Zustand
// - Nachdem ein `ConcreteObserver` über eine Änderung im `ConcreteSubject`
//   informiert wurde, holt es den neuen Zustand vom Subjekt
// - `ConcreteObserver` verwendet diese Informationen, um seinen Zustand mit dem
//   des Subjects in Einklang zu bringen


// %% [markdown]
//
// ### Struktur: Push Observer
//
// <img src="img/pat_observer_push.png"
//      style="display:block;margin:auto;width:100%"/>

// %% [markdown]
//
// ### Interaktion: Push Observer
//
// <img src="img/pat_observer_push_seq.png"
//      style="display:block;margin:auto;width:65%"/>

// %% [markdown]
//
// ### Konsequenzen
//
// - `Subject` und `Observer` können unabhängig voneinander
//    - variiert werden
//    - wiederverwendet werden
// - Hinzufügen neuer `Observer` ohne Änderungen am `Subject`
// - Abstrakte Kopplung zwischen `Subject` und `Observer`
// - Unterstützung für Broadcast-Kommunikation
// - Unerwartete Updates

// %% [markdown]
//
// ### Praxisbeispiele
//
// - Event-Listener in Benutzeroberflächen (SWT)
//
// ### Verwandte Patterns
//
// - Mediator: Durch die Kapselung komplexer Update-Semantik fungiert der
//   `ChangeManager` als Mediator zwischen Subjects und Observers
// - Singleton: ...

// %% [markdown]
//
// ## Workshop: Produktion von Werkstücken
//
// In einem Produktionssystem wollen Sie verschiedene andere System
// benachrichtigen, wenn Sie ein Werkstück erzeugt haben. Dabei sollen diese
// Systeme vom konkreten Produzenten unabhängig sein und auch der Produzent
// keine (statische) Kenntnis über die benachrichtigten System haben.
//
// Implementieren Sie ein derartiges System mit dem Observer-Pattern.
// Implementieren Sie dazu einen konkreten Observer `PrintingObserver`, der den
// Zustand des beobachteten Objekts ausgibt.
//
// *Hinweis:* Sie können das System sowohl mit Pull- als auch mit Push-Observern
// implementieren. Es ist eine gute Übung, wenn Sie beide Varianten
// implementieren und vergleichen.


// %%
public static class ListFormatter
{
    public static string ToString(List<int> vec) {
        string result = "[";
        string sep = "";
        foreach (var i in vec) {
            result += sep + i;
            sep = ", ";
        }
        result += "]";
        return result;
    }
}


// %%
interface IPullObserver
{
    void Update();
    int GetId();
}

// %%
class PullSubject
{
    public void Attach(IPullObserver observer)
    {
        observers.Add(new WeakReference<IPullObserver>(observer));
        PrintObservers("after attaching");
    }

    public void Detach(IPullObserver observer)
    {
        observers.RemoveAll(o => o.TryGetTarget(out var oTarget) && oTarget == observer);
        PrintObservers("after detaching");
    }

    public void Notify()
    {
        PrintObservers("before notifying");
        foreach (var o in observers)
        {
            if (o.TryGetTarget(out var observerPtr))
            {
                observerPtr.Update();
            }
        }
    }

    public virtual List<int> GetState() => throw new NotImplementedException();

    private void PrintObservers(string context)
    {
        Console.WriteLine($"Observers {context} are:");
        foreach (var o in observers)
        {
            if (o.TryGetTarget(out var observerPtr))
            {
                Console.Write($" Observer-{observerPtr.GetId()}");
            }
            else
            {
                Console.Write(" Observer-<deleted>");
            }
        }
        Console.WriteLine();
    }

    private List<WeakReference<IPullObserver>> observers = new();
}

// %%
class PrintingPullObserver : IPullObserver
{
    public PrintingPullObserver(int id)
    {
        this.id = id;
    }

    public void Update()
    {
        Console.WriteLine($"Observer {id}: Observing {subject}.");
        Console.WriteLine($"  Old observer state is {ListFormatter.ToString(observerState)}.");
        observerState = subject.GetState();
        Console.WriteLine($"  New observer state is {ListFormatter.ToString(observerState)}.");
    }

    public int GetId() => id;

    public void AttachTo(PullSubject subject)
    {
        subject.Attach(this);
        this.subject = subject;
    }

    public void DetachFromSubject()
    {
        if (subject != null)
        {
            subject.Detach(this);
            subject = null;
        }
    }

    private int id;
    private PullSubject subject;
    private List<int> observerState = new();
}

// %%
class PullProducer : PullSubject
{
    public void ProduceItem(int item) {
        availableItems.Add(item);
        Notify();
    }

    public override List<int> GetState() => availableItems;

    private List<int> availableItems = new();
}

// %%
var p = new PullProducer();
var o1 = new PrintingPullObserver(1);
o1.AttachTo(p);
var o2 = new PrintingPullObserver(2);
o2.AttachTo(p);

// %%
p.ProduceItem(1);
p.ProduceItem(2);

// %%
o1.DetachFromSubject();
p.ProduceItem(3);

// %%
o1.AttachTo(p);
o2.DetachFromSubject();
p.ProduceItem(4);

// %%
interface IPushObserver {
    void Update(int item);
    int GetId();
}

// %%
class PushSubject {
    public void Attach(IPushObserver observer)
    {
        observers.Add(new WeakReference<IPushObserver>(observer));
        PrintObservers("after attaching");
    }

    public void Detach(IPushObserver observer)
    {
        observers.RemoveAll(o => o.TryGetTarget(out var oTarget) && oTarget == observer);
        PrintObservers("after detaching");
    }

    public void Notify(int item)
    {
        PrintObservers("before notifying");
        foreach (var o in observers)
        {
            if (o.TryGetTarget(out var observerPtr))
            {
                observerPtr.Update(item);
            }
        }
    }

    private void PrintObservers(string context)
    {
        Console.WriteLine($"Observers {context} are:");
        foreach (var o in observers)
        {
            if (o.TryGetTarget(out var observerPtr))
            {
                Console.Write($" Observer-{observerPtr.GetId()}");
            }
            else
            {
                Console.Write(" Observer-<deleted>");
            }
        }
        Console.WriteLine();
    }

    private List<WeakReference<IPushObserver>> observers = new();
}

// %%
class PrintingPushObserver : IPushObserver
{
    public PrintingPushObserver(int id)
    {
        this.id = id;
    }

    public void Update(int item)
    {
        Console.WriteLine($"Observer {id}");
        Console.WriteLine($"  Received item {item}.");
    }

    public int GetId() => id;
    private int id;
}

// %%
class PushProducer : PushSubject
{
    public void ProduceItem(int item)
    {
        availableItems.Add(item);
        Notify(item);
    }

    private List<int> availableItems = new();
}

// %%
var pPush = new PushProducer();
var o1Push = new PrintingPushObserver(1);
pPush.Attach(o1Push);
var o2Push = new PrintingPushObserver(2);
pPush.Attach(o2Push);

// %%
pPush.ProduceItem(1);
pPush.ProduceItem(2);

// %%
pPush.Detach(o1Push);
GC.Collect();
pPush.ProduceItem(3);

// %%
pPush.Attach(o1Push);
o2Push = null;
GC.Collect();
pPush.ProduceItem(4);
