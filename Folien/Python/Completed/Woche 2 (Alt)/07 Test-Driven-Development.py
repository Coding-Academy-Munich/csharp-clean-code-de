// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Test-Driven-Development</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
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
// ## Der TDD-Zyklus
//
// - Schreibe einen (minimalen) Test
//   - Der Test testet nur ein einziges neues (Teil-)Feature: **Baby Steps**
//   - Dieser Test schlägt fehl
// - Implementiere die minimale Funktionalität, um den Test zum Laufen zu bringen
//   - Dabei muss man nicht auf sauberen Code oder gutes Design achten
//   - Aber: **Solve Simply**
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
// - Eine flexible, generische Lösung erhöht oft die Komplexität des Systems
// - Das lohnt sich nur, wenn die Flexibilität auch benötigt wird
// - Entwickler können meist schlecht vorhersehen, an welchen Stellen
//   Flexibilität/Erweiterbarkeit benötigt wird
// - Eine flexible, generische Lösung ist oft sehr viel schwerer zu implementieren
//   als eine einfache Lösung für einen spezielleren Anwendungsfall
// - Die naheliegendste flexible, generische Lösung ist oft nicht der sauberste und
//   wartbare Code

// %% [markdown]
//
// ## Annahmen von Solve Simply
//
// - Es ist durch Refactoring möglich, Code in einen sauberen, wartbaren Zustand zu
//   bekommen, ohne dadurch die Funktionalität zu verändern
// - Es ist möglich, Code iterativ zu erweitern und flexibler zu machen,
//   wenn das erforderlich ist
// - Es ist einfacher, die Refactoring- und Iterations-Schritte durchzuführen, als
//   gleich die endgültige Lösung zu entwickeln
// - Diese Annahmen sind nur dann erfüllt, wenn hinreichend viele, gute
//   Unit-Tests vorliegen

// %% [markdown]
//
// ## Noch besser: TDD + Vorbereitungsschritt
//
// - Refactore den Code, sodass die Änderung einfach wird
//   - Das ist oft nicht so einfach…
//   - Wenn beim Refactoring klar wird, dass Tests fehlen, so werden diese hinzugefügt
// - Führe die einfache Änderung mit dem TDD-Zyklus durch
// - Wiederhole diese Schritte immer wieder
