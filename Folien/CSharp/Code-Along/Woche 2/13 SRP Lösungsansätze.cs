// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>SRP: Lösungsansätze</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Ein Änderungsgrund?
//
// <img src="img/book_01.png"
//      style="display:block;margin:auto;width:35%"/>
//

// %% [markdown]
//
// ## Verletzung des SRPs
//
// <img src="img/book_02.png"
//      style="display:block;margin:auto;width:60%"/>

// %%
public class Book
{
    public Book(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
    }

    public void Print()
    {
        // Lots of code that handles the printer
        Console.WriteLine($"Printing {Title} to printer.");
    }

    public void Save()
    {
        // Lots of code that handles the database
        Console.WriteLine($"Saving {Title} to database.");
    }

    public string Title { get; }
    public string Author { get; }
    public int Pages { get; }
}

// %%
var book = new Book("Clean Code", "Robert C. Martin", 464);

// %%

// %%

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 1a)
//
// Vorschlag in Clean Architecture:
//
// <img src="img/book_resolution_1a_srp.png"
//      style="display:block;margin:auto;width:40%"/>

// %%
public class BookV1
{
    public BookV1(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
    }

    public string Title { get; }
    public string Author { get; }
    public int Pages { get; }
}

// %%
public class BookPrinterV1a
{
    public BookPrinterV1a(BookV1 book)
    {
        this.book = book;
    }

    public void Print()
    {
        // Lots of code that handles the printer
        Console.WriteLine($"Printing {book.Title} to printer.");
    }

    private readonly BookV1 book;
}

// %%
public class BookDatabaseV1a
{
    public BookDatabaseV1a(BookV1 book)
    {
        this.book = book;
    }

    public void Save()
    {
        // Lots of code that handles the database
        Console.WriteLine($"Saving {book.Title} to database.");
    }

    private readonly BookV1 book;
}

// %%
var bookV1 = new BookV1("Clean Code", "Robert C. Martin", 464);

// %%
var bookPrinterV1a = new BookPrinterV1a(bookV1);

// %%

// %%
var bookDatabaseV1a = new BookDatabaseV1a(bookV1);

// %%

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 1a mit Fassade)
//
// <img src="img/book_resolution_1a_srp_facade.png"
//      style="display:block;margin:auto;width:50%"/>

// %%
public class BookPrinterFacadeV1a
{
    public BookPrinterFacadeV1a(BookV1 book)
    {
        this.bookPrinter = new BookPrinterV1a(book);
        this.bookDatabase = new BookDatabaseV1a(book);
    }

    public void Print()
    {
        bookPrinter.Print();
    }

    public void Save()
    {
        bookDatabase.Save();
    }

    private readonly BookPrinterV1a bookPrinter;
    private readonly BookDatabaseV1a bookDatabase;
}

// %%
var bookPrinterFacadeV1a = new BookPrinterFacadeV1a(bookV1);

// %%

// %%

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 1b)
//
// <img src="img/book_resolution_1_srp.png"
//      style="display:block;margin:auto;width:50%"/>

// %%
public class BookPrinterV1b
{
    public void Print(BookV1 book)
    {
        // Lots of code that handles the printer
        Console.WriteLine($"Printing {book.Title} to printer.");
    }
}

// %%
public class BookDatabaseV1b
{
    public void Save(BookV1 book)
    {
        // Lots of code that handles the database
        Console.WriteLine($"Saving {book.Title} to database.");
    }
}

// %%
var bookV1 = new BookV1("Clean Code", "Robert C. Martin", 464);

// %%
var bookPrinterV1b = new BookPrinterV1b();

// %%

// %%
var bookDatabaseV1b = new BookDatabaseV1b();

// %%

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 1b mit Fassade)
//
// <img src="img/book_resolution_1_srp_facade.png"
//      style="display:block;margin:auto;width:50%"/>

// %%
public class BookFacadeV1b
{
    public BookFacadeV1b(BookV1 book)
    {
        this.book = book;
        this.bookPrinter = new BookPrinterV1b();
        this.bookDatabase = new BookDatabaseV1b();
    }

    public void Print()
    {
        bookPrinter.Print(book);
    }

    public void Save()
    {
        bookDatabase.Save(book);
    }

    private readonly BookV1 book;
    private readonly BookPrinterV1b bookPrinter;
    private readonly BookDatabaseV1b bookDatabase;
}

// %%
var bookFacadeV1 = new BookFacadeV1b(bookV1);

// %%

// %%

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 1c)

// %%
public static class BookPrinterV1c
{
    public static void Print(BookV1 book)
    {
        // Lots of code that handles the printer
        Console.WriteLine($"Printing {book.Title} to printer.");
    }
}

// %%
public static class BookDatabaseV1c
{
    public static void Save(BookV1 book)
    {
        // Lots of code that handles the database
        Console.WriteLine($"Saving {book.Title} to database.");
    }
}

