// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GRASP: Verbindungen</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Aktueller Stand des Adventure-Spiels
//
// - `Code/Completed/GraspAdventure/AdventureV2`
// - Klassen `Location` und `World`
// - `World` erzeugt `Location`-Objekte (Creator)
// - `World` kann `Location`-Objekte finden (Information Expert)

// %% [markdown]
//
// ### Nächster Schritt
//
// - Verbinden der `Location`-Objekte
// - Zwei neue Verantwortlichkeiten:
//   - Erstellen der Verbindungen (Doing)
//   - Speichern der Verbindungen (Knowing)

// %% [markdown]
//
// <img src="img/adventure-v2-overview.png"
//      style="display:block;margin:auto;width:40%"/>

// %% [markdown]
//
// ### Wer soll die Verbindungen erzeugen?
//
// **Creator:** Wer hat die Initialisierungsdaten?
//
// <ul class="fragment">
// <li><tt>World</tt></li>
// <li>(Wir brauchen die Daten aller Locations)</li>
// </ul>

// %% [markdown]
//
// ### Wer soll die Verbindungen speichern?
//
// **Information Expert:** Wer hat die Daten?
//
// <ul class="fragment">
//   <li>Die Klasse mit den meisten Informationen</li>
//   <li><tt>World</tt> erzeugt alle Verbindungsdaten</li>
//   <li>Daher ist <tt>World</tt> der Information Expert</li>
//   <li>Die Verbindungsdaten werden in <tt>World</tt> gespeichert</li>
// </ul>

// %% [markdown]
//
// **Das ist keine Richtige Anwendung von Information Expert!**

// %% [markdown]
//
// ### Was ist das Problem?
//
// - `Code/Completed/GraspAdventure/AdventureV3a`

// %% [markdown]
//
// ```csharp
// public class Connection
// {
//     public Location From { get; }
//     public string Direction { get; }
//     public Location To { get; }
//
//     public Connection(Location from, string direction, Location to)
//     {
//         From = from;
//         Direction = direction;
//         To = to;
//     }
// }
// ```

// %% [markdown]
//
// ```csharp
// public class World
// {
//     // ...
//     public Location GetConnectedLocation(Location location, string direction)
//     {
//         // Implementation
//     }
//
//     private List<Connection> connections = new List<Connection>();
// }
// ```

// %% [markdown]
//
// <img src="img/adventure-v3a-overview.png"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// - Häufigster Anwendungsfall für "Verbindungen in Richtung":
//   - Navigation von der Location, auf der ein Pawn steht zu einer anderen
//     Location
// - Wie geht das mit dieser Implementierung?
//   - `world.GetConnectedLocation(room1, "north")`
//   - Wir haben eine Abhängigkeit von `World` und `Location`!

// %% [markdown]
//
// <img src="img/adv-domain-03-small.png"
//      style="display:block;margin:auto;width:70%"/>

// %% [markdown]
//
// ### Wie können wir das vermeiden?
//
// - Verlagern der Verantwortlichkeit für die Navigation
// - Jede `Location` kennt ihre ausgehenden Verbindungen
// - `room1.GetConnectedLocation("north")`

// %% [markdown]
//
// ### Hat uns Information Expert in die Irre geführt?
//
// Nein. In diesem Fall sagt der Information Expert nicht sehr viel aus, wenn
// wir ihn korrekt anwenden:
//
// - Die Verantwortung, die wir zuweisen wollen, ist das Speichern der
//   Verbindungen
// - Jede Verbindung ist eine Beziehung zwischen zwei Locations
// - Es spielt keine Rolle, dass `World` alle Locations kennt
// - Wir haben keinen Informations-Experten für das Speichern der Verbindungen

// %% [markdown]
//
// ### Lösungen
//
// - Zugeordnete Doing-Verantwortlichkeit:
//   - Finden Sie eine zugehörige Doing-Verantwortlichkeit,
//   - Wählen Sie die Klasse, die verantwortlich sein sollte, und
//   - Ordnen Sie die Daten so zu, dass diese Klasse zum Information Expert wird
// - Verwenden Sie andere Muster oder Techniken, um die Verantwortlichkeit
//   zuzuweisen

// %% [markdown]
//
// <img src="img/adventure-v3b-overview.png"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ### Merkregel: Lokale Informationen > Globaler Zustand
//
// - Versuchen Sie, die Verantwortlichkeit für Informationen möglichst lokal zu
//   halten
// - Das verringert fast immer die Kopplung im System
