// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>SOLID: Open-Closed Prinzip</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Open-Closed Prinzip (SOLID)
//
// Klassen sollen
//
// - Offen für Erweiterung
// - Geschlossen für Modifikation
//
// sein.

// %%
public enum MovieKindV0
{
    Regular,
    Children
}

// %%
public class MovieV0
{
    public MovieV0(string title, MovieKindV0 kind)
    {
        Title = title;
        Kind = kind;
    }

    public string Title { get; }
    public MovieKindV0 Kind { get; }

    public decimal ComputePrice()
    {
        return Kind switch
        {
            MovieKindV0.Regular => 4.99m,
            MovieKindV0.Children => 5.99m,
            _ => 0.0m,
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {ComputePrice()}");
    }
}

// %%
MovieV0 m1 = new MovieV0("Casablanca", MovieKindV0.Regular);
MovieV0 m2 = new MovieV0("Shrek", MovieKindV0.Children);

// %%
m1.PrintInfo();
m2.PrintInfo();

// %% [markdown]
//
// <img src="img/movie_v0.png" alt="MovieV0"
//      style="display:block;margin:auto;width:50%"/>


// %% [markdown]
//
// Was passiert, wenn wir eine neue Filmart hinzufügen wollen?

// %%
public enum MovieKind
{
    Regular,
    Children,
    NewRelease
}

// %%
public class MovieV1
{
    public MovieV1(string title, MovieKind kind)
    {
        Title = title;
        Kind = kind;
    }

    public string Title { get; }
    public MovieKind Kind { get; }

    public decimal ComputePrice()
    {
        return Kind switch
        {
            MovieKind.Regular => 4.99m,
            MovieKind.Children => 5.99m,
            MovieKind.NewRelease => 6.99m,
            _ => 0.0m,
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {ComputePrice()}");
    }
}

// %%
MovieV1 m1 = new MovieV1("Casablanca", MovieKind.Regular);
MovieV1 m2 = new MovieV1("Shrek", MovieKind.Children);
// MovieV1 m3 = new MovieV1("Brand New", MovieKind.NewRelease);

// %%
m1.PrintInfo();
m2.PrintInfo();
// m3.PrintInfo();

// %% [markdown]
//
// <img src="img/movie_v1.png" alt="MovieV1"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## OCP-Verletzung
//
// - Neue Filmarten erfordern Änderungen an `MovieV1`
// - `MovieV1` ist nicht geschlossen für Modifikation

// %% [markdown]
//
// ## Auflösung (Versuch 1: Vererbung)
//
// - Neue Filmarten werden als neue Klassen implementiert
// - `MovieV2` wird abstrakt
// - `MovieV2` ist geschlossen für Modifikation

// %%
public abstract class MovieV2
{
    public MovieV2(string title)
    {
        Title = title;
    }

    public string Title { get; }

    public abstract decimal ComputePrice();

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {ComputePrice()}");
    }
}

// %%
public class RegularMovie : MovieV2
{
    public RegularMovie(string title) : base(title) { }

    public override decimal ComputePrice()
    {
        return 4.99m;
    }
}

// %%
public class ChildrenMovie : MovieV2
{
    public ChildrenMovie(string title) : base(title) { }

    public override decimal ComputePrice()
    {
        return 5.99m;
    }
}

// %%
public class NewReleaseMovie : MovieV2
{
    public NewReleaseMovie(string title) : base(title) { }

