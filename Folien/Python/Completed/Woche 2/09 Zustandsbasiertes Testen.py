// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Zustandsbasiertes Testen</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Zustandsbasiertes Testen
//
// Kann man das Verhalten eines Objekts durch ein Zustandsdiagramm beschreiben,
// so kann man sich beim Testen an den Zuständen und Transitionen orientieren
//
// - Ein zustandsbasierter Test wird durch eine Folge von Events beschrieben,
//   die die Zustandsmaschine steuern
// - Die erwarteten Ergebnisse sind
//   - die Zustände (falls beobachtbar) und
//   - die Aktivitäten bzw. Ausgaben, die durch die Eingabe-Events verursacht
//     werden
// - Es gibt verschiedene Methoden, um fehlerhafte Aktivitäten bzw. Ausgaben und
//   falsche Zustandsübergänge zu finden (z.B. Transition Tour, Distinguishing
//   Sequence)

// %% [markdown]
//
// ## Beispiel: Ampel
//
// <img src="img/traffic-light.png"
//      alt="Zustandsmaschine für eine Ampel"
//      style="width: 50%; margin-left: auto; margin-right: auto;"/>

// %%
public class TrafficLight
{
    private enum State { Red, Yellow, Green }
    private State currentState = State.Red;

    public void Change()
    {
        switch (currentState)
        {
            case State.Red:
                currentState = State.Green;
                break;
            case State.Yellow:
                currentState = State.Red;
                break;
            case State.Green:
                currentState = State.Yellow;
                break;
        }
    }

    public string GetState()
    {
        return currentState.ToString();
    }
}

// %%
#r "nuget: xunit, *"

// %%
using Xunit;

// %%
#load "XunitTestRunner.cs"

// %%
using static XunitTestRunner;


// %%
public class TrafficLightTest
{
    [Fact]
    public void TestTrafficLightStateTransitions()
    {
        var light = new TrafficLight();

        Assert.Equal("Red", light.GetState());

        light.Change();
        Assert.Equal("Green", light.GetState());

        light.Change();
        Assert.Equal("Yellow", light.GetState());

        light.Change();
        Assert.Equal("Red", light.GetState());
    }
}

// %%
RunTests<TrafficLightTest>();

// %%
public class TrafficLightTest2
{
    private TrafficLight light;

    [Fact]
    public void TestInitialState()
    {
        // Arrange
        light = new TrafficLight();

        // Act
        // No action needed for initial state

        // Assert
        Assert.Equal("Red", light.GetState());
    }

    [Fact]
    public void TestRedToGreen()
    {
        // Arrange
        light = new TrafficLight();
        Assert.Equal("Red", light.GetState());

        // Act
        light.Change();

        // Assert
        Assert.Equal("Green", light.GetState());
    }

    [Fact]
    public void TestGreenToYellow()
    {
        // Arrange
        light = new TrafficLight();
        light.Change();
        Assert.Equal("Green", light.GetState());

        // Act
        light.Change();

        // Assert
        Assert.Equal("Yellow", light.GetState());
    }

    [Fact]
    public void TestYellowToRed()
    {
        // Arrange
        light = new TrafficLight();
        light.Change();
        light.Change();
        Assert.Equal("Yellow", light.GetState());

        // Act
        light.Change();

        // Assert
        Assert.Equal("Red", light.GetState());
    }
}

// %%
RunTests<TrafficLightTest2>();

// %%
using System.Linq;
using System.Collections.Generic;

// %%
public class TrafficLightTest3
{
    private TrafficLight light;

    [Theory]
    [MemberData(nameof(GetRedStateTestCases))]
    public void TransitionsFromRedState(string description, Action<TrafficLight> action, string expectedState)
    {
        // Arrange
        light = new TrafficLight();
        Assert.Equal("Red", light.GetState());

        // Act
        action(light);

        // Assert
        Assert.Equal(expectedState, light.GetState());
    }

    [Theory]
    [MemberData(nameof(GetGreenStateTestCases))]
    public void TransitionsFromGreenState(string description, Action<TrafficLight> action, string expectedState)
    {
        // Arrange
        light = new TrafficLight();
        light.Change();
        Assert.Equal("Green", light.GetState());

        // Act
        action(light);

        // Assert
        Assert.Equal(expectedState, light.GetState());
    }

