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
var mockedList = new Mock<IList<string>>();

// %%
mockedList.Object.Add("Hello!");

// %%
mockedList.Verify(list => list.Add("Hello!"));

// %%
mockedList.Verify(list => list.Add("Hello!"));

// %%
// mockedList.Verify(list => list.Add("World!"));

// %% [markdown]
//
// - Überprüfen der Anzahl der Aufrufe:

// %%
var mockedListCount = new Mock<IList<string>>();

// %%
mockedListCount.Object.Add("Hello!");

// %%
mockedListCount.Verify(list => list.Add("Hello!"));

// %%
mockedListCount.Object.Add("Hello!");

// %%
mockedListCount.Verify(list => list.Add("Hello!"));

// %%
mockedListCount.Verify(list => list.Add("Hello!"), Times.Exactly(2));

// %% [markdown]
//
// - Überprüfen, dass eine Methode nicht aufgerufen wurde:

// %%
var mockedListNever = new Mock<IList<string>>();

// %%
mockedListNever.Verify(list => list.Clear(), Times.Never);

// %% [markdown]
//
// - Argument Matcher:
//   - `It.IsAny<T>()`, `It.IsAny<string>()`, ...
//   - `It.IsNull()`, `It.EndsWith()`, `It.Is<T>(...)`, ...

// %%
var mockedListMatchers = new Mock<IList<string>>();
mockedListMatchers.Object.Add("Hello!");

// %%
mockedListMatchers.Verify(m => m.Add(It.IsAny<string>()));

// %%
mockedListMatchers.Verify(m => m.Add(It.IsNotNull<string>()));

// %%
mockedListMatchers.Verify(m => m.Add(It.Is<string>(s => s.EndsWith("lo!"))));

// %%
// mockedListMatchers.Verify(m => m.Add(It.Is<string>(s => s.StartsWith("No"))));

// %%
var mockedListIsNull = new Mock<IList<string>>();
mockedListIsNull.Object.Add(null);

// %%
mockedListIsNull.Verify(m => m.Add(null));

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
mockedListLimits.Verify(m => m.Add("Once!"), Times.AtLeastOnce());

// %%
mockedListLimits.Verify(m => m.Add("Twice!"), Times.AtLeastOnce());

// %%
mockedListLimits.Verify(m => m.Add("Twice!"), Times.AtLeast(2));

// %%
mockedListLimits.Verify(m => m.Add("Three times!"), Times.AtMost(3));

// %% [markdown]
//
// ## Stubbing
//
// - Manchmal ist es notwendig, das Verhalten eines Mock-Objekts zu definieren
// - Mit `Setup()` und `Returns()` und `Throws()` kann das Verhalten festgelegt werden

// %%
var mockedListStub = new Mock<IList<string>>();

// %%
mockedListStub.Setup(m => m[0]).Returns("Hello!");

// %%
mockedListStub.Setup(m => m[1]).Throws(new System.Exception("No Value!"));

// %%
mockedListStub.Object[0]

// %%
// mockedListStub.Object[1];

// %%
mockedListStub.Verify(m => m[0]);

