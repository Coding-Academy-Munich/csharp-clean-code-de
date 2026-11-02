// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Workshop: Adapter Pattern</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
//
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

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
