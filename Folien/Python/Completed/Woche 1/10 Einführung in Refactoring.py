// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Einführung in Refactoring</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Was ist Refactoring?
//
// - Ändern eines Software-Systems
// - In kleinen Schritten
// - Ohne dessen externes Verhalten zu ändern
// - Um dessen interne Struktur zu verbessern
//
// *Im Wesentlichen verbessern Sie beim Refactoring das Design des Codes, nachdem er
// geschrieben wurde.*
//
// (Martin Fowler)

// %% [markdown]
//
// ## Warum Refactoring?
//
// - Code wird verständlicher
// - Code wird einfacher zu warten
// - Code wird einfacher zu erweitern
// - Code wird einfacher zu testen
// - ...

// %% [markdown]
//
// ## Was ist Refactoring nicht?
//
// - Große Änderungen am Code in einem Schritt
// - Hinzufügen von neuen Features
// - Beheben von Bugs
// - **Ändern des externen APIs**

// %% [markdown]
//
// ## Wann Refactoring?
//
// - Wenn Code verstanden werden muss
// - Wenn Code erweitert werden muss
// - Wenn wir schlechten Code finden, den wir ändern müssen
// - ...
//
// **Fast immer, wenn wir am Code arbeiten**

// %% [markdown]
//
// ## Sollen wir extra Zeit für Refactoring einplanen?
//
// - Normalerweise nicht
// - Refactoring ist Teil der Entwicklung und sollte permanent stattfinden
// - Möglicherweise:
//   - Zeit zum Refaktorisieren einplanen, wenn wir an Code arbeiten, der
//     schlecht ist
//
// *Refactoring ist keine Aktivität, die vom Programmieren getrennt ist -
// genauso wenig, wie Sie Zeit zum Schreiben von if-Anweisungen einplanen.*
//
// (Martin Fowler)
