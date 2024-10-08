// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF: Strategy Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ### Zweck
//
// - Austauschbare Algorithmen / austauschbares Verhalten
// - Algorithmen unabhängig von Klassen, die sie verwenden

// %% [markdown]
//
// ### Auch bekannt als
//
// Policy

// %% [markdown]
//
// ### Motivation
//
// - Wir wollen einen Text in einem Feld mit begrenzter Breite darstellen
// - Dafür gibt es verschiedene Möglichkeiten:
//   - Abschneiden nach einer bestimmten Anzahl von Zeichen (mit/ohne Ellipse)
//   - Umbruch nach einer bestimmten Anzahl von Zeichen
//     - Umbruch mitten im Wort
//     - Umbruch bei Leerzeichen (greedy/dynamische Programmierung)

// %% [markdown]
//
// ## Struktur
//
// <img src="img/pat_strategy.png"
//      style="display:block;margin:auto;width:80%"/>

// %% [markdown]
//
// ## Teilnehmer
//
// - `Strategy`
//   - gemeinsames Interface für alle unterstützten Algorithmen
// - `ConcreteStrategy`
//   - implementiert den Algorithmus
// - `Context`
//   - wird mit einem `ConcreteStrategy`-Objekt konfiguriert
//   - kennt sein `Strategy`-Objekt
//   - optional: Interface, das der Strategie Zugriff die Kontext-Daten ermöglicht

// %%
using System;

// %%
public interface IStrategy
{
    double AlgorithmInterface();
}

// %%
public class Context
{
    private IStrategy _strategy;

    public Context(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public double ContextInterface()
    {
        return _strategy.AlgorithmInterface();
    }

    public void SetStrategy(IStrategy strategy)
    {
        _strategy = strategy;
    }
}

// %%
public class ConcreteStrategyA : IStrategy
{
    public double AlgorithmInterface()
    {
        return 1.5f;
    }
}
// %%
public class ConcreteStrategyB : IStrategy
{
    public double AlgorithmInterface()
    {
        return 2.0f;
    }
}

// %%
using System;
using System.Collections.Generic;

// %%
Context context = new Context(new ConcreteStrategyA());

// %%
Console.WriteLine("Strategy A: " + context.ContextInterface());

// %%
context.SetStrategy(new ConcreteStrategyB());

// %%
Console.WriteLine("Strategy B: " + context.ContextInterface());

// %% [markdown]
//
// ### Interaktionen
//
// - Strategie und Kontext interagieren, um den gewählten Algorithmus zu implementieren.
//   - Kontext kann Daten an Strategie übergeben
//   - Kontext kann sich selber an Strategie übergeben
// - Ein Kontext leitet Anfragen seiner Clients an seine Strategie weiter. [...]

// %% [markdown]
//
// ### Implementierung
//
// - `ConcreteStrategy` benötigt effizienten Zugriff auf alle benötigten Daten
// - ...

// %% [markdown]
//
// ## Beispielcode: Textumbruch für ein Blog

// %%
using System;
using System.Collections.Generic;

public interface ITextWrapStrategy
{
    List<string> Wrap(string text, int width);
}

// %%
public class TruncationStrategy : ITextWrapStrategy
{
    public List<string> Wrap(string text, int width)
    {
        if (text.Length <= width)
        {
            return new List<string> { text };
        }
        return new List<string> { text.Substring(0, width - 3) + "..." };
    }
}

// %%
public class BreakAnywhereStrategy : ITextWrapStrategy
{
    public List<string> Wrap(string text, int width)
    {
        string remainingText = text;
        List<string> lines = new List<string>();
        while (remainingText.Length > width)
        {
            lines.Add(remainingText.Substring(0, width));
            remainingText = remainingText.Substring(width);
        }
        lines.Add(remainingText);
        return lines;
    }
}
// %%
public class BreakOnSpaceStrategy : ITextWrapStrategy {
    public List<string> Wrap(string text, int width) {
        List<string> lines = new List<string>();
        string remainingText = text;

        while (remainingText.Length > width) {
            int pos = remainingText.LastIndexOf(' ', width);
            if (pos == -1) {
                pos = width;
            }
            lines.Add(remainingText.Substring(0, pos));
            remainingText = remainingText.Substring(pos + 1);
        }

        lines.Add(remainingText);
        return lines;
    }
}

// %%
public class BlogPost {
    public string Author { get; }
    public string Title { get; }
    public string Text { get; }

    public BlogPost(string author, string title, string text) {
        Author = author;
        Title = title;
        Text = text;
    }
}

// %%
public class Blog {
    private readonly List<BlogPost> posts = new List<BlogPost>();
    private ITextWrapStrategy strategy;

    public Blog(ITextWrapStrategy strategy) {
        this.strategy = strategy;
    }

