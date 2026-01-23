// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Refactoring: Extrahiere Funktion</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Extrahiere Funktion
//
// - Anwendung:
//   - Funktion ist zu groß
//   - Funktion ist zu komplex
//   - Funktion ist schwer zu verstehen
//   - Funktion hat zu viele Aufgaben
// - Invers zu *Inline Function*

// %%
using System;
using System.Collections.Generic;

// %%
public class OrderLine
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

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
public static List<OrderLine> MakeOrderLines()
{
    List<OrderLine> orderLines = new List<OrderLine>();
    orderLines.Add(new OrderLine("Apple", 2, 0.5m));
    orderLines.Add(new OrderLine("Banana", 3, 0.3m));
    orderLines.Add(new OrderLine("Orange", 1, 0.8m));
    return orderLines;
}

// %% [markdown]
//
// ## Beispiel

// %%
using System;
using System.Collections.Generic;

// %%
public class ReceiptPrinter
{
    public static void PrintReceipt(List<OrderLine> orderLines)
    {
        // Print order lines
        foreach (var orderLine in orderLines)
        {
            Console.WriteLine($"{orderLine.Product,-12} {orderLine.Quantity,4} x {orderLine.Price,6:F2}€");
        }
        // Compute total
        decimal total = 0.0m;
        foreach (var orderLine in orderLines)
        {
            total += orderLine.Quantity * orderLine.Price;
        }
        // Print total
        Console.WriteLine($"Total: {total:F2}€");
    }
}

// %%
ReceiptPrinter.PrintReceipt(MakeOrderLines());

// %%
public class ReceiptPrinter
{
    public static void PrintReceipt(List<OrderLine> orderLines)
    {
        // Print order lines
        foreach (var orderLine in orderLines)
        {
            Console.WriteLine($"{orderLine.Product,-12} {orderLine.Quantity,4} x {orderLine.Price,6:F2}€");
        }
        // Compute total
        decimal total = 0.0m;
        foreach (var orderLine in orderLines)
        {
            total += orderLine.Quantity * orderLine.Price;
        }
        // Print total
        Console.WriteLine($"Total: {total:F2}€");
    }
}

// %%
ReceiptPrinter.PrintReceipt(MakeOrderLines());

// %% [markdown]
//
// #### Motivation
//
// - Jedes Code-Fragment, das man nicht unmittelbar versteht, sollte in eine
//   Funktion extrahiert werden
// - Name der Funktion spiegelt wieder, **was** die Intention des Codes ist
// - Das kann zu Funktionen führen, die nur eine Zeile Code enthalten
// - Es ist dabei besonders wichtig, dass die Funktionen **gute** Namen haben
// - Kommentare in Funktionen, die sagen, was der nächste Code-Block macht,
//   sind ein Hinweis auf eine mögliche Extraktion

// %% [markdown]
//
// #### Mechanik
//
// - Erzeuge eine neue Funktion
//   - Benenne sie nach der Intention des Codes
// - Kopiere den extrahierten Code in die neue Funktion
// - Übergebe alle Variablen, die die Funktion benötigt, als Parameter
//   - Wenn vorher deklarierte Variablen nur in der neuen Funktion verwendet
//     werden, können sie auch als lokale Variablen in der neuen Funktion
//     deklariert werden
// - Breche die Extraktion ab, falls die Funktion zu viele Parameter bekommt
//   - Wende stattdessen andere Refactorings an, die die Extraktion einfacher
//     machen (Split Variables, Replace Temp with Query, ...)

// %% [markdown]
//
// ### Beispiel für fehlgeschlagene Extraktion
//
// - Siehe `Code/Completed/Invoice/V2` für ein Beispiel einer Extraktion,
//   die abgebrochen wird
//   - IntelliJ kommt beim Refactoring dieser Funktion in eine Endlosschleife
//     und erzeugt ungültigen Code, wenn man sie abbricht
// - Siehe `Code/Completed/Invoice/V3` für eine erfolgreiche
//   Extraktion (nach etwas Refactoring)

// %% [markdown]
//
// ## Workshop: Extrahiere Funktion
//
// - Wenden Sie das "Extrahiere Funktion"-Refactoring auf die Methode `processUserData`
//   an:

// %%
using System;
using System.Collections.Generic;
using System.Linq;

// %%
public class UserDataProcessor
{
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public static void ProcessUserData(string userData)
    {
        // Convert string data to User objects
        string[] userStrings = userData.Split(';');
        List<User> users = new List<User>();
        foreach (string userString in userStrings)
        {
            string[] parts = userString.Split(',');
            users.Add(new User(parts[0], int.Parse(parts[1])));
        }

        // Calculate average age
        int totalAge = 0;
        foreach (User user in users)
        {
            totalAge += user.Age;
        }
        double averageAge = (double)totalAge / users.Count;

        // Print report
        Console.WriteLine("User Report:");
        foreach (User user in users)
        {
            Console.WriteLine($"{user.Name} is {user.Age} years old");
        }
        Console.WriteLine($"Average age: {averageAge:F1} years");
    }

    public static void Run()
    {
        string userData = "Alice,30;Bob,25;Charlie,35";
        ProcessUserData(userData);
    }
}

// %%

// %%

// %%

// %%
