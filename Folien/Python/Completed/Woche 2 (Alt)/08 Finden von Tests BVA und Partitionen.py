// -*- coding: utf-8 -*-
// %% [markdown]
//
// <div style="text-align:center; font-size:200%;">
//  <b>Finden von Tests: BVA und Partitionen</b>
// </div>
// <br/>
// <div style="text-align:center; font-size:120%;">Dr. Matthias Hölzl</div>
// <br/>
// <div style="text-align:center;">Coding-Akademie München</div>
// <br/>

// %% [markdown]
//
// # Strategien zum Finden von (guten) Tests
//
// - Analyse von Randwerten (Boundary Value Analysis, BVA)
// - Partitionierung
// - Zustandsbasiertes Testen
// - Kontrollflussbasiertes Testen
// - Richtlinien
// - Kenntnis häufiger Fehler in Software
// - (Kenntnis häufiger Probleme von Tests (Test Smells))

// %% [markdown]
//
// ## Boundary Value Analysis
//
// - Viele Fehler treten am "Rand" des Definitionsbereichs von Funktionen
//   oder Methoden auf
// - Eine gute Strategie zum effizienten Testen ist es daher, derartige
//   Randwerte zu betrachten
//   - Der/die letzten gültigen Werte
//   - Werte, die gerade außerhalb des Definitionsbereichs liegen
// - Ist z.B. eine Funktion für ganzzahlige Werte zwischen 0 und 5
//   definiert, so kann sie mit Eingaben -1, 0, 5, 6 getestet werden

// %% [markdown]
//
// ## Boundary Value Analysis
//
// ### Vorteil:
//
// - Man konzentriert sich auf empirisch häufige Fehlerquellen
//
// ### Schwierigkeiten:
//
// - Bei vielen Bereichen ist nicht klar, was "Randwerte" sind
//   - (Allerdings lassen sich oft alternative Kriterien finden, z.B.
//     Länge von Collection-Argumenten)
// - Werte außerhalb des Definitionsbereichs können manchmal zu undefiniertem Verhalten führen
// - Bei Funktionen mit vielen Parametern gibt es eine kombinatorische
//   Explosion von Randwerten

// %% [markdown]
//
// ## Partitionierung
//
// - Argumente von Funktionen, Ein/Ausgabe des Programms und Zustände
//   von Klassen können oft in Äquivalenzklassen unterteilt werden, sodass…
//   - Das Verhalten für Elemente aus der gleichen Äquivalenzklasse ähnlich
//     ist (z.B. den gleichen Kontrollflusspfad nimmt)
//   - Elemente aus unterschiedlichen Klassen verschiedenes Verhalten zeigen
//   - Beispiel: Die Argumente der sqrt-Funktion können unterteilt werden in
//       - Positive Zahlen und 0
//       - Negative Zahlen
//   - Eine feinere Unterteilung wäre additionally in Quadratzahlen und Nicht-Quadratzahlen
// - Eine derartige Äquivalenzklasse heißt Partition (oder Domäne)

// %% [markdown]
//
// ## Partitionierung
//
// Finde Partitionen für das getestete Element und teste die folgenden Elemente:
//
// - Einen Wert aus der "Mitte" der Partition
// - Einen Wert auf oder nahe jeder Partitionsgrenze
//
// Häufig findet man Partitionen durch BVA.
//
// Beispiel: Um die Quadratwurzelfunktion zu testen, schreibe Tests für:
// - `sqrt(0.0)`
// - `sqrt(2.0)`
// - `sqrt(-2.0)`

// %% [markdown]
//
// ## Black-Box vs. White-Box Partitionierung
//
// - Wir können die Partitionierung sowohl als Black-Box- als auch als
//   White-Box-Technik verwenden
// - Bei der Black-Box-Partitionierung betrachten wir nur die Spezifikation
//   der Funktion
// - Bei der White-Box-Partitionierung betrachten wir auch die Implementierung
//   der Funktion
// - Die White-Box-Partitionierung kann uns helfen, Partitionen zu finden, die
//   auf spezifischen Kontrollflusspfaden durch die Funktion basieren
// - In vielen Fällen führen beide Vorgehensweisen zum gleichen Ergebnis

// %% [markdown]
//
// ## Beispiel: Discount-Berechnung
//
// - In einem Online-Shop haben wir eine Funktion, die den Rabatt für einen
//   Einkauf berechnet
// - Sie hat folgende Spezifikation:
//   - Eingabe: Gesamtbetrag des Einkaufs
//   - Rückgabewert: Preis nach Abzug des Rabatts
//   - Für Einkäufe unter 50€ gibt es keinen Rabatt
//   - Der maximale Rabatt wird ab 200€ Einkaufswert gewährt und beträgt 20%
// - Welche Partitionen können wir für diese Funktion identifizieren?
// - Wie können wir Werte aus jeder Partition testen?

