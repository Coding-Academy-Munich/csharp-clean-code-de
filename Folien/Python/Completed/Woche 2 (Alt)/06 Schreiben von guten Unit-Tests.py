// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Schreiben von guten Unit-Tests</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Mechanik von Unit-Tests
//
// Unit-Tests sollen
// - automatisiert sein: keine manuelle Interaktion
// - selbsttestend sein: Pass/Fail
// - feingranular sein
// - schnell sein
// - isoliert sein
// - zu jedem Zeitpunkt erfolgreich ausführbar sein

// %% [markdown]
//
// ## Einfache Struktur!
//
// <ul>
//   <li>Einfache, standardisierte Struktur<br>&nbsp;<br>
//     <table style="display:inline;margin:20px 20px;">
//     <tr><td style="text-align:left;width:60px;padding-left:15px;">Arrange</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid
//         black;">Given</td> <td
//         style="text-align:left;width:800px;padding-left:15px;border-left:1px solid
//         black;">
//           Bereite das Test-Environment vor</td></tr>
//     <tr><td style="text-align:left;padding-left:15px;">Act</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid
//         black;">
//            When</td>
//         <td style="text-align:left;width:800px;padding-left:15px;border-left:1px
//         solid black;">
//            Führe die getestete Aktion aus (falls vorhanden)</td></tr>
//     <tr><td style="text-align:left;padding-left:15px;">Assert</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid
//         black;">
//            Then</td>
//         <td style="text-align:left;width:800px;padding-left:15px;border-left:1px
//         solid black;">
//            Überprüfe die Ergebnisse</td></tr>
//     </table>
//     <br>&nbsp;
//   </li>
//   <li>Wenig Code
//     <ul>
//       <li>Wenig Boilerplate</li>
//       <li>Factories, etc. für Tests</li>
//     </ul>
//   </li>
// </ul>

// %%
#r "nuget: xunit, *"

// %%
#load "XunitTestRunner.cs"

// %%
using Xunit;
using static XunitTestRunner;

// %%
using System.Collections.Generic;

// %%
void TestInsert() {
    // Arrange
    List<int> x = new List<int> { 1, 2, 3 };
    List<int> y = new List<int> { 10, 20 };

    // Act
    x.AddRange(y);

    // Assert
    Assert.Equal(x, new List<int> { 1, 2, 3, 10, 20 });
}

// %%
TestInsert();

// %% [markdown]
//
// - Wie viele Tests wollen wir haben?
// - Wie viele Werte wollen wir überprüfen?

// %% [markdown]
//
// ## Versuch: Erschöpfendes Testen
//
// - Wir schreiben erschöpfende Tests, d.h. Tests, die alle möglichen Eingaben eines
//   Programms abdecken

// %% [markdown]
//
// - Erschöpfendes Testen ist nicht möglich
// - Beispiel Passworteingabe:
//   - Angenommen, Passwörter mit maximal 20 Zeichen sind zulässig,
//     80 Eingabezeichen sind erlaubt (große und kleine Buchstaben, Sonderzeichen)
//   - Das ergibt $80^{20}$ = 115.292.150.460.684.697.600.000.000.000.000.000.000
//     mögliche Eingaben
//   - Bei 10ns für einen Test würde man ca. $10^{24}$ Jahre brauchen, um alle Eingaben
//     zu testen
//   - Das Universum ist ungefähr $1.4 \times 10^{10}$ Jahre alt

// %% [markdown]
//
// ## Effektivität und Effizienz von Tests
//
// - Unit-Tests sollen effektiv und effizient sein
//   - Effektiv: Die Tests sollen so viele Fehler wie möglich finden
//   - Effizient: Wir wollen die größte Anzahl an Fehlern mit der geringsten Anzahl
//     an möglichst einfachen Tests finden
// - Effizienz ist wichtig, da Tests selbst Code sind, der gewartet werden muss und
//   Fehler enthalten kann

// %% [markdown]
//
// ## Wie schreibt man gute Unit-Tests?
//
// - Teste beobachtbares Verhalten, nicht Implementierung
// - Bevorzuge Tests von Werten gegenüber Tests von Zuständen
// - Bevorzuge Tests von Zuständen gegenüber Tests von Interaktion
// - Verwende Test-Doubles dann (aber auch nur dann), wenn eine Abhängigkeit
//   "eine Rakete abfeuert"
// - (Diese Regeln setzen voraus, dass der Code solche Tests erlaubt)

// %% [markdown]
//
// ## Warum Tests von beobachtbarem Verhalten, nicht Implementierung?
//
// Beobachtbares Verhalten
// - ist leichter zu verstehen
// - ist stabiler als Implementierung
// - entspricht eher dem Kundennutzen

