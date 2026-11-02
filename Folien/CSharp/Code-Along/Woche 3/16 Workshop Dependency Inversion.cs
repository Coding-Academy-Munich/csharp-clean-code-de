// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Workshop: Dependency Inversion</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

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

// %%