// %% [markdown]
//
// - Black-Box Partitionen:
//   - Einkaufswert < 50€
//   - 50€ ≤ Einkaufswert < 200€
//   - Einkaufswert ≥ 200€
// - Testfälle:
//   - Einkaufswert = 0€
//   - Einkaufswert = 25€
//   - Einkaufswert = 49.99€
//   - Einkaufswert = 50€ (?)
//   - Einkaufswert = 100€
//   - Einkaufswert = 199.99€
//   - Einkaufswert = 200€
//   - Einkaufswert = 250€

// %% [markdown]
//
// | Einkaufswert | Test für den Ausgabewert y    |
// |--------------|:-----------------------------:|
// | $0€$         | $y = 0€$                      |
// | $25€$        | $y = 0€$                      |
// | $49.99€$     | $y = 0€$                      |
// | $50€$        | $0€ \leq y < 50€ \cdot 20\%$  |
// | $100€$       | $0€ < y < 100€ \cdot 20\%$    |
// | $199.99€$    | $0€ < y < 199.99€ \cdot 20\%$ |
// | $200€$       | $y = 200€ \cdot 20\%$         |
// | $250€$       | $y = 250€ \cdot 20\%$         |

// %% [markdown]
//
// - Hier ist jetzt die konkrete Implementierung der Funktion.

// %%
public static class DiscountCalculator
{
    public static double CalculateDiscount(double purchaseAmount)
    {
        if (purchaseAmount < 0) {
            throw new ArgumentException("Purchase amount cannot be negative");
        }

        if (purchaseAmount < 50) {
            return 0;
        } else if (purchaseAmount < 100) {
            return purchaseAmount * 0.1;
        } else if (purchaseAmount < 200) {
            return purchaseAmount * 0.15;
        } else {
            return Math.Min(purchaseAmount * 0.2, 100);
        }
    }
}

// %% [markdown]
//
// - White-Box Partitionen:
//   - Einkaufswert < 0€
//   - 0€ ≤ Einkaufswert < 50€
//   - 50€ ≤ Einkaufswert < 100€
//   - 100€ ≤ Einkaufswert < 200€
//   - 200€ ≤ Einkaufswert < 500€ (Maximaler Rabatt von 100€)
//   - Einkaufswert ≥ 500€

// %%
#r "nuget: xunit, *"

// %%
using Xunit;

// %%
#load "XunitTestRunner.cs"

// %%
using static XunitTestRunner;

// %%
public class DiscountCalculatorBlackBoxTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(25)]
    [InlineData(49.99)]
    public void TestPartitionBelow50(double purchaseAmount)
    {
        Assert.Equal(0, DiscountCalculator.CalculateDiscount(purchaseAmount));
    }

    [Theory]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(199.99)]
    public void TestPartition50to200(double purchaseAmount)
    {
        Assert.True(0 < DiscountCalculator.CalculateDiscount(purchaseAmount));
        Assert.True(DiscountCalculator.CalculateDiscount(purchaseAmount) <= purchaseAmount * 0.2);
    }

    [Theory]
    [InlineData(200)]
    [InlineData(250)]
    public void TestPartitionAbove200(double purchaseAmount)
    {
        Assert.Equal(purchaseAmount * 0.2, DiscountCalculator.CalculateDiscount(purchaseAmount), 0.001);
    }
}

// %%
RunTests<DiscountCalculatorBlackBoxTest>();

// %%
public class DiscountCalculatorWhiteBoxTest
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(-1000)]
    public void TestNegativePurchaseAmount(double purchaseAmount)
    {
        Assert.Throws<ArgumentException>(() => DiscountCalculator.CalculateDiscount(purchaseAmount));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(25)]
    [InlineData(49.99)]
    public void TestPartitionBelow50(double purchaseAmount)
    {
        Assert.Equal(0, DiscountCalculator.CalculateDiscount(purchaseAmount));
    }

    [Theory]
    [InlineData(50)]
    [InlineData(75)]
    [InlineData(99.99)]
    public void TestPartition50to200(double purchaseAmount)
    {
        Assert.Equal(purchaseAmount * 0.1, DiscountCalculator.CalculateDiscount(purchaseAmount), 0.001);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(150)]
    [InlineData(199.99)]
    public void TestPartition100to200(double purchaseAmount)
    {
        Assert.Equal(purchaseAmount * 0.15, DiscountCalculator.CalculateDiscount(purchaseAmount), 0.001);
    }

    [Theory]
    [InlineData(200)]
    [InlineData(250)]
    [InlineData(499.99)]
    public void TestPartitionAbove200(double purchaseAmount)
    {
        Assert.Equal(purchaseAmount * 0.2, DiscountCalculator.CalculateDiscount(purchaseAmount), 0.001);
    }

    [Theory]
    [InlineData(500)]
    [InlineData(1000)]
    [InlineData(10000)]
    public void TestPartitionAbove500(double purchaseAmount)
    {
        Assert.Equal(100, DiscountCalculator.CalculateDiscount(purchaseAmount));
    }
}

