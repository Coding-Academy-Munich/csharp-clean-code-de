// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>xUnit: Assertions</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
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
Assert.True(5 > 3);

// %%
Assert.False(2 > 5);

// %%
// Assert.True(1 > 4);

// %% [markdown]
//
// ### Gleichheits-Assertions

// %%
Assert.Equal(4, 2 + 2);

// %%
Assert.NotEqual(5, 2 + 2);

// %%
string str1 = new string("Hello");
string str2 = new string("Hello");

// %%
Assert.Equal(str1, str2);

// %%
Assert.True(str1.Equals(str2));

// %%
// Assert.True("Hello".Equals("World"));

// %%
// Assert.Equal("Hello", "World");

// %% [markdown]
//
// #### Vergleich von Gleitkommazahlen

// %%
// Assert.Equal(0.1 + 0.2, 0.3);

// %%
Assert.Equal(0.1 + 0.2, 0.3, 5);

// %%
// Assert.Equal(0.1, 0.11, 2);

// %%
Assert.Equal(0.1, 0.11, 1);

// %% [markdown]
//
// ### Identitäts-Assertions

// %%
Assert.Same("Hello", "Hello");

// %%
// Assert.Same(str1, str2);

// %%
Assert.Same(str1, str1);

// %%
// Assert.NotSame(str1, str1);

// %% [markdown]
//
// ### Null-Assertions

// %%
Assert.Null(null);

// %%
// Assert.Null(0);

// %%
Assert.NotNull(123);

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
Func<int, int, int> add = (x, y) => x + y;

// %%
add(2, 3)

// %% [markdown]
//
// #### Anweisungs-Lambdas

// %%
Action<string> print = s => {
    Console.Write("Hello, ");
    Console.WriteLine(s);
};

// %%
print("World!");

// %% [markdown]
//
// ### Exception-Assertions

// %%
int n = 0;

// %%
Assert.Throws<DivideByZeroException>(() => 1 / n);

// %%
// Assert.Throws<DivideByZeroException>(() => 1 / 1);

// %%
// Assert.Throws<ArithmeticException>(() => 1 / 0);

// %%
Assert.ThrowsAny<DivideByZeroException>(() => 1 / n);

// %%
Assert.ThrowsAny<ArithmeticException>(() => 1 / n);

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
Assert.Equal("olleH", utils.Reverse("Hello"));

// %% [markdown]
//
// `IsPalindrome` erkennt Palindrome (unabhängig von Groß-/Kleinschreibung):

// %%
Assert.True(utils.IsPalindrome("Racecar"));
Assert.False(utils.IsPalindrome("Hello"));

// %% [markdown]
//
// `Capitalize` formatiert den String korrekt:

// %%
Assert.Equal("Hello", utils.Capitalize("hELLO"));
Assert.StartsWith("H", utils.Capitalize("hELLO"));

// %% [markdown]
//
// `CountWords` zählt die Wörter korrekt:

// %%
Assert.Equal(2, utils.CountWords("Hello World"));
Assert.Equal(0, utils.CountWords(""));
Assert.Equal(2, utils.CountWords("  spaced  out  "));

// %% [markdown]
//
// `Capitalize` wirft bei `null` keine Exception:

// %%
Assert.Null(utils.Capitalize(null));
