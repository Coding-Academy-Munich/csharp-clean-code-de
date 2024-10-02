// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GRASP: Informations-Experte</b>
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
//   - `World` und `Location` Klassen
//   - `World` erzeugt alle `Location` Objekte
// - Nächster Schritt:
//   - Speichern von Information über die Verbindung zwischen den `Location`
//     Objekten
//   - Hilfreich dazu: Finden von Locations anhand ihres Namens
// - Frage:
//   - Wer findet `Location` Objekte anhand ihres Namens?

// %% [markdown]
//
// ## Kandidaten

// %% [markdown]
// <div style="float:left;margin:auto;padding:80px 0;width:25%">
// <ul>
// <li> <code>Player</code></li>
// <li> <code>Game</code></li>
// <li> <code>Pawn</code></li>
// <li> <code>Location</code></li>
// <li> <code>World</code></li>
// </ul>
// </div>
// <img src="img/adv-domain-03-small.png"
//      style="float:right;margin:auto;width:70%"/>

// %% [markdown]
//
// ## Informations-Experte (engl. "Information Expert", GRASP)
//
// ### Frage
//
// An welche Klasse sollen wir eine Verantwortung delegieren?
//
// ### Antwort
//
// An die Klasse, die die meisten Informationen hat, die für die Erfüllung der
// Verantwortung notwendig sind.
// %% [markdown]
//
// ## Wer ist der Informationsexperte?

// %% [markdown]
// <div style="float:left;margin:auto;padding:80px 0;width:25%">
// <ul>
// <li> <strike><code>Player</code></strike></li>
// <li> <strike><code>Game</code></strike></li>
// <li> <strike><code>Pawn</code></strike></li>
// <li> <strike><code>Location</code></strike></li>
// <li> <b><code>World</code></b></li>
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

    public Location GetLocationByName(string name)
    {
        return Locations[name];
    }

    public override string ToString()
    {
        return $"World(Initial location = '{InitialLocationName}', {Locations.Count} locations)";
    }
}

// %%
World world = World.FromJsonFile("simple-locations.json");

// %%
world

// %%
world.GetLocationByName("Room 1");

// %%
world.GetLocationByName("Room 2");

// %% [markdown]
//
// - `Code\Completed\GraspAdventure\AdventureV2` entspricht
//   unserem aktuellen Stand
