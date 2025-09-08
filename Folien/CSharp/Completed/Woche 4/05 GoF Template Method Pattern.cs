// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF: Template Method Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
// ## Template Method (Behavioral Pattern)
//
// ### Zweck
//
// Definiere ein Verhalten oder einen Algorithmus, in dem bestimmte Schritte zur
// Implementierung oder Modifikation in Unterklassen vorgesehen sind. Die
// Unterklassen können individuelle Schritte, die vom Algorithmus ausgeführt
// werden überschreiben, haben aber keinen Einfluss auf seine Gesamtstruktur.

// %% [markdown]
//
// ## Template Method (Behavioral Pattern)
//
// ### Motivation
//
// - Eine Anwendung, die mit verschiedenen Arten von Dokumenten umgehen muss.
// - In manchen Operationen der Anwendung gibt es Gemeinsamkeiten, aber auch
//   Unterschiede zwischen den verschiedenen Dokumenten
// - Z.B. ist der Ablauf zum Speichern eines Dokuments immer gleich, aber die
//   Art und Weise, wie die einzelnen Dokumenttypen gespeichert werden, ist
//   unterschiedlich
// - Die Anwendung definiert konkrete Operationen, die abstrakte Operationen
//   aufrufen, die Teil ihrer Ausführung sind
// - Instanzen der Anwendung implementieren die virtuellen Methoden

// %% [markdown]
//
// ### Klassendiagramm
//
// <img src="img/template_method_example.svg"
//      style="display:block;margin:auto;width:40%"/>

// %%
using System;

public abstract class Document
{
    public virtual void Save()
    {
        Console.WriteLine("Saving to existing file.");
        DoSave();
        Console.WriteLine("Document saved.\n");
    }

    public virtual void SaveAs()
    {
        Console.WriteLine("Asking user for file name.");
        DoSave();
        Console.WriteLine("Document saved.\n");
    }

    protected abstract void DoSave();
}

// %%
public class TextDocument : Document
{
    protected override void DoSave()
    {
        Console.WriteLine("-> Saving text document in DOCX format.");
    }
}

// %%
public class SpreadsheetDocument : Document
{
    protected override void DoSave()
    {
        Console.WriteLine("-> Saving spreadsheet in XLSX format.");
    }
}

// %%
SpreadsheetDocument spreadsheet = new SpreadsheetDocument();
spreadsheet.Save();
spreadsheet.SaveAs();

// %%
TextDocument text = new TextDocument();
text.Save();
text.SaveAs();

// %%
Document doc = spreadsheet;
doc.Save();

// %%
doc = text;
doc.SaveAs();

// %% [markdown]
//
// ## Template Method (Behavioral Pattern)
//
// ### Anwendbarkeit
//
// - Um die invarianten Teile eines Algorithmus einmal zu implementieren und
//   Verhaltensvariationen in Unterklassen zu realisieren
// - Zentralisieren von gemeinsamem Verhalten in einer Klasse, um
//   Code-Duplizierung zu vermeiden
// - Kontrolle der Erweiterung von Verhalten durch Unterklassen

// %% [markdown]
//
// ## Template Method (Behavioral Pattern)
//
// ### Structure
//
// <img src="img/pat_template_method.svg"
//      style="display:block;margin:auto;width:40%"/>

// %% [markdown]
//
// ## Template Method (Behavioral Pattern)
//
// ### Teilnehmer
//
// - `AbstractClass`:
//   - definiert abstrakte *primitive Operationen*, die konkrete Unterklassen
//     implementieren (Hooks)
//   - implementiert eine Template-Methode, die das Skelett eines Algorithmus
//     definiert
// - `ConcreteClass`:
//   - implementiert die primitiven Operationen

// %% [markdown]
//
// ## Template Method (Behavioral Pattern)
//
// ### Interaktionen
//
// `ConcreteClass` verlässt sich auf `AbstractClass`, um die invarianten Schritte
// des Algorithmus zu implementieren

// %% [markdown]
//
// ## Template Method (Behavioral Pattern)
//
// ### Consequences
//
// - Template Methods sind eine grundlegende Strategie, zur Wiederverwendung von
//   Code
// - Sie führen zu einem invertierten Kontrollfluss, der oft als "Hollywood
//   Prinzip" bezeichnet wird ("Don't call us, we'll call you")
// - Es ist für Template Methods essentiell, klar zu definieren, welche
//   Methoden überschrieben werden *müssen* und welche *optional* sind

