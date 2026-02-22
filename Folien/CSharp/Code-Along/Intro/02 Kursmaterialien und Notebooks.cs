// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Kursmaterialien und Notebooks</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Kursmaterialien
//
// - Sie erhalten alle Folien und Code-Beispiele
// - Entpacken Sie die Materialien in einen Ordner Ihrer Wahl
// - Merken Sie sich den Speicherort!

// %% [markdown]
//
// # Ordnerstruktur
//
// - `Folien`: Kursfolien in drei Formaten
//   - `Notebooks`: Interaktive Jupyter Notebooks (empfohlen)
//   - `Html`: Statische HTML-Versionen (im Browser anzeigbar)
//   - `CSharp`: C#-Quellcode der Folien (zum Kopieren von Code-Schnipseln)
// - `Code`: C#-Projekte für Workshops
//   - `StarterKits`: Projekt-Gerüste als Ausgangspunkt
//   - `Completed`: Vollständige Lösungen

// %% [markdown]
//
// ## Ordnerstruktur (Übersicht)
//
// ```
// Folien/
// ├── Notebooks/
// │   ├── Code-Along/
// │   │   ├── Intro/
// │   │   └── Woche 1/
// │   └── Completed/
// │       ├── Intro/
// │       └── Woche 1/
// ├── Html/
// │   ├── Code-Along/
// │   └── Completed/
// └── CSharp/
//     ├── Code-Along/
//     └── Completed/
// Code/
// ├── StarterKits/
// └── Completed/
// ```

// %% [markdown]
//
// # Code-Along vs. Completed
//
// - **Code-Along**: Ausgangspunkt zum Mitmachen
//   - Teile des Codes fehlen (werden live geschrieben)
//   - Workshop-Lösungen fehlen
//   - Verwenden Sie diese für die Workshops!
// - **Completed**: Vollständige Referenz
//   - Kompletter Code und alle Lösungen
//   - Zum Nachschlagen, wenn Sie nicht weiterkommen

// %% [markdown]
//
// # Der Code-Ordner
//
// - `StarterKits`: Projekt-Gerüste für größere Workshops
//   - Haben ein `Sk`-Suffix im Namen (z.B. `FizzBuzzNunitSk`)
// - `Completed`: Fertige Projekte und Musterlösungen
//   - Zu jedem StarterKit gibt es ein fertiges Projekt
// - Projekte sind .NET-Solutions (`.sln` + `.csproj`)
// - Öffnen Sie die Projekte in Rider, Visual Studio oder VS Code

// %% [markdown]
//
// # Was sind Jupyter Notebooks?
//
// - Interaktive Dokumente, die Text und ausführbaren Code kombinieren
// - Ursprünglich aus der Data-Science-Welt (Python)
// - Für C# unterstützt durch Microsofts .NET Interactive
// - Code wird in einzelnen Zellen ausgeführt
// - Ergebnisse werden sofort angezeigt

// %% [markdown]
//
// # Notebooks verwenden
//
// - **Empfohlen:** Polyglot Notebooks in VS Code
//   - Einfachste Installation, beste C#-Unterstützung
// - **Alternativ:** Docker (JupyterLab im Browser)
//   - Keine lokale Installation nötig
// - **Fallback:** HTML-Folien
//   - Gleicher Inhalt, aber nicht interaktiv
// - In den nächsten Videos zeigen wir die Installation
