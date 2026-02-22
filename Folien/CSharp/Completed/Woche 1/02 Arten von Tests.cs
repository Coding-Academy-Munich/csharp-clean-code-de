// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Arten von Tests</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Nach Größe des Systems under Test (SuT)
// - Unit Tests:
//   - Testen einzelne Methoden oder Klassen,
//   - Typischerweise isoliert
// - Komponenten-/Integrationstests:
//   - Testen einzelne Komponenten in Isolation
// - End-to-End/Systemtests:
//   - Testen das komplette System.


// %% [markdown]
//
// ### Nach Fokus (Testquadranten)
//
// <img src="img/testing-styles.png"
//      alt="Testing Styles"
//      style="width: 75%; margin-left: auto; margin-right: auto;"/>

// %% [markdown]
//
// ## Ausgewählte Arten von Tests
//
// - Performanztests
//   - Lasttests, Stresstests, Spike-Tests, ...
// - Benutzbarkeitstests (Usability Tests):
//   - Wie gut kommen Benutzer mit dem System zurecht?

// %% [markdown]
//
// ## Regressions-Tests
//
// - Tests, die bereits behobene Fehler testen
// - Verhindern das erneute Einführen von Fehlern

// %% [markdown]
//
// ## White Box vs. Black Box
//
// - Wie viel Wissen über das System haben die Tester?

// %% [markdown]
//
// ## White-Box Tests
//
// - Auch Glass-Box oder strukturelles Testen genannt
// - Tester haben Zugriff auf
//   - Design und
//   - Implementierung des Systems

// %% [markdown]
//
// ## Black-Box Tests
//
// - Tester haben keinen Zugriff auf Systeminterna
// - Testen das Systemverhalten gegen Spezifikation
// - (Klassisches) Fuzzing ist automatisiertes Black-Box Testen

// %% [markdown]
//
// ## Tipp: White-Box vs. Black-Box Tests
//
// - Schreibe Tests als Black-Box Tests
// - Evaluiere sie als White-Box Tests
