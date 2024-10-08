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
    protected double length;
    protected double width;

    public Rectangle(double l, double w)
    {
        this.length = l;
        this.width = w;
    }

    public double Area()
    {
        return length * width;
    }

    public double Length
    {
        get { return length; }
        set { length = value; }
    }

    public double Width
    {
        get { return width; }
        set { width = value; }
    }
}

// %%
public class Square : Rectangle
{
    public Square(double l) : base(l, l) { }

    public new double Length
    {
        get { return length; }
        set
        {
            length = value;
            width = value;
        }
    }

    public new double Width
    {
        get { return width; }
        set
        {
            length = value;
            width = value;
        }
    }
}

// %%
public static void ResizeRectangle(Rectangle r)
{
    r.Length = 4;
    r.Width = 5;
    Console.WriteLine($"Length: {r.Length}, Width: {r.Width}, Area: {r.Area()}");
}

// %%
Rectangle r = new Rectangle(3, 4);
ResizeRectangle(r);

// %%
Square s = new Square(3);
ResizeRectangle(s);
