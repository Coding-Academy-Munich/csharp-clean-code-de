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
// ## Observer Pattern
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
// - Ein *Publisher* kann beliebig viele *Subscribers* haben
// - *Subscribers* werden automatisch über Änderungen am *Publisher* benachrichtigt

// %% [markdown]
//
// ## Observer Pattern vs. C# Events
//
// ### Terminologie-Mapping
//
// | **GoF Pattern** | **C# Implementation** | **Beschreibung** |
// |-----------------|----------------------|------------------|
// | Subject | Publisher/Event Source | Objekt das Ereignisse auslöst |
// | Observer | Subscriber/Event Handler | Objekt das auf Ereignisse reagiert |
// | attach() | += (Event Subscription) | Hinzufügen eines Beobachters |
// | detach() | -= (Event Unsubscription) | Entfernen eines Beobachters |
// | notify() | Event.Invoke() | Benachrichtigung aller Beobachter |
//
// ### C# bietet native Unterstützung
// - Das Observer Pattern ist in C# durch Events direkt in die Sprache integriert
// - Keine manuelle Implementierung von Subscriber-Listen notwendig
// - Thread-Sicherheit und Multicast-Delegates automatisch verfügbar

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
// ### Klassendiagramm
//
// <img src="img/stock_example_csharp.png"
//      style="display:block;margin:auto;width:90%"/>

// %% [markdown]
//
// ## C# Events - Die native Implementierung
//
// C# bietet mit Events eine eingebaute Implementierung des Observer Patterns:
// - `event` Schlüsselwort für typsichere Multicast-Delegates
// - Automatische Verwaltung von Subscribern
// - Thread-sichere Addition/Entfernung von Event-Handlern
// - Standardmäßige `EventArgs`-Konvention

// %%
using System;
using System.Collections.Generic;
using System.Linq;

// %%
public class Stock
{
    public string Name { get; }
    public decimal Price { get; set; }

    public Stock(string name, decimal price)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
    }

    public override string ToString() => $"{Name}: {Price:C}";
}

// %%
// Modern C# implementation using Events
public class StockPriceChangedEventArgs : EventArgs
{
    public List<Stock> UpdatedStocks { get; }

    public StockPriceChangedEventArgs(List<Stock> updatedStocks)
    {
        UpdatedStocks = updatedStocks ?? new List<Stock>();
    }
}

// %%
// Publisher (was Subject in GoF pattern)
public class StockMarket
{
    private readonly Dictionary<string, Stock> _stocks = new();
    private readonly Random _random = new();

    // Event declaration - the C# way of implementing Observer
    public event EventHandler<StockPriceChangedEventArgs> PricesUpdated;

    public void AddStock(Stock stock)
    {
        if (stock == null) throw new ArgumentNullException(nameof(stock));
        _stocks[stock.Name] = stock;
    }

    public void UpdatePrices()
    {
        var stocksToUpdate = SelectStocksToUpdate();
        UpdatePricesFor(stocksToUpdate);
        OnPricesUpdated(new StockPriceChangedEventArgs(stocksToUpdate));
    }

    // Protected virtual method for raising the event
    protected virtual void OnPricesUpdated(StockPriceChangedEventArgs e)
    {
        PricesUpdated?.Invoke(this, e);
    }

    private List<Stock> SelectStocksToUpdate()
    {
        var numToSelect = (int)(_stocks.Count * _random.NextDouble() * 0.8);
        return _stocks.Values
            .OrderBy(x => _random.Next())
            .Take(numToSelect)
            .ToList();
    }

    private void UpdatePricesFor(List<Stock> stocks)
    {
        foreach (var stock in stocks)
        {
            var changePercent = (decimal)(_random.NextDouble() * 0.2 - 0.1);
            stock.Price *= (1 + changePercent);
        }
    }
}

// %%
// Subscriber implementations (were Observers in GoF pattern)
public class PrintingStockObserver
{
    private readonly string _name;

    public PrintingStockObserver(string name)
    {
        _name = name;
    }