// %%
BookV1 bookV1 = new BookV1("Clean Code", "Robert C. Martin", 464);

// %%

// %%

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 2)
//
// <img src="img/book_resolution_2_srp.png"
//      style="display:block;margin:auto;width:60%"/>

// %%
public interface IBook
{
    string GetTitle();
}

// %%
public class BookPrinterV2
{
    public void Print(IBook book)
    {
        // Lots of code that handles the printer
        Console.WriteLine($"Printing {book.GetTitle()} to printer.");
    }
}

// %%
public class BookDatabaseV2
{
    public void Save(IBook book)
    {
        // Lots of code that handles the database
        Console.WriteLine($"Saving {book.GetTitle()} to database.");
    }
}

// %%
public class BookV2 : IBook
{
    public string Title { get; }

    public BookV2(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
    }

    public string GetTitle() => Title;
    public string GetAuthor() => Author;
    public int GetPages() => Pages;

    public void Print() => bookPrinter.Print(this);
    public void Save() => bookDatabase.Save(this);

    private readonly string Author;
    private readonly int Pages;
    private readonly BookPrinterV2 bookPrinter = new BookPrinterV2();
    private readonly BookDatabaseV2 bookDatabase = new BookDatabaseV2();
}

// %%
var bookV2 = new BookV2("Clean Code", "Robert C. Martin", 464);

// %%

// %%

// %% [markdown]
//
// ## Vergleich
//
// <div>
// <img src="img/book_resolution_1a_srp.png"
//      style="float:left;padding:5px;width:40%"/>
// <img src="img/book_resolution_2_srp.png"
//      style="float:right;padding:5px;width:50%"/>
// </div>

// %% [markdown]
//
// ## Workshop: Wetter-App
//
// Sie arbeiten an einer vielseitigen Wetteranwendung namens WeatherWise. Die
// WeatherWise App bietet ihren Benutzern aktuelle Wetterinformationen aus
// verschiedenen Online-Plattformen. Über die Anzeige der aktuellen Bedingungen
// hinaus ermöglicht die App den Benutzern, die Vorhersage in verschiedenen
// visuellen Formaten anzuzeigen, und sie protokolliert Fehler für alle Probleme
// während des Datenabrufs oder der Analyse.
//
// Während WeatherWise für seine Funktionen gut ankommt, sieht sich das
// Entwicklungsteam mit Herausforderungen bei der Wartung und Erweiterung der
// Anwendung konfrontiert. Die Entwickler haben festgestellt, dass die
// Kernklasse, `Weather`, zunehmend komplex wird. Sie behandelt alles von der
// Datenbeschaffung bis zur Datendarstellung. Diese Komplexität erschwert die
// Einführung neuer Funktionen, ohne dass dabei die Gefahr besteht, Fehler
// einzuführen.
//
// Ihre Aufgabe: Refaktorisieren Sie die Klasse `Weather`, indem Sie
// sicherstellen, dass jede Klasse im System dem Single Responsibility Principle
// entspricht.

// %% [markdown]
//
// ### Klassendiagramm der Wetter-App
//
// <img src="img/weather_app_class.png"
//      style="display:block;margin:auto;width:40%"/>

// %% [markdown]
//
// ### RunWeatherApp() Sequenzdiagramm
//
// <img src="img/weather_app_sequence.png"
//      style="display:block;margin:auto;width:40%"/>

// %%
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// %%
public class Weather
{
    public void FetchDataFromSource()
    {
        // Simulating fetching data from some source
        rawData = "Sunny, 25°C";
        Console.WriteLine("Data fetched from source.");
    }

    public void ParseData()
    {
        // Simulate data parsing
        if (string.IsNullOrEmpty(rawData))
        {
            LogError("No data available");
            return;
        }
        data = new List<double> { 10.0, 12.0, 8.0, 15.0, 20.0, 22.0, 25.0 };
        Console.WriteLine("Data parsed: " + FormatData());
    }

    public void DisplayInFormatA()
    {
        // Simulating one display format
        if (data.Count == 0)
        {
            LogError("No data available");
            return;
        }
        Console.WriteLine("Format A: " + FormatData());
    }

    public void DisplayInFormatB()
    {
        // Simulating another display format
        if (data.Count == 0)
        {
            LogError("No data available");
            return;
        }
        Console.WriteLine("Format B: === " + FormatData() + " ===");
    }

    public void LogError(string errorMsg)
    {
        // Simulating error logging
        Console.WriteLine("Error: " + errorMsg);
    }

    private string FormatData()
    {
        return string.Join(", ", data);
    }

    private string rawData = "";
    private List<double> data = new List<double>();
}

// %%
public void RunWeatherApp(bool introduceError)
{
    Weather w = new Weather();
    w.FetchDataFromSource();
    if (!introduceError)
    {
        w.ParseData();
    }
    w.DisplayInFormatA();
    w.DisplayInFormatB();
}

// %%
RunWeatherApp(false);

// %%
RunWeatherApp(true);