    public override decimal ComputePrice()
    {
        return 6.99m;
    }
}

// %%
MovieV2 m1 = new RegularMovie("Casablanca");
MovieV2 m2 = new ChildrenMovie("Shrek");
MovieV2 m3 = new NewReleaseMovie("Brand New");

// %%
m1.PrintInfo();
m2.PrintInfo();
m3.PrintInfo();

// %% [markdown]
//
// <img src="img/movie_v2.png" alt="MovieV0"
//      style="display:block;margin:auto;width:100%"/>

// %% [markdown]
//
// - `MovieV2` ist offen für Erweiterung
// - Neue Filmarten können hinzugefügt werden, ohne die bestehenden Klassen zu
//   ändern
// - Aber: Die Vererbungshierarchie umfasst die ganze Klasse
//   - Nur eine Art von Variabilität
// - Was ist, wenn wir für andere Zwecke eine andere Klassifikation brauchen?
//   - Z.B. DVD, BluRay, Online?
// - Mehrfachvererbung?
// - Produkt von Klassen?
//   - `ChildrenDVD`, `ChildrenBluRay`, `ChildrenOnline`, ...

// %% [markdown]
//
// ## Bessere Auflösung: Strategy Pattern
//
// - Das Strategy-Pattern erlaubt es uns, die Vererbung auf kleinere Teile der
//   Klasse anzuwenden
// - In fast allen Fällen ist das die bessere Lösung!
// - Vererbung ist ein sehr mächtiges Werkzeug
// - Aber je kleiner und lokaler wir unsere Vererbungshierarchien halten, desto
//   besser

// %% [markdown]
//
// ## Workshop: Smart Home Device Control System
//
// In diesem Workshop arbeiten wir mit einem Szenario, das ein Smart Home
// Gerätesteuerungssystem betrifft. Leider verletzt das vorhandene System
// das Open-Closed-Prinzip.

// %% [markdown]
//
// ### Szenario
//
// Wir haben ein Smart-Home-System. Dieses System steuert verschiedene
// Geräte: Lichter, Thermostate, Sicherheitskameras und intelligente Schlösser.
// Jeder Gerätetyp benötigt einen eigenen Steuerungsmechanismus und eigene
// Automatisierungsregeln.
//
// Nun muss das Steuerungssystem des Smart Homes diese Geräte verwalten. Das
// Problem mit dem aktuellen System ist die Verwendung eines Enums zur Bestimmung
// des Gerätetyps und basierend darauf seiner Steuermethode. Dieser Ansatz ist
// nicht skalierbar und verletzt das OCP.

// %%
using System;
using System.Collections.Generic;

// %%
public enum DeviceType
{
    Light,
    Thermostat,
    SecurityCamera,
    SmartLock
}

// %%
public class DeviceV0
{
    public DeviceV0(DeviceType type)
    {
        this.type = type;
    }

    public string Control()
    {
        return type switch
        {
            DeviceType.Light => "Turning light on/off.",
            DeviceType.Thermostat => "Adjusting temperature.",
            DeviceType.SecurityCamera => "Activating motion detection.",
            DeviceType.SmartLock => "Locking/Unlocking door.",
            _ => "Unknown device control!"
        };
    }

    public string GetStatus()
    {
        return type switch
        {
            DeviceType.Light => "Light is on/off.",
            DeviceType.Thermostat => "Current temperature: 22°C.",
            DeviceType.SecurityCamera => "Camera is active/inactive.",
            DeviceType.SmartLock => "Door is locked/unlocked.",
            _ => "Unknown device status!"
        };
    }

    private readonly DeviceType type;
}

// %%
List<DeviceV0> devicesOriginal = new List<DeviceV0>
{
    new DeviceV0(DeviceType.Light),
    new DeviceV0(DeviceType.Thermostat),
    new DeviceV0(DeviceType.SecurityCamera)
};

// %%
public static void ManageDevices(List<DeviceV0> devices)
{
    foreach (DeviceV0 device in devices)
    {
        Console.WriteLine(device.Control() + " " + device.GetStatus());
    }
}

// %%
ManageDevices(devicesOriginal);

// %% [markdown]
//
// - Beseitigen Sie das Problem mit der OCP-Verletzung im vorhandenen Code
// - Sie können entweder den vorhandenen Code ändern oder eine neue Lösung von
//   Grund auf neu erstellen

// %%
using System;
using System.Collections.Generic;

// %%
public interface IDevice
{
    string Control();
    string GetStatus();
}

// %%
public class Light : IDevice
{
    public string Control()
    {
        return "Turning light on/off.";
    }

    public string GetStatus()
    {
        return "Light is on/off.";
    }
}

// %%
public class Thermostat : IDevice
{
    public string Control()
    {
        return "Adjusting temperature.";
    }

    public string GetStatus()
    {
        return "Current temperature: 22°C.";
    }
}

// %%
public class SecurityCamera : IDevice
{
    public string Control()
    {
        return "Activating motion detection.";
    }

    public string GetStatus()
    {
        return "Camera is active/inactive.";
    }
}

// %%
public class SmartLock : IDevice
{
    public string Control()
    {
        return "Locking/Unlocking door.";
    }

    public string GetStatus()
    {
        return "Door is locked/unlocked.";
    }
}

// %%
List<IDevice> devicesRefactored = new List<IDevice>
{
    new Light(),
    new Thermostat(),
    new SecurityCamera()
};

// %%
public static void ManageDevices(List<IDevice> devices)
{
    foreach (IDevice device in devices)
    {
        Console.WriteLine(device.Control() + " " + device.GetStatus());
    }
}

// %%
ManageDevices(devicesRefactored);
