// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Mocking mit Moq</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Das Moq Mocking Framework (für C#)
//
// - [Moq](https://github.com/moq/moq4) ist ein Mocking-Framework für C#
// - Ermöglicht das Erstellen von Mock-Objekten

// %%
#r "nuget: Moq, 4.17"
#r "nuget: xunit, *"

// %%
#load "XunitTestRunner.cs"

// %% [markdown]
//
// ## Beispiel: Mocken einer Liste
//
// - Erstellen eines Mock-Objekts für eine Liste
// - Implementiert alle Methoden des `IList`-Interfaces
// - Kann verwendet werden, um Methodenaufrufe zu überprüfen

// %%
using Moq;
using System.Collections.Generic;

// %%

// %%

// %%
mockedList.Verify(list => list.Add("Hello!"));

// %%

// %%

// %% [markdown]
//
// - Überprüfen der Anzahl der Aufrufe:

// %%
var mockedListCount = new Mock<IList<string>>();

// %%

// %%

// %%

// %%

// %%

// %% [markdown]
//
// - Überprüfen, dass eine Methode nicht aufgerufen wurde:

// %%
var mockedListNever = new Mock<IList<string>>();

// %%

// %% [markdown]
//
// - Argument Matcher:
//   - `It.IsAny<T>()`, `It.IsAny<string>()`, ...
//   - `It.IsNull()`, `It.EndsWith()`, `It.Is<T>(...)`, ...

// %%
var mockedListMatchers = new Mock<IList<string>>();
mockedListMatchers.Object.Add("Hello!");

// %%

// %%

// %%

// %%

// %%
var mockedListIsNull = new Mock<IList<string>>();
mockedListIsNull.Object.Add(null);

// %%

// %% [markdown]
//
// - Mindest- und Maximalanzahl von Aufrufen:

// %%
var mockedListLimits = new Mock<IList<string>>();

// %%
mockedListLimits.Object.Add("Once!");
mockedListLimits.Object.Add("Twice!");
mockedListLimits.Object.Add("Twice!");
mockedListLimits.Object.Add("Three times!");
mockedListLimits.Object.Add("Three times!");
mockedListLimits.Object.Add("Three times!");

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ## Stubbing
//
// - Manchmal ist es notwendig, das Verhalten eines Mock-Objekts zu definieren
// - Mit `Setup()` und `Returns()` und `Throws()` kann das Verhalten festgelegt werden

// %%
var mockedListStub = new Mock<IList<string>>();

// %%

// %%

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ## Verwendung von Moq in Tests
//
// - In xUnit-Tests können Mock-Objekte verwendet werden, um Abhängigkeiten zu simulieren
// - Die `Verify()`-Methode wirft eine Exception, wenn sie einen Fehler findet
// - Das führt zu einem fehlgeschlagenen Test

// %%
using Xunit;
using Moq;

public class ListTest {
    [Fact]
    public void TestList() {
        var mockedList = new Mock<IList<string>>();

        mockedList.Object.Add("Hello!");

        mockedList.Verify(m => m.Add("Hello!"));
    }

    [Fact]
    public void FailedTest() {
        var mockedList = new Mock<IList<string>>();

        mockedList.Object.Add("Hello!");

        mockedList.Verify(m => m.Add("World!")); // This will fail
    }
}

// %%

// %% [markdown]
//
// ## Workshop: Test eines Steuerungssystems für ein Raumschiff
//
// In diesem Workshop arbeiten Sie mit einem einfachen Steuerungssystem für ein
// Raumschiff. Das System interagiert mit verschiedenen Sensoren und einem
// Funksender. Ihre Aufgabe ist es, Tests für dieses System mit Moq zu
// schreiben.
//
// Hier ist eine einfache Implementierung unseres Raumschiff-Steuerungssystems:

// %%
public interface ITemperatureSensor {
    double GetTemperature();
}

// %%
public interface IFuelSensor {
    double GetFuelLevel();
}

// %%
public interface IRadioTransmitter {
    void Transmit(string message);
}

// %%
public class SpacecraftControlSystem {
    private readonly ITemperatureSensor _tempSensor;
    private readonly IFuelSensor _fuelSensor;
    private readonly IRadioTransmitter _radio;

    public SpacecraftControlSystem(ITemperatureSensor tempSensor, IFuelSensor fuelSensor, IRadioTransmitter radio) {
        _tempSensor = tempSensor;
        _fuelSensor = fuelSensor;
        _radio = radio;
    }

    public void CheckAndReportStatus() {
        var temp = _tempSensor.GetTemperature();
        var fuel = _fuelSensor.GetFuelLevel();

        if (temp > 100) {
            _radio.Transmit("Warning: High temperature!");
        }

        if (fuel < 10) {
            _radio.Transmit("Warning: Low fuel!");
        }

        _radio.Transmit($"Status: Temperature {temp}, Fuel {fuel}");
    }
}

// %% [markdown]
//
// ## Beispiel
//
// So könnten Sie dieses System verwenden:

// %%
public class RealTemperatureSensor : ITemperatureSensor {
    public double GetTemperature() {
        return 75.0; // Simulated temperature reading
    }
}

// %%
public class RealFuelSensor : IFuelSensor {
    public double GetFuelLevel() {
        return 50.0; // Simulated fuel level
    }
}

// %%
public class RealRadioTransmitter : IRadioTransmitter {
    public void Transmit(string message) {
        Console.WriteLine("Transmitting: " + message);
    }
}

// %%
ITemperatureSensor realTempSensor = new RealTemperatureSensor();
IFuelSensor realFuelSensor = new RealFuelSensor();
IRadioTransmitter realRadio = new RealRadioTransmitter();

// %%
SpacecraftControlSystem spacecraft = new SpacecraftControlSystem(
                                            realTempSensor, realFuelSensor, realRadio);

// %%
spacecraft.CheckAndReportStatus();

// %% [markdown]
//
// Ihre Aufgabe ist es, Tests für das `SpacecraftControlSystem` unter Verwendung
// von Moq zu schreiben. Implementieren Sie die folgenden Testfälle:
//
// 1. Testen des normalen Betriebs:
//    - Überprüfen Sie den normalen Betrieb des Raumschiffs, wenn die Temperatur
//      normal und der Kraftstoffstand ausreichend ist.
// 2. Testen der Warnung bei hoher Temperatur:
//    - Überprüfen Sie, dass das Raumschiff eine Warnung bei einer Temperatur
//      über 100 Grad überträgt.
// 3. Testen der Warnung bei niedrigem Kraftstoffstand:
//    - Überprüfen Sie, dass das Raumschiff eine Warnung bei einem Kraftstoffstand
//      unter 10 überträgt.
// 4. Testen mehrerer Warnungen:
//    - Überprüfen Sie, dass das Raumschiff sowohl eine Warnung bei hoher Temperatur
//      als auch bei niedrigem Kraftstoffstand überträgt, wenn beide Bedingungen
//      erfüllt sind.

// %% [markdown]
//
// #### Bonusaufgabe:
//
// 5. Testen der Fehlerbehandlung:
//    - Ändern Sie das `SpacecraftControlSystem`, um Ausnahmen von den Sensoren
//      zu behandeln, und schreiben Sie einen Test, um dieses Verhalten zu überprüfen.

// %%
using Xunit;
using Moq;

// %%

// %%

// %%
using Xunit;
using Moq;

// %%

// %%

// %%
