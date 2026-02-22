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

// %% [markdown]
//
// ## Mini-Workshop: xUnit Assertions
//
// Hier ist eine einfache Klasse `StringUtils`. Schreiben Sie Assertions, um
// die Methoden dieser Klasse zu überprüfen. Verwenden Sie dabei verschiedene
// xUnit Assertions.

// %%
public class StringUtils
{
    public string Reverse(string s)
    {
        char[] chars = s.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }

    public bool IsPalindrome(string s)
    {
        string lower = s.ToLower();
        return lower == Reverse(lower);
    }

    public string Capitalize(string s)
    {
        if (string.IsNullOrEmpty(s)) return s;
        return char.ToUpper(s[0]) + s.Substring(1).ToLower();
    }

    public int CountWords(string s)
    {
        if (string.IsNullOrWhiteSpace(s)) return 0;
        return s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

// %% [markdown]
//
// Schreiben Sie Assertions für die folgenden Fälle:

// %% [markdown]
//
// `Reverse` gibt den String umgekehrt zurück:

// %%
var utils = new StringUtils();
// Assert that Reverse("Hello") returns "olleH"

// %% [markdown]
//
// `IsPalindrome` erkennt Palindrome (unabhängig von Groß-/Kleinschreibung):

// %%
// Assert that "Racecar" is a palindrome
// Assert that "Hello" is not a palindrome

// %% [markdown]
//
// `Capitalize` formatiert den String korrekt:

// %%
// Assert that Capitalize("hELLO") returns "Hello"
// Assert that the result starts with "H"

// %% [markdown]
//
// `CountWords` zählt die Wörter korrekt:

// %%
// Assert that "Hello World" has 2 words
// Assert that an empty string has 0 words
// Assert that "  spaced  out  " has 2 words

// %% [markdown]
//
// `Capitalize` wirft bei `null` keine Exception:

// %%
// Assert that Capitalize(null) returns null
