// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>GRASP: Controller</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # GRASP: Controller
//
// - Für jedes Modul/Subsystem: Externe Meldungen werden von Controller-Objekten
//   bearbeitet, die
//   - nicht Teil der Benutzeroberfläche sind
//   - jeweils ein Subsystem oder einen Anwendungsfall abdecken
// - Der Controller ist das erste Objekt nach der Benutzeroberfläche, das
//   Ereignisse/Meldungen bearbeitet. Er koordiniert das System.
// - Der Controller ist oft eine Fassade, d.h. er delegiert seine Arbeit an andere
//   Objekte.
// - Ein Use-Case-Controller bearbeitet immer einen kompletten Use-Case (Controller
//   können aber auch mehr als einen Use-Case bearbeiten).

// %% [markdown]
//
// ## Beispiel: Controller als Subsystem-Fassade
//
// ### Billing System Controller
//
// - Controller: `BillingController`
// - Kontext: Ein einzelner Controller dient als Einstiegspunkt für das gesamte
//   Abrechnungssubsystem.
// - Funktionsweise: Die Benutzeroberfläche (UI) sendet eine einzige Nachricht
//   an den `BillingController`, um eine Bestellung zu verarbeiten.
//   - UI-Aufruf: `billingController.processOrder(cart, customer)`
//   - Delegation: Der Controller koordiniert dann die internen Domänenobjekte:
//     1.  Ruft den `TaxCalculator` auf, um die Steuern zu berechnen.
//     2.  Weist den `InvoiceGenerator` an, eine Rechnung zu erstellen.
//     3.  Beauftragt das `PaymentGateway`, die Zahlung abzuwickeln.
// - Der `BillingController` bietet alle Funktionalität des Abrechnungssubsystems an, z.B.:
//   - `processOrder(cart, customer)`
//   - `refundOrder(orderId)`
//   - `getOrders(customerId)`
// - GRASP-Prinzip: Der `BillingController` kapselt die Komplexität des
//   Abrechnungssubsystems und bietet eine einfache Schnittstelle. Er ist nicht
//   Teil der UI und koordiniert die Systemantwort.

// %%
// Example Domain Objects (for illustration)
public class ShoppingCart { /* ... */ }
public class CustomerInfo {
    public static CustomerInfo FromOrderId(string orderId) => new CustomerInfo();
    /* ... */
}

// %%
// The services within the Billing Subsystem
public class TaxCalculator {
    public void CalculateTaxes(ShoppingCart cart) => Console.WriteLine("Taxes calculated.");
}
public class InvoiceGenerator {
    public void CreateInvoice(ShoppingCart cart) => Console.WriteLine("Invoice created.");
}
public class PaymentGateway {
    public void ProcessPayment(CustomerInfo customer) => Console.WriteLine("Payment processed.");
    public void ProcessRefund(CustomerInfo customer) => Console.WriteLine("Refund processed.");
}

// %% [markdown]
//
// Der GRASP-Controller für das Abrechnungssubsystem

// %%
public class BillingController {
    private readonly TaxCalculator _taxCalculator = new();
    private readonly InvoiceGenerator _invoiceGenerator = new();
    private readonly PaymentGateway _paymentGateway = new();

    // The entry points for the UI
    public void ProcessOrder(ShoppingCart cart, CustomerInfo customer) {
        Console.WriteLine("--- BillingController: Processing new order ---");
        _taxCalculator.CalculateTaxes(cart);
        _invoiceGenerator.CreateInvoice(cart);
        _paymentGateway.ProcessPayment(customer);
        Console.WriteLine("--- Order processing complete ---");
    }

    public void RefundOrder(string orderId) {
        Console.WriteLine($"--- BillingController: Refunding order {orderId} ---");
        // Logic to refund the order
        CustomerInfo customer = CustomerInfo.FromOrderId(orderId); // Hypothetical method
        _paymentGateway.ProcessRefund(customer);
        Console.WriteLine("--- Refund complete ---");
    }

    public void GetOrders(string customerId) {
        Console.WriteLine($"--- BillingController: Retrieving orders for customer {customerId} ---");
        // Logic to get orders for the customer
        Console.WriteLine("--- Orders retrieved ---");
    }
}

