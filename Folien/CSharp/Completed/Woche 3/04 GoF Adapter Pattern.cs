// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF Adapter Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Zweck
//
// - Anpassung der Schnittstelle einer Klasse an ein erwartetes Interface
// - Zusammenarbeit von Klassen, die aufgrund inkompatibler Schnittstellen nicht
//   zusammenarbeiten können

// %% [markdown]
//
// ## Auch bekannt als
//
// - Wrapper

// %% [markdown]
//
// ## Motivation
//
// - Nutzung einer Bibliotheks-Klasse aufgrund inkompatibler Schnittstellen nicht
//   möglich
// - Beispiel: Grafischer Editor
//   - Grafik-Objekte sind relativ einfach zu realisieren
//   - Text ist komplexer, möglicherweise ist der Einsatz einer externen Bibliothek
//     sinnvoll
// - Die Schnittstelle dieser Bibliothek ist nicht kompatibel mit der Schnittstelle,
//   die unser Editor erwartet
// - Mit einem Adapter können wir die Schnittstelle der Bibliothek an die
//   Schnittstelle unseres Editors anpassen

// %% [markdown]
//
// <img src="img/adapter_example.png"
//      style="display:block;margin:auto;width:80%"/>

// %% [markdown]
//
// ## Anwendbarkeit
//
// - Nutzung einer bestehenden Klasse mit inkompatibler Schnittstelle
// - [...]

// %% [markdown]
//
// ## Struktur
//
// - Es werden zwei Varianten definiert: Klassenadapter und Objektadapter
// - Klassenadapter verwenden Mehrfachvererbung und werden seltener eingesetzt
// - Klassendiagramm für Objektadapter:
//
// <img src="img/pat_adapter.png"
//      style="display: block; margin: auto; width: 80%;"/>

// %% [markdown]
//
// ## Teilnehmer
//
// - `Client`
//   - Nutzt das Interface von `Target`
// - `Target`
//   - Definiert das Interface, das vom `Client` verwendet wird
// - `Adapter`
//   - Implementiert das Interface von `Target` und hält eine Referenz auf das `Adaptee`
// - `Adaptee`
//   - Die Klasse, deren Schnittstelle angepasst werden soll

// %% [markdown]
//
// ## Interaktionen
//
// - Der Client ruft eine Target-Methode auf einem Adapter-Objekt auf
// - Der Adapter ruft die entsprechende Methode auf dem Adaptee auf

// %% [markdown]
//
// ## Konsequenzen
//
// - Klassenadapter
//   - ...
// - Objektadapter
//   - ein Adapter kann mit mehreren adaptierten Objekten zusammenarbeiten
//   - erschwert das Überschreiben von Adaptee-Methoden

// %% [markdown]
//
// ## Beispielcode
//
// - Verwaltung von Mitarbeitern in einer Firma
// - Ein Teil der Daten wird von einem Legacy-System bereitgestellt
// - Die Schnittstelle des Legacy-Systems ist nicht kompatibel mit der Schnittstelle
//   der neuen Software
// - Wir erstellen einen Adapter, der die Schnittstelle des Legacy-Systems an die
//   Schnittstelle der neuen Software anpasst

// %%
public interface IEmployee
{
    string Name { get; }
    decimal Salary { get; }
}

// %%
public class NewEmployee(string name, decimal salary) : IEmployee
{
    public string Name { get; } = name;
    public decimal Salary { get; } = salary;
}

// %%
public class LegacyEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Pay { get; set; } // In cents

    public LegacyEmployee(string firstName, string lastName, int pay)
    {
        FirstName = firstName;
        LastName = lastName;
        Pay = pay;
    }
}

// %%
public class Company
{
    private List<IEmployee> _employees;
    private decimal _monthlyRent;

    public Company(List<IEmployee> employees, decimal monthlyRent = 1000.0m)
    {
        _employees = employees;
        _monthlyRent = monthlyRent;
    }

    public decimal MonthlyExpenses()
    {
        decimal totalSalary = _employees.Aggregate(0.0m, (acc, employee) => acc + employee.Salary);
        return totalSalary + _monthlyRent;
    }

    public string Employees()
    {
        return _employees.Aggregate("", (acc, employee) => acc + employee.Name + "\n");
    }
}

// %%
public class LegacyEmployeeAdapter : IEmployee
{
    private readonly LegacyEmployee _legacyEmployee;

    public LegacyEmployeeAdapter(LegacyEmployee legacyEmployee)
    {
        _legacyEmployee = legacyEmployee;
    }

    public string Name => $"{_legacyEmployee.FirstName} {_legacyEmployee.LastName}";
    public decimal Salary => _legacyEmployee.Pay / 100.0m;
}

// %%
var legacyEmployee1 = new LegacyEmployeeAdapter(new LegacyEmployee("John", "Doe", 150_000));
var legacyEmployee2 = new LegacyEmployeeAdapter(new LegacyEmployee("Jane", "Miller", 200_000));
var newEmployee1 = new NewEmployee("Max Mustermann", 2500.0m);
var newEmployee2 = new NewEmployee("Erica Jones", 3000.0m);

// %%
var employees = new List<IEmployee>
{
    legacyEmployee1,
    legacyEmployee2,
    newEmployee1,
    newEmployee2
};

// %%
var company = new Company(employees);

// %%
Console.WriteLine("Monthly expenses: " + company.MonthlyExpenses());

// %%
Console.WriteLine("Employees:\n" + company.Employees());

// %% [markdown]
//
// ## Praxisbeispiele
//
// - ET++ Draw
// - InterViews 2.6
// - ...

// %% [markdown]
//
// ## Verwandte Muster
//
// - Bridge: ähnliche Struktur, aber andere Absicht (Trennung von Schnittstelle und
//   Implementierung)
// - Decorator: erweitert anderes Objekt, ohne die Schnittstelle zu ändern
// - Proxy: Stellvertreter für ein Objekt, das die gleiche Schnittstelle hat
