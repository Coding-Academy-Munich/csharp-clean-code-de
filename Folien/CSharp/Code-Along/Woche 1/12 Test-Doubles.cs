// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Test-Doubles</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Test Doubles
//
// - Test Double: Vereinfachte Version einer Abhängigkeit im System
//   - z.B. Ersetzen einer Datenbankabfrage durch einen fixen Wert
// - Test Doubles sind wichtig zum Vereinfachen von Tests
// - Sie benötigen typischerweise ein Interface, das sie implementieren
// - Aber: zu viele oder komplexe Test Doubles machen Tests unübersichtlich
//   - Was wird von einem Test eigentlich getestet?

// %% [markdown]
//
// ## Arten von Test Doubles
//
// - Ausgehende Abhängigkeiten ("Mocks")
//   - Mocks
//   - Spies
// - Eingehende Abhängigkeiten ("Stubs")
//   - Dummies
//   - Stubs
//   - Fakes

// %% [markdown]
//
// ## Dummy
//
// - Objekt, das nur als Platzhalter dient
// - Wird übergeben, aber nicht verwendet
// - In C# manchmal `null`
// - Auch für ausgehende Abhängigkeiten

// %% [markdown]
//
// ## Stub
//
// - Objekt, das eine minimale Implementierung einer Abhängigkeit bereitstellt
// - Gibt typischerweise immer den gleichen Wert zurück
// - Wird verwendet um
//  - komplexe Abhängigkeiten zu ersetzen
//  - Tests deterministisch zu machen

// %% [markdown]
//
// ## Fake
//
// - Objekt, das eine einfachere Implementierung einer Abhängigkeit bereitstellt
// - Kann z.B. eine In-Memory-Datenbank sein
// - Wird verwendet um
//   - Tests zu beschleunigen
//   - Konfiguration von Tests zu vereinfachen

// %% [markdown]
//
// ## Spy
//
// - Objekt, das Informationen über die Interaktion mit ihm speichert
// - Wird verwendet um
//   - zu überprüfen, ob eine Abhängigkeit korrekt verwendet wird

// %% [markdown]
//
// ## Mock
//
// - Objekt, das Information über die erwartete Interaktion speichert
// - Typischerweise deklarativ konfigurierbar
// - Automatisierte Implementierung von Spies
// - Wird verwendet um
//   - zu überprüfen, ob eine Abhängigkeit korrekt verwendet wird

// %%
public interface IDataSource
{
    int GetValue();
}

// %%
public interface IDataSink
{
    void SetValue(int value);
}

// %%
public class Processor
{
    private IDataSource source;
    private IDataSink sink;

    public Processor(IDataSource source, IDataSink sink)
    {
        this.source = source;
        this.sink = sink;
    }

    public void Process()
    {
        int value = source.GetValue();
        sink.SetValue(value);
    }
}

// %%
public class DataSourceStub : IDataSource
{
    public int GetValue() { return 42; }
}

// %%
public class DataSinkSpy : IDataSink
{
    public List<int> Values { get; } = new List<int>();

    public void SetValue(int value)
    {
        Values.Add(value);
    }
}

// %%
void Check(bool condition)
{
    if (!condition)
    {
        Console.WriteLine("Test failed!");
    }
    else
    {
        Console.WriteLine("Success!");
    }
}

// %%
void TestProcessor()
{
    var source = new DataSourceStub();
    var sink = new DataSinkSpy();
    var processor = new Processor(source, sink);

    processor.Process();

    Check(sink.Values.Count == 1);
    Check(sink.Values[0] == 42);
}

// %%

// %% [markdown]
//
// ## Typischer Einsatz von Test Doubles
//
// - Zugriff auf Datenbank, Dateisystem
// - Zeit, Zufallswerte
// - Nichtdeterminismus
// - Verborgener Zustand

// %% [markdown]
//
// ## Workshop: Test Doubles
//
// Wir haben die folgenden Interfaces, die von der Funktion `TestMe()`
// verwendet werden:

// %%
public interface IService1
{
    int GetValue();
}

// %%
public interface IService2
{
    void SetValue(int value);
}

// %%
public void TestMe(int i, int j, IService1 service1, IService2 service2)
{
    int value = 0;
    if (i > 0)
    {
        value = service1.GetValue();
    }
    if (j > 0)
    {
        service2.SetValue(value);
    }
}

// %% [markdown]
//
// Welche Arten von Test-Doubles brauchen Sie um die Funktion `TestMe()` für
// die angegebenen Werte von `i` und `j` zu testen?
//
// | i | j | Service1 | Service2 |
// |---|---|----------|----------|
// | 0 | 0 |          |          |
// | 0 | 1 |          |          |
// | 1 | 0 |          |          |
// | 1 | 1 |          |          |

// %% [markdown]
//
// Implementieren Sie die entsprechenden Doubles und schreiben Sie die Tests

// %%

// %%

// %%

// %%