    // Event handler method matching EventHandler<T> signature
    public void OnPricesUpdated(object sender, StockPriceChangedEventArgs e)
    {
        Console.WriteLine($"PrintingStockObserver {_name} received update:");
        foreach (var stock in e.UpdatedStocks)
        {
            Console.WriteLine($"  {stock}");
        }
    }
}

// %%
public class RisingStockObserver
{
    private readonly string _name;
    private readonly Dictionary<string, decimal> _previousPrices = new();

    public RisingStockObserver(string name)
    {
        _name = name;
    }

    public void OnPricesUpdated(object sender, StockPriceChangedEventArgs e)
    {
        Console.WriteLine($"RisingStockObserver {_name} received update:");
        foreach (var stock in e.UpdatedStocks)
        {
            if (_previousPrices.TryGetValue(stock.Name, out var oldPrice)
                && stock.Price > oldPrice)
            {
                Console.WriteLine($"  {stock.Name}: {oldPrice:C} -> {stock.Price:C}");
            }
            _previousPrices[stock.Name] = stock.Price;
        }
    }
}

// %%
// Usage example with C# events
var market = new StockMarket();  // Publisher

var printingObserver = new PrintingStockObserver("PrintingObserver");  // Subscriber
var risingObserver = new RisingStockObserver("RisingObserver");  // Subscriber

// Subscribe to events using += operator
market.PricesUpdated += printingObserver.OnPricesUpdated;
market.PricesUpdated += risingObserver.OnPricesUpdated;

// %%
market.AddStock(new Stock("Microsoft", 300m));
market.AddStock(new Stock("Apple", 180m));
market.AddStock(new Stock("Amazon", 140m));
market.AddStock(new Stock("Google", 150m));

// %%
for (int i = 0; i < 3; i++)
{
    Console.WriteLine($"============= Update {i + 1} =============");
    market.UpdatePrices();
}

// %%
// Unsubscribe using -= operator
Console.WriteLine("\n>>> Unsubscribing PrintingObserver <<<\n");
market.PricesUpdated -= printingObserver.OnPricesUpdated;

// %%
for (int i = 0; i < 2; i++)
{
    Console.WriteLine($"============= Update {i + 1} =============");
    market.UpdatePrices();
}

// %% [markdown]
//
// ### Anwendbarkeit
//
// - Ein Publisher muss mehrere Subscriber benachrichtigen, ohne Details zu kennen
// - Eine Änderung in einem Publisher führt zu Änderungen in (beliebig vielen)
//   Subscribern
// - Eine Abstraktion hat zwei Aspekte, wobei einer vom anderen abhängt

// %% [markdown]
//
// ## Observer (Behavioral Pattern)
//
// ### Teilnehmer
//
// - `Publisher` (Subject im GoF Pattern)
//   - kennt seine Subscriber. Jede Anzahl von Subscribern kann einen Publisher
//     beobachten
//   - stellt eine Schnittstelle zum Hinzufügen und Entfernen von Subscribern
//     bereit (in C# durch += und -= Operatoren)
// - `Subscriber` (Observer im GoF Pattern)
//   - definiert eine Aktualisierungs-Schnittstelle für Objekte, die über
//     Änderungen eines Publishers informiert werden sollen
//   - in C# implementiert als Event Handler Methoden

// %% [markdown]
//
// ### Konsequenzen
//
// - `Publisher` und `Subscriber` können unabhängig voneinander
//    - variiert werden
//    - wiederverwendet werden
// - Hinzufügen neuer `Subscriber` ohne Änderungen am `Publisher`
// - Abstrakte Kopplung zwischen `Publisher` und `Subscriber`
// - Unterstützung für Broadcast-Kommunikation
// - Unerwartete Updates

// %% [markdown]
//
// ### C# Spezifische Vorteile
//
// - **Events**: Eingebaute Sprachunterstützung für das Observer Pattern
// - **Delegates**: Type-sichere Funktionszeiger für Event-Handler
// - **Lambda Expressions**: Kompakte Event-Handler Definition
// - **LINQ**: Elegante Filterung und Transformation von Event-Daten
// - **async/await**: Asynchrone Event-Handler für nicht-blockierende Updates

