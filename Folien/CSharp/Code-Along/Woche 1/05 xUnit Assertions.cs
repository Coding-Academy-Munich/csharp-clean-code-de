// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>xUnit: Assertions</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Was ist xUnit?
//
// - xUnit ist ein modernes Test-Framework für C#
// - Open-Source
// - Einfach in Projekte zu integrieren
// - Viele Features
// - Struktur ähnlich zu xUnit-Test-Frameworks

// %% [markdown]
//
// ## Features von xUnit
//
// - Verwaltung von Tests und Test-Suites
// - Assertion-Bibliothek für Testfälle
// - Ausführung von Tests (Test Runner)
// - Versetzen des SuT in einen definierten Zustand (Test Fixtures)
// - Unterstützung für parametrisierte Tests (Theories)

// %% [markdown]
//
// ## Assertions in xUnit
//
// - `Assert.True`, `Assert.False` um Bedingungen zu prüfen
// - `Assert.Equal`, `Assert.NotEqual` um Werte zu prüfen
// - `Assert.Same`, `Assert.NotSame` um auf Referenzen zu prüfen
// - `Assert.Null`, `Assert.NotNull` um auf `null` zu prüfen
// - `Assert.Throws`, `Assert.ThrowsAny` um Exceptions zu prüfen

// %%
#r "nuget: xunit, *"

// %%
using Xunit;

// %% [markdown]
//
// ### Boolesche Assertions

// %%

// %%

// %%

// %% [markdown]
//
// ### Gleichheits-Assertions

// %%

// %%

// %%
string str1 = new string("Hello");
string str2 = new string("Hello");

// %%

// %%

// %%

// %%

// %% [markdown]
//
// #### Vergleich von Gleitkommazahlen

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ### Identitäts-Assertions

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ### Null-Assertions

// %%

// %%

// %%

// %% [markdown]
//
// ### Einschub: Lambda-Ausdrücke
//
// - Lambda-Ausdrücke sind eine Möglichkeit, Funktionen als Argumente zu
//   übergeben.
// - Sie können in Delegat-Typen konvertiert werden.

// %% [markdown]
//
// #### Ausdrucks-Lambdas

// %%

// %%

// %% [markdown]
//
// #### Anweisungs-Lambdas

// %%

// %%

// %% [markdown]
//
// ### Exception-Assertions

// %%
int n = 0;

// %%

// %%

// %%

// %%

// %%
