// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Polyglot Notebooks in VS Code</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Was sind Polyglot Notebooks?
//
// - Microsoft-Erweiterung für Visual Studio Code
// - Interaktive Notebooks direkt in VS Code
// - Unterstützt C#, F#, PowerShell, SQL und mehr
// - Beste Unterstützung für C# (Code-Vervollständigung, Fehlerdiagnose)
// - Kein Docker erforderlich

// %% [markdown]
//
// # Hinweis: Deprecation
//
// - Microsoft hat angekündigt, Polyglot Notebooks ab März 2026 nicht mehr
//   weiterzuentwickeln
// - Die Erweiterung funktioniert weiterhin und wird nicht deaktiviert
// - Für diesen Kurs ist das kein Problem
// - Falls die Erweiterung irgendwann nicht mehr funktioniert: Docker oder
//   HTML-Folien als Alternative verwenden

// %% [markdown]
//
// # Voraussetzungen
//
// - [.NET SDK](https://dotnet.microsoft.com/download) (Version 8 oder neuer)
//   - Test: `dotnet --version`
// - [Visual Studio Code](https://code.visualstudio.com/)

// %% [markdown]
//
// # Installation der Erweiterung
//
// - Erweiterungen in VS Code öffnen (`Ctrl+Shift+X`)
// - Nach "Polyglot Notebooks" suchen
// - Erweiterung von Microsoft installieren
//   - ID: `ms-dotnettools.dotnet-interactive-vscode`

// %% [markdown]
//
// # Testen der Installation
//
// - Command Palette öffnen: `Ctrl+Shift+P`
// - "Polyglot Notebook: Create new blank notebook" auswählen
// - Als Sprache "C#" (`.NET Interactive`) auswählen
// - In die erste Zelle `1 + 1` eingeben
// - Mit `Ctrl+Enter` ausführen
// - Ergebnis `2` sollte angezeigt werden

// %% [markdown]
//
// # Weitere Tests
//
// - Neue Zelle hinzufügen (+ Code)
// - `Console.WriteLine("Hello, World!");` eingeben und ausführen
// - In einer weiteren Zelle: `string name = "World";`
// - In der nächsten Zelle: `Console.WriteLine($"Hello, {name}!");`
// - Variablen aus vorherigen Zellen bleiben verfügbar

// %% [markdown]
//
// # Kurs-Notebooks öffnen
//
// - Kurs-Materialien entpacken
// - In VS Code: `File` > `Open Folder...` > Notebooks-Ordner auswählen
// - `.ipynb`-Dateien im Explorer anklicken
// - Polyglot Notebooks öffnet sie automatisch

// %% [markdown]
//
// # Alternative: Docker
//
// - Docker-basierte Notebooks als gleichwertige Alternative
// - Läuft im Browser (JupyterLab)
// - Nur Docker Desktop erforderlich
// - Siehe Video "Docker für Notebooks"

// %% [markdown]
//
// # Fallback: HTML-Folien
//
// - Alle Notebooks gibt es auch als HTML-Folien (gleicher Inhalt, nicht
//   interaktiv)
// - Falls keine der Notebook-Installationen funktioniert: HTML-Folien
//   verwenden
// - Wir empfehlen die Notebook-Variante, wenn möglich