// %% [markdown]
//
// ### Praxisbeispiele in C#
//
// - **Windows Forms/WPF**: Event-Handler für UI-Interaktionen
// - **INotifyPropertyChanged**: Data Binding in MVVM
// - **Reactive Extensions (Rx.NET)**: Erweiterte Event-Stream-Verarbeitung
// - **ASP.NET Core**: Middleware-Pipeline und Event-basierte Kommunikation
// - **.NET Events**: System.ComponentModel Events

// %% [markdown]
//
// ### Verwandte Patterns
//
// - **Mediator**: Durch die Kapselung komplexer Update-Semantik fungiert der
//   `ChangeManager` als Mediator zwischen Publishers und Subscribers
// - **Singleton**: Oft wird ein Singleton als zentraler Event-Bus verwendet
// - **Command**: Events können als Commands implementiert werden

// %% [markdown]
//
// ## Workshop: Produktion von Werkstücken
//
// In einem Produktionssystem wollen Sie verschiedene andere System
// benachrichtigen, wenn Sie ein Werkstück erzeugt haben. Dabei sollen diese
// Systeme vom konkreten Produzenten unabhängig sein und auch der Produzent
// keine (statische) Kenntnis über die benachrichtigten System haben.
//
// Implementieren Sie ein derartiges System mit dem Observer-Pattern unter Verwendung von C# Events.
// Implementieren Sie dazu:
// 1. Eine `ItemProducedEventArgs` Klasse mit Informationen über das produzierte Werkstück
// 2. Einen `Producer` (Publisher) mit einem `ItemProduced` Event
// 3. Verschiedene Subscriber die auf das Event reagieren

// %%
// Workshop Solution: Event-based implementation
public class ItemProducedEventArgs : EventArgs
{
    public int ItemId { get; }
    public string ItemType { get; }
    public DateTime ProducedAt { get; }

    public ItemProducedEventArgs(int itemId, string itemType)
    {
        ItemId = itemId;
        ItemType = itemType;
        ProducedAt = DateTime.Now;
    }
}

// %%
// Producer acts as Publisher
public class Producer
{
    private int _nextItemId = 1;
    private readonly List<int> _producedItems = new();

    // Event declaration
    public event EventHandler<ItemProducedEventArgs> ItemProduced;

    public void ProduceItem(string itemType)
    {
        var itemId = _nextItemId++;
        _producedItems.Add(itemId);

        Console.WriteLine($"Producer: Creating item {itemId} of type {itemType}");

        // Raise the event (notify all subscribers)
        OnItemProduced(new ItemProducedEventArgs(itemId, itemType));
    }

    protected virtual void OnItemProduced(ItemProducedEventArgs e)
    {
        ItemProduced?.Invoke(this, e);
    }

    public IReadOnlyList<int> ProducedItems => _producedItems.AsReadOnly();
}

// %%
// QualityControlObserver acts as Subscriber
public class QualityControlObserver
{
    private readonly string _name;

    public QualityControlObserver(string name)
    {
        _name = name;
    }

    public void OnItemProduced(object sender, ItemProducedEventArgs e)
    {
        Console.WriteLine($"QC {_name}: Inspecting item {e.ItemId} of type {e.ItemType}");
        Console.WriteLine($"  Produced at: {e.ProducedAt:HH:mm:ss}");
    }
}

// %%
// InventoryObserver acts as Subscriber
public class InventoryObserver
{
    private readonly Dictionary<string, int> _inventory = new();

    public void OnItemProduced(object sender, ItemProducedEventArgs e)
    {
        if (!_inventory.ContainsKey(e.ItemType))
            _inventory[e.ItemType] = 0;

        _inventory[e.ItemType]++;

        Console.WriteLine($"Inventory: Added item {e.ItemId}");
        Console.WriteLine($"  Current stock of {e.ItemType}: {_inventory[e.ItemType]}");
    }

