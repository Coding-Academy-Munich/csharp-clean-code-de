// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Adventure: Version 1</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// Wie fangen wir an?

// %% [markdown]
//
// ## Niedrige Repräsentationslücke (Low Representational Gap)
//
// - Idee: Konzepte aus der Domäne in Code übertragen
// - Implementieren Sie ein Szenario aus einem Use Case
// - Nehmen Sie die Domänen-Konzepte als Kandidaten für die ersten Klassen her

// %% [markdown]
//
// - Use Case: "Spiel initialisieren"
// - Haupterfolgsszenario ohne Laden eines Spiels

// %% [markdown]
//
// ## Domänenmodell
//
// Hier ist noch einmal der relevante Teil des Domänenmodells:

// %% [markdown]
// style="display:block;margin:auto;width:80%"/> %% [markdown] lang="de"
// tags=["notes"]
//
// - Hier ist der relevante Teil unseres Domänenmodells.
// - Wir wollen uns im Moment auf die Konzepte `World` und `Location`
//   konzentrieren.
// - In der Implementierung bekommen wir daraus die Klassen `World` und
//   `Location`.
// - Wir haben die Stelligkeiten schon angegeben: Eine Welt kann mehrere Orte
//   enthalten, ein Ort gehört genau zu einer Welt.
// - Bei der Implementierung müssen wir uns noch überlegen, wie wir die
//   Beziehungen zwischen den Klassen abbilden.
// - In diesem Fall ist es relativ einfach: Wir können die Orte z.B. in einer
//   Liste in der Welt speichern
// - Damit können wir von der Welt zu allen Orten navigieren, aber nicht zurück.
// - Das scheint im Moment ausreichend zu sein.
// - Es ist vielleicht Overkill für dieses einfache Beispiel ein Design-Modell
//   zu malen, besser gesagt, es ist sicherlich Overkill es für dieses Beispiel
//   zu machen, aber wenn Sie wollen können Sie das natürlich machen.
// - Und ich mache es hier auch, damit Sie sehen, wie das aussehen könnte.

// %% [markdown]
//
// ## Statisches Designmodell

// %% [markdown]
// <img src="img/adv-world-cd-01.png"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## Implementierung
//
// - Implementierung: `Code/Completed/GraspAdventure/AdventureV1`
// - Starter-Kit: `Code/StarterKits/GraspAdventureSk/AdventureSk`

// %%
using System;

// %%
public record Location(string Name, string Description);

// %%
new Location("Here", "Where I am")

// %% [markdown]
//
// ## Konstruktion von Location Instanzen
//
// - [Einfache Orte](./simple-locations.json)
// - [Dungeon](./dungeon-locations.json)

// %% [markdown]
//
// - Es kann sein, dass der Pfad unseres Notebooks
//   auf ein Elternverzeichnis des gesuchten Verzeichnisses zeigt
// - Deshalb suchen wir in allen Unterverzeichnissen nach der JSON-Datei:

// %%
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// %%
public static class FileFinder
{
    public static string Find(string name)
    {
        return Directory.GetFiles(".", name, SearchOption.AllDirectories)
            .FirstOrDefault() ?? throw new FileNotFoundException($"File {name} not found");
    }
}

// %%
string simpleLocationsPath = FileFinder.Find("simple-locations.json");

// %%
Path.GetFileName(simpleLocationsPath)

// %%
Path.GetFullPath(simpleLocationsPath)

// %%
#r "nuget: Newtonsoft.Json, 13.0.1"

// %%
using Newtonsoft.Json;
using System.Collections.Generic;

// %%
public static class JsonLoader
{
    public static List<Dictionary<string, object>> LoadData(string fileName)
    {
        try
        {
            string json = File.ReadAllText(FileFinder.Find(fileName));
            return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Dictionary<string, object>>();
        }
    }
}



// %%
List<Dictionary<string, object>> simpleLocationsData = JsonLoader.LoadData("simple-locations.json");

// %%
foreach (var room in simpleLocationsData)
{
    Console.WriteLine(string.Join(", ", room.Select(kvp => $"{kvp.Key}: {kvp.Value}")));
}
// %% [markdown]
//
// ### Erzeugen von Location Instanzen aus JSON Daten
//
// - Wir können eine statische Methode in der `Location` Klasse implementieren,
//   die eine `Location` Instanz aus einer Map erzeugt
// - Eine solche Methode nennt man eine Fabrikmethode oder Factory-Methode oder
//   genauer eine statische Factory-Methode
// - Fügen wir also eine statische Methode `FromData()` zur `Location` Klasse
//   hinzu

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
Location.FromData(simpleLocationsData[0])

// %%
using System.Linq;

// %%
List<Location> locations = simpleLocationsData.Select(Location.FromData).ToList();

// %%
locations

// %%
Dictionary<string, Location> locationMap = simpleLocationsData
    .Select(Location.FromData)
    .ToDictionary(location => location.Name);

// %%
locationMap

// %% [markdown]
//
// ## Implementierung der World Klasse
//
// - Beliebige Anzahl von `Location`-Instanzen
// - Zugriff auf `Location`-Instanzen über Namen
// - Speicherung des initialen Ortsnamens

// %%
using System.Collections.Generic;

// %%
public record World(Dictionary<string, Location> Locations, string InitialLocationName)
{
    public override string ToString()
    {
        return $"World(Initial location = '{InitialLocationName}', {Locations.Count} locations)";
    }
}

// %%
World myWorld = new World(locationMap, "Room 1");

// %%
myWorld