    public void Print(int width) {
        foreach (var post in posts) {
            Console.WriteLine(new string('-', width));
            Console.WriteLine("Title: " + post.Title);
            Console.WriteLine("Author: " + post.Author);
            foreach (var line in strategy.Wrap(post.Text, width)) {
                Console.WriteLine(line);
            }
            Console.WriteLine(new string('-', width));
        }
    }

    public void AddPost(BlogPost post) {
        posts.Add(post);
    }

    public void SetStrategy(ITextWrapStrategy strategy) {
        this.strategy = strategy;
    }
}

// %%
string firstPost = "The quick brown fox jumps over the lazy dog. Lorem ipsum dolor sit amet, "
                    + "consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et "
                    + "dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco "
                    + "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in "
                    + "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. "
                    + "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt "
                    + "mollit anim id est laborum.";
string secondPost = "To be or not to be that is the question. Whether 'tis nobler in the mind to suffer "
                    + "the slings and arrows of outrageous fortune or to take arms against a sea of "
                    + "troubles and by opposing end them. To die, to sleep no more and by a sleep to say we "
                    + "end the heart-ache and the thousand natural shocks that flesh is heir to. 'Tis a "
                    + "consummation devoutly to be wish'd. To die, to sleep to sleep perchance to dream. "
                    + "Ay, there's the rub. For in that sleep of death what dreams may come when we have "
                    + "shuffled off this mortal coil must give us pause.";

// %%
Blog blog = new Blog(new TruncationStrategy());

// %%
blog.AddPost(new BlogPost("John Doe", "My first post", firstPost));
blog.AddPost(new BlogPost("Jane Doe", "My second post", secondPost));

// %%
blog.Print(40);

// %%
blog.SetStrategy(new BreakAnywhereStrategy());

// %%
blog.Print(40);

// %%
blog.SetStrategy(new BreakOnSpaceStrategy());

// %%
blog.Print(40);

// %% [markdown]
//
// ### Anwendbarkeit
//
// - Konfiguration von Objekten mit einer von mehreren Verhaltensweisen
// - Verschiedene Varianten eines Algorithmus
// - Kapseln von Daten mit Algorithmus (Client muss Daten nicht kennen)
// - Vermeidung von bedingten Anweisungen zur Auswahl eines Algorithmus

// %% [markdown]
//
// ### Konsequenzen
//
// - Familien wiederverwendbarer, verwandter Algorithmen
// - Alternative zu Vererbung
// - Auswahl einer Strategie ohne bedingte Anweisungen
// - Context/Clients muss die möglichen Strategien kennen
// - Kommunikations-Overhead zwischen Strategie und Kontext
// - Erhöhte Anzahl von Objekten

// %% [markdown]
//
// ### C# Implementierungs-Tipp
//
// In C# kann das Strategy Pattern oft einfach durch ein Funktions-Objekt als
// Member implementiert werden:

// %%
using System;
using System.Collections.Generic;
using System.Linq;

public class FunBlog
{
    private List<BlogPost> _posts = new List<BlogPost>();
    private Func<string, int, List<string>> _strategy;

    public FunBlog(Func<string, int, List<string>> strategy)
    {
        _strategy = strategy;
    }

    public void Print(int width)
    {
        foreach (var post in _posts)
        {
            Console.WriteLine(new string('-', width));
            Console.WriteLine("Title: " + post.Title);
            Console.WriteLine("Author: " + post.Author);
            foreach (var line in _strategy(post.Text, width))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine(new string('-', width));
        }
    }

    public void AddPost(BlogPost post)
    {
        _posts.Add(post);
    }

    public void SetStrategy(Func<string, int, List<string>> strategy)
    {
        _strategy = strategy;
    }
}

// %%
public class Fun1
{
    public static List<string> TruncateLines(string text, int width)
    {
        if (text.Length <= width)
        {
            return new List<string> { text };
        }
        return new List<string> { text.Substring(0, width - 3) + "..." };
    }
}
// %% [markdown]
//
// - Hier haben wir eine Funktion `TruncateLines()` definiert, die die gleiche
//   Funktionalität hat wie unsere `TruncationStrategy`

// %%
FunBlog blog = new FunBlog(Fun1.TruncateLines);

// %%
blog.AddPost(new BlogPost("John Doe", "My first post", firstPost));
blog.AddPost(new BlogPost("Jane Doe", "My second post", secondPost));

// %%
blog.Print(40);

// %%
blog.SetStrategy((text, width) => {
    if (text.Length <= width) {
        return new List<string> { text };
    }
    return new List<string> { text.Substring(0, width - 3) + "..." };
});

// %%
blog.Print(40);

// %% [markdown]
//
// ## Mini-Workshop: Vorhersagen
