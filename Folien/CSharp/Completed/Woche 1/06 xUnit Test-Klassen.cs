// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>xUnit: Test-Klassen</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Test-Klassen
//
// - Tests werden in Klassen organisiert
// - `[Fact]`-Attribut an Methoden um Tests zu definieren
// - Assertions wie oben besprochen

// %%
#r "nuget: xunit, *"

// %%
using Xunit;

// %%
public class XUnitBasicsTest
{
    [Fact]
    public void TestAddition()
    {
        Assert.Equal(4, 2 + 2);
    }

    [Fact]
    public void TestFailure()
    {
        Assert.Equal(1, 2);
    }
}

// %%
var tests = new XUnitBasicsTest();

// %%
tests.TestAddition();

// %%
// tests.TestFailure();

// %%
#load "XunitTestRunner.cs"

// %%
using static XunitTestRunner;

// %%
RunTests<XUnitBasicsTest>();

// %%
RunTests(typeof(XUnitBasicsTest));

// %% [markdown]
//
// ## Workshop: xUnit Basics im Notebook
//
// In diesem Workshop sollen Sie eine einfache Testklasse schreiben und die
// Tests mit xUnit ausführen.
//
// Hier ist der Code, den Sie testen sollen:

// %%
public class SimpleMath
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public int Multiply(int a, int b)
    {
        return a * b;
    }

    public int Divide(int a, int b)
    {
        return a / b;
    }
}

// %% [markdown]
//
// - Schreiben Sie Tests, die die Methoden der Klasse `SimpleMath` überprüfen.
// - Sie können dabei die folgende Klasse `SimpleMathTest` erweitern.

// %%
public class SimpleMathTest
{
    [Fact]
    public void TestAddition()
    {
        SimpleMath math = new SimpleMath();
        Assert.Equal(4, math.Add(2, 2));
    }

    [Fact]
    public void TestSubtraction()
    {
        SimpleMath math = new SimpleMath();
        Assert.Equal(0, math.Subtract(2, 2));
    }

    [Fact]
    public void TestMultiplication()
    {
        SimpleMath math = new SimpleMath();
        Assert.Equal(6, math.Multiply(2, 3));
    }

    [Fact]
    public void TestDivision()
    {
        SimpleMath math = new SimpleMath();
        Assert.Equal(2, math.Divide(6, 3));
    }

    [Fact]
    public void TestDivisionByZero()
    {
        SimpleMath math = new SimpleMath();
        Assert.Throws<DivideByZeroException>(() => math.Divide(1, 0));
    }
}

// %% [markdown]
//
// - Mit dem folgenden Code können Sie die Tests ausführen.

// %%
RunTests(typeof(SimpleMathTest));
