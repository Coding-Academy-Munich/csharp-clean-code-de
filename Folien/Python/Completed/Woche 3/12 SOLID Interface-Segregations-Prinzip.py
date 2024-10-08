// -*- coding: utf-8 -*-
// %% [markdown]
// <!--
// clang-format off
// -->
//
// <div style="text-align:center; font-size:200%;">
//  <b>SOLID: Interface-Segregations-Prinzip</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## SOLID : Interface-Segregations-Prinzip
//
// - Kein Client einer Klasse `C` sollte von Methoden abhängen, die er nicht
//   benutzt.
// - Wenn das nicht der Fall ist
// - Unterteile die Schnittstelle von `C` in mehrere unabhängige Schnittstellen.
// - Ersetze `C` in jedem Client durch die vom Client tatsächlich verwendeten
//   Schnittstellen.

// %%
public class Car
{
    public void Drive()
    {
        Console.WriteLine("Accelerating.");
    }

    public void Repair()
    {
        Console.WriteLine("Repairing.");
    }
}

// %%
public class Driver
{
    public void Drive(Car car)
    {
        car.Drive();
    }
}

// %%
public class Mechanic
{
    public void WorkOn(Car car)
    {
        car.Repair();
    }
}

// %%
Driver d = new Driver();
Mechanic m = new Mechanic();
Car c = new Car();

// %%
d.Drive(c);

// %%
m.WorkOn(c);

// %%
public interface IDrivable
{
    void Drive();
}

// %%
public interface IRepairable
{
    void Repair();
}

// %%
public class Car2 : IDrivable, IRepairable
{
    public void Drive()
    {
        Console.WriteLine("Accelerating.");
    }

    public void Repair()
    {
        Console.WriteLine("Repairing.");
    }
}
// %%
public class Driver2
{
    public void Drive(IDrivable car)
    {
        car.Drive();
    }
}

// %%
public class Mechanic2
{
    public void WorkOn(IRepairable car)
    {
        car.Repair();
    }
}

// %%
Driver2 d2 = new Driver2();
Mechanic2 m2 = new Mechanic2();
Car2 c2 = new Car2();

// %%
d2.Drive(c2);

// %%
m2.WorkOn(c2);

// %% [markdown]
//
// ## Workshop:
//
// In diesem Workshop werden wir an einem Restaurant-Management-System arbeiten.
//
// Stellen Sie sich vor, Sie haben den Code eines Restaurant-Management-Systems
// erhalten. Das System hat derzeit eine einzige Schnittstelle
// `IRestaurantOperations`, die alle Operationen definiert, die in einem
// Restaurant durchgeführt werden können. Verschiedene Rollen im Restaurant, wie
// der Kunde, der Koch, der Kassierer und der Hausmeister, verwenden alle
// dieselbe Schnittstelle, aber jede Rolle verwendet nur einen Teil ihrer
// Funktionen.
//
// Ihre Aufgabe ist es, dieses System so umzubauen, dass es dem
// Interface-Segregations-Prinzip entspricht. Das bedeutet, dass kein Client
// gezwungen werden sollte, von Schnittstellen abhängig zu sein, die er nicht
// verwendet.

// %% [markdown]
//
// 1. Identifizieren Sie, welche Operationen für welche Rollen relevant sind.
// 2. Teilen Sie das `IRestaurantOperations`-Interface in kleinere,
//    rollenspezifische Schnittstellen auf.
// 3. Passen Sie die `Restaurant`-Klasse und die rollenbasierten Client-Klassen
//    (`Customer`, `Chef`, `Cashier`, `Janitor`) an die neuen Schnittstellen an.
// 4. Stellen Sie sicher, dass jede Client-Klasse nur über die für ihre Rolle
//    relevanten Operationen Bescheid weiß.

// %%
public interface IRestaurantOperations
{
    void PlaceOrder();
    void CookOrder();
    void CalculateBill();
    void CleanTables();
}

