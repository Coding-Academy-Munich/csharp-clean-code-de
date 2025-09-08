// -*- coding: utf-8 -*-
// %% [markdown]
// <!--
// clang-format off
// -->
//
// <div style="text-align:center; font-size:200%;">
//  <b>SOLID: Liskov-Substitutions-Prinzip</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # SOLID: Liskov Substitutions-Prinzip
//
// Ein Objekt einer Unterklasse soll immer für ein Objekt der Oberklasse
// eingesetzt werden können.

// %% [markdown]
//
// ## LSP Verletzung

// %%
public class Rectangle
{
    public virtual double Length { get; set; }
    public virtual double Width { get; set; }

    public double Area => Length * Width;
}

// %%
public class Square : Rectangle
{
    public override double Length
    {
        get => side;
        set => side = value;
    }

    public override double Width
    {
        get => side;
        set => side = value;
    }

    private double side;
}

// %%
Rectangle r = new Rectangle { Length = 2, Width = 3 };

// %%
r

// %%
Square s = new Square { Length = 2, Width = 3 };

// %%
s

// %%
new Square { Width = 3, Length = 2 }

// %%
r.Length = 4;
r.Width = 3;
r.Area

// %%
s.Length = 4;
s.Width = 3;
s.Area

// %%
r.Width = 3;
r.Length = 4;
r.Area

// %%
s.Width = 3;
s.Length = 4;
s.Area
