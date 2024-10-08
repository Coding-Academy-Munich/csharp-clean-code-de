// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Concrete Factory</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Adventure SpielVersion 3b
//
// <img src="img/adventure-v3b-overview.png" alt="Adventure Version 3c"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ### Adventure Spiel Version 3b
//
// - Zuweisung von Funktionalität zu `World` und `Location` ist sinnvoll
// - Wir sehen, dass `World` in Gefahr ist, zu viele "Änderungsgründe" zu haben
//   - Änderungen an der Implementierung der Spiele-Welt
//   - Änderungen an den Initialisierungsdaten (z.B. XML statt JSON)
// - Können wir das vermeiden?

// %% [markdown]
//
// # Concrete Factory (Simple Factory)
//
// ### Frage
//
// - Wer soll ein Objekt erzeugen, wenn es Umstände gibt, die gegen Creator
//   sprechen?
//   - komplexe Logik zur Erzeugung
//   - Kohäsion
//   - Viele Parameter zur Erzeugung notwendig
//
// ### Antwort
//
// - Eine Klasse, die nur für die Erzeugung von Objekten zuständig ist
// - Diese Klassen werden oft als *Factory* bezeichnet

// %% [markdown]
//
// - Factories sind Beispiele für das GRASP Pattern "Pure Fabrication"
// - Sie können die Kohäsion von Klassen erhöhen und ihre Komplexität reduzieren
// - Sie erhöhen aber auch die Gesamtkomplexität des Systems

// %% [markdown]
//
// ## Beispiel
//
// - In Version 3b ist der Konstruktor von `World` relativ komplex
// - Wir können die Erzeugung in eine Factory auslagern
// - Siehe `Code/Completed/GraspAdventure/AdventureV3c`

// %% [markdown]
//
// ## Version 3c: Factory
//
// <img src="img/adventure-v3c-overview.png" alt="Adventure Version 3c"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## Workshop: Adressbuch
//
// In diesem Workshop sollen Sie eine Adressbuchanwendung erstellen, die
// Kontaktinformationen aus einer Liste von kommaseparierten Werten (CSV)
// importieren kann. Jede Zeile in der CSV-Datei repräsentiert einen Kontakt.
//
// ### Anforderungen
//
// - Implementieren Sie eine `Contact`-Klasse mit Eigenschaften wie Name, E-Mail
//   und Telefonnummer.
// - Erstellen Sie eine `AddressBook`-Klasse, die mehrere `Contact`-Objekte
//   enthalten kann.
// - Entwerfen Sie eine Factory, die `AddressBook`-Objekte aus CSV-Strings erstellen
//   kann.
// - Verwenden Sie dazu das Concrete Factory Pattern.

// %%
using System;
using System.Collections.Generic;
using System.Linq;

// %%
public class Contact
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Email: {Email}, Phone: {PhoneNumber}";
    }
}

// %%
public class AddressBook
{
    private List<Contact> contacts;

    public AddressBook()
    {
        contacts = new List<Contact>();
    }

    public void AddContact(Contact contact)
    {
        contacts.Add(contact);
    }

    public List<Contact> GetContacts()
    {
        return contacts;
    }

    public override string ToString()
    {
        return string.Join("\n", contacts);
    }
}

// %%
public class AddressBookFactory
{
    public AddressBook CreateFromCsv(string csvString)
    {
        AddressBook addressBook = new AddressBook();

        string[] lines = csvString.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
            {
                string[] values = line.Split(',');
                if (values.Length == 3)
                {
                    Contact contact = new Contact
                    {
                        Name = values[0].Trim(),
                        Email = values[1].Trim(),
                        PhoneNumber = values[2].Trim()
                    };
                    addressBook.AddContact(contact);
                }
            }
        }

        return addressBook;
    }
}

// %%
string csvData = @"John Doe,john@example.com,123-456-7890
Jane Smith,jane@example.com,987-654-3210
Mike Johnson,mike@example.com,555-123-4567";

// %%
AddressBookFactory factory = new AddressBookFactory();
AddressBook addressBook = factory.CreateFromCsv(csvData);

// %%
System.Console.WriteLine(addressBook);
