// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Strategy Pattern: Funktionale Variante und Workshop</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

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
    public FunBlog(Func<string, int, List<string>> strategy)
    {
        Strategy = strategy;
    }

    public Func<string, int, List<string>> Strategy { get; set;}

    public void Print(int width)
    {
        foreach (var post in _posts)
        {
            Console.WriteLine(new string('-', width));
            Console.WriteLine("Title: " + post.Title);
            Console.WriteLine("Author: " + post.Author);
            foreach (var line in Strategy(post.Text, width))
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

    private List<BlogPost> _posts = new List<BlogPost>();
}

// %%
public class Wrap
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
FunBlog blog = new FunBlog(Wrap.TruncateLines);

// %%
blog.AddPost(new BlogPost("John Doe", "My first post", firstPost));
blog.AddPost(new BlogPost("Jane Doe", "My second post", secondPost));

// %%
blog.Print(40);

// %%
blog.Strategy = (text, width) => {
    if (text.Length <= width)
    {
        return new List<string> { text };
    }
    return [text.Substring(0, width - 3) + "..." ];
};

// %%
blog.Print(40);

// %% [markdown]
//
// ## Workshop: Vorhersagen
//
// Sie wollen ein System schreiben, das Vorhersagen für Aktienkurse treffen kann.
//
// Schreiben Sie dazu eine Klasse `Predictor` mit einer Methode
//
// ```csharp
// decimal Predict(List<decimal> values)
// ```
//
// Verwenden Sie das Strategy Pattern, um mindestens zwei verschiedene
// Vorhersage-Varianten zu ermöglichen:
//
// - Die Vorhersage ist der Mittelwert aller Werte aus `values`
// - Die Vorhersage ist der letzte Wert in `values` (oder 0, wenn `values` leer ist)
//
// Testen Sie Ihre Implementierung mit einigen Beispieldaten.

// %%
public interface IPredictionStrategy {
    decimal Predict(List<decimal> values);
}

// %%
public class LastValueStrategy : IPredictionStrategy {
    public decimal Predict(List<decimal> values) {
        return values.Count == 0 ? 0.0m : values[^1];
    }
}

// %%
public class MeanValueStrategy : IPredictionStrategy {
    public decimal Predict(List<decimal> values) {
        return values.Count == 0 ? 0.0m : values.Average();
    }
}

// %%
public class Predictor {
    public IPredictionStrategy Strategy { get; set; }

    public Predictor(IPredictionStrategy strategy) {
        Strategy = strategy;
    }

    public decimal Predict(List<decimal> values) {
        return Strategy.Predict(values);
    }

}

// %%
Predictor p = new Predictor(new MeanValueStrategy());
List<decimal> values = [ 1.0m, 2.0m, 3.0m ];

// %%
Console.WriteLine("Default prediction: " + p.Predict(values));

// %%
p.Strategy = new LastValueStrategy();
Console.WriteLine("Last value prediction: " + p.Predict(values));

// %%
p.Strategy = new MeanValueStrategy();
Console.WriteLine("Mean value prediction: " + p.Predict(values));

// %%
public static decimal Mean(List<decimal> values) {
    return values.Count == 0 ? 0.0m : values.Average();
}

// %%
public class PredictorFun {
    public Func<List<decimal>, decimal> Strategy { get; set; }

    public PredictorFun(Func<List<decimal>, decimal> strategy) {
        Strategy = strategy;
    }

    public decimal Predict(List<decimal> values) {
        return Strategy(values);
    }

}

// %%
PredictorFun pf = new PredictorFun(Mean);
List<decimal> myValues = [ 1.0m, 2.0m, 3.0m ];

// %%
Console.WriteLine("Default prediction: " + pf.Predict(myValues));

// %%
pf.Strategy = values => values.Count == 0 ? 0.0m : values[^1];
Console.WriteLine("Last value prediction: " + pf.Predict(myValues));

// %%
pf.Strategy = Mean;
Console.WriteLine("Mean value prediction: " + pf.Predict(myValues));

// %%