    // Lambda event handler example
    public static EventHandler<ItemProducedEventArgs> CreateSimpleHandler()
    {
        return (sender, e) => Console.WriteLine($"Lambda: Item {e.ItemId} produced!");
    }
}

// %%
// Usage of the workshop solution
var producer = new Producer();  // Publisher
var qcObserver = new QualityControlObserver("QC-1");  // Subscriber
var inventoryObserver = new InventoryObserver();  // Subscriber

// Subscribe using method references
producer.ItemProduced += qcObserver.OnItemProduced;
producer.ItemProduced += inventoryObserver.OnItemProduced;

// Subscribe using lambda expression
producer.ItemProduced += (sender, e) =>
    Console.WriteLine($"Logging: Item {e.ItemId} of type {e.ItemType} produced");

// Subscribe using static lambda
producer.ItemProduced += InventoryObserver.CreateSimpleHandler();

// %%
producer.ProduceItem("Widget");
producer.ProduceItem("Gadget");
producer.ProduceItem("Widget");

// %%
// Unsubscribe
Console.WriteLine("\n>>> Unsubscribing QC Observer <<<\n");
producer.ItemProduced -= qcObserver.OnItemProduced;

producer.ProduceItem("Gadget");

// %% [markdown]
//
// ## INotifyPropertyChanged für Data Binding
//
// ### Was ist INotifyPropertyChanged?
//
// - Standard-Interface in .NET für Property Change Notification
// - Kernbestandteil des Data Binding in WPF, WinForms, Xamarin, MAUI
// - Ermöglicht automatische UI-Updates bei Datenänderungen
// - Basis für das MVVM (Model-View-ViewModel) Pattern
//
// ### Warum wird es verwendet?
//
// - **Automatische UI-Synchronisation**: UI aktualisiert sich automatisch bei Datenänderungen
// - **Entkopplung**: View muss nicht direkt mit Model kommunizieren
// - **Two-Way Binding**: Änderungen können bidirektional propagiert werden
// - **Performance**: Nur geänderte Properties lösen Updates aus

// %% [markdown]
//
// ### Einsatzgebiete von INotifyPropertyChanged
//
// - **WPF Applications**: Binding in XAML
// - **Windows Forms**: Data Binding mit BindingSource
// - **Xamarin/MAUI**: Cross-platform mobile apps
// - **Blazor**: Component state management
// - **Business Objects**: Automatische Validierung und Änderungsverfolgung
// - **Undo/Redo Systeme**: Tracking von Eigenschaftsänderungen

// %%
using System.ComponentModel;
using System.Runtime.CompilerServices;

// %%
public class PriceChangedEventArgs : EventArgs
{
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }

    public PriceChangedEventArgs(decimal oldPrice, decimal newPrice)
    {
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

// %%
// ObservableStock implements INotifyPropertyChanged
public class ObservableStock : INotifyPropertyChanged
{
    private string _name;
    private decimal _price;

    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value;
                OnPropertyChanged();  // Notify subscribers of property change
            }
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (_price != value)
            {
                var oldValue = _price;
                _price = value;
                OnPropertyChanged();  // Standard property change notification
                OnPriceChanged(oldValue, value);  // Custom event for price changes
            }
        }
    }

    // Standard INotifyPropertyChanged event
    public event PropertyChangedEventHandler PropertyChanged;

    // Custom event for specific price changes
    public event EventHandler<PriceChangedEventArgs> PriceChanged;

    // CallerMemberName automatically provides the property name
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnPriceChanged(decimal oldPrice, decimal newPrice)
    {
        PriceChanged?.Invoke(this, new PriceChangedEventArgs(oldPrice, newPrice));
    }
}

// %% [markdown]
//
// ### Verwendungsbeispiele für ObservableStock

// %%
// Example 1: Simple property change monitoring
var observableStock = new ObservableStock { Name = "Tesla", Price = 850m };

