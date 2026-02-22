// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Fluent Assertions</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Was ist FluentAssertions?
//
// - Bibliothek für lesbare Test-Assertions in C#
// - Verwendet eine fluente API mit `Should()`-Syntax
// - Bessere Fehlermeldungen als Standard-xUnit-Assertions
// - Unterstützt Strings, Zahlen, Collections, Exceptions, ...
// - NuGet-Paket: `FluentAssertions`

// %%
#r "nuget: xunit, 2.4.1"
#r "nuget: FluentAssertions, 6.12.0"

// %%
using Xunit;
using FluentAssertions;

// %%
#load "XunitTestRunner.cs"

// %% [markdown]
//
// ## Vergleich: xUnit vs. FluentAssertions

// %% [markdown]
//
// ### xUnit-Style

// %%
Assert.Equal(42, 42);
Assert.NotEqual(42, 0);
Assert.True(10 > 5);

// %% [markdown]
//
// ### FluentAssertions-Style

// %%
42.Should().Be(42);
42.Should().NotBe(0);
(10 > 5).Should().BeTrue();

// %% [markdown]
//
// ## Numerische Assertions
//
// - `Be()`, `NotBe()` — Gleichheit
// - `BeGreaterThan()`, `BeLessThan()` — Vergleiche
// - `BeInRange()` — Wertebereich
// - `BeApproximately()` — Gleitkommazahlen

// %%
int value = 42;

value.Should().BeGreaterThan(0);
value.Should().BeLessThanOrEqualTo(100);
value.Should().BeInRange(1, 50);

// %%
double pi = 3.14159;

pi.Should().BeApproximately(3.14, 0.01);

// %% [markdown]
//
// ## String-Assertions
//
// - `Be()`, `NotBe()` — Gleichheit
// - `Contain()`, `StartWith()`, `EndWith()` — Teilstrings
// - `BeEmpty()`, `NotBeNullOrEmpty()` — Leer/Null
// - `HaveLength()`, `MatchRegex()` — Länge und Pattern

// %%
string greeting = "Hello, World!";

greeting.Should().StartWith("Hello");
greeting.Should().EndWith("!");
greeting.Should().Contain("World");
greeting.Should().HaveLength(13);

// %%
string name = "Alice";

name.Should().NotBeNullOrEmpty();
name.Should().NotBe("Bob");

// %% [markdown]
//
// ## Collection-Assertions
//
// - `HaveCount()` — Anzahl der Elemente
// - `Contain()`, `NotContain()` — Elemente prüfen
// - `BeInAscendingOrder()` — Sortierung
// - `OnlyContain()` — Bedingung für alle Elemente
// - `BeEquivalentTo()` — gleiche Elemente, beliebige Reihenfolge

// %%
using System.Collections.Generic;

// %%
List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

numbers.Should().HaveCount(5);
numbers.Should().Contain(3);
numbers.Should().NotContain(42);
numbers.Should().BeInAscendingOrder();
numbers.Should().OnlyContain(n => n > 0);

// %%
List<string> fruits = new List<string> { "Apple", "Banana", "Cherry" };

fruits.Should().HaveCount(3);
fruits.Should().Contain("Banana");
fruits.Should().BeEquivalentTo(new[] { "Cherry", "Apple", "Banana" });

// %% [markdown]
//
// ## Exception-Assertions
//
// - `action.Should().Throw<T>()` — Exception erwartet
// - `action.Should().NotThrow()` — keine Exception erwartet
// - `.WithMessage()` — Fehlermeldung prüfen

// %%
using System;

// %%
Action act = () => throw new InvalidOperationException("Something went wrong");

act.Should().Throw<InvalidOperationException>()
   .WithMessage("*went wrong*");

// %%
Action safeAct = () => { int x = 1 + 1; };

safeAct.Should().NotThrow();

// %% [markdown]
//
// ## Fehlermeldungen
//
// Ein großer Vorteil von FluentAssertions: bessere Fehlermeldungen

// %% [markdown]
//
// **xUnit:**
// ```
// Assert.Equal() Failure
// Expected: 5
// Actual:   3
// ```
//
// **FluentAssertions:**
// ```
// Expected numbers to contain 5 items, but found 3.
// ```

// %% [markdown]
//
// ## Test-Beispiel

// %%
public class FluentAssertionsBasicsTest
{
    [Fact]
    public void TestStringOperations()
    {
        string result = "Hello" + " " + "World";
        result.Should().Be("Hello World");
        result.Should().StartWith("Hello");
        result.Should().HaveLength(11);
    }

    [Fact]
    public void TestCollectionOperations()
    {
        var numbers = new List<int> { 3, 1, 4, 1, 5 };
        numbers.Should().HaveCount(5);
        numbers.Should().Contain(4);
        numbers.Should().ContainInOrder(new[] { 3, 4, 5 });
    }

    [Fact]
    public void TestNumericComparisons()
    {
        double result = 0.1 + 0.2;
        result.Should().BeApproximately(0.3, 0.001);
    }
}

// %%
XunitTestRunner.RunTests(typeof(FluentAssertionsBasicsTest));

// %% [markdown]
//
// ## Workshop: Fluent Assertions
//
// Schreiben Sie die folgenden xUnit-Assertions als FluentAssertions um:

// %%
public class XunitStyleTests
{
    [Fact]
    public void TestEquality()
    {
        Assert.Equal(10, 5 + 5);
    }

    [Fact]
    public void TestStringContains()
    {
        string text = "The quick brown fox";
        Assert.Contains("brown", text);
    }

    [Fact]
    public void TestListCount()
    {
        var items = new List<string> { "A", "B", "C" };
        Assert.Equal(3, items.Count);
        Assert.Contains("B", items);
    }

    [Fact]
    public void TestException()
    {
        Assert.Throws<ArgumentNullException>(() => {
            string s = null;
            if (s == null) throw new ArgumentNullException(nameof(s));
        });
    }
}

// %%
XunitTestRunner.RunTests(typeof(XunitStyleTests));

// %%

// %%
XunitTestRunner.RunTests(typeof(FluentStyleTests));
