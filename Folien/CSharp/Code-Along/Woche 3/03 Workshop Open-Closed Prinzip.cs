// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Workshop: Open-Closed Prinzip</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

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
