// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GRASP: Lose Kopplung</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Lose Kopplung (Low Coupling, GRASP)
//
// ### Frage
//
// Wie können wir den negativen Einfluss von Änderungen minimieren?
//
// ### Antwort
//
// Weise Verantwortlichkeiten so zu, dass die (unnötige) Kopplung minimiert wird

// %% [markdown]
//
// - Kopplung: Abhängigkeit zwischen zwei Artefakten
// - [Verschiedene Arten von
//   Kopplung](https://de.wikipedia.org/wiki/Kopplung_(Softwareentwicklung))
// - Lose Kopplung ist ein allgemeines Prinzip
// - Lose Kopplung wichtig von *stabilen* zu *instabilen* Artefakten

// %% [markdown]
//
// ### Einfluss von Kopplung
//
// - Wenn **A** eng an **B** gekoppelt ist (**A → B**), dann können wir **A**
//   nicht unabhängig von **B** verstehen und ändern
// - Das ist transitiv: Wenn **A → B → C → D**, dann müssen wir potenziell
//   **A**, **B** und **C** ändern, wenn wir eine Änderung an **D** vornehmen

// %% [markdown]
//
// ### Vermeidung von Kopplung
//
// - Ein gewisses Maß an Kopplung ist **unvermeidlich**
//   - Wann immer zwei Komponenten zusammenarbeiten entsteht Kopplung
// - Enge Kopplung an **stabile** Komponenten ist typischerweise kein Problem
//   - Beispiel: `System.Collections.Generic.List<T>`
// - Unerwünschte enge Kopplung auf lokaler Ebene
//   - Ist nicht gut, aber oft kein großes Problem
//   - Kann typischerweise relativ leicht behoben werden
// - Unerwünschte enge Kopplung auf globaler Ebene
//   - Ist sehr schwer zu beseitigen
//   - Sollte während der Entwicklung immer im Fokus sein

// %% [markdown]
//
// ### Was weist auf hohe Kopplung hin?
//
// - Langsames Kompilieren für die meisten Änderungen
// - Schrotflinten Operationen (Shotgun Surgery)
//   - Kleine Änderungen in der Funktionalität erfordern Änderungen in vielen
//     Teilen des Codes
// - Divergente Änderungen
//   - Viele Änderungen in der Funktionalität erfordern Änderungen im gleichen
//     Teil des Codes
// - Gesetz von Demeter
//   - Eine Komponente sollte nur mit Komponenten kommunizieren, die sie direkt
//     kennt
//
// Typischerweise ist hohe Kopplung auch mit niedriger Kohäsion verbunden.

// %% [markdown]
//
// ### Wie können wir Kopplung vermeiden?
//
// - GRASP:
//   - Creator, Information Expert
//   - Pure Fabrication, Indirection, Protected Variations
// - SOLID: Single Responsibility Principle, Dependency Inversion Principle
