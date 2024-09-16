// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Clean Code: Namen (Teil 1)</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// Namen sind ein mächtiges Kommunikationsmittel.
//
// - Sie sind überall im Programm zu finden
// - Sie verbinden den Code mit Domänen-Konzepten.

// %%
using System;

// %%
double Foo(double a, double b)
{
    if (b > 40.0)
    {
        throw new ArgumentException("Not allowed!");
    }
    return 40.0 * a + 60.0 * b;
}

// %%
Console.WriteLine(Foo(40.0, 3.5));


// %%
const double RegularPayPerHour = 40.0;
const double OvertimePayPerHour = 60.0;
const double MaxAllowedOvertime = 40.0;

// %%
double ComputeTotalSalary(double regularHoursWorked, double overtimeHoursWorked)
{
    if (overtimeHoursWorked > MaxAllowedOvertime)
    {
        throw new ArgumentException("Not allowed!");
    }
    double regularPay = regularHoursWorked * RegularPayPerHour;
    double overtimePay = overtimeHoursWorked * OvertimePayPerHour;
    return regularPay + overtimePay;
}

// %%
Console.WriteLine(ComputeTotalSalary(40.0, 3.5));

// %% [markdown]
//
// ### Enumerationen

// %%
const int High = 0;
const int Medium = 1;
const int Low = 2;

// %%
int severity = High;

// %%
enum Severity
{
    High,
    Medium,
    Low
}

// %%
Severity severity = Severity.High;

// %% [markdown]
//
// ### Klassen und Structs

// %%
using System;

// %%
(int, string) AnalyzeReview(string text)
{
    return (5, "Overall positive");
}

// %%

public class Issue
{
    public int Score { get; set; }
    public string Sentiment { get; set; }
}

// %%
Issue AnalyzeReview(string text) => new Issue { Score = 5, Sentiment = "Overall positive" };


// %% [markdown]
//
// ## Was ist ein guter Name?
//
// - Präzise (sagt was er meint, meint was er sagt)
// - Beantwortet
//   - Warum gibt es diese Variable (Funktion, Klasse, Modul, Objekt...)?
//   - Was macht sie?
//   - Wie wird sie verwendet?
//
// Gute Namen sind schwer zu finden!

// %% [markdown]
//
// ## Was ist ein schlechter Name?
//
// - Braucht einen Kommentar
// - Verbreitet Disinformation
// - Entspricht nicht den Namensregeln

// %% [markdown]
//
// ## Workshop: Rezepte
//
// In `code/starter_kits/recipes_sk` ist ein Programm, mit denen sich ein
// Kochbuch verwalten lässt. Leider hat der Programmierer sehr schlechte Namen
// verwendet, dadurch ist das Programm schwer zu verstehen.
//
// Ändern Sie die Namen so, dass das Programm leichter verständlich wird.
//
// ### Hinweis
//
// Verwenden Sie die Refactoring-Tools Ihrer Entwicklungsumgebung!