// %%
RunTests<DiscountCalculatorWhiteBoxTest>();

// %% [markdown]
//
// ## Workshop: Partitionierung
//
// In diesem Workshop werden wir die gelernten Techniken auf eine einfache
// Funktion anwenden. Wir werden eine Funktion `validateAndFormatPhoneNumber`
// implementieren und testen.
//
// Die Funktion soll eine US-Amerikanische Telefonnummer validieren und im
// Format `(XXX) XXX-XXXX` zurückgeben.
//
// - Die Telefonnummer darf nicht `null` oder leer sein, sonst wird eine
//   `ArgumentException` geworfen.
// - Die Telefonnummer besteht aus 10 oder 11 Ziffern.
// - Bei 11 Ziffern muss die Telefonnummer mit einer `1` beginnen.
// - Wenn eine Telefonnummer weniger als 10 oder mehr als 11 Ziffern hat, wird
//   eine `ArgumentException` geworfen.

// %%
public static class PhoneNumberFormatter
{
    public static string ValidateAndFormatPhoneNumber(string phoneNumber) {
        if (phoneNumber == null || phoneNumber.Length == 0) {
            throw new ArgumentException("Phone number cannot be null or empty");
        }

        string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

        if (digitsOnly.Length < 10 || digitsOnly.Length > 11) {
            throw new ArgumentException("Phone number must have 10 or 11 digits");
        }

        if (digitsOnly.Length == 11 && !digitsOnly.StartsWith("1")) {
            throw new ArgumentException("11-digit numbers must start with 1");
        }

        string areaCode = digitsOnly.Substring(digitsOnly.Length - 10, 3);
        string firstPart = digitsOnly.Substring(digitsOnly.Length - 7, 3);
        string secondPart = digitsOnly.Substring(digitsOnly.Length - 4, 4);

        return $"({areaCode}) {firstPart}-{secondPart}";
    }
}

// %%
PhoneNumberFormatter.ValidateAndFormatPhoneNumber("1234567890")

// %% [markdown]
//
// - Identifizieren Sie Äquivalenzklassen für die Eingaben der Funktion.
// - Implementieren Sie Testfälle für jede Partition.
// - Welche Black-Box-Partitionen können Sie identifizieren?
// - Welche White-Box-Partitionen können Sie identifizieren?

// %%
public class PhoneNumberFormatterTest
{
    // Black-box partitioning tests
    [Fact]
    public void TestValidTenDigitNumber()
    {
        Assert.Equal("(123) 456-7890", PhoneNumberFormatter.ValidateAndFormatPhoneNumber("1234567890"));
    }

    [Fact]
    public void TestValidElevenDigitNumber()
    {
        Assert.Equal("(234) 567-8901", PhoneNumberFormatter.ValidateAndFormatPhoneNumber("12345678901"));
    }

    [Fact]
    public void TestInvalidLengthNumber()
    {
        Assert.Throws<ArgumentException>(() => PhoneNumberFormatter.ValidateAndFormatPhoneNumber("123456789"));
    }

    [Fact]
    public void TestInvalidElevenDigitNumber()
    {
        Assert.Throws<ArgumentException>(() => PhoneNumberFormatter.ValidateAndFormatPhoneNumber("21234567890"));
    }

    [Fact]
    public void TestNullInput()
    {
        Assert.Throws<ArgumentException>(() => PhoneNumberFormatter.ValidateAndFormatPhoneNumber(null));
    }

    [Fact]
    public void TestEmptyInput()
    {
        Assert.Throws<ArgumentException>(() => PhoneNumberFormatter.ValidateAndFormatPhoneNumber(""));
    }

    // White-box partitioning tests
    [Theory]
    [InlineData("1234567890")]
    [InlineData("(123) 456-7890")]
    [InlineData("123-456-7890")]
    [InlineData("123.456.7890")]
    public void TestValidTenDigitNumbersWithDifferentFormats(string input)
    {
        Assert.Equal("(123) 456-7890", PhoneNumberFormatter.ValidateAndFormatPhoneNumber(input));
    }

    [Theory]
    [InlineData("11234567890")]
    [InlineData("1-123-456-7890")]
    [InlineData("1 (123) 456-7890")]
    public void TestValidElevenDigitNumbersWithDifferentFormats(string input)
    {
        Assert.Equal("(123) 456-7890", PhoneNumberFormatter.ValidateAndFormatPhoneNumber(input));
    }

    [Fact]
    public void TestNumberWithNonDigitCharacters()
    {
        Assert.Equal("(123) 456-7890", PhoneNumberFormatter.ValidateAndFormatPhoneNumber("a1b2c3d4e5f6g7h8i9j0"));
    }
}

// %%
RunTests<PhoneNumberFormatterTest>();
