// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Concrete Factory</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Adventure Spiel Version 3b
//
// <img src="img/adventure-v3b-overview.png" alt="Adventure Version 3c"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ### Adventure Spiel Version 3b
//
// - Zuweisung von Funktionalität zu `World` und `Location` ist sinnvoll
// - Wir sehen, dass `World` in Gefahr ist, zu viele "Änderungsgründe" zu haben
//   - Änderungen an der Implementierung der Spiele-Welt
//   - Änderungen an den Initialisierungsdaten (z.B. XML statt JSON)
// - Können wir das vermeiden?

// %% [markdown]
//
// # Concrete Factory (Simple Factory)
//
// ### Frage
//
// - Wer soll ein Objekt erzeugen, wenn es Umstände gibt, die gegen Creator
//   sprechen?
//   - komplexe Logik zur Erzeugung
//   - Kohäsion
//   - Viele Parameter zur Erzeugung notwendig
//
// ### Antwort
//
// - Eine Klasse, die nur für die Erzeugung von Objekten zuständig ist
// - Diese Klassen werden oft als *Factory* bezeichnet

// %% [markdown]
//
// - Factories sind Beispiele für das GRASP Pattern "Pure Fabrication"
// - Sie können die Kohäsion von Klassen erhöhen und ihre Komplexität reduzieren
// - Sie erhöhen aber auch die Gesamtkomplexität des Systems

// %% [markdown]
//
// ## Beispiel
//
// - In Version 3b ist der Konstruktor von `World` relativ komplex
// - Wir können die Erzeugung in eine Factory auslagern
// - Siehe `Code/Completed/GraspAdventure/AdventureV3c`

// %% [markdown]
//
// ## Version 3c: Factory
//
// <img src="img/adventure-v3c-overview.png" alt="Adventure Version 3c"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## Workshop: Adressbuch
//
// In diesem Workshop sollen Sie eine Adressbuchanwendung erstellen, die
// Kontaktinformationen aus einer Liste von kommaseparierten Werten (CSV)
// importieren kann. Jede Zeile in der CSV-Datei repräsentiert einen Kontakt.
//
// ### Anforderungen
//
// - Implementieren Sie eine `Contact`-Klasse mit Eigenschaften wie Name, E-Mail
//   und Telefonnummer.
// - Erstellen Sie eine `AddressBook`-Klasse, die mehrere `Contact`-Objekte
//   enthalten kann.
// - Entwerfen Sie eine Factory, die `AddressBook`-Objekte aus CSV-Strings erstellen
//   kann.
// - Verwenden Sie dazu das Concrete Factory Pattern.

// %%

// %%