    [Theory]
    [MemberData(nameof(GetYellowStateTestCases))]
    public void TransitionsFromYellowState(string description, Action<TrafficLight> action, string expectedState)
    {
        // Arrange
        light = new TrafficLight();
        light.Change();
        light.Change();
        Assert.Equal("Yellow", light.GetState());

        // Act
        action(light);

        // Assert
        Assert.Equal(expectedState, light.GetState());
    }

    public static IEnumerable<object[]> GetRedStateTestCases()
    {
        yield return new object[] { "RED -- change --> GREEN", (Action<TrafficLight>)(tl => tl.Change()), "Green" };
    }

    public static IEnumerable<object[]> GetGreenStateTestCases()
    {
        yield return new object[] { "GREEN -- change --> YELLOW", (Action<TrafficLight>)(tl => tl.Change()), "Yellow" };
    }

    public static IEnumerable<object[]> GetYellowStateTestCases()
    {
        yield return new object[] { "YELLOW -- change --> RED", (Action<TrafficLight>)(tl => tl.Change()), "Red" };
    }
}

// %%
XunitTestRunner.RunTests(typeof(TrafficLightTest3));

// %% [markdown]
//
// ## Workshop: Zustandsbasiertes Testen eines Kaffeeautomaten
//
// Implementieren Sie eine `CoffeeMachine` Klasse mit folgenden Eigenschaften:
//
// - Zustände: `OFF`, `READY`, `BREWING`, `MAINTENANCE`
// - Methoden:
//   - `TurnOn()`: Schaltet die Maschine ein (von `OFF` zu `READY`)
//   - `TurnOff()`: Schaltet die Maschine aus (von jedem Zustand zu `OFF`)
//   - `Brew()`: Startet den Brühvorgang (von `READY` zu `BREWING,` dann
//     automatisch zurück zu `READY`)
//   - `StartMaintenance()`: Startet den Wartungsmodus (von `READY` zu
//     `MAINTENANCE`)
//   - `FinishMaintenance()`: Beendet den Wartungsmodus (von `MAINTENANCE` zu
//     `READY`)
//   - `GetState()`: Gibt den aktuellen Zustand zurück
//
// Schreiben Sie Tests, die alle möglichen Zustandsübergänge abdecken und
// überprüfen Sie, ob "unerlaubte" Übergänge korrekt behandelt werden, d.h. dass
// Aktionen den Zustand der Zustandsmaschine nicht ändern, wenn sie in Zuständen
// ausgeführt werden, für die keine Transition angegeben ist.

// %%
public class CoffeeMachine
{
    public enum State { OFF, READY, BREWING, MAINTENANCE }

    private State currentState;

    public CoffeeMachine()
    {
        currentState = State.OFF;
    }

    public void TurnOn()
    {
        if (currentState == State.OFF)
        {
            currentState = State.READY;
        }
    }

    public void TurnOff()
    {
        currentState = State.OFF;
    }

    public void Brew()
    {
        if (currentState == State.READY)
        {
            currentState = State.BREWING;
            // Simulate brewing process
            currentState = State.READY;
        }
    }

    public void StartMaintenance()
    {
        if (currentState == State.READY)
        {
            currentState = State.MAINTENANCE;
        }
    }

    public void FinishMaintenance()
    {
        if (currentState == State.MAINTENANCE)
        {
            currentState = State.READY;
        }
    }

    public State GetState()
    {
        return currentState;
    }
}

// %%
public class CoffeeMachineTest
{
    private CoffeeMachine machine;

    [Fact]
    public void TestInitialState()
    {
        machine = new CoffeeMachine();
        Assert.Equal(CoffeeMachine.State.OFF, machine.GetState());
    }

    [Theory]
    [MemberData(nameof(GetOffStateTestCases))]
    public void TransitionsFromOff(string description, Action<CoffeeMachine> action, CoffeeMachine.State expectedState)
    {
        machine = new CoffeeMachine();
        Assert.Equal(CoffeeMachine.State.OFF, machine.GetState());

        action(machine);
        Assert.Equal(expectedState, machine.GetState());
    }

