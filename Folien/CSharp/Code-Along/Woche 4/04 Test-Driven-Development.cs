// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Test-Driven-Development</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Idee
//
// - Verwende Tests, um das Design und die Feature-Entwicklung des Programms
//   voranzutreiben
// - Jeder neue Test beschreibt ein Feature-Inkrement des Programms
// - (Gut testbarer Code entsteht dabei quasi als Nebenprodukt)

// %% [markdown]
//
// ## Problem
//
// Wie können Tests das Design des Programms vorantreiben?

// %% [markdown]
//
// ## Mögliche Antworten
//
// - Tests verwenden die Funktionalität und zeigen komplizierte Interfaces auf
// - Tests ermöglichen Refactoring

// %% [markdown]
//
// ## Refactoring
//
// - Verbessern der Code-Struktur ohne Änderung des Verhaltens
// - Vorgehen in kleinen Schritten
//   - Nach jedem Schritt ist der Code wieder ausführbar
// - Ziele:
//   - Verbessern des Codes
//   - Verbesserung des Designs

// %% [markdown]
//
// ## Refactoring und Tests
//
// - Durch Refactoring wird das Design des Programms in kleinen Schritten verbessert
// - Die Korrektheit dieser Schritte wird durch Tests abgesichert

// %% [markdown]
//
// ## So what???
//
// <img src="img/dev-velocity.png"
//      style="display:block;margin:auto;width:70%"/>

// %% [markdown]
//
// ## Test-Driven Development
//
// - Ziel beim TDD ist nicht in erster Linie, eine hohe Testabdeckung zu erreichen
// - Ziel beim TDD ist es, durch Tests ein gutes Design zu entdecken
//   - Beim Schreiben der Tests versucht man, das Interface von Klassen und Funktionen
//     so zu gestalten, dass es leicht zu benutzen ist
//   - Dadurch, dass alle wesentlichen Teile des Programms durch Tests abgesichert
//     sind, kann man das Design durch Refactoring permanent an das aktuelle Feature-Set
//     anpassen

// %% [markdown]
//
// ## Der TDD-Zyklus (1/2)
//
// - Schreibe einen (minimalen) Test
//   - Der Test testet nur ein einziges neues (Teil-)Feature: **Baby Steps**
//   - Dieser Test schlägt fehl
// - Implementiere die minimale Funktionalität, um den Test zum Laufen zu bringen
//   - Dabei muss man nicht auf sauberen Code oder gutes Design achten
//   - Aber: **Solve Simply**

// %% [markdown]
//
// ## Der TDD-Zyklus (2/2)
//
// - Verbessere den Code
//   - Entferne die unsauberen Konstrukte, die im vorhergehenden Schritt eingefügt wurden
//   - Generalisiere die Implementierung, wenn zu viel Wiederholung entstanden ist
//   - **Dieser Schritt ist nicht optional!!!**

// %% [markdown]
//
// ## Der TDD-Zyklus
//
// - <span style="color: red"><b>Red (fehlschlagender Test)</b></span>
// - <span style="color: green"><b>Green (alle Tests sind wieder grün)</b></span>
// - <span style="color: blue"><b>Clean/Refactor (der Code ist wieder sauber)</b></span>

// %% [markdown]
//
// ## Baby-Steps
//
// - Das System ist nicht stunden- oder tagelang in einem Zustand, in dem es nicht
//   baubar, testbar oder ausführbar ist
// - Dadurch bekommt man bei jeder Änderung schnell Feedback vom Code
// - Häufiges Mergen und CI wird möglich

// %% [markdown]
//
// ## Warum Solve Simply?
//
// - Flexible, generische Lösungen erhöhen Komplexität
// - Lohnt sich nur bei tatsächlichem Bedarf
// - Flexibilitätsbedarf schwer vorhersagbar
// - Generische Lösungen schwerer zu implementieren
// - Naheliegende generische Lösung oft nicht wartbar

// %% [markdown]
//
// ## Annahmen von Solve Simply
//
// - Refactoring kann Code sauber machen, ohne Funktionalität zu ändern
// - Iterative Erweiterung und Flexibilisierung möglich
// - Refactoring-Schritte einfacher als direkte endgültige Lösung
// - Voraussetzung: hinreichend viele, gute Unit-Tests

// %% [markdown]
//
// ## Noch besser: TDD + Vorbereitungsschritt
//
// - Refactoring, damit die Änderung einfach wird
//   - Oft nicht trivial
//   - Fehlende Tests werden dabei ergänzt
// - Einfache Änderung mit TDD-Zyklus durchführen
// - Schritte wiederholen
