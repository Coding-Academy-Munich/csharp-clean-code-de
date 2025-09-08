// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Code Smells</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Was sind Code Smells?
//
// Code Smells sind Hinweise darauf, dass wir schlechten Code haben und refactoren
// sollten.

// %% [markdown]
//
// ## Code Smells
//
// - Mysteriöser Name
//   - Namen verschleiern statt zu klären
// - Duplizierter Code
//   - Mehrfache Vorkommen von identischem oder ähnlichem Code
// - Lange Funktion
// - Lange Parameterliste
// - Globale Daten
// - Veränderliche Daten (Mutable Data)
//   - Kapselung von Zugriff auf veränderliche Daten
//   - Command Query Separation

// %% [markdown]
//
// - Divergente Änderung
//   - Ein Modul ändert sich bei vielen verschiedenen Änderungen
// - Schrotflinten-Operation
//   - Selbst einfache Änderungen betreffen viele Module
// - Feature-Neid
//   - Eine Funktion kommuniziert mit vielen Funktionen aus einem anderen Modul
// - Datenklumpen
//   - Die gleichen Daten erscheinen an vielen Stellen zusammen ohne in eine
//     Klasse zusammengefasst zu werden


// %% [markdown]
//
// - Obsession mit primitiven Datentypen (Primitive Obsession)
//   - Ausgiebiger Gebrauch von eingebauten Typen anstelle von sinnvollen Klassen
// - Wiederholte Switches
//   - Switches über die gleichen Daten an vielen Stellen
// - Schleifen
//   - Können oft durch Pipelines ersetzt werden
// - Faules Element (Lazy Element)
//   - Programmelemente, die keinen Mehrwert haben

// %% [markdown]
//
// - Spekulative Allgemeinheit
//   - Flexibilität, die momentan nicht benötigt wird
// - Temporäres Feld (Temporary Field)
//   - Attribute, die nur zeitweise verwendet werden/gültig sind
// - Ketten von Nachrichten
//   - Lange Ketten von Getter-Aufrufen oder Hilfsvariablen
// - Mittelsmann
//   - Die meisten Methoden einer Klasse delegieren nur an eine andere Klasse

// %% [markdown]
//
// - Insider Trading
//   - Klassen haben zu viel Wissen über die Interna von anderen Klassen
// - Große Klasse
//   - Zu viele Verantwortlichkeiten
// - Alternative Klassen mit unterschiedlichen Interfaces
//   - Klassen, die austauschbar sein sollten, aber nicht sind
// - Datenklassen
//   - Klassen, die nur Getter und Setter haben
// - Verweigerte Erbschaft
//   - Unterklassen erben Funktionalität, die sie nicht brauchen
// - Kommentare
//   - Falls sie als benötigt werden, weil der Code schlecht ist
