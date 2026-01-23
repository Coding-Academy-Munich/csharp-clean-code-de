// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Was sind gute Tests?</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// <img src="img/velocity-tests-03.png"
//      alt="Velocity vs. Tests 3"
//      style="width: 75%; margin-left: auto; margin-right: auto;"/>

// %% [markdown]
//
// ## Welche Eigenschaften sollte ein Test haben?
//
// <ul>
// <li class="fragment">Zeigt viele Fehler/Regressionen im Code auf</li>
// <li class="fragment">Gibt schnelle Rückmeldung</li>
// <li class="fragment">Ist deterministisch</li>
// <li class="fragment">Ist leicht zu warten und verstehen</li>
// <li class="fragment"><b>Unempfindlich gegenüber Refactorings</b></li>
// </ul>
//
// <p class="fragment">
//   Leider stehen diese Eigenschaften oft im Konflikt zueinander!
// </p>

// %% [markdown]
//
// ## Aufzeigen von Fehlern/Regressionen
//
// ### Einflüsse
//
// <ul>
//   <li class="fragment">Menge des abgedeckten Codes</li>
//   <li class="fragment">Komplexität des abgedeckten Codes</li>
//   <li class="fragment">Interaktion mit externen Systemen</li>
//   <li class="fragment">Signifikanz des abgedeckten Codes für die Domäne/das
//   System</li>
// </ul>

// %%
public class Item
{
    public Item(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; init; }

    public decimal Price {
        get { return price; }
        set { value = (value < 0m) ? -value : value; price = value; }
    }

    public override string ToString()
    {
        return string.Format("Item({0}, {1:F2})", Name, Price);
    }

    private decimal price; // always positive
}

// %%
using System;
using System.Collections.Generic;

// %%
public class Order
{
    private List<Item> items;

    public Order(List<Item> itemList)
    {
        items = new List<Item>(itemList);
    }

    public List<Item> Items { get { return items; } }

    public decimal Total
    {
        get
        {
            decimal total = 0m;
            foreach (Item item in items)
            {
                total += item.Price;
            }
            return total;
        }
    }

    public override string ToString()
    {
        string itemsString = string.Join(", ", items.Select(item => item.ToString()));
        return $"Order([{itemsString}]), total = {Total:F2}";
    }
}

// %%
void Check(bool condition, string message)
{
    if (!condition)
    {
        Console.Error.WriteLine(message);
    }
    else
    {
        Console.WriteLine("Success.");
    }
}

// %%

// %%

// %%

// %%

// %%

// %%

// %% [markdown]
//
// ## Schnelle Rückmeldung
//
// ### Einflüsse
//
// - Menge des abgedeckten Codes
// - Komplexität/Anzahl Iterationen des abgedeckten Codes
// - Interaktion mit externen Systemen

// %% [markdown]
//
// ## Deterministisch
//
// <ul>
//   <li class="fragment">Gleicher Code führt immer zum gleichen Ergebnis</li>
//   <li class="fragment">Gründe für Nichtdeterminismus
//     <ul>
//       <li class="fragment">Zufallszahlen</li>
//       <li class="fragment">Zeit/Datum</li>
//       <li class="fragment">Interaktion mit externen Systemen</li>
//       <li class="fragment">Nicht initialisierte Variablen</li>
//       <li class="fragment">Kommunikation zwischen Tests</li>
//     </ul>
//   </li>
//   <li class="fragment">
//      Tests, die falsche Warnungen erzeugen sind nicht
//      hilfreich sondern schädlich!
//   </li>
// <ul>

// %%
using System;

// %%
void TestRandomBad()
{
    Random random = new Random();
    int roll = random.Next(1, 3);

    Check(roll == 2, "Roll is not 2");
}

// %%

// %%
void TestRandomBetter()
{
    Random random = new Random();
    int roll = random.Next(1, 3);

    Check(roll == 2, "Roll is not 2");
}

// %%

// %%
void TestDateBad()
{
    DateTime now = DateTime.Now;

    Check(now.Year == 2024, "Year is not 2024");
    Check(now.Second % 2 == 0, "Second is not even");
}

// %%

// %%
void TestDateBetter()
{
    DateTime now = DateTime.Now;

    Check(now.Year == 2024, "Year is not 2024");
    Check(now.Second % 2 == 0, "Second is not even");
}

// %%

// %% [markdown]
//
// ## Leicht zu warten
//
// <ul>
//   <li>Einfache, standardisierte Struktur<br>&nbsp;<br>
//     <table style="display:inline;margin:20px 20px;">
//     <tr><td style="text-align:left;width:60px;padding-left:15px;">Arrange</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid black;">Given</td>
//         <td style="text-align:left;width:600px;padding-left:15px;border-left:1px solid black;">
//           Bereite das Test-Environment vor</td></tr>
//     <tr><td style="text-align:left;padding-left:15px;">Act</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid black;">
//            When</td>
//         <td style="text-align:left;width:600px;padding-left:15px;border-left:1px solid black;">
//            Führe die getestete Aktion aus (falls vorhanden)</td></tr>
//     <tr><td style="text-align:left;padding-left:15px;">Assert</td>
//         <td style="text-align:left;width:60px;padding-left:15px;border-left:1px solid black;">
//            Then</td>
//         <td style="text-align:left;width:600px;padding-left:15px;border-left:1px solid black;">
//            Überprüfe die Ergebnisse</td></tr>
//     </table>
//     <br>&nbsp;<br>
//   </li>
//   <li>Wenig Code
//     <ul>
//       <li>Wenig Boilerplate</li>
//       <li>Factories, etc. für Tests</li>
//     </ul>
//   </li>
// </ul>

// %% [markdown]
//
// ## Unempfindlich gegenüber Refactorings
//
// - Möglichst wenige falsche Positive!
// - Typischerweise vorhanden oder nicht, wenig Zwischenstufen
//
// ### Einflüsse
//
// - Bezug zu Domäne/System
// - Zugriff auf interne Strukturen

// %%
#pragma warning disable CS0414
public class VeryPrivate
{
    private int secret = 42;
}
#pragma warning restore CS0414

// %%
using System.Reflection;

// %%
public void TestVeryPrivate()
{
    VeryPrivate unit = new VeryPrivate();

    Type veryPrivateType = unit.GetType();
    FieldInfo secretField = veryPrivateType.GetField("secret", BindingFlags.NonPublic | BindingFlags.Instance);
    int secretValue = (int)secretField.GetValue(unit);

    Check(secretValue == 42, "Secret is not 42");
}

// %%

// %% [markdown]
//
// Die folgenden Einflüsse stehen im Konflikt zueinander:
//
// - Erkennen von Fehlern/Regressionen
// - Schnelle Rückmeldung
// - Unempfindlich gegenüber Refactorings
//
// Die Qualität eines Tests hängt vom *Produkt* dieser Faktoren ab!

// %% [markdown]
//
// ## Wie finden wir den Trade-Off?
//
// - Unempfindlich gegenüber Refactorings kann *nie* geopfert werden
// - Wir müssen also einen Kompromiss finden zwischen
//   - Erkennen von Fehlern/Regressionen
//   - Schnelle Rückmeldung
//
// ### Typischerweise
//
// - Schnelles Feedback für die meisten Tests (Unit-Tests)
// - Gründliche Fehlererkennung für wenige Tests (Integrationstests)
