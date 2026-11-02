// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Refactoring: Extrahiere Funktion</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
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
// ### Code Smell: Lange Funktion
//
// - Extract Function ist das wichtigste Refactoring gegen den **Lange
//   Funktion**-Smell
// - Hinweise: Kommentare wie "Berechne...", "Drucke...", "Validiere..."
//   zeigen mögliche Extraktionen an
// - Tipp: Nutzen Sie die IDE (Ctrl+Alt+M in Rider, Ctrl+. in VS) für
//   sicheres Extrahieren

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