// %%
public class Restaurant : IRestaurantOperations
{
    public void PlaceOrder()
    {
        Console.WriteLine("Order has been placed.");
    }

    public void CookOrder()
    {
        Console.WriteLine("Order is being cooked.");
    }

    public void CalculateBill()
    {
        Console.WriteLine("Bill is being calculated.");
    }

    public void CleanTables()
    {
        Console.WriteLine("Tables are being cleaned.");
    }
}

// %%
public class Customer
{
    private IRestaurantOperations _restaurant;

    public Customer(IRestaurantOperations restaurant)
    {
        _restaurant = restaurant;
    }

    public void MakeOrder()
    {
        _restaurant.PlaceOrder();
        _restaurant.CalculateBill();
    }
}
// %%
public class Chef
{
    private IRestaurantOperations _restaurant;

    public Chef(IRestaurantOperations restaurant)
    {
        _restaurant = restaurant;
    }

    public void PrepareFood()
    {
        _restaurant.CookOrder();
    }
}

// %%
public class Cashier
{
    private IRestaurantOperations _restaurant;

    public Cashier(IRestaurantOperations restaurant)
    {
        _restaurant = restaurant;
    }

    public void GenerateBill()
    {
        _restaurant.CalculateBill();
    }
}

// %%
public class Janitor
{
    private IRestaurantOperations _restaurant;

    public Janitor(IRestaurantOperations restaurant)
    {
        _restaurant = restaurant;
    }

    public void Clean()
    {
        _restaurant.CleanTables();
    }
}

// %%
Restaurant restaurant = new Restaurant();
Customer customer = new Customer(restaurant);
Chef chef = new Chef(restaurant);
Cashier cashier = new Cashier(restaurant);
Janitor janitor = new Janitor(restaurant);

// %%
customer.MakeOrder();
chef.PrepareFood();
cashier.GenerateBill();
janitor.Clean();

// %%
public interface IOrdering
{
    void PlaceOrder();
}

// %%
public interface ICooking
{
    void CookOrder();
}

// %%
public interface IBilling
{
    void CalculateBill();
}

// %%
public interface ICleaning
{
    void CleanTables();
}

// %%
public class Restaurant : IOrdering, ICooking, IBilling, ICleaning
{
    public void PlaceOrder()
    {
        Console.WriteLine("Order has been placed.");
    }

    public void CookOrder()
    {
        Console.WriteLine("Order is being cooked.");
    }

    public void CalculateBill()
    {
        Console.WriteLine("Bill is being calculated.");
    }

    public void CleanTables()
    {
        Console.WriteLine("Tables are being cleaned.");
    }
}

// %%
public class Customer
{
    private IOrdering _ordering;
    private IBilling _billing;

    public Customer(IOrdering ordering, IBilling billing)
    {
        _ordering = ordering;
        _billing = billing;
    }

    public void MakeOrder()
    {
        _ordering.PlaceOrder();
        _billing.CalculateBill();
    }
}

// %%
public class Chef
{
    private ICooking _cooking;

    public Chef(ICooking cooking)
    {
        _cooking = cooking;
    }

    public void PrepareFood()
    {
        _cooking.CookOrder();
    }
}

// %%
public class Cashier
{
    private IBilling _billing;

    public Cashier(IBilling billing)
    {
        _billing = billing;
    }

    public void GenerateBill()
    {
        _billing.CalculateBill();
    }
}

// %%
public class Janitor
{
    private ICleaning _cleaning;

    public Janitor(ICleaning cleaning)
    {
        _cleaning = cleaning;
    }

    public void Clean()
    {
        _cleaning.CleanTables();
    }
}

// %%
Restaurant restaurant = new Restaurant();
Customer customer = new Customer(restaurant, restaurant);
Chef chef = new Chef(restaurant);
Cashier cashier = new Cashier(restaurant);
Janitor janitor = new Janitor(restaurant);

// %%
customer.MakeOrder();
chef.PrepareFood();
cashier.GenerateBill();
janitor.Clean();