// %% [markdown]
//
// ## Teste beobachtbares Verhalten, nicht Implementierung
//
// - Abstrahiere so weit wie möglich von Implementierungsdetails
//   - Auch auf Unit-Test Ebene
// - Oft testen sich verschiedene Methoden gegenseitig
// - Dies erfordert manchmal die Einführung von zusätzlichen Methoden
//     - Diese Methoden sollen für Anwender sinnvoll sein, nicht nur für Tests
//     - Oft "abstrakter Zustand" von Objekten
//     - **Nicht:** konkreten Zustand öffentlich machen

// %%
using System.Collections.Generic;

// %%
public class Stack<T> {
    private List<T> items = new List<T>();

    public void Push(T item) {
        items.Add(item);
    }

    public T Pop() {
        if (items.Count == 0) {
            throw new System.InvalidOperationException("Stack is empty");
        }
        T item = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return item;
    }

    // Good extension: useful, doesn't expose implementation
    public int Size {
        get {
            return items.Count;
        }
    }

    // Bad extension: exposes implementation
    public List<T> Items {
        get {
            return items;
        }
    }
}

// %% [markdown]
//
// ### Tests, wenn nur `Push()` und `Pop()` verfügbar sind

// %%
public static void TestStack1() {
    Stack<int> s = new Stack<int>();
    s.Push(5);
    Assert.Equal(5, s.Pop());
}

// %%
TestStack1();

// %%
public static void TestStack2() {
    Stack<int> s = new Stack<int>();
    s.Push(5);
    s.Push(10);
    Assert.Equal(10, s.Pop());
    Assert.Equal(5, s.Pop());
}

// %%
TestStack2();

// %%
public static void TestStack3() {
    Stack<int> s = new Stack<int>();
    Assert.Throws<InvalidOperationException>(() => s.Pop());
}

// %%
TestStack3();

// %% [markdown]
//
// ### Tests, wenn `Size()` verfügbar ist

// %%
public static void TestStackWithSize1() {
    Stack<int> s = new Stack<int>();
    Assert.Equal(0, s.Size);
}

// %%
TestStackWithSize1();

// %%
public static void TestStackWithSize2() {
    Stack<int> s = new Stack<int>();
    s.Push(5);
    s.Push(10);
    Assert.Equal(2, s.Size);
}

// %%
TestStackWithSize2();

// %% [markdown]
//
// ### Tests, wenn `Items` verfügbar ist


// %%
public static void TestStackWithItems() {
    Stack<int> s = new Stack<int>();
    s.Push(5);
    s.Push(10);
    Assert.Equal([5, 10], s.Items);
}

// %%
TestStackWithItems();

// %% [markdown]
//
// ## Werte > Zustand > Interaktion
//
// - Verständlicher
// - Leichter zu testen
// - Oft stabiler gegenüber Refactorings
//
// Ausnahme: Testen von Protokollen

// %% [markdown]
//
// ### Funktionen/Werte

// %%
public static int Add(int x, int y)
{
    return x + y;
}

// %%
Assert.Equal(5, Add(2, 3));

// %% [markdown]
//
// ### Zustand

// %%
public class Adder {
    public int X { get; set; }
    public int Y { get; set; }
    public int Result { get => result; }
    public void Add() => result = X + Y;

    private int result;
}

// %%
void TestAdder() {
    Adder adder = new Adder() { X = 2, Y = 3 };

    adder.Add();

    Assert.Equal(5, adder.Result);
}

// %%
TestAdder();

// %% [markdown]
//
// ### Seiteneffekt/Interaktion
//
// - Mit Interface

// %%
public interface IAbstractAdder {
    void Add(int x, int y);
}

// %%
public class InteractionAdder {
    private readonly IAbstractAdder adder;

    public InteractionAdder(IAbstractAdder adder) {
        this.adder = adder;
    }

    public void Add(int x, int y) {
        adder.Add(x, y);
    }
}

// %% [markdown]
//
// Test benötigt Mock/Spy

// %%
public class AdderSpy : IAbstractAdder {
    public List<(int, int)> Calls { get; } = [];

    public void Add(int x, int y) {
        Calls.Add((x, y));
    }
}

// %%
public static void TestAdderWithSpy() {
    AdderSpy spy = new AdderSpy();
    InteractionAdder adder = new InteractionAdder(spy);
    adder.Add(2, 3);

    Assert.Equal(1, spy.Calls.Count);
    Assert.Equal(2, spy.Calls[0].Item1);
    Assert.Equal(3, spy.Calls[0].Item2);
}

// %%
TestAdderWithSpy();

// %% [markdown]
//
// ### Seiteneffekt/Interaktion
//
// - Ohne Interface

// %%
public class SideEffectAdder
{
    public static void AddAndPrint(int x, int y)
    {
        Console.WriteLine("Result: " + (x + y));
    }
}

// %%
SideEffectAdder.AddAndPrint(1, 2);

// %%
using System.IO;