// %% [markdown]
//
// - Der einzige Einstiegspunkt für die UI in das Abrechnungssubsystem ist der
//   `BillingController`.
// - Er koordiniert die internen Domänenobjekte für die verschiedenen Use-Cases
//   des Abrechnungssubsystems.

// %%
// How the UI layer might use the BillingController
var billingController = new BillingController();
var cart = new ShoppingCart();
var customer = new CustomerInfo();

// %%
billingController.ProcessOrder(cart, customer);

// %%
billingController.RefundOrder("order-123");

// %%
billingController.GetOrders("customer-456");

// %% [markdown]
//
// ## Beispiel: Use-Case-Controller
//
// ### E-Commerce Auftragsverwaltung
//
// - Subsystem: Auftragsverwaltung
// - Kontext: Anstatt eines großen Controllers für das gesamte Subsystem wird
//   für jeden Anwendungsfall ein eigener, fokussierter Controller erstellt.
// - Controller:
//   - `PlaceOrderController`: Bearbeitet den kompletten Anwendungsfall
//     "Bestellung aufgeben". Die UI ruft `placeOrderController.execute(cart)`
//     auf. Dieser koordiniert den `InventoryService` und `PaymentService`.
//   - `CancelOrderController`: Bearbeitet den Anwendungsfall "Bestellung
//     stornieren". Die UI ruft `cancelOrderController.execute(orderId)` auf.
//     Dieser koordiniert `PaymentService` (für die Rückerstattung) und
//     `InventoryService` (um Artikel wieder einzulagern).
// - GRASP-Prinzip: Jeder Controller ist für einen vollständigen,
//   abgeschlossenen Geschäftsprozess (Use Case) verantwortlich. Dies erhöht die
//   Kohäsion und vereinfacht das System.

// %%
// Shared services used by multiple controllers
public class InventoryService {
    public void ReserveItems(string cartId) => Console.WriteLine("Inventory: Items reserved.");
    public void RestockItems(string orderId) => Console.WriteLine("Inventory: Items restocked.");
}

public class PaymentService {
    public void CollectPayment(string cartId) => Console.WriteLine("Payment: Payment collected.");
    public void IssueRefund(string orderId) => Console.WriteLine("Payment: Refund issued.");
}

// %% [markdown]
//
// Use Case 1: Bestellung aufgeben

// %%
public class PlaceOrderController {
    private readonly InventoryService _inventory = new();
    private readonly PaymentService _payment = new();

    public void Execute(string cartId) {
        Console.WriteLine("\n--- PlaceOrderController: Executing ---");
        _inventory.ReserveItems(cartId);
        _payment.CollectPayment(cartId);
        Console.WriteLine("--- Order placed successfully ---");
    }
}

// %% [markdown]
//
// Use Case 2: Bestellung stornieren

// %%
public class CancelOrderController {
    private readonly PaymentService _payment = new();
    private readonly InventoryService _inventory = new();

    public void Execute(string orderId) {
        Console.WriteLine("\n--- CancelOrderController: Executing ---");
        _payment.IssueRefund(orderId);
        _inventory.RestockItems(orderId);
        Console.WriteLine("--- Order cancelled successfully ---");
    }
}

// %% [markdown]
//
// Verwendung verschiedener Controller durch die UI-Schicht:
// - Benutzer klickt auf "Bestellung aufgeben" -> `PlaceOrderController`

// %%
var placeOrderController = new PlaceOrderController();
placeOrderController.Execute("cart-123");

// %% [markdown]
//
// - Später klickt der Benutzer auf die Schaltfläche "Bestellung stornieren"
//   -> `CancelOrderController`

// %%
var cancelOrderController = new CancelOrderController();
cancelOrderController.Execute("order-abc");

// %% [markdown]
//
// - Verwandt: Fassaden-Pattern (Domänen-Fassade), Domänencontroller
// - Siehe hexagonale Architektur: Controller sind die Ports in der hexagonalen
//   Architektur
// - Tests: Controller bieten eine zentrale Schnittstelle für einzelne
//   Subsysteme oder Anwendungsfälle
