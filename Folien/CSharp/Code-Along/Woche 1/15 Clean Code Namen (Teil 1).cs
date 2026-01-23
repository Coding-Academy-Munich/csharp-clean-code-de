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
Foo(40.0, 3.5)


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
ComputeTotalSalary(40.0, 3.5)

// %%
int severity = 30; // Is this high or low?

// %%
severity

// %% [markdown]
//
// ### Konstanten
//
// - Namen für besondere Werte
// - Klarer, als die Werte direkt zu verwenden
// - Direkter Bezug zwischen Name und Wert
// - Wenig Information über alle möglichen Werte
// - Wenig Bezug zum Typsystem

// %%
const int SeverityHigh = 30;

// %% [markdown]
//
// ### Enumerationen
//
// - Alternative zu Konstanten
// - Dokumentieren, welche Werte erwartet werden
// - Bessere Typsicherheit
// - Weniger Flexibilität

// %%
public enum Severity {
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
    return (5, "Generally positive");
}

// %%
AnalyzeReview("Some review text")


// %%
public class AnalysisResult {
    public int Score { get; }
    public string Sentiment { get; }

    public AnalysisResult(int score, string sentiment) {
        Score = score;
        Sentiment = sentiment;
    }

    public override string ToString() {
        return $"Score: {Score}, Sentiment: {Sentiment}";
    }
}

// %%
AnalysisResult AnalyzeReview(string text) {
    return new AnalysisResult(5, "Overall positive");
}

// %%
var result = AnalyzeReview("Some review text");
Console.WriteLine(result);

// %%
public enum Score {
    High,
    ModeratelyHigh,
    Medium,
    ModeratelyLow,
    Low,
    Unknown,
}

// %%
public class AnalysisResult {
    public Score Score { get; }
    public string Sentiment { get; }

    public AnalysisResult(Score score, string sentiment) {
        Score = score;
        Sentiment = sentiment;
    }

    public override string ToString() {
        return $"Score: {Score}, Sentiment: {Sentiment}";
    }
}

// %%
AnalysisResult AnalyzeReview(string text) {
    return new AnalysisResult(Score.ModeratelyHigh, "Overall positive");
}

// %%
AnalyzeReview("Some review text")

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
// In `Code/StarterKits/RecipesSk` ist ein Programm, mit dem sich ein
// Kochbuch verwalten lässt. Leider hat der Programmierer sehr schlechte Namen
// verwendet, dadurch ist das Programm schwer zu verstehen.
//
// Ändern Sie die Namen so, dass das Programm leichter verständlich wird.
//
// ### Hinweis
//
// Verwenden Sie die Refactoring-Tools Ihrer Entwicklungsumgebung!
