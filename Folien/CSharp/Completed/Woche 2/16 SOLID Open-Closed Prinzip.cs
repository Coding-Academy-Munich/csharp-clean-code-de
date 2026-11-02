// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>SOLID: Open-Closed Prinzip</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Open-Closed Prinzip (SOLID)
//
// Klassen sollen
//
// - Offen für Erweiterung
// - Geschlossen für Modifikation
//
// sein.

// %%
public enum MovieKindV0
{
    Regular,
    Children
}

// %%
public class MovieV0
{
    public MovieV0(string title, MovieKindV0 kind)
    {
        Title = title;
        Kind = kind;
    }

    public string Title { get; }
    public MovieKindV0 Kind { get; }

    public decimal ComputePrice()
    {
        return Kind switch
        {
            MovieKindV0.Regular => 4.99m,
            MovieKindV0.Children => 5.99m,
            _ => 0.0m,
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {ComputePrice()}");
    }
}

// %%
MovieV0 m1 = new MovieV0("Casablanca", MovieKindV0.Regular);
MovieV0 m2 = new MovieV0("Shrek", MovieKindV0.Children);

// %%
m1.PrintInfo();
m2.PrintInfo();

// %% [markdown]
//
// <img src="img/movie_v0.png" alt="MovieV0"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// Was passiert, wenn wir eine neue Filmart hinzufügen wollen?

// %%
public enum MovieKind
{
    Regular,
    Children,
    NewRelease
}

// %%
public class MovieV1
{
    public MovieV1(string title, MovieKind kind)
    {
        Title = title;
        Kind = kind;
    }

    public string Title { get; }
    public MovieKind Kind { get; }

    public decimal ComputePrice()
    {
        return Kind switch
        {
            MovieKind.Regular => 4.99m,
            MovieKind.Children => 5.99m,
            MovieKind.NewRelease => 6.99m,
            _ => 0.0m,
        };
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {ComputePrice()}");
    }
}

// %%
MovieV1 m1 = new MovieV1("Casablanca", MovieKind.Regular);
MovieV1 m2 = new MovieV1("Shrek", MovieKind.Children);
// MovieV1 m3 = new MovieV1("Brand New", MovieKind.NewRelease);

// %%
m1.PrintInfo();
m2.PrintInfo();
// m3.PrintInfo();

// %% [markdown]
//
// <img src="img/movie_v1.png" alt="MovieV1"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## OCP-Verletzung
//
// - Neue Filmarten erfordern Änderungen an `MovieV1`
// - `MovieV1` ist nicht geschlossen für Modifikation

// %% [markdown]
//
// ## Auflösung (Versuch 1: Vererbung)
//
// - Neue Filmarten werden als neue Klassen implementiert
// - `MovieV2` wird abstrakt
// - `MovieV2` ist geschlossen für Modifikation

// %%
public abstract class MovieV2
{
    public MovieV2(string title)
    {
        Title = title;
    }

    public string Title { get; }

    public abstract decimal ComputePrice();

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {ComputePrice()}");
    }
}

// %%
public class RegularMovie : MovieV2
{
    public RegularMovie(string title) : base(title) { }

    public override decimal ComputePrice()
    {
        return 4.99m;
    }
}

// %%
public class ChildrenMovie : MovieV2
{
    public ChildrenMovie(string title) : base(title) { }

    public override decimal ComputePrice()
    {
        return 5.99m;
    }
}

// %%
public class NewReleaseMovie : MovieV2
{
    public NewReleaseMovie(string title) : base(title) { }

    public override decimal ComputePrice()
    {
        return 6.99m;
    }
}

// %%
MovieV2 m1 = new RegularMovie("Casablanca");
MovieV2 m2 = new ChildrenMovie("Shrek");
MovieV2 m3 = new NewReleaseMovie("Brand New");

// %%
m1.PrintInfo();
m2.PrintInfo();
m3.PrintInfo();

// %% [markdown]
//
// <img src="img/movie_v2.png" alt="MovieV0"
//      style="display:block;margin:auto;width:100%"/>

// %% [markdown]
//
// - `MovieV2` ist offen für Erweiterung
// - Neue Filmarten können hinzugefügt werden, ohne die bestehenden Klassen zu
//   ändern
// - Aber: Die Vererbungshierarchie umfasst die ganze Klasse
//   - Nur eine Art von Variabilität
// - Was ist, wenn wir für andere Zwecke eine andere Klassifikation brauchen?
//   - Z.B. DVD, BluRay, Online?
// - Mehrfachvererbung?
// - Produkt von Klassen?
//   - `ChildrenDVD`, `ChildrenBluRay`, `ChildrenOnline`, ...

// %% [markdown]
//
// ## Bessere Auflösung: Strategy Pattern
//
// - Das Strategy-Pattern erlaubt es uns, die Vererbung auf kleinere Teile der
//   Klasse anzuwenden
// - In fast allen Fällen ist das die bessere Lösung!
// - Vererbung ist ein sehr mächtiges Werkzeug
// - Aber je kleiner und lokaler wir unsere Vererbungshierarchien halten, desto
//   besser
