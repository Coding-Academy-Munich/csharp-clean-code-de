// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Bibliothekssystem: Einführung</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Bibliotheks-Verwaltungssystem
//
// - System zur Verwaltung von Benutzern und Medienbestand in Bibliotheken
// - Sowohl für Bibliothekare als auch Benutzer
// - Aktivitäten: Registrierung, Suche, Ausleihe, Rückgabe, Strafzahlungen
// - Suche von Medien in anderen Bibliotheken und Online-Repositories
// - Empfehlungen für Benutzer
// - Verschiedene Oberflächen (Web, App, Terminal)

// %% [markdown]
//
// - Wie könnte das Domänenmodell für ein solches System aussehen?
// - Welche Konzepte gibt es?
// - Welche Use-Cases/Anwendungsfälle gibt es?

// %% [markdown]
//
// ## Domänenmodell: Konzepte
//
// - Medien
//   - Bücher, Videos, Musik, ...
//   - Unterschiedliche Metadaten für verschiedene Medien
// - Benutzer
//   - Mitglieder, Besucher
//   - verschiedene Typen von Mitgliedern: Kinder, Studenten, Senioren, ...
//   - Unterschiedliche Privilegien
//   - Aktivitäten: Ausleihen von Medien, Rückgabe, Suche, Strafzahlungen
// - Bibliothekare
//   - Verwalten von Benutzern und Medien
//   - Aktivitäten: Registrierung, Suche, Ausleihe, Rückgabe, Strafzahlungen

// %% [markdown]
//
// ## Aktivitäten (Bibliothekar)
//
// - Verwalten von Mitgliedern (Registrierung, Löschen, Suche, Modifikation,
//   ...)
// - Verwaltung von Medien (Hinzufügen, Löschen, Suche, Modifikation ...)
// - Ausleihen und Rückgabe von Medien
// - Veranlassen von Erinnerungen, Strafzahlungen
// - Anzeige bisheriger Aktivitäten (Hinzufügen, Ausleihen, ...) für alle
//   Benutzer
// - Anzeige von Aktivitäten für alle Medien

// %% [markdown]
//
// ## Aktivitäten (Benutzer)
//
// - Registrierung, Abmeldung, Mitteilung von Adressänderungen
// - Suche nach Medien
// - Ausleihen und Rückgabe von Medien (Benutzer-Seite)
// - Anzeige der bisherigen Aktivitäten (Ausleihen, Rückgaben, Strafzahlungen)
//   für den Benutzer

// %% [markdown]
//
// ## Workshop: Bibliothekssystem (Setup)
//
// Starter Kit: `Code/StarterKits/LibrarySk`
//
// - Versuchen Sie das Starter Kit zu kompilieren und auszuführen.
// - Fügen Sie eine Klasse `Book` hinzu, die ein Attribut `title` hat.
// - Fügen Sie einen Getter für das Attribut `title` hinzu.
// - Schreiben Sie einen Test, der überprüft, dass der Getter funktioniert.
// - Erstellen Sie ein Buch im Hauptprogramm und geben Sie den Titel aus.
// - Entfernen Sie die Dummy-Klasse `DeleteMe` und die Tests dieser Klasse.
//   Stellen Sie sicher, dass Sie das Projekt immer noch bauen und das
//   Hauptprogramm und die Tests ausführen können.
//
// **Hinweis**: Das ist natürlich kein sinnvoller Test, er dient nur dazu, dass
// Sie mit der Infrastruktur, die wir in diesem Kurs verwenden, vertraut werden.

// %% [markdown]
//
// ## Workshop: Bibliotheks-Verwaltungssystem (Teil 1)
//
// - Entwickeln Sie ein erstes Domänenmodell für das Bibliotheks-Verwaltungssystem
//   - Sie können z.B. ein Klassendiagramm verwenden oder einfach nur eine Liste
//     von Klassen und Attributen
// - Welche Klassen in Ihrem Domänenmodell haben Assoziationen zu
//   - Mitgliedern?
//   - Büchern?

// %% [markdown]
//
// - Verwenden Sie das Creator Pattern um zu entscheiden, welche Klasse die
//   Verantwortung für das Erstellen von Mitgliedern und welche die Verantwortung
//   für das Erstellen von Büchern hat
// - Verwenden Sie das Information Expert Pattern um zu entscheiden, welche Klasse
//   die Verantwortung für das Suchen von Mitgliedern und welche die Verantwortung
//   für das Suchen von Büchern hat
// - Implementieren Sie diesen Teil des Domänenmodells in C#
// - Versuchen Sie dabei das Prinzip der niedrigen Repräsentationslücke anzuwenden

// %%
using System;
using System.Collections.Generic;

// %%
public class Member
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Email { get; private set; }

    public Member(string name, string address, string email)
    {
        Name = name;
        Address = address;
        Email = email;
    }

    public override string ToString()
    {
        return $"Member({Name}, {Address}, {Email})";
    }
}

// %%
public class Book
{
    public string Title { get; private set; }
    public string Isbn { get; private set; }

    public Book(string title, string isbn)
    {
        Title = title;
        Isbn = isbn;
    }

    public override string ToString()
    {
        return $"Book({Title}, {Isbn})";
    }
}

// %%
public class LibrarySystem
{
    private List<Member> members;
    private List<Book> books;

    public LibrarySystem()
    {
        members = new List<Member>();
        books = new List<Book>();
    }

    public override string ToString()
    {
        var result = "Members:\n";
        foreach (var member in members)
        {
            result += $"  {member.Name}\n";
        }
        result += "Books:\n";
        foreach (var book in books)
        {
            result += $"  {book.Title}\n";
        }
        return result;
    }

    public void AddMember(string name, string address, string email)
    {
        var member = new Member(name, address, email);
        members.Add(member);
    }

    public void AddBook(string title, string isbn)
    {
        var book = new Book(title, isbn);
        books.Add(book);
    }

    public Member FindMember(string name)
    {
        return members.Find(member => member.Name == name);
    }

    public Book FindBook(string title)
    {
        return books.Find(book => book.Title == title);
    }
}

// %%
var library = new LibrarySystem();

// %%
library.AddMember("Max Mustermann", "Musterstraße 1", "max@example.com");

// %%
library.AddBook("Design Patterns", "978-0-20163-361-0");

// %%
library.FindMember("Max Mustermann");

// %%
library.FindBook("Design Patterns");

// %%
Console.WriteLine(library);