// %% [markdown]
//
// ## Workshop: Mode-Design
//
// ### Stilvolle Kleidung mit dem Template Method Pattern
//
// Die Welt der Mode ist groß, mit unzähligen Stilen, Materialien und
// Designmustern. Wenn es jedoch um den eigentlichen Herstellungsprozess
// verschiedener Kleidungsstücke geht, gibt es bestimmte Schritte, die relativ
// konstant bleiben. Zum Beispiel könnte der Prozess Folgendes beinhalten:
// - Entwickeln eines Designmusters für das Kleidungsstück
// - Auswahl des richtigen Materials
// - Schneiden des Materials nach dem Muster
// - Zusammennähen der Teile
// - Hinzufügen von Veredelungs-Details

// %% [markdown]
//
// Ihre Aufgabe als angehender Modedesigner (und Programmierer!) ist es, einen
// systematischen Ansatz zur Gestaltung verschiedener Arten von Kleidung zu
// entwickeln. Aber nicht nur irgendeinen Ansatz, wir brauchen einen effizienten
// und erweiterbaren Ansatz. Nehmen wir an, Sie möchten sowohl Kleider als auch
// Anzüge entwerfen. Während der Gesamtprozess ähnlich ist, variieren die
// Details erheblich. Wenn Sie z.B. Veredelungs-details hinzufügen, benötigen
// Kleider möglicherweise Spitzen-Details, während bei Anzügen die Platzierung
// der Knöpfe wichtig ist.
//
// Das Ziel ist es, ein System zu entwickeln, in dem die generischen Schritte
// einmal definiert werden, die spezifischen Schritte jedoch je nach
// Kleidungsstück angepasst werden können.
//
// 1. Definieren Sie einen generischen Prozess zur Herstellung von Kleidung
//    basierend auf den oben beschriebenen Schritten.
// 2. Identifizieren Sie Schritte, die allen Kleidungsstücken gemeinsam sind.
// 3. Identifizieren Sie Schritte, die für jeden Kleidungstyp einzigartig und
//    spezifisch sind.
// 4. Implementieren Sie diesen Prozess mit dem Template Method Design Pattern
//    in C#.
// 5. Implementieren Sie konkrete Klassen zur Herstellung von Anzügen und
//    Kleidern.

// %%
using System;

public abstract class Clothing
{
    // The template method
    public void DesignClothing()
    {
        DevelopDesignPattern();
        ChooseMaterial();
        CutPattern();
        Sew();
        AddDetails();
    }

    protected abstract void DevelopDesignPattern();

    protected virtual void ChooseMaterial()
    {
        Console.WriteLine("You can't go wrong with cotton as material.");
    }

    protected void CutPattern()
    {
        Console.WriteLine("Cutting based on the design pattern.");
    }

    protected void Sew()
    {
        Console.WriteLine("Sewing pieces together.");
    }

    protected abstract void AddDetails();
}

// %%
public class Dress : Clothing
{
    protected override void DevelopDesignPattern()
    {
        Console.WriteLine("Dress: Developing a design pattern for the dress.");
    }

    protected override void ChooseMaterial()
    {
        Console.WriteLine("Dress: Choosing satin material.");
    }

    protected override void AddDetails()
    {
        Console.WriteLine("Dress: Adding lace and sequins to the dress.");
    }
}

// %%
public class Suit : Clothing
{
    protected override void DevelopDesignPattern()
    {
        Console.WriteLine("Suit: Developing a design pattern for the suit.");
    }

    protected override void ChooseMaterial()
    {
        Console.WriteLine("Suit: Choosing wool material.");
    }

    protected override void AddDetails()
    {
        Console.WriteLine("Suit: Adding buttons and pockets to the suit.");
    }
}

// %%
public class TShirt : Clothing
{
    protected override void DevelopDesignPattern()
    {
        Console.WriteLine("TShirt: Developing a design pattern for the T-Shirt.");
    }

    protected override void AddDetails()
    {
        Console.WriteLine("TShirt: Adding a logo to the T-Shirt.");
    }
}

// %%
Console.WriteLine("Designing a Dress:\n");
Dress dress = new Dress();
dress.DesignClothing();

// %%
Console.WriteLine("\nDesigning a Suit:\n");
Suit suit = new Suit();
suit.DesignClothing();

// %%
Console.WriteLine("\nDesigning a T-Shirt:\n");
TShirt tShirt = new TShirt();
tShirt.DesignClothing();
