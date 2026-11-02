// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Workshop: OCP mit Strategy Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Workshop: Berechnung von ÖPNV-Fahrpreisen
//
// In einer modernen Stadt stehen verschiedene öffentliche Verkehrsmittel zur
// Verfügung – Busse, U-Bahnen, Züge, Boote, etc. Jedes dieser Verkehrsmittel
// hat seine eigene Methode zur Fahrpreisberechnung. Zum Beispiel können
// Bustarife auf Pauschalpreisen basieren, U-Bahnen können auf
// Entfernungstarifen basieren und Boote können Premiumtarife für
// landschaftlich reizvolle Strecken haben.

// %% [markdown]
//
// Sie haben ein rudimentäres Fahrpreisberechnungssystem, das den Fahrpreis
// basierend auf dem Verkehrsmittel bestimmt. Leider verstößt dieses System
// gegen das OCP, da es ohne Modifikation nicht für die Erweiterung geöffnet
// ist. Jedes Mal, wenn ein neues Verkehrsmittel hinzugefügt werden muss, muss
// das Kernsystem geändert werden.
//
// Ihre Aufgabe ist es, das System so zu refaktorisieren, dass es dem OCP
// entspricht. Genauer gesagt, werden Sie die `switch`-Anweisung aus der
// Fahrpreisberechnungslogik entfernen. Das Ziel ist es, das System leicht
// erweiterbar zu machen, so dass neue Verkehrsmittel hinzugefügt werden können,
// ohne den vorhandenen Code zu ändern.

// %%
using System;
using System.Collections.Generic;

// %%
public enum TransportType
{
    Bus,
    Subway,
    Train,
    Boat
}

// %%
public class Transport
{
    private TransportType Type;

    public Transport(TransportType type)
    {
        Type = type;
    }

    public decimal CalculateFare(decimal distance)
    {
        switch (Type)
        {
            case TransportType.Bus: return 2.50m; // flat rate
            case TransportType.Subway: return 1.50m + (distance * 0.20m); // base rate + per km
            case TransportType.Train: return 5.00m + (distance * 0.15m); // base rate + per km
            case TransportType.Boat: return 10.00m; // premium rate
            default: return 0.0m;
        }
    }
}

// %%
Transport bus = new Transport(TransportType.Bus);
Console.WriteLine($"Bus fare: ${bus.CalculateFare(10)}");

// %%
Transport subway = new Transport(TransportType.Subway);
Console.WriteLine($"Subway fare: ${subway.CalculateFare(10)}");

// %%
Transport train = new Transport(TransportType.Train);
Console.WriteLine($"Train fare: ${train.CalculateFare(10)}");

// %%
Transport boat = new Transport(TransportType.Boat);
Console.WriteLine($"Boat fare: ${boat.CalculateFare(10)}");

// %%
using System;
using System.Collections.Generic;

// %%
public interface IFareCalculationStrategy
{
    decimal CalculateFare(decimal distance);
}

// %%
public class BusFare : IFareCalculationStrategy
{
    public decimal CalculateFare(decimal distance)
    {
        return 2.50m; // flat rate
    }
}

// %%
public class SubwayFare : IFareCalculationStrategy
{
    public decimal CalculateFare(decimal distance)
    {
        return 1.50m + (distance * 0.20m); // base rate + per km
    }
}

// %%
public class TrainFare : IFareCalculationStrategy
{
    public decimal CalculateFare(decimal distance)
    {
        return 5.00m + (distance * 0.15m); // base rate + per km
    }
}

// %%
public class BoatFare : IFareCalculationStrategy
{
    public decimal CalculateFare(decimal distance)
    {
        return 10.00m; // premium rate
    }
}

// %%
public class Transport
{
    private IFareCalculationStrategy FareStrategy;

    public Transport(IFareCalculationStrategy fareStrategy)
    {
        FareStrategy = fareStrategy;
    }

    public decimal ComputeFare(decimal distance)
    {
        return FareStrategy.CalculateFare(distance);
    }
}

// %%
Transport bus = new Transport(new BusFare());
Console.WriteLine($"Bus fare: ${bus.ComputeFare(10)}");

// %%
Transport subway = new Transport(new SubwayFare());
Console.WriteLine($"Subway fare: ${subway.ComputeFare(10)}");

// %%
Transport train = new Transport(new TrainFare());
Console.WriteLine($"Train fare: ${train.ComputeFare(10)}");

// %%
Transport boat = new Transport(new BoatFare());
Console.WriteLine($"Boat fare: ${boat.ComputeFare(10)}");

// %% [markdown]
//
// ## Extra-Workshop: Smart Home Device Control System mit Strategy
//
// In einem früheren Workshop haben wir ein System zur Kontrolle von Smart Home
// Devices implementiert.
//
// Lösen Sie das OCP-Problem für dieses System mit dem Strategy-Muster.

// %%
public interface IDeviceStrategy
{
    void Control();
    void GetStatus();
}

// %%
public class LightStrategy : IDeviceStrategy
{
    public void Control()
    {
        Console.WriteLine("Light control");
    }

    public void GetStatus()
    {
        Console.WriteLine("Light status");
    }
}

// %%
public class ThermostatStrategy : IDeviceStrategy
{
    public void Control()
    {
        Console.WriteLine("Thermostat control");
    }

    public void GetStatus()
    {
        Console.WriteLine("Thermostat status");
    }
}

// %%
public class SecurityCameraStrategy : IDeviceStrategy
{
    public void Control()
    {
        Console.WriteLine("Security camera control");
    }

    public void GetStatus()
    {
        Console.WriteLine("Security camera status");
    }
}

// %%
public class SmartLockStrategy : IDeviceStrategy
{
    public void Control()
    {
        Console.WriteLine("Smart lock control");
    }

    public void GetStatus()
    {
        Console.WriteLine("Smart lock status");
    }
}

// %%
using System;
using System.Collections.Generic;

// %%
public class SmartHomeDevice
{
    private IDeviceStrategy Strategy;

    public SmartHomeDevice(IDeviceStrategy strategy)
    {
        Strategy = strategy;
    }

    public void Control()
    {
        Strategy.Control();
    }

    public void GetStatus()
    {
        Strategy.GetStatus();
    }
}

// %%
using System;
using System.Collections.Generic;

// %%
List<SmartHomeDevice> devices = new List<SmartHomeDevice>();

// %%
devices.Add(new SmartHomeDevice(new LightStrategy()));

devices.Add(new SmartHomeDevice(new ThermostatStrategy()));
devices.Add(new SmartHomeDevice(new SecurityCameraStrategy()));
devices.Add(new SmartHomeDevice(new SmartLockStrategy()));

// %%
foreach (var device in devices)
{
    device.Control();
    device.GetStatus();
}
