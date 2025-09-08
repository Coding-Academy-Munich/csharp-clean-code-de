// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>SRP und Hohe Kohäsion</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// - Problem: Zu viel Funktionalität in einer Klasse
// - Sowohl SOLID als auch GRASP haben jeweils ein Pattern dafür/dagegen

// %% [markdown]
//
// ## SOLID: Single Responsibility Principle (SRP)
//
// Alternative Formulierungen:
//
// - Ein Modul sollte nur einen einzigen Grund haben, sich zu ändern
// - Ein Modul sollte nur gegenüber einem einzigen Akteur verantwortlich sein.

// %% [markdown]
//
// - Beispiel (aus Clean Architecture):
//
// ```csharp
// class Employee {
//     public void CalculatePay() { }  // <-- Finanzabteilung (CFO)
//     public void ReportHours() { }   // <-- Human Resources (COO)
//     public void Save() { }          // <-- Datenbank Administrator (CTO)
// }
// ```

// %% [markdown]
//
// ## GRASP: Hohe Kohäsion (High Cohesion)
//
// ### Problem
//
// - Wie definieren wir Klassen, die fokussiert, verständlich und wartbar sind?
//
// ### Lösung
//
// - Weise Verantwortlichkeiten so zu, dass die Kohäsion hoch ist
// - Verwende dieses Prinzip um Design-Alternativen zu bewerten
//
// - Als Nebeneffekt wird die Kopplung reduziert

// %% [markdown]
//
// ### Was ist Kohäsion?
//
// Kohäsion ist ein Maß dafür, wie gut verschiedene Teile eines Artefakts
// zusammenpassen
//
// - Klare Abstraktion
// - Guter Name
// - Wenige Änderungsgründe

// %% [markdown]
//
// ### Hohe Kohäsion
//
// - Erleichtert Entwicklung, Wiederverwendung, Testen, Performanz
//
// ### Niedrige Kohäsion
//
// - Erschwert das Verständnis des Codes, das Finden von Stellen für Änderungen
// - Führt zu großen Klassen und/oder hoher Kopplung
// - Hat fast immer einen **großen negativen Effekt**
// - Ist **schwer zu beheben**

// %% [markdown]
//
// ### Wie erkennen wir niedrige Kohäsion?
//
// - Schwierigkeiten, Namen zu finden
// - Viele Änderungsgründe
// - Code Smells:
//   - Divergente Änderungen (divergent change)
//   - Schrotflinten-Operation (shotgun surgery)

// %% [markdown]
//
// ### Verwandte Themen
//
// - Schichten/Hexagonale Architektur
// - SRP
// - Command/Query Separation (CQS)
// - Aspekte (Separation of Concerns)
