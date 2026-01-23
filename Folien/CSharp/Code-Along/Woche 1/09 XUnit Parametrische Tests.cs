// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>xUnit Parametrische Tests</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %%

// %%
#r "nuget: xunit, *"

// %%
#load "XunitTestRunner.cs"

// %%
using static XunitTestRunner;

// %%
using Xunit;

// %%

// %%

// %%
public class LeapYearTestsV2 {
    [Fact]
    public void YearDivisibleBy4ButNot100IsLeapYear() {
        Assert.True(IsLeapYear(2004));
    }

    [Fact]
    public void YearDivisibleBy400IsLeapYear() {
        Assert.True(IsLeapYear(2000));
    }

    [Fact]
    public void YearsNotDivisibleBy4AreNotLeapYears() {
        Assert.False(IsLeapYear(2001));
        Assert.False(IsLeapYear(2002));
        Assert.False(IsLeapYear(2003));
    }

    [Fact]
    public void YearDivisibleBy100ButNot400IsNotLeapYear() {
        Assert.False(IsLeapYear(1900));
    }
}

// %%

// %%

// %%

// %% [markdown]
//
// ## `MemberData` und `ClassData`
//
// - `[MemberData]` liefert Argumente mittels (statischer) Factory-Methode
//   - Methodenname wird als String übergeben
//   - Gibt `IEnumerable` von Argumenten zurück
// - `[ClassData]` liefert Argumente mittels (statischer) Factory-Klasse
//   - Klasse wird als Typ übergeben
//   - Klasse implementiert `IEnumerable<object[]>`

// %%
using System;
using System.Collections.Generic;

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ## Workshop: Parametrisierte Tests
//
// - Schreiben Sie parametrisierte Tests für die folgenden Funktionen.
// - Verwenden Sie dabei `InlineSource` und `MethodSource` jeweils
//   mindestens einmal.

// %%
static bool IsPrime(int n) {
    if (n <= 1) {
        return false;
    }
    for (int i = 2; i <= Math.Sqrt(n); i++) {
        if (n % i == 0) {
            return false;
        }
    }
    return true;
}

// %%

// %%
static bool IsPalindrome(string s) {
    return s.Equals(new string(s.Reverse().ToArray()));
}

// %%

// %%
static bool ContainsDigit(int n, int digit) {
    return n.ToString().Contains(digit.ToString());
}

// %%

// %%
static string SubstringFollowing(string s, string prefix) {
    int index = s.IndexOf(prefix);
    if (index == -1) {
        return s;
    }
    return s.Substring(index + prefix.Length);
}

// %%
