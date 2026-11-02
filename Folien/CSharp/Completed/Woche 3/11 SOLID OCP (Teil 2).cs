// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>SOLID: OCP (Teil 2)</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Wiederholung: OCP-Verletzung
//
// <img src="img/movie_v0.png" alt="MovieV0"
//      style="display:block;margin:auto;width:50%"/>

// %% [markdown]
//
// ## Lösungsversuch 1: Vererbung
//
// <img src="img/movie_v2.png" alt="MovieV2"
//      style="display:block;margin:auto;width:70%"/>

// %% [markdown]
//
// - OCP ist erfüllt
// - Großer Scope der Vererbung
//   - Preisberechnung ist das wichtigste an Filmen?
// - Nur eindimensionale Klassifikation
// - Keine Möglichkeit, Preisschema zu wechseln

// %% [markdown]
//
// ## Lösungsversuch 2: Strategie-Muster
//
// <img src="img/movie_v3.png" alt="MovieV3"
//      style="display:block;margin:auto;width:80%"/>

// %% [markdown]
//
// - OCP ist erfüllt
// - Vererbung ist auf die Preisberechnung beschränkt
// - Mehrdimensionale Klassifikation ist einfach
// - Preisschema kann zur Laufzeit gewechselt werden

// %% [markdown]
//
// ## Implementierung

// %%
public interface IMovie
{
    decimal Price { get; }
    void PrintInfo();
}

// %%
public interface IPricingStrategy
{
    decimal ComputePrice(IMovie movie);
}

// %%
public class RegularPricingStrategy : IPricingStrategy
{
    public decimal ComputePrice(IMovie movie)
    {
        return 4.99m;
    }
}

// %%
public class ChildrenPricingStrategy : IPricingStrategy
{
    public decimal ComputePrice(IMovie movie)
    {
        return 5.99m;
    }
}

// %%
public class Movie : IMovie
{
    public Movie(string title, IPricingStrategy pricingStrategy)
    {
        Title = title;
        PricingStrategy = pricingStrategy;
    }

    public string Title { get; }
    public IPricingStrategy PricingStrategy { get; set; }
    public decimal Price => PricingStrategy.ComputePrice(this);

    public void PrintInfo()
    {
        Console.WriteLine($"{Title} costs {Price}");
    }
}

// %%
List<IMovie> movies = [
    new Movie("Casablanca", new RegularPricingStrategy()),
    new Movie("Shrek", new ChildrenPricingStrategy()),
];

// %%
movies.ForEach(m => m.PrintInfo());

// %%
public class NewReleasePricingStrategy : IPricingStrategy
{
    public decimal ComputePrice(IMovie movie)
    {
        return 6.99m;
    }
}

// %%
var godfather = new Movie("The Godfather", new NewReleasePricingStrategy());

// %%
movies.Add(godfather);

// %%
movies.ForEach(m => m.PrintInfo());

// %%
godfather.PricingStrategy = new RegularPricingStrategy();

// %%
movies.ForEach(m => m.PrintInfo());
