// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Adventure: Spielfigur</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
// <img src="img/adv-domain-03.png"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## Version 4a: Spielfiguren
//
// <img src="img/adventure-v4a-overview.png" alt="Adventure Version 4a"
//      style="display:block;margin:auto;width:40%"/>

// %% [markdown]
//
// Siehe `code/completed/adventure/v4a` für den vollständigen Code.

// %% [markdown]
//
// ## Version 4b: Enumeration der Aktionen
//
// - Enumeration `Action` mit allen möglichen Aktionen
// - `Pawn`-Klasse hat nur noch eine `perform()`-Methode
// - `perform()`-Methode bekommt eine `action` als Parameter

// %% [markdown]
//
// ## Version 4b: Spielfiguren mit Enumeration
//
// <img src="img/adventure-v4b-overview.png" alt="Adventure Version 4b"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ```csharp
// public enum Action { Move, SkipTurn }
// ```
//
// ```csharp
// public class Pawn
// {
//     private Location location;
//
//     public void Perform(Action action, string direction)
//     {
//         switch (action)
//         {
//             case Action.Move:
//                 location = location.GetConnectedLocation(direction);
//                 break;
//             case Action.SkipTurn:
//                 break;
//         }
//     }
//
//     public void PerformIfPossible(Action action, string direction)
//     {
//         try
//         {
//             Perform(action, direction);
//         }
//         catch (Exception)
//         {
//             // Ignore the exception
//         }
//     }
// }
// ```

// %% [markdown]
//
// ## GRASP und SOLID Prinzipien
//
// - GRASP:
//   - Geschützte Variation (Protected Variation)
//   - Indirektion
//   - Polymorphie
// - SOLID:
//   - Open-Closed Prinzip
