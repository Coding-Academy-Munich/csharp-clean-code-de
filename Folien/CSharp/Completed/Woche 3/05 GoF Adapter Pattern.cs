// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GoF Adapter Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// ## Zweck
//
// - Anpassung der Schnittstelle einer Klasse an ein erwartetes Interface
// - Zusammenarbeit von Klassen, die aufgrund inkompatibler Schnittstellen nicht
//   zusammenarbeiten können

// %% [markdown]
//
// ## Auch bekannt als
//
// - Wrapper

// %% [markdown]
//
// ## Motivation
//
// - Nutzung einer Bibliotheks-Klasse aufgrund inkompatibler Schnittstellen nicht
//   möglich
// - Beispiel: Grafischer Editor
//   - Grafik-Objekte sind relativ einfach zu realisieren
//   - Text ist komplexer, möglicherweise ist der Einsatz einer externen Bibliothek
//     sinnvoll
// - Die Schnittstelle dieser Bibliothek ist nicht kompatibel mit der Schnittstelle,
//   die unser Editor erwartet
// - Mit einem Adapter können wir die Schnittstelle der Bibliothek an die
//   Schnittstelle unseres Editors anpassen

// %% [markdown]
//
// <img src="img/adapter_example.png"
//      style="display:block;margin:auto;width:80%"/>

// %% [markdown]
//
// ## Anwendbarkeit
//
// - Nutzung einer bestehenden Klasse mit inkompatibler Schnittstelle
// - [...]

// %% [markdown]
//
// ## Struktur
//
// - Es werden zwei Varianten definiert: Klassenadapter und Objektadapter
// - Klassenadapter verwenden Mehrfachvererbung und werden seltener eingesetzt
// - Klassendiagramm für Objektadapter:
//
// <img src="img/pat_adapter.png"
//      style="display: block; margin: auto; width: 80%;"/>

// %% [markdown]
//
// ## Teilnehmer
//
// - `Client`
//   - Nutzt das Interface von `Target`
// - `Target`
//   - Definiert das Interface, das vom `Client` verwendet wird
// - `Adapter`
//   - Implementiert das Interface von `Target` und hält eine Referenz auf das `Adaptee`
// - `Adaptee`
//   - Die Klasse, deren Schnittstelle angepasst werden soll

// %% [markdown]
//
// ## Interaktionen
//
// - Der Client ruft eine Target-Methode auf einem Adapter-Objekt auf
// - Der Adapter ruft die entsprechende Methode auf dem Adaptee auf

// %% [markdown]
//
// ## Konsequenzen
//
// - Klassenadapter
//   - ...
// - Objektadapter
//   - ein Adapter kann mit mehreren adaptierten Objekten zusammenarbeiten
//   - erschwert das Überschreiben von Adaptee-Methoden

// %% [markdown]
//
// ## Beispielcode
//
// - Verwaltung von Mitarbeitern in einer Firma
// - Ein Teil der Daten wird von einem Legacy-System bereitgestellt
// - Die Schnittstelle des Legacy-Systems ist nicht kompatibel mit der Schnittstelle
//   der neuen Software
// - Wir erstellen einen Adapter, der die Schnittstelle des Legacy-Systems an die
//   Schnittstelle der neuen Software anpasst

// %%
public interface IEmployee
{
    string Name { get; }
    decimal Salary { get; }
}

// %%
public class NewEmployee(string name, decimal salary) : IEmployee
{
    public string Name { get; } = name;
    public decimal Salary { get; } = salary;
}

// %%
public class LegacyEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Pay { get; set; } // In cents

    public LegacyEmployee(string firstName, string lastName, int pay)
    {
        FirstName = firstName;
        LastName = lastName;
        Pay = pay;
    }
}

// %%
public class Company
{
    private List<IEmployee> _employees;
    private decimal _monthlyRent;

    public Company(List<IEmployee> employees, decimal monthlyRent = 1000.0m)
    {
        _employees = employees;
        _monthlyRent = monthlyRent;
    }

    public decimal MonthlyExpenses()
    {
        decimal totalSalary = _employees.Aggregate(0.0m, (acc, employee) => acc + employee.Salary);
        return totalSalary + _monthlyRent;
    }

    public string Employees()
    {
        return _employees.Aggregate("", (acc, employee) => acc + employee.Name + "\n");
    }
}

// %%
public class LegacyEmployeeAdapter : IEmployee
{
    private readonly LegacyEmployee _legacyEmployee;

    public LegacyEmployeeAdapter(LegacyEmployee legacyEmployee)
    {
        _legacyEmployee = legacyEmployee;
    }

    public string Name => $"{_legacyEmployee.FirstName} {_legacyEmployee.LastName}";
    public decimal Salary => _legacyEmployee.Pay / 100.0m;
}

// %%
var legacyEmployee1 = new LegacyEmployeeAdapter(new LegacyEmployee("John", "Doe", 150_000));
var legacyEmployee2 = new LegacyEmployeeAdapter(new LegacyEmployee("Jane", "Miller", 200_000));
var newEmployee1 = new NewEmployee("Max Mustermann", 2500.0m);
var newEmployee2 = new NewEmployee("Erica Jones", 3000.0m);

// %%
var employees = new List<IEmployee>
{
    legacyEmployee1,
    legacyEmployee2,
    newEmployee1,
    newEmployee2
};

// %%
var company = new Company(employees);

// %%
Console.WriteLine("Monthly expenses: " + company.MonthlyExpenses());

// %%
Console.WriteLine("Employees:\n" + company.Employees());