// %%
// mockedListStub.Verify(m => m[1]);

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
XunitTestRunner.RunTests(typeof(ListTest));

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
public class SpacecraftControlSystemTest {
    [Fact]
    public void TestNormalOperation() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(75.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(50.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Status: Temperature 75, Fuel 50"));
        radio.Verify(r => r.Transmit("Warning: High temperature!"), Times.Never);
        radio.Verify(r => r.Transmit("Warning: Low fuel!"), Times.Never);
    }

    [Fact]
    public void TestHighTemperatureWarning() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(110.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(50.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Warning: High temperature!"));
        radio.Verify(r => r.Transmit("Status: Temperature 110, Fuel 50"));
        radio.Verify(r => r.Transmit("Warning: Low fuel!"), Times.Never);
    }

    [Fact]
    public void TestLowFuelWarning() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(75.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(5.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Warning: Low fuel!"));
        radio.Verify(r => r.Transmit("Status: Temperature 75, Fuel 5"));
        radio.Verify(r => r.Transmit("Warning: High temperature!"), Times.Never);
    }

    [Fact]
    public void TestMultipleWarnings() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(110.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(5.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Warning: High temperature!"));
        radio.Verify(r => r.Transmit("Warning: Low fuel!"));
        radio.Verify(r => r.Transmit("Status: Temperature 110, Fuel 5"));
    }
}

// %%
XunitTestRunner.RunTests(typeof(SpacecraftControlSystemTest));

// %% [markdown]
// ## Bonusaufgabe: Fehlerbehandlung

// %%
public class SpacecraftControlSystemWithExceptionHandling {
    private readonly ITemperatureSensor _tempSensor;
    private readonly IFuelSensor _fuelSensor;
    private readonly IRadioTransmitter _radio;

    public SpacecraftControlSystemWithExceptionHandling(ITemperatureSensor tempSensor, IFuelSensor fuelSensor, IRadioTransmitter radio) {
        _tempSensor = tempSensor;
        _fuelSensor = fuelSensor;
        _radio = radio;
    }

    public void CheckAndReportStatus() {
        try {
            var temp = _tempSensor.GetTemperature();
            var fuel = _fuelSensor.GetFuelLevel();

            if (temp > 100) {
                _radio.Transmit("Warning: High temperature!");
            }

            if (fuel < 10) {
                _radio.Transmit("Warning: Low fuel!");
            }

            _radio.Transmit($"Status: Temperature {temp}, Fuel {fuel}");
        } catch (Exception e) {
            _radio.Transmit("Error: Sensor malfunction - " + e.Message);
        }
    }
}

// %%
public class SpacecraftControlSystemWithExceptionHandlingTest {
    [Fact]
    public void TestExceptionHandling() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Throws(new Exception("Temperature sensor failure"));
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(50.0);

        var spacecraft = new SpacecraftControlSystemWithExceptionHandling(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Error: Sensor malfunction - Temperature sensor failure"));
        radio.Verify(r => r.Transmit(It.Is<string>(s => s.StartsWith("Status:"))), Times.Never);
        radio.Verify(r => r.Transmit("Warning: High temperature!"), Times.Never);
    }

    [Fact]
    public void TestNormalOperationWithExceptionHandling() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(75.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(50.0);

        var spacecraft = new SpacecraftControlSystemWithExceptionHandling(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Status: Temperature 75, Fuel 50"));
        radio.Verify(r => r.Transmit("Warning: High temperature!"), Times.Never);
        radio.Verify(r => r.Transmit("Warning: Low fuel!"), Times.Never);
        radio.Verify(r => r.Transmit(It.Is<string>(s => s.StartsWith("Error:"))), Times.Never);
    }
}

// %%
XunitTestRunner.RunTests(typeof(SpacecraftControlSystemWithExceptionHandlingTest));

// %%
using Xunit;
using Moq;

// %%
public class SpacecraftControlSystemTest {
    [Fact]
    public void TestNormalOperation() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(75.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(50.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Status: Temperature 75, Fuel 50"));
        radio.Verify(r => r.Transmit("Warning: High temperature!"), Times.Never);
        radio.Verify(r => r.Transmit("Warning: Low fuel!"), Times.Never);
    }

    [Fact]
    public void TestHighTemperatureWarning() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(110.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(50.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Warning: High temperature!"));
        radio.Verify(r => r.Transmit("Status: Temperature 110, Fuel 50"));
        radio.Verify(r => r.Transmit("Warning: Low fuel!"), Times.Never);
    }

    [Fact]
    public void TestLowFuelWarning() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(75.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(5.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Warning: Low fuel!"));
        radio.Verify(r => r.Transmit("Status: Temperature 75, Fuel 5"));
        radio.Verify(r => r.Transmit("Warning: High temperature!"), Times.Never);
    }

    [Fact]
    public void TestMultipleWarnings() {
        var tempSensor = new Mock<ITemperatureSensor>();
        var fuelSensor = new Mock<IFuelSensor>();
        var radio = new Mock<IRadioTransmitter>();

        tempSensor.Setup(t => t.GetTemperature()).Returns(110.0);
        fuelSensor.Setup(f => f.GetFuelLevel()).Returns(5.0);

        var spacecraft = new SpacecraftControlSystem(tempSensor.Object, fuelSensor.Object, radio.Object);
        spacecraft.CheckAndReportStatus();

        radio.Verify(r => r.Transmit("Warning: High temperature!"));
        radio.Verify(r => r.Transmit("Warning: Low fuel!"));
        radio.Verify(r => r.Transmit("Status: Temperature 110, Fuel 5"));
    }
}

// %%
XunitTestRunner.RunTests(typeof(SpacecraftControlSystemTest));

// %%
