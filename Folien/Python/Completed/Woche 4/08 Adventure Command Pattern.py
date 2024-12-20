// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Adventure: Command Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Letzter Stand: Spielfiguren mit Enumeration
//
// <img src="img/adventure-v4b-overview.png" alt="Adventure Version 4b"
//      style="display:block;margin:auto;width:70%"/>

// %% [markdown]
//
// ### Probleme
//
// - Open-Closed Prinzip verletzt
//   - Neue Aktionen benötigen Änderungen an `Pawn` und `Action`
// - Signatur von `Pawn.Perform()` ist nicht klar
//   - Verschiedene Aktionen benötigen verschiedene Parameter

// %% [markdown]
//
// ### Lösung: Command Pattern
//
// - Aktionen werden in eigene Klassen ausgelagert
// - `Pawn.Perform()` nimmt ein `IAction`-Objekt
// - Die zur Ausführung der Aktion benötigten Daten werden im `IAction`-Objekt
//   gespeichert
// - `IAction`-Objekte können zusätzliche Funktion zur Verfügung stellen, z.B.
//    Texte für das UI bereitstellen

// %% [markdown]
//
// ## Version 4c: Command Pattern
//
// <img src="img/adventure-v4c-overview.png" alt="Adventure Version 4b"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ### Vorteile
//
// - Open-Closed Prinzip wird eingehalten
// - `Pawn.Perform()` hat eine klare Signatur
// - `IAction`-Klassen können zusätzliche Funktionen bereitstellen
