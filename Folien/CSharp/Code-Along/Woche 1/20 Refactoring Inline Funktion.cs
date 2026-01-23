// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Refactoring: Inline Funktion</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Inline Function
//
// - Anwendung
//   - Wenn eine Funktion nicht mehr tut als ihr Rumpf und der Name nicht mehr
//     aussagt als der Rumpf
//   - Wenn wir eine schlecht strukturierte Hilfsfunktion haben
// - Invers zu *Extrahiere Funktion*

// %%
using System;
using System.Collections.Generic;

// %%
public class OrderLine
{
    public string Product { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public OrderLine(string product, int quantity, decimal price)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
    }

    public override string ToString()
    {
        return $"OrderLine({Quantity} x {Product} @ ${Price})";
    }
}

// %%
static List<OrderLine> MakeOrderLines()
{
    List<OrderLine> orderLines = new List<OrderLine>();
    orderLines.Add(new OrderLine("Apple", 2, 0.5m));
    orderLines.Add(new OrderLine("Banana", 3, 0.3m));
    orderLines.Add(new OrderLine("Orange", 1, 0.8m));
    return orderLines;
}

// %%
public static class OrderLineProcessor
{
    public static decimal OrderLinePrice(OrderLine orderLine)
    {
        return orderLine.Price;
    }

    public static decimal OrderLineQuantity(OrderLine orderLine)
    {
        return orderLine.Quantity;
    }

    public static decimal ComputeTotal(List<OrderLine> orderLines)
    {
        decimal total = 0.0m;
        foreach (OrderLine orderLine in orderLines)
        {
            total += OrderLineQuantity(orderLine) * OrderLinePrice(orderLine);
        }
        return total;
    }
}

// %%
OrderLineProcessor.ComputeTotal(MakeOrderLines());

// %%
public static class OrderLineProcessor
{
    public static decimal OrderLinePrice(OrderLine orderLine)
    {
        return orderLine.Price;
    }

    public static decimal OrderLineQuantity(OrderLine orderLine)
    {
        return orderLine.Quantity;
    }

    public static decimal ComputeTotal(List<OrderLine> orderLines)
    {
        decimal total = 0.0m;
        foreach (OrderLine orderLine in orderLines)
        {
            total += OrderLineQuantity(orderLine) * OrderLinePrice(orderLine);
        }
        return total;
    }
}

// %%
OrderLineProcessor.ComputeTotal(MakeOrderLines());

// %% [markdown]
//
// #### Motivation
//
// - Manchmal ist eine Funktion nicht leichter zu verstehen als ihr Rumpf
// - Manchmal sind die von einer Funktion verwendeten Hilfsfunktionen nicht gut
//   strukturiert
// - Generell: Potentiell anwendbar, wenn man aufgrund zu vieler Indirektionen
//   den Überblick verliert

// %% [markdown]
//
// #### Mechanik
//
// - Überprüfe, dass die Funktion nicht virtuell ist
//   - Eine virtuelle Funktion, die von Unterklassen überschrieben wird, können
//     wir nicht entfernen, ohne die Semantik des Programms zu ändern
// - Finde alle Aufrufe der Funktion
// - Ersetze jeden Aufruf durch den Rumpf der Funktion
// - Test nach jedem Schritt!
// - Entferne die Funktionsdefinition
// - Brich ab, falls Schwierigkeiten wegen Rekursion, mehrerer
//   `return`-Anweisungen, etc. auftreten

// %% [markdown]
//
// ## Workshop: Inline Function
//
// In der folgenden Klasse wurden Hilfsfunktionen verwendet, die die Berechnung
// nicht übersichtlicher gestalten.
//
// Entfernen Sie diese Hilfsfunktionen mit dem *Inline Function*-Refactoring.

// %%
public static class SimpleMathOperations
{
    public static int Add(int a, int b)
    {
        return a + b;
    }

    public static int Subtract(int a, int b)
    {
        return a - b;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public static int Calculate(int x, int y, int z)
    {
        int result = Add(x, y);
        result = Multiply(result, 2);
        result = Subtract(result, z);
        return result;
    }

    public static void Run()
    {
        Console.WriteLine(Calculate(5, 3, 4));
    }
}

// %%
SimpleMathOperations.Run();

// %%

// %%
SimpleMathOperationsV1.Run();

// %%

// %%
SimpleMathOperationsV2.Calculate(5, 3, 4);
