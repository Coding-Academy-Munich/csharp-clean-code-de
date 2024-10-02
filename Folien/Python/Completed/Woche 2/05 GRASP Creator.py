// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GRASP: Creator</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// - Use Case "Spiel initialisieren"
// - Bisher:
//   - `World`- und `Location`-Klassen
//   - Attribute und Getter
// - Frage:
//   - Wer erzeugt die `Location`-Instanzen?

// %% [markdown]
//
// ## Kandidaten
//
// <div style="float:left;margin:auto;padding:80px 0;width:25%">
// <ul>
// <li> <code>Player</code></li>
// <li> <code>Game</code></li>
// <li> <code>Pawn</code></li>
// <li> <code>Location</code></li>
// <li> <code>World</code></li>
// <li> Eine andere Klasse?</li>
// </ul>
// </div>
// <img src="img/adv-domain-03-small.png"
//      style="float:right;margin:auto;width:70%"/>

// %% [markdown]
//
// ## Das Creator Pattern (GRASP)
//
// ### Frage
//
// - Wer ist verantwortlich für die Erzeugung eines Objekts?
//
// ### Antwort
//
// Klasse `A` bekommt die Verantwortung, ein Objekt der Klasse `B` zu erzeugen,
// wenn eine oder mehrere der folgenden Bedingungen zutreffen:
//
// - `A` enthält `B` (oder ist Eigentümer von `B`)
// - `A` verwaltet `B` (registriert, zeichnet auf)
// - `A` verwendet `B` intensiv
// - `A` hat die initialisierenden Daten, die `B` benötigt

// %% [markdown]
//
// ### Bemerkung
//
// - Factory ist oft eine Alternative zu Creator

// %% [markdown]
//
// ## Creator
//
// <div style="float:left;margin:auto;padding:80px 0;width:25%">
// <ul>
// <li> <strike><code>Player</code></strike></li>
// <li> <strike><code>Game</code></strike></li>
// <li> <code>Pawn</code></li>
// <li> <code>Location</code></li>
// <li> <b><code>World</code></b></li>
// <li> <strike>Eine andere Klasse?</strike></li>
// </ul>
// </div>
// <img src="img/adv-domain-03-small.png"
//      style="float:right;margin:auto;width:70%"/>

// %%
#r "nuget: Newtonsoft.Json, 13.0.1"

// %%
#load "JsonLoader.cs"

// %%
var simpleLocationsData = JsonLoader.LoadData("simple-locations.json");

// %%
public record Location(string Name, string Description)
{
    public static Location FromData(Dictionary<string, object> data)
    {
        string name = data["name"].ToString() ?? "<missing name>";
        string description = data.GetValueOrDefault("description")?.ToString() ?? "";
        return new Location(name, description);
    }
}

// %%
using System.Collections.Generic;
using System.Linq;

// %%
public record World(Dictionary<string, Location> Locations, string InitialLocationName)
{
    public static World FromLocationData(List<Dictionary<string, object>> locationData)
    {
        var locations = locationData.Select(Location.FromData).ToDictionary(location => location.Name);
        string InitialLocationName = (string)locationData[0]["name"];
        return new World(locations, InitialLocationName);
    }

    public static World FromJsonFile(string fileName)
    {
        List<Dictionary<string, object>> locationData = JsonLoader.LoadData(fileName);
        return FromLocationData(locationData);
    }

    public override string ToString()
    {
        return $"World(Initial location = '{InitialLocationName}', {Locations.Count} locations)";
    }
}

// %% [markdown]
//
// - Wir können die `World`-Klasse jetzt verwenden.

// %%
var world = World.FromJsonFile("simple-locations.json");

// %%
Console.WriteLine(world);