    [Theory]
    [MemberData(nameof(GetReadyStateTestCases))]
    public void TransitionsFromReady(string description, Action<CoffeeMachine> action, CoffeeMachine.State expectedState)
    {
        machine = new CoffeeMachine();
        machine.TurnOn();
        Assert.Equal(CoffeeMachine.State.READY, machine.GetState());

        action(machine);
        Assert.Equal(expectedState, machine.GetState());
    }

    [Theory]
    [MemberData(nameof(GetMaintenanceStateTestCases))]
    public void TransitionsFromMaintenance(string description, Action<CoffeeMachine> action, CoffeeMachine.State expectedState)
    {
        machine = new CoffeeMachine();
        machine.TurnOn();
        machine.StartMaintenance();
        Assert.Equal(CoffeeMachine.State.MAINTENANCE, machine.GetState());

        action(machine);
        Assert.Equal(expectedState, machine.GetState());
    }

    public static IEnumerable<object[]> GetOffStateTestCases()
    {
        yield return new object[] { "OFF -- TurnOn --> READY", (Action<CoffeeMachine>)(m => m.TurnOn()), CoffeeMachine.State.READY };
        yield return new object[] { "OFF -- TurnOff --> OFF", (Action<CoffeeMachine>)(m => m.TurnOff()), CoffeeMachine.State.OFF };
        yield return new object[] { "OFF -- Brew --> OFF", (Action<CoffeeMachine>)(m => m.Brew()), CoffeeMachine.State.OFF };
        yield return new object[] { "OFF -- StartMaintenance --> OFF", (Action<CoffeeMachine>)(m => m.StartMaintenance()), CoffeeMachine.State.OFF };
        yield return new object[] { "OFF -- FinishMaintenance --> OFF", (Action<CoffeeMachine>)(m => m.FinishMaintenance()), CoffeeMachine.State.OFF };
    }

    public static IEnumerable<object[]> GetReadyStateTestCases()
    {
        yield return new object[] { "READY -- TurnOn --> READY", (Action<CoffeeMachine>)(m => m.TurnOn()), CoffeeMachine.State.READY };
        yield return new object[] { "READY -- TurnOff --> OFF", (Action<CoffeeMachine>)(m => m.TurnOff()), CoffeeMachine.State.OFF };
        yield return new object[] { "READY -- Brew --> READY", (Action<CoffeeMachine>)(m => m.Brew()), CoffeeMachine.State.READY };
        yield return new object[] { "READY -- StartMaintenance --> MAINTENANCE", (Action<CoffeeMachine>)(m => m.StartMaintenance()), CoffeeMachine.State.MAINTENANCE };
        yield return new object[] { "READY -- FinishMaintenance --> READY", (Action<CoffeeMachine>)(m => m.FinishMaintenance()), CoffeeMachine.State.READY };
    }

    public static IEnumerable<object[]> GetMaintenanceStateTestCases()
    {
        yield return new object[] { "MAINTENANCE -- TurnOn --> MAINTENANCE", (Action<CoffeeMachine>)(m => m.TurnOn()), CoffeeMachine.State.MAINTENANCE };
        yield return new object[] { "MAINTENANCE -- TurnOff --> OFF", (Action<CoffeeMachine>)(m => m.TurnOff()), CoffeeMachine.State.OFF };
        yield return new object[] { "MAINTENANCE -- Brew --> MAINTENANCE", (Action<CoffeeMachine>)(m => m.Brew()), CoffeeMachine.State.MAINTENANCE };
        yield return new object[] { "MAINTENANCE -- StartMaintenance --> MAINTENANCE", (Action<CoffeeMachine>)(m => m.StartMaintenance()), CoffeeMachine.State.MAINTENANCE };
        yield return new object[] { "MAINTENANCE -- FinishMaintenance --> READY", (Action<CoffeeMachine>)(m => m.FinishMaintenance()), CoffeeMachine.State.READY };
    }
}

// %%
RunTests<CoffeeMachineTest>();

// %%
public class ObservableCoffeeMachine
{
    public enum State
    {
        OFF, READY, BREWING, MAINTENANCE
    }

    private State currentState;
    private List<StateChangeObserver> observers = new List<StateChangeObserver>();

    public ObservableCoffeeMachine()
    {
        currentState = State.OFF;
    }

    public void AddObserver(StateChangeObserver observer)
    {
        observers.Add(observer);
    }

