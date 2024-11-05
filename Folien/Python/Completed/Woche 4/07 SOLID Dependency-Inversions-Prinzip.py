// -*- coding: utf-8 -*-
// %% [markdown]
// <!--
// clang-format off
// -->
//
// <div style="text-align:center; font-size:200%;">
//  <b>SOLID: Dependency-Inversions-Prinzip</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Abhängigkeiten
//
// - Wir müssen zwei Arten von Abhängigkeiten unterscheiden:
//   - Daten- und Kontrollfluss
//   - Quellcode-/Modul-/Package-Abhängigkeiten
// - Daten- und Kontrollfluss-Abhängigkeiten sind inhärent in der Logik
// - Quellcode-Abhängigkeiten können wir durch die Architektur kontrollieren

// %% [markdown]
//
// ## Beispiel
//
// <img src="img/db-example-01.png"
//      style="display:block;margin:auto;width:75%"/>
//
// Die Quellcode-Abhängigkeit geht in die gleiche Richtung wie der Datenfluss:
//
// `MyModule.cs` ⟹ `Database.cs`

// %% [markdown]
//
// Modul `Database.cs`

// %%
public class Database
{
    public List<string> Execute(string query, string data)
    {
        // Simulate database interaction
        List<string> result = new List<string>();
        if (query.StartsWith("SELECT"))
        {
            result.Add("Data from the database");
        }
        else if (query.StartsWith("INSERT"))
        {
            Console.WriteLine("Inserted: " + data);
        }
        return result;
    }
}

// %% [markdown]
//
// Modul `MyModule.cs`:

// %%
public class MyDomainClassV1
{
    private readonly Database db = new Database();

    public void PerformWork(string data)
    {
        data = "Processed: " + data;
        db.Execute("INSERT INTO my_table VALUES (?)", data);
    }

    public List<string> RetrieveResult()
    {
        return db.Execute("SELECT * FROM my_table", "");
    }
}

// %%
MyDomainClassV1 myDomainObjectV1 = new MyDomainClassV1();

// %%
myDomainObjectV1.PerformWork("Hello World")

// %%
myDomainObjectV1.RetrieveResult()

// %% [markdown]
//
// Wir würden derartige Abhängigkeiten im Kern unsere Anwendung gerne vermeiden
//
// - Einfacher zu testen
// - Einfacher externe Abhängigkeiten zu ersetzen
// - Einfacher den Code zu verstehen
// - ...

// %% [markdown]
//
// <img src="img/db-example-02.png"
//      style="display:block;margin:auto;width:75%"/>

// %% [markdown]
//
// - Modul `MyModule.cs`:
//   - Keine Abhängigkeit mehr zu `Database.cs`
//   - Adapter Pattern

// %%
public interface IAbstractDatabaseAdapter
{
    void SaveObject(string data);
    List<string> RetrieveData();
}

// %%
public class MyDomainClassV2
{
    private readonly IAbstractDatabaseAdapter db;

    public MyDomainClassV2(IAbstractDatabaseAdapter db)
    {
        this.db = db;
    }

    public void PerformWork(string data)
    {
        data = "Processed: " + data;
        db.SaveObject(data);
    }

    public List<string> RetrieveResult()
    {
        return db.RetrieveData();
    }
}

// %% [markdown]
//
// - Modul `ConcreteDatabaseAdapter.cs`:
//   - Implementiert `IAbstractDatabaseAdapter` für `Database.cs`
//   - Hängt von `Database.cs` ab

// %%
public class ConcreteDatabaseAdapter : IAbstractDatabaseAdapter
{
    private readonly Database db = new Database();

    public void SaveObject(string data)
    {
        db.Execute("INSERT INTO my_table VALUES (?)", data);
    }

    public List<string> RetrieveData()
    {
        return db.Execute("SELECT * FROM my_table", "");
    }
}

// %% [markdown]
//
// - Modul `Main.cs`:

// %%
IAbstractDatabaseAdapter dbAdapter = new ConcreteDatabaseAdapter();

// %%
MyDomainClassV2 myDomainObjectV2 = new MyDomainClassV2(dbAdapter);

// %%
myDomainObjectV2.PerformWork("Hello World");

// %%
myDomainObjectV2.RetrieveResult()

// %% [markdown]
//
// # SOLID: Dependency Inversion Prinzip
//
// - Die Kernfunktionalität eines Systems hängt nicht von seiner Umgebung ab
//   - **Konkrete Artefakte hängen von Abstraktionen ab** (nicht umgekehrt)
//   - **Instabile Artefakte hängen von stabilen Artefakten ab** (nicht umgekehrt)
//   - **Äußere Schichten** der Architektur **hängen von inneren Schichten ab**
//     (nicht umgekehrt)
//   - Klassen/Module hängen von Abstraktionen (z. B. Schnittstellen) ab,
//     nicht von anderen Klassen/Modulen
// - Abhängigkeitsinversion (Dependency Inversion) erreicht dies durch die Einführung
//   von Schnittstellen, die "die Abhängigkeiten umkehren"

