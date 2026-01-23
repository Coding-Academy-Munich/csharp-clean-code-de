// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Adventure: Strategy Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Version 4c: Command Pattern
//
// <img src="img/adventure-v4c-overview.png" alt="Adventure Version 4c"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// - Sowohl menschliche als auch computergesteuerte Spieler
// - Dazu notwendig:
//   - Generieren aller möglichen Aktionen (situationsabhängig)
//   - Auswählen einer Aktion
//   - Ausführen der Aktion
// - Wer soll diese Verantwortlichkeiten übernehmen?

// %% [markdown]
//
// - Informationsexperte:
//   - Im Moment weiß niemand, alle Aktionen, die möglich sind
//   - `Pawn` weiß, wo er sich befindet
//   - Für weitere Aktionen:
//     - `Pawn` enthält wahrscheinlich die meisten benötigten Informationen
// - Sollen wir die Verantwortung an `Pawn` übergeben?

// %% [markdown]
//
// ### Gegenargumente
//
// - Damit bekommt `Pawn` potentiell zu viele Verantwortlichkeiten
//   - Bewegung auf dem Spielfeld
//   - Darstellung der Grafik
//   - Strategie für computergesteuerte Spieler
//   - Interaktion mit menschlichem Benutzer
// - Niedrige Repräsentationslücke?

// %% [markdown]
//
// - Sehen wir im Domänenmodell nach:

// %% [markdown]
// <img src="img/adv-domain-03.png"
//      style="display:block;margin:auto;width:60%"/>

// %% [markdown]
//
// - `Player` ist für die Strategie zuständig
// - Wir sollten eine `Player`-Klasse einführen und ihr die Verantwortung
//   für die neuen Aufgaben übergeben

// %% [markdown]
//
// ## Version 5a: `Player`-Klasse
//
// <img src="img/adventure-v5a.png" alt="Adventure Version 5a"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// - `Player`-Klasse ist für die Strategie zuständig
// - Im Moment nur eine fest verdrahtete Strategie:
//   - Spieler nimmt immer letzten Eintrag in der Liste der Aktionen
// - Wir wollen mehrere Strategien unterstützen
// - Mit einer "interaktiven" Strategie wollen wir den menschlichen Spieler
//   einbinden
// - Versuchen wir eine Enumeration aller möglichen Strategien

// %% [markdown]
//
// ## Version 5b: Mehrere Strategien
//
// <img src="img/adventure-v5b.png" alt="Adventure Version 5b"
//      style="display:block;margin:auto;width:90%"/>

// %% [markdown]
//
// - Das Klassendiagramm sieht nicht so schlecht aus
// - Implementierung ist unübersichtlich
// - Open-Closed Prinzip ist verletzt
// - Besser: Strategie Pattern

// %% [markdown]
//
// ## Version 5c: Mehrere Strategien
//
// <img src="img/adventure-v5c.png" alt="Adventure Version 4b"
//      style="display:block;margin:auto;height:60%"/>

// %% [markdown]
//
// ### Nächste Schritte
//
// - Verbesserung der interaktiven Strategie / Kommunikation von Informationen
// - Leichtere Benutzbarkeit

