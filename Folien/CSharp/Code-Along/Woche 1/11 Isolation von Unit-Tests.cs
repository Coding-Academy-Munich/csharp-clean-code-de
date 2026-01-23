// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Isolation von Unit-Tests</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Unit-Test
//
// - Testet einen kleinen Teil des Codes (eine **"Unit"**)
// - Hat kurze Laufzeit
// - *Ist isoliert*

// %% [markdown]
//
// ## Was ist eine "Unit"?
//
// - Ein Verhalten?
//   - *Unit of behavior*
//   - Teil eines Szenarios/Use-Cases/...
//   - Ursprüngliche Intention von Kent Beck
// - Ein Code-Bestandteil?
//   - *Unit of code*
//   - Oft Unit = Klasse
//   - In der Literatur weit verbreitete Ansicht

// %% [markdown]
//
// ## Was bedeutet "isolierter" Test?
//
// - Keine Interaktion zwischen Tests?
//   - Isolierte Testfälle
//   - Klassische Unit-Tests (Detroit School, Kent Beck)
// - Keine Interaktion zwischen getesteter Einheit und dem Rest des Systems?
//   - Abhängigkeiten werden durch einfache Simulationen ersetzt (Test-Doubles)
//   - London School

// %% [markdown]
//
// ## Isolierte Testfälle (Detroit School)
//
// - Jeder Testfall ist unabhängig von den anderen
// - Tests können in beliebiger Reihenfolge ausgeführt werden
// - Tests können parallel ausgeführt werden

// %% [markdown]
//
// ### Gegenbeispiel: Nicht isolierte Testfälle

// %%

// %%

// %%

// %%

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ## Gründe für nicht isolierte Testfälle
//
// - Veränderlicher globaler/statischer Zustand
// - Veränderliche externe Ressourcen (Dateien, Datenbanken, Netzwerk, ...)

// %% [markdown]
//
// ## Isolation der getesteten Unit
//
// - Die getestete Unit wird von allen anderen Units isoliert
// - Test-Doubles für alle Abhängigkeiten

// %% [markdown]
//
// ### Gegenbeispiel: Nicht isolierte Unit
//
// - Verkäufer von Veranstaltungs-Tickets
// - Konkrete Klasse `Show` repräsentiert eine Veranstaltung

// %%

// %%

// %%

// %%
#r "nuget: Xunit, *"

// %%
#load "XunitTestRunner.cs"

// %%
using Xunit;
using static XunitTestRunner;

// %%

// %%

// %%
using System;
using System.Collections.Generic;

// %%
public interface IShow
{
    string Name { get; }
    int Capacity { get; }
    void Purchase(int numTickets);
}

// %%
public class ConcreteShow : IShow
{
    public ConcreteShow(string name, int capacity)
    {
        Name = name;
        Capacity = capacity;
    }

    public string Name { get; }
    public int Capacity { get; private set; }

    public void Purchase(int numTickets)
    {
        if (numTickets > Capacity)
        {
            throw new InvalidOperationException("Not enough capacity");
        }
        Capacity -= numTickets;
    }
}

// %%
public class TicketOffice
{
    public void AddShow(IShow show)
    {
        shows[show.Name] = show;
    }

    public IShow GetShow(string showName)
    {
        return shows[showName];
    }

    public bool PurchaseTickets(string showName, int numTickets)
    {
        if (shows.ContainsKey(showName))
        {
            try
            {
                shows[showName].Purchase(numTickets);
                return true;
            }
            catch (InvalidOperationException e)
            {
                Console.Error.WriteLine($"Cannot purchase {numTickets} tickets for {showName}");
                Console.Error.WriteLine(e.Message);
                return false;
            }
        }
        return false;
    }

    private Dictionary<string, IShow> shows = new Dictionary<string, IShow>();
}

// %% [markdown]
//
// ### Isolation von `TicketOffice` für Tests
//
// - Entkopplung von allen Abhängigkeiten
// - `ShowMock`-Implementierung für Shows

// %%
using System.Collections.Generic;

// %%
public class ShowMock : IShow
{
    public List<int> PurchaseArgs = new List<int>();

    public string Name => "C# Conference";
    public int Capacity => 90;
    public void Purchase(int numTickets) => PurchaseArgs.Add(numTickets);
}

// %%
public class TestPurchaseTicketsWithMock
{
    [Fact]
    public void PurchaseTickets_CallsPurchaseWithNumberOfTickets()
    {
        var ticketOffice = new TicketOffice();
        var showMock = new ShowMock();
        ticketOffice.AddShow(showMock);
        const int numTickets = 10;

        bool result = ticketOffice.PurchaseTickets("C# Conference", numTickets);

        Assert.True(result);
        Assert.Equal(90, ticketOffice.GetShow("C# Conference").Capacity);
        Assert.Equal(new List<int> { numTickets }, showMock.PurchaseArgs);
    }
}

// %%
RunTests<TestPurchaseTicketsWithMock>();

// %% [markdown]
//
// ## Vorteile der Isolation der getesteten Unit
//
// - Einfache Struktur der Tests
//   - Jeder Test gehört zu genau einer Unit
// - Genaue Identifikation von Fehlern
// - Aufbrechen von Abhängigkeiten/des Objektgraphen

// %% [markdown]
//
// ## Nachteile der Isolation der getesteten Unit
//
// - Potenziell höherer Aufwand (z.B. Mocks)
// - Fehler in der Interaktion zwischen Units werden nicht gefunden
// - Verleiten zum Schreiben von "Interaktionstests"
// - **Risiko von Kopplung an Implementierungsdetails**

// %% [markdown]
//
// ## Empfehlung
//
// - Verwenden Sie isolierte Unit-Tests (Detroit School)
// - Isolieren Sie Abhängigkeiten, die "eine Rakete starten"
//   - nicht-deterministisch (z.B. Zufallszahlen, aktuelle Zeit, aktuelles Datum)
//   - langsam
//   - externe Systeme (z.B. Datenbank)
// - Isolieren Sie Abhängigkeiten, die ein komplexes Setup benötigen

// %% [markdown]
//
// ## Workshop: Virtuelle Universität
//
// - Im `code`-Ordner finden Sie eine Implementierung eins sehr einfachen
//   Verwaltungssystems für eine Universität:
// - Es gibt Kurse, Professoren, die die Kurse halten, Studenten, die Aufgaben
//   bearbeiten und abgeben müssen.
// - Der Code ist in `Code/StarterKits/virtual-university-sk` zu finden.
// - Die `Main.cs`-Datei illustriert, wie die Klassen zusammenarbeiten und
//   verwendet werden können.

// %% [markdown]
//
// - Identifizieren Sie, welche Klassen und Methoden zu den "wertvollsten"
//   Unit-Tests führen.
// - Implementieren Sie diese Unit-Tests mit xUnit.net.
//   - Idealerweise implementieren sie Tests für alle Klassen, die sinnvolle
//     Tests haben.
//   - Falls Sie dafür nicht genug Zeit haben, können Sie auch nur Tests für
//     einen Teil des Codes schreiben.
//   - Die Klasse `Student` ist ein ganz guter Startpunkt, weil sie eine sehr
//     begrenzte Funktionalität hat, die Sie mit relativ wenigen Tests abdecken
//     können.
// - Sind Ihre Tests isoliert?
//   - Nach der Detroit- oder London-School?

// %% [markdown]
//
// - Falls Sie Ihre Tests nach der Detroit-School isoliert haben:
//   - Überlegen Sie, wie Sie das System überarbeiten müssten, um die Klassen in
//     Tests vollständig zu isolieren, also im London School Stil zu testen.
