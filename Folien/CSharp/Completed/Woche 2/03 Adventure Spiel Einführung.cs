// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Adventure Spiel: Einführung</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Beispiel: Adventure-Game
//
// - Der Spieler kontrolliert eine Spielfigur,
// - Die Spielfigur kann sich durch eine Welt bewegen, die aus miteinander
//   verbundenen Orten besteht
// - In manchen Räumen sind Objekte zu finden, mit denen der Spieler
//   interagieren kann, z.B. Behälter mit Beute, usw.
// - In der Welt gibt es vom Computer kontrolliere Figuren (NPCs)
// - Der Spieler kann mit NPCs interagieren (z.B. reden, kämpfen, handeln)

// %% [markdown]
//
// - Siehe Game Pitch für Details (Ordner `Extras`)
// - Design-Modelle als UML-Diagramme
//   - Wichtig: Verständnis für die Domäne
//   - Nicht wichtig: Details der Modellierung, UML vs. ...
//   - Nicht wichtig: Entwicklungsprozess, ...

// %% [markdown]
//
// ## Domänenmodell: Statische Struktur

// %% [markdown]
// <img src="img/adv-domain-01.png"
//      style="display:block;margin:auto;width:50%"/>
// %% [markdown]
// <img src="img/adv-domain-02.png"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
// <img src="img/adv-domain-03.png"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## Domänenmodell: Verhalten
//
// - Verständnis von Verhalten ist wichtig
// - Viele Möglichkeiten, Verhalten zu beschreiben
//   - Use Cases
//   - User Stories
//   - Aktivitätsdiagramme
//   - Sequenzdiagramme
//   - ...

// %% [markdown]
//
// ### Use Cases
//
// - Wir wollen für unser Spiel die folgenden Use Cases betrachten:
//   - Initialisierung des Spiels
//   - Haupt-Spiel-Schleife (Main Game Loop)
//   - Spieler macht einen Zug
