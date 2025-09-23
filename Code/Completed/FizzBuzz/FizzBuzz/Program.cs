using static FizzBuzz.FizzBuzz;

WriteSeparator();
PrintFizzBuzz(16);

// Example usage with custom output
WriteSeparator();
using var stringWriter = new StringWriter();
PrintFizzBuzz(16, stringWriter);

Console.WriteLine("Custom output:");
Console.Write(stringWriter.ToString());
WriteSeparator();
return;

void WriteSeparator() => Console.WriteLine("----------------------------------");