// %% [markdown]
//
// ### Vorher
// <img src="img/dependency-01.png"
//      style="display:block;margin:auto;width:75%"/>
//
// ### Nachher
// <img src="img/dependency-02.png"
//      style="display:block;margin:auto;width:75%"/>

// %% [markdown]
//
// <img src="img/dip-01.png"
//      style="display:block;margin:auto;width:95%"/>

// %% [markdown]
//
// <img src="img/dip-02.png"
//      style="display:block;margin:auto;width:95%"/>

// %% [markdown]
//
// <img src="img/dip-03.png"
//      style="display:block;margin:auto;width:95%"/>

// %% [markdown]
//
// ## Workshop: Wetterbericht
//
// Wir haben ein Programm geschrieben, das einen Wetterbericht von einem Server
// abruft. Leider ist dabei die Abhängigkeit zum Server vom Typ
// `LegacyWeatherServer` hart kodiert. Aufgrund der Popularität des Programms
// müssen wir jedoch mit einem neuen Typ von Server `NewWeatherServer`
// kompatibel werden. Dazu refaktorisieren wir den Code nach dem
// Dependency-Inversion-Prinzip und Implementieren dann einen zusätzlichen
// Adapter für `NewWeatherServer`.
//
// - Führen Sie eine Abstraktion ein, um die Abhängigkeit umzukehren
// - Schreiben Sie eine konkrete Implementierung der Abstraktion für
//   `LegacyWeatherServer`
// - Testen Sie die Implementierung
// - Implementieren Sie einen Adapter für `NewWeatherServer`
// - Testen Sie den Adapter

// %%
using System;

// %%
public class WeatherReport
{
    public double Temperature { get; }
    public double Humidity { get; }

    public WeatherReport(double temperature, double humidity)
    {
        Temperature = temperature;
        Humidity = humidity;
    }
}

// %%
public class LegacyWeatherServer
{
    private static readonly Random random = new Random();

    public WeatherReport GetWeatherReport()
    {
        return new WeatherReport(20.0 + 10.0 * random.NextDouble(), 0.5 + 0.5 * random.NextDouble());
    }
}

// %%
public class NewWeatherServer
{
    private static readonly Random random = new Random();

    public WeatherReport FetchWeatherData()
    {
        double temperature = 10.0 + 20.0 * random.NextDouble();
        double humidity = 0.7 + 0.4 * random.NextDouble();
        return new WeatherReport(temperature, humidity);
    }
}

// %%
public class WeatherReporter
{
    private readonly LegacyWeatherServer server;

    public WeatherReporter(LegacyWeatherServer server)
    {
        this.server = server;
    }

    public string Report()
    {
        WeatherReport report = server.GetWeatherReport();
        return report.Temperature > 25.0 ? "It's hot" : "It's not hot";
    }
}

// %%
LegacyWeatherServer server = new LegacyWeatherServer();
WeatherReporter reporter = new WeatherReporter(server);

// %%
Console.WriteLine(reporter.Report());

// %%
public interface IWeatherDataSource
{
    WeatherReport GetWeatherReport();
}

// %%
public class DiWeatherReporter
{
    private readonly IWeatherDataSource dataSource;

    public DiWeatherReporter(IWeatherDataSource dataSource)
    {
        this.dataSource = dataSource;
    }

    public string Report()
    {
        WeatherReport report = dataSource.GetWeatherReport();
        return report.Temperature > 25.0 ? "It's hot" : "It's not hot";
    }
}

// %%
public class LegacyWeatherServerAdapter : IWeatherDataSource
{
    private readonly LegacyWeatherServer server;

    public LegacyWeatherServerAdapter(LegacyWeatherServer server)
    {
        this.server = server;
    }

    public WeatherReport GetWeatherReport()
    {
        return server.GetWeatherReport();
    }
}

// %%
DiWeatherReporter reporter = new DiWeatherReporter(new LegacyWeatherServerAdapter(server));

// %%
Console.WriteLine(reporter.Report());

// %%
public class NewWeatherServerAdapter : IWeatherDataSource
{
    private readonly NewWeatherServer server;

    public NewWeatherServerAdapter(NewWeatherServer server)
    {
        this.server = server;
    }

    public WeatherReport GetWeatherReport()
    {
        return server.FetchWeatherData();
    }
}

// %%
NewWeatherServer newServer = new NewWeatherServer();
DiWeatherReporter newReporter = new DiWeatherReporter(new NewWeatherServerAdapter(newServer));

// %%
Console.WriteLine(newReporter.Report());

// %%
