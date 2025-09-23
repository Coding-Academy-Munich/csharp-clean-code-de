namespace FizzBuzz;

using System;
using System.Collections.Generic;
using System.IO;

public static class FizzBuzz
{
    public static List<string> GenerateFizzBuzz(int n)
    {
        if (n < 0)
            throw new ArgumentException("Input must be non-negative", nameof(n));

        var result = new List<string>();
        for (int i = 1; i <= n; i++)
        {
            if (i % 3 == 0 && i % 5 == 0)
                result.Add("FizzBuzz");
            else if (i % 3 == 0)
                result.Add("Fizz");
            else if (i % 5 == 0)
                result.Add("Buzz");
            else
                result.Add(i.ToString());
        }

        return result;
    }

    public static void PrintFizzBuzz(int n, TextWriter? output = null)
    {
        output = output ?? Console.Out;
        List<string> fizzBuzzList = GenerateFizzBuzz(n);
        foreach (string item in fizzBuzzList)
        {
            output.WriteLine(item);
        }
    }
}