    private void NotifyObservers(State oldState, State newState)
    {
        foreach (StateChangeObserver observer in observers)
        {
            observer.OnStateChange(oldState, newState);
        }
    }

    private void ChangeState(State newState)
    {
        State oldState = currentState;
        currentState = newState;
        if (oldState != newState)
        {
            NotifyObservers(oldState, newState);
        }
    }

    public void TurnOn()
    {
        if (currentState == State.OFF)
        {
            ChangeState(State.READY);
        }
    }

    public void TurnOff()
    {
        ChangeState(State.OFF);
    }

    public void Brew()
    {
        if (currentState == State.READY)
        {
            ChangeState(State.BREWING);
            // Simulate brewing process
            ChangeState(State.READY);
        }
    }

    public void StartMaintenance()
    {
        if (currentState == State.READY)
        {
            ChangeState(State.MAINTENANCE);
        }
    }

    public void FinishMaintenance()
    {
        if (currentState == State.MAINTENANCE)
        {
            ChangeState(State.READY);
        }
    }

    public State GetState()
    {
        return currentState;
    }

    public interface StateChangeObserver
    {
        void OnStateChange(State oldState, State newState);
    }
}

// %%
public class StateChangeSpy : ObservableCoffeeMachine.StateChangeObserver
{
    private List<ObservableCoffeeMachine.State> stateChanges = new List<ObservableCoffeeMachine.State>();

    public void OnStateChange(ObservableCoffeeMachine.State oldState, ObservableCoffeeMachine.State newState)
    {
        stateChanges.Add(newState);
    }

    public List<ObservableCoffeeMachine.State> GetStateChanges()
    {
        return new List<ObservableCoffeeMachine.State>(stateChanges);
    }

    public void Reset()
    {
        stateChanges.Clear();
    }
}

// %%
public class ObservableCoffeeMachineTest
{
    private ObservableCoffeeMachine machine;
    private StateChangeSpy spy;

    [Fact]
    public void TestInitialState()
    {
        machine = new ObservableCoffeeMachine();
        spy = new StateChangeSpy();
        machine.AddObserver(spy);

        Assert.Equal(ObservableCoffeeMachine.State.OFF, machine.GetState());
    }

    [Theory]
    [MemberData(nameof(GetOffStateTestCases))]
    public void TransitionsFromOff(
        string description,
        Action<ObservableCoffeeMachine> action,
        ObservableCoffeeMachine.State expectedState,
        ObservableCoffeeMachine.State[] expectedTransitions)
    {
        machine = new ObservableCoffeeMachine();
        spy = new StateChangeSpy();
        machine.AddObserver(spy);

        Assert.Equal(ObservableCoffeeMachine.State.OFF, machine.GetState());
        spy.Reset();

        action(machine);
        Assert.Equal(expectedState, machine.GetState());
        Assert.Equal(expectedTransitions, spy.GetStateChanges());
    }

    [Theory]
    [MemberData(nameof(GetReadyStateTestCases))]
    public void TransitionsFromReady(
        string description,
        Action<ObservableCoffeeMachine> action,
        ObservableCoffeeMachine.State expectedState,
        ObservableCoffeeMachine.State[] expectedTransitions)
    {
        machine = new ObservableCoffeeMachine();
        spy = new StateChangeSpy();
        machine.AddObserver(spy);

        machine.TurnOn();
        Assert.Equal(ObservableCoffeeMachine.State.READY, machine.GetState());
        spy.Reset();

        action(machine);
        Assert.Equal(expectedState, machine.GetState());
        Assert.Equal(expectedTransitions, spy.GetStateChanges());
    }

    [Theory]
    [MemberData(nameof(GetMaintenanceStateTestCases))]
    public void TransitionsFromMaintenance(
        string description,
        Action<ObservableCoffeeMachine> action,
        ObservableCoffeeMachine.State expectedState,
        ObservableCoffeeMachine.State[] expectedTransitions)
    {
        machine = new ObservableCoffeeMachine();
        spy = new StateChangeSpy();
        machine.AddObserver(spy);

        machine.TurnOn();
        machine.StartMaintenance();
        Assert.Equal(ObservableCoffeeMachine.State.MAINTENANCE, machine.GetState());
        spy.Reset();

        action(machine);
        Assert.Equal(expectedState, machine.GetState());
        Assert.Equal(expectedTransitions, spy.GetStateChanges());
    }

