// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GRASP: Controller</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # GRASP: Controller
//
// - Für jedes Modul/Subsystem: Externe Meldungen werden von Controller-Objekten
//   bearbeitet, die
//   - nicht Teil der Benutzeroberfläche sind
//   - jeweils ein Subsystem oder einen Anwendungsfall abdecken
// - Der Controller ist das erste Objekt nach der Benutzeroberfläche, das
//   Ereignisse/Meldungen bearbeitet. Er koordiniert das System.
// - Der Controller ist oft eine Fassade, d.h. er delegiert seine Arbeit an andere
//   Objekte.
// - Ein Use-Case-Controller bearbeitet immer einen kompletten Use-Case (Controller
//   können aber auch mehr als einen Use-Case bearbeiten).

// %% [markdown]
//
// ## Controller
//
// - Verwandt: Fassaden-Pattern (Domänen-Fassade), Domänencontroller
// - Siehe hexagonale Architektur: Controller sind die Ports in der hexagonalen
//   Architektur
// - Tests: Controller bieten eine zentrale Schnittstelle für einzelne
//   Subsysteme oder Anwendungsfälle