// %%
string AddAndCaptureOutput(int x, int y)
{
    // Save the original Console output stream
    var originalOut = Console.Out;

    using (var writer = new StringWriter())
    {
        // Redirect the output to the writer
        Console.SetOut(writer);

        // Call the method
        SideEffectAdder.AddAndPrint(x, y);

        // Restore the original output and return the result
        Console.SetOut(originalOut);
        return writer.ToString().Trim();
    }
}

// %%
AddAndCaptureOutput(2, 3);

// %%
Assert.Equal("Result: 5", AddAndCaptureOutput(2, 3));

// %% [markdown]
//
// ## Wie schreibt man testbaren Code?
//
// - Umwandeln von weniger testbarem in besser testbaren Stil
//   - Beobachtbarkeit (`code/completed/observable-state-machine`)
//   - Keine globalen oder statischen Daten
//   - Unveränderliche Datenstrukturen (Werte)
// - Gutes objektorientiertes Design
//   - Hohe Kohäsion
//   - Geringe Kopplung, Management von Abhängigkeiten
// - Etc.

// %% [markdown]
//
// ## Prozess
//
// - Iteratives Vorgehen
//   - Kleine Schritte mit Tests
// - Test-Driven Development (TDD)
//   - Schreiben von Tests vor Code

// %% [markdown]
//
// ## Mini-Workshop: Bessere Testbarkeit
//
// - Wie können Sie Tests für die folgenden Funktionen/Klassen schreiben?
// - Wie können Sie die folgenden Funktionen/Klassen verbessern, um sie besser
//   testbar zu machen?
// - Was für Nachteile ergeben sich dadurch?

// %%
using System.Collections.Generic;

// %%
public class Counter {
    private static int c = 0;
    public static int Count() {
        return c++;
    }
}

// %%
for (int i = 0; i < 3; ++i) {
    System.Console.WriteLine(Counter.Count());
}

// %%
public class Counter2 {
    private int c = 0;
    public int Invoke() => c++;
}

// %%
Counter2 counter = new Counter2();

// %%
for (int i = 0; i < 3; ++i) {
    System.Console.WriteLine(counter.Invoke());
}

// %%
public enum State {
    OFF,
    ON
}

// %%
public class Switch {
    private State state = State.OFF;

    public void Toggle() {
        state = state == State.OFF ? State.ON : State.OFF;
        System.Console.WriteLine("Switch is " + (state == State.OFF ? "OFF" : "ON"));
    }
}

// %%
Switch s = new Switch();

// %%
for (int i = 0; i < 3; ++i) {
    s.Toggle();
}

// %%
public class SwitchWithGetter {
    private State state = State.OFF;

    public void Toggle() {
        state = state == State.OFF ? State.ON : State.OFF;
        System.Console.WriteLine("Switch is " + (state == State.OFF ? "OFF" : "ON"));
    }

    public State GetState() => state;
}

// %%
SwitchWithGetter sg = new SwitchWithGetter();

// %%
for (int i = 0; i < 3; ++i) {
    sg.Toggle();
}

// %%
System.Console.WriteLine("Switch is " + (sg.GetState() == State.OFF ? "OFF" : "ON"));

// %%
using System;

public class ObservableSwitch {
    private State state = State.OFF;
    private List<Action<State>> observers = new List<Action<State>>();

    public void Toggle() {
        state = state == State.OFF ? State.ON : State.OFF;
        Notify(state);
    }

    public void RegisterObserver(Action<State> f) => observers.Add(f);

    private void Notify(State s) {
        foreach (var f in observers) {
            f(s);
        }
    }
}

// %%
ObservableSwitch os = new ObservableSwitch();

// %%
os.RegisterObserver(s =>
    System.Console.WriteLine("Switch is " + (s == State.OFF ? "OFF" : "ON")));

// %%
for (int i = 0; i < 3; ++i) {
    os.Toggle();
}

// %%
public static void PrintFib(int n) {
    int a = 0;
    int b = 1;
    for (int i = 0; i < n; ++i) {
        System.Console.WriteLine("fib(" + i + ") = " + b);
        int tmp = a;
        a = b;
        b = tmp + b;
    }
}

// %%
PrintFib(5);

// %%
public static int Fib1(int n) {
    int a = 0;
    int b = 1;
    for (int i = 0; i < n; ++i) {
        int tmp = a;
        a = b;
        b = tmp + b;
    }
    return b;
}

// %%
public static void PrintFib1(int n) {
    for (int i = 0; i < n; ++i) {
        System.Console.WriteLine("fib(" + i + ") = " + Fib1(i));
    }
}

// %%
PrintFib1(5);

// %%
using System;

public static void FibGen(int n, Action<int, int> f) {
    int a = 0;
    int b = 1;
    for (int i = 0; i < n; ++i) {
        f(i, b);
        int tmp = a;
        a = b;
        b = tmp + b;
    }
}

// %%
public static void PrintFib2(int n) {
    FibGen(n, (i, x) =>
        System.Console.WriteLine("fib(" + i + ") = " + x));
}

// %%
PrintFib2(5);
