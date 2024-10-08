// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>SRP: Lösungsansätze</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
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
book.Print();

// %%
book.Save();

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
bookPrinterV1a.Print();

// %%
var bookDatabaseV1a = new BookDatabaseV1a(bookV1);

// %%
bookDatabaseV1a.Save();

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
bookPrinterFacadeV1a.Print();

// %%
bookPrinterFacadeV1a.Save();

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
bookPrinterV1b.Print(bookV1);

// %%
var bookDatabaseV1b = new BookDatabaseV1b();

// %%
bookDatabaseV1b.Save(bookV1);

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
bookFacadeV1.Print();

// %%
bookFacadeV1.Save();

// %% [markdown]
//
// ## Auflösung der SRP-Verletzung (Version 2)
//
// <img src="img/book_resolution_2_srp.png"
//      style="display:block;margin:auto;width:60%"/>

// %%
using System;
using System.Collections.Generic;

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
bookV2.Print();

// %%
bookV2.Save();

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
// entspricht. Damit legen Sie die Grundlage für eine wartbarere und
// skalierbare Anwendung.

// %% [markdown]
//
// ### Klassendiagramm der Wetter-App
//
// <img src="img/weather_app_class.png"
//      style="display:block;margin:auto;width:40%"/>

// %% [markdown]
//
// ### RunWeatherApp() Sequenzendiagramm
//
// <img src="img/weather_app_sequence.png"
//      style="display:block;margin:auto;width:15%"/>

// %%
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// %%
using System;
using System.Collections.Generic;

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

// %% [markdown]
//
// ### Implementierung nach Auflösung der SRP-Verletzungen:

// %% [markdown]
// ### Klassendiagramm der Wetter-App
//
// <img src="img/weather_app_class_srp.png"
//      style="display:block;margin:auto;width:50%"/>


// %% [markdown]
//
// ### RunWeatherApp() Sequenzendiagramm
//
// <div/>
// <img src="img/weather_app_sequence_srp.png"
//      style="display:block;margin:auto;width:75%"/>

// %%
public class WeatherErrorLogger
{
    // Assume that the logger has internal state and this cannot be static.
    public void LogError(string errorMsg)
    {
        Console.WriteLine("Error: " + errorMsg);
    }
}

// %%
public class WeatherData
{
    public WeatherData(List<double> data)
    {
        this.data = data;
    }

    public WeatherData()
    {
        this.data = new List<double>();
    }

    public List<double> GetData()
    {
        return data;
    }

    public string FormattedData()
    {
        if (data.Count == 0)
        {
            throw new Exception("No data available!");
        }
        return string.Join(", ", data);
    }

    private List<double> data;
}

// %%
class WeatherDataSource
{
    public WeatherDataSource(WeatherErrorLogger errorLogger)
    {
        this.errorLogger = errorLogger;
    }

    public void FetchData()
    {
        // Simulating fetching data from some source
        rawData = "Sunny, 25°C";
        hasData = true;
        Console.WriteLine("Data fetched from source.");
    }

    public string GetRawData()
    {
        if (hasData)
        {
            return rawData;
        }
        else
        {
            errorLogger.LogError("WeatherDataSource has no data!");
            return "";
        }
    }

    private string rawData = "";
    private bool hasData = false;
    private readonly WeatherErrorLogger errorLogger;
}

// %%
class WeatherDataParser
{
    public WeatherDataParser(WeatherErrorLogger errorLogger)
    {
        this.errorLogger = errorLogger;
    }

    public WeatherData Parse(string rawData)
    {
        // Simulate data parsing
        if (string.IsNullOrEmpty(rawData))
        {
            errorLogger.LogError("No data available for parsing!");
            return new WeatherData();
        }

        var data = new List<double> { 10.0, 12.0, 8.0, 15.0, 20.0, 22.0, 25.0 };
        Console.WriteLine("Data parsed.");
        return new WeatherData(data);
    }

    private WeatherErrorLogger errorLogger;
}

// %%
class WeatherDisplay
{
    public WeatherDisplay(WeatherErrorLogger errorLogger)
    {
        this.errorLogger = errorLogger;
    }

    public void DisplayInFormatA(WeatherData data)
    {
        try
        {
            string formattedData = data.FormattedData();
            Console.WriteLine("Format A: " + formattedData);
        }
        catch (Exception e)
        {
            errorLogger.LogError("In DisplayInFormatA: " + e.Message);
        }
    }

    public void DisplayInFormatB(WeatherData data)
    {
        try
        {
            string formattedData = data.FormattedData();
            Console.WriteLine("Format B: === " + formattedData + " ===");
        }
        catch (Exception e)
        {
            errorLogger.LogError("In DisplayInFormatB: " + e.Message);
        }
    }

    private WeatherErrorLogger errorLogger;
}

// %%
public void RunWeatherAppSrp(bool introduceError)
{
    WeatherErrorLogger errorLogger = new WeatherErrorLogger();
    WeatherDataParser parser = new WeatherDataParser(errorLogger);

    WeatherDataSource dataSource = new WeatherDataSource(errorLogger);
    if (!introduceError)
    {
        dataSource.FetchData();
    }

    WeatherData weatherData = parser.Parse(dataSource.GetRawData());

    WeatherDisplay weatherDisplay = new WeatherDisplay(errorLogger);
    weatherDisplay.DisplayInFormatA(weatherData);
    weatherDisplay.DisplayInFormatB(weatherData);
}

// %%
RunWeatherAppSrp(false);

// %%
RunWeatherAppSrp(true);

// %% [markdown]
//
// - Mit diesem refaktorierten Code erfüllt jede Klasse das Single
//   Responsibility Prinzip.
// - Jede Klasse behandelt eine eigene Verantwortlichkeit: Datenbeschaffung,
//   Datenanalyse, Datenanzeige und Fehlerprotokollierung.
// - Diese Trennung ermöglicht eine einfachere Wartung, Tests und potenzielle
//   zukünftige Erweiterungen.