    public static IEnumerable<object[]> GetOffStateTestCases()
    {
        yield return new object[] { "OFF -- turnOn --> READY",
            (Action<ObservableCoffeeMachine>)(m => m.TurnOn()),
            ObservableCoffeeMachine.State.READY,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.READY } };
        yield return new object[] { "OFF -- turnOff --> OFF",
            (Action<ObservableCoffeeMachine>)(m => m.TurnOff()),
            ObservableCoffeeMachine.State.OFF,
            new ObservableCoffeeMachine.State[] { } };
        yield return new object[] { "OFF -- brew --> OFF",
            (Action<ObservableCoffeeMachine>)(m => m.Brew()),
            ObservableCoffeeMachine.State.OFF,
            new ObservableCoffeeMachine.State[] { } };
        yield return new object[] { "OFF -- startMaintenance --> OFF",
            (Action<ObservableCoffeeMachine>)(m => m.StartMaintenance()),
            ObservableCoffeeMachine.State.OFF,
            new ObservableCoffeeMachine.State[] { } };
        yield return new object[] { "OFF -- finishMaintenance --> OFF",
            (Action<ObservableCoffeeMachine>)(m => m.FinishMaintenance()),
            ObservableCoffeeMachine.State.OFF,
            new ObservableCoffeeMachine.State[] { } };
    }

    public static IEnumerable<object[]> GetReadyStateTestCases()
    {
        yield return new object[] { "READY -- turnOn --> READY",
            (Action<ObservableCoffeeMachine>)(m => m.TurnOn()),
            ObservableCoffeeMachine.State.READY,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.READY } };
        yield return new object[] { "READY -- turnOff --> OFF",
            (Action<ObservableCoffeeMachine>)(m => m.TurnOff()),
            ObservableCoffeeMachine.State.OFF,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.OFF } };
        yield return new object[] { "READY -- brew --> READY",
            (Action<ObservableCoffeeMachine>)(m => m.Brew()),
            ObservableCoffeeMachine.State.READY,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.BREWING, ObservableCoffeeMachine.State.READY } };
        yield return new object[] { "READY -- startMaintenance --> MAINTENANCE",
            (Action<ObservableCoffeeMachine>)(m => m.StartMaintenance()),
            ObservableCoffeeMachine.State.MAINTENANCE,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.MAINTENANCE } };
        yield return new object[] { "READY -- finishMaintenance --> READY",
            (Action<ObservableCoffeeMachine>)(m => m.FinishMaintenance()),
            ObservableCoffeeMachine.State.READY,
            new ObservableCoffeeMachine.State[] { } };
    }

    public static IEnumerable<object[]> GetMaintenanceStateTestCases()
    {
        yield return new object[] { "MAINTENANCE -- turnOn --> MAINTENANCE",
            (Action<ObservableCoffeeMachine>)(m => m.TurnOn()),
            ObservableCoffeeMachine.State.MAINTENANCE,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.MAINTENANCE } };
        yield return new object[] { "MAINTENANCE -- turnOff --> OFF",
            (Action<ObservableCoffeeMachine>)(m => m.TurnOff()),
            ObservableCoffeeMachine.State.OFF,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.OFF } };
        yield return new object[] { "MAINTENANCE -- brew --> MAINTENANCE",
            (Action<ObservableCoffeeMachine>)(m => m.Brew()),
            ObservableCoffeeMachine.State.MAINTENANCE,
            new ObservableCoffeeMachine.State[] { } };
        yield return new object[] { "MAINTENANCE -- startMaintenance --> MAINTENANCE",
            (Action<ObservableCoffeeMachine>)(m => m.StartMaintenance()),
            ObservableCoffeeMachine.State.MAINTENANCE,
            new ObservableCoffeeMachine.State[] { } };
        yield return new object[] { "MAINTENANCE -- finishMaintenance --> READY",
            (Action<ObservableCoffeeMachine>)(m => m.FinishMaintenance()),
            ObservableCoffeeMachine.State.READY,
            new ObservableCoffeeMachine.State[] { ObservableCoffeeMachine.State.READY } };
    }
}

// %%
XunitTestRunner.RunTests<ObservableCoffeeMachineTest>();

// %%