// %%
// Subscribe to PropertyChanged event
observableStock.PropertyChanged += (sender, e) =>
{
    Console.WriteLine($"Property '{e.PropertyName}' changed on {((ObservableStock)sender).Name}");
};

// %%
// Subscribe to custom PriceChanged event
observableStock.PriceChanged += (sender, e) =>
{
    var stock = (ObservableStock)sender;
    var changePercent = ((e.NewPrice - e.OldPrice) / e.OldPrice) * 100;
    Console.WriteLine($"{stock.Name}: ${e.OldPrice:F2} → ${e.NewPrice:F2} ({changePercent:+0.00;-0.00}%)");
};

// %%
observableStock.Price = 900m;  // Triggers both PropertyChanged and PriceChanged

// %%
observableStock.Name = "TSLA";  // Triggers only PropertyChanged

// %%
// Example 2: Portfolio monitoring with multiple stocks
public class Portfolio
{
    private readonly List<ObservableStock> _stocks = new();
    private decimal _totalValue;

    public decimal TotalValue => _totalValue;

    public void AddStock(ObservableStock stock, int quantity)
    {
        _stocks.Add(stock);

        // Subscribe to each stock's price changes
        stock.PropertyChanged += OnStockPropertyChanged;

        UpdateTotalValue();
    }

    private void OnStockPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ObservableStock.Price))
        {
            UpdateTotalValue();
            var stock = (ObservableStock)sender;
            Console.WriteLine($"Portfolio update: {stock.Name} price changed. New total: ${TotalValue:F2}");
        }
    }

    private void UpdateTotalValue()
    {
        _totalValue = _stocks.Sum(s => s.Price);
    }
}

// %%
// Usage of the examples
Console.WriteLine("=== Portfolio Example ===");
var portfolio = new Portfolio();

// %%
var apple = new ObservableStock { Name = "Apple", Price = 180m };
var microsoft = new ObservableStock { Name = "Microsoft", Price = 300m };

// %%
portfolio.AddStock(apple, 10);
portfolio.AddStock(microsoft, 5);

// %%
// Simulate price changes
apple.Price = 185m;

// %%
microsoft.Price = 295m;

// %%
// Example 3: WPF-style data binding simulation
public class StockViewModel : INotifyPropertyChanged
{
    private ObservableStock _model;
    private bool _isTracking;

    public StockViewModel(ObservableStock model)
    {
        _model = model;
        _model.PropertyChanged += ModelPropertyChanged;
    }

    public string DisplayName => $"{_model.Name} - ${_model.Price:F2}";

    public bool IsTracking
    {
        get => _isTracking;
        set
        {
            if (_isTracking != value)
            {
                _isTracking = value;
                OnPropertyChanged();
                Console.WriteLine($"Tracking {_model.Name}: {value}");
            }
        }
    }

    private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        // When model changes, notify that DisplayName might have changed
        OnPropertyChanged(nameof(DisplayName));

        if (IsTracking)
        {
            Console.WriteLine($"[Tracked] {DisplayName}");
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

// %%
Console.WriteLine("\n=== ViewModel Example ===");
var googleStock = new ObservableStock { Name = "Google", Price = 150m };
var viewModel = new StockViewModel(googleStock);

// %%
// Enable tracking
viewModel.IsTracking = true;

// %%
// Change the stock price - ViewModel will be notified
googleStock.Price = 155m;

// %%
googleStock.Price = 152m;

// %% [markdown]
//
// ## Zusammenfassung
//
// - Das Observer Pattern ist in C# nativ durch Events implementiert
// - Publisher/Subscriber Terminologie ersetzt Subject/Observer in C#
// - Events bieten typsichere, elegante und performante Implementierung
// - Delegates und Lambda Expressions ermöglichen flexible Event-Handler
// - `INotifyPropertyChanged` ist Standard für Data Binding in .NET UI Frameworks
// - Reactive Extensions (Rx.NET) erweitern das Pattern für komplexe Szenarien
// - async/await ermöglicht asynchrone Event-Verarbeitung
