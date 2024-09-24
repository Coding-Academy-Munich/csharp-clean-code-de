// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>JUnit Parametrische Tests</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %%
static bool IsLeapYear(int year) {
    return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
}

// %%
#r "nuget: xunit, *"

// %%
#load "XunitTestRunner.cs"

// %%
using static XunitTestRunner;

// %%
using Xunit;

// %%
public class LeapYearTestsV1 {
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
RunTests<LeapYearTestsV1>();

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

    [Theory]
    [InlineData(2001)]
    [InlineData(2002)]
    [InlineData(2003)]
    public void YearsNotDivisibleBy4AreNotLeapYears(int year) {
        Assert.False(IsLeapYear(year));
    }

    [Fact]
    public void YearDivisibleBy100ButNot400IsNotLeapYear() {
        Assert.False(IsLeapYear(1900));
    }
}

// %%
RunTests<LeapYearTestsV2>();

// %%
public class LeapYearTestsV3 {
    [Theory]
    [InlineData(2001)]
    [InlineData(2002)]
    [InlineData(2003)]
    [InlineData(1900)]
    public void NonLeapYears(int year) {
        Assert.False(IsLeapYear(year));
    }

    [Theory]
    [InlineData(2004)]
    [InlineData(2000)]
    public void LeapYears(int year) {
        Assert.True(IsLeapYear(year));
    }
}

// %%
RunTests<LeapYearTestsV3>();

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
public class LeapYearTestsV4 {
    [Theory]
    [MemberData("NonLeapYears")]
    public void NonLeapYears(int year) {
        Assert.False(IsLeapYear(year));
    }

    public static IEnumerable<object[]> NonLeapYears() {
        return new[] {
            new object[] { 2001 },
            new object[] { 2002 },
            new object[] { 2003 },
            new object[] { 1900 }
        };
    }

    [Theory]
    [MemberData("LeapYears")]
    public void LeapYears(int year) {
        Assert.True(IsLeapYear(year));
    }

    public static IEnumerable<object[]> LeapYears() {
        return new[] {
            new object[] { 2004 },
            new object[] { 2000 }
        };
    }
}

// %%
RunTests<LeapYearTestsV4>();

// %%
public class LeapYearTestsV5 {
    [Theory]
    [MemberData("Years")]
    public void TestLeapYear(int year, bool expected) {
        Assert.Equal(expected, IsLeapYear(year));
    }

    public static IEnumerable<object[]> Years() {
        return new[] {
            new object[] { 2001, false },
            new object[] { 2002, false },
            new object[] { 2003, false },
            new object[] { 1900, false },
            new object[] { 2004, true },
            new object[] { 2000, true }
        };
    }
}

// %%
RunTests<LeapYearTestsV5>();

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
public class PrimeTests {
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(12)]
    [InlineData(14)]
    [InlineData(15)]
    [InlineData(16)]
    [InlineData(18)]
    [InlineData(20)]
    public void TestNonPrimes(int n) {
        Assert.False(IsPrime(n));
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(11)]
    [InlineData(13)]
    [InlineData(17)]
    [InlineData(19)]
    public void TestPrimes(int n) {
        Assert.True(IsPrime(n));
    }
}

// %%
static bool IsPalindrome(string s) {
    return s.Equals(new string(s.Reverse().ToArray()));
}

// %%
public class PalindromeTests {
    [Theory]
    [InlineData("")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aba")]
    [InlineData("abba")]
    [InlineData("abcba")]
    public void TestPalindromes(string s) {
        Assert.True(IsPalindrome(s));
    }

    [Theory]
    [InlineData("ab")]
    [InlineData("abc")]
    [InlineData("abcd")]
    [InlineData("abcde")]
    public void TestNonPalindromes(string s) {
        Assert.False(IsPalindrome(s));
    }
}

// %%
static bool ContainsDigit(int n, int digit) {
    return n.ToString().Contains(digit.ToString());
}

// %%
public class ContainsDigitTests {
    [Theory]
    [MemberData("Digits")]
    public void TestContainsDigit(int n, int digit, bool expected) {
        Assert.Equal(expected, ContainsDigit(n, digit));
    }

    public static IEnumerable<object[]> Digits() {
        return new[] {
            new object[] { 123, 1, true },
            new object[] { 123, 2, true },
            new object[] { 123, 3, true },
            new object[] { 123, 4, false },
            new object[] { 123, 5, false },
            new object[] { 123, 6, false }
        };
    }
}

// %%
static string SubstringFollowing(string s, string prefix) {
    int index = s.IndexOf(prefix);
    if (index == -1) {
        return s;
    }
    return s.Substring(index + prefix.Length);
}

// %%
public class SubstringFollowingTests {
    [Theory]
    [InlineData("Hello", "He", "llo")]
    [InlineData("Hello", "Hel", "lo")]
    [InlineData("Hello", "Hello", "")]
    [InlineData("Hello", "ello", "Hello")]
    [InlineData("Hello", "o", "Hello")]
    public void TestSubstringFollowing(string s, string prefix, string expected) {
        Assert.Equal(expected, SubstringFollowing(s, prefix));
    }
}