// %% [markdown]
//
// ## Praxisbeispiele
//
// - ET++ Draw
// - InterViews 2.6
// - ...

// %% [markdown]
//
// ## Verwandte Muster
//
// - Bridge: ähnliche Struktur, aber andere Absicht (Trennung von Schnittstelle und
//   Implementierung)
// - Decorator: erweitert anderes Objekt, ohne die Schnittstelle zu ändern
// - Proxy: Stellvertreter für ein Objekt, das die gleiche Schnittstelle hat

// %% [markdown]
//
// ## Workshop: Einheitliche Schnittstelle für einen Chat-Client
//
// In diesem Workshop sollen verschiedene Messaging-Dienste, wie SMS, E-Mail und
// eine In-App-Chat-System, zu einer Chat Applikation hinzugefügt werden. Die
// Herausforderung besteht darin, dass jeder dieser Dienste seine eigene Art
// hat, Nachrichten zu senden und zu empfangen.
//
// Im Folgenden finden Sie den Startercode mit einer Klasse für Benutzer des
// Dienstes und separaten Klassen für jeden Messaging-Dienst.

// %%
public class User
{
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailAddress { get; set; }

    public User(string userName, string phoneNumber, string emailAddress)
    {
        UserName = userName;
        PhoneNumber = phoneNumber;
        EmailAddress = emailAddress;
    }
}
// %%
public class SMS
{
    public void SendText(string number, string message)
    {
        Console.WriteLine($"Sending SMS to {number}: {message}");
    }

    public void ReceiveText(string number)
    {
        Console.WriteLine($"Receiving a SMS from {number}");
    }
}

// %%
public class Email
{
    public void SendEmail(string emailAddress, string subject, string message)
    {
        Console.WriteLine($"Sending email to {emailAddress} with subject '{subject}': {message}");
    }

    public void ReceiveEmail(string emailAddress)
    {
        Console.WriteLine($"Receiving an email from {emailAddress}");
    }
}

// %%
public class InAppChat
{
    public void SendMessage(User user, string message)
    {
        Console.WriteLine($"Sending message to {user.UserName}: {message}");
    }

    public void ReceiveMessage(User user)
    {
        Console.WriteLine($"Receiving a message from {user.UserName}");
    }
}

// %%
public interface IMessagingService
{
    void Send(User to, string message);
    void Receive(User from);
}

// %%
public class ChatApplication
{
    private List<IMessagingService> adapters;

    public ChatApplication(List<IMessagingService> adapters)
    {
        this.adapters = adapters;
    }

    public void SendMessage(User to, string message)
    {
        foreach (var adapter in adapters)
        {
            adapter.Send(to, message);
        }
    }

    public void ReceiveMessage(User from)
    {
        foreach (var adapter in adapters)
        {
            adapter.Receive(from);
        }
    }
}

// %% [markdown]
//
// Die folgenden Variablen definieren die Messaging-Dienste, die von der Chat-Anwendung
// verwendet werden sollen. Sie müssen nicht geändert werden.

// %%
var sms = new SMS();
var email = new Email();
var chat = new InAppChat();

// %% [markdown]
//
// - Definieren Sie Adapter für die drei Messaging-Dienste

// %%
public class SMSAdapter : IMessagingService
{
    private readonly SMS sms;

    public SMSAdapter(SMS sms)
    {
        this.sms = sms;
    }

    public void Send(User to, string message)
    {
        sms.SendText(to.PhoneNumber, message);
    }

    public void Receive(User from)
    {
        sms.ReceiveText(from.PhoneNumber);
    }
}

// %%
public class EmailAdapter : IMessagingService
{
    private readonly Email email;

    public EmailAdapter(Email email)
    {
        this.email = email;
    }

    public void Send(User to, string message)
    {
        email.SendEmail(to.EmailAddress, "Mail from Chat App", message);
    }

    public void Receive(User from)
    {
        email.ReceiveEmail(from.EmailAddress);
    }
}

// %%
public class InAppChatAdapter : IMessagingService
{
    private readonly InAppChat chat;

    public InAppChatAdapter(InAppChat chat)
    {
        this.chat = chat;
    }

    public void Send(User to, string message)
    {
        chat.SendMessage(to, message);
    }

    public void Receive(User from)
    {
        chat.ReceiveMessage(from);
    }
}

// %% [markdown]
//
// - Erstellen Sie hier Adapter für die Messaging-Dienste:

// %%
var smsAdapter = new SMSAdapter(sms);
var emailAdapter = new EmailAdapter(email);
var chatAdapter = new InAppChatAdapter(chat);

// %%
var messagingServices = new List<IMessagingService>();

// %% [markdown]
//
// - Fügen Sie die Adapter zur Liste `messagingServices` hinzu

// %%
messagingServices.Add(smsAdapter);
messagingServices.Add(emailAdapter);
messagingServices.Add(chatAdapter);

// %% [markdown]
//
// - Überprüfen Sie, dass Ihre Adapter funktionieren, indem Sie die folgenden
//   Zeilen ausführen
// - Sie sollten eine Ausgabe für jeden Messaging-Dienst sehen, in der die
//   entsprechende Nachricht angezeigt wird

// %%
var chatApp = new ChatApplication(messagingServices);

// %%
chatApp.SendMessage(new User("Joe Example", "555-1234", "joe@example.org"), "Hello!");

// %%
chatApp.ReceiveMessage(new User("Joe Example", "555-1234", "joe@example.org"));

// %%
