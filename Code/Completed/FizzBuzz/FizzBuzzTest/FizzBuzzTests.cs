namespace FizzBuzzTest;

public class FizzBuzzTests
{
    [Fact]
    public void GenerateFizzBuzz_ReturnsCorrectSequenceFor15()
    {
        var expected = new List<string>
        {
            "1", "2", "Fizz", "4", "Buzz", "Fizz", "7", "8", "Fizz", "Buzz",
            "11", "Fizz", "13", "14", "FizzBuzz"
        };
        var result = FizzBuzz.FizzBuzz.GenerateFizzBuzz(15);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, "1")]
    [InlineData(3, "Fizz")]
    [InlineData(5, "Buzz")]
    [InlineData(15, "FizzBuzz")]
    public void GenerateFizzBuzz_ReturnsCorrectValueForSpecificNumbers(int number, string expected)
    {
        var result = FizzBuzz.FizzBuzz.GenerateFizzBuzz(number);
        Assert.Equal(expected, result[^1]);
    }

    [Fact]
    public void GenerateFizzBuzz_ReturnsEmptyListForZero()
    {
        var result = FizzBuzz.FizzBuzz.GenerateFizzBuzz(0);
        Assert.Empty(result);
    }

    [Fact]
    public void GenerateFizzBuzz_ThrowsArgumentExceptionForNegativeNumber()
    {
        Assert.Throws<ArgumentException>(() => FizzBuzz.FizzBuzz.GenerateFizzBuzz(-1));
    }

    [Fact]
    public void GenerateFizzBuzz_ReturnsCorrectSequenceFor100()
    {
        var result = FizzBuzz.FizzBuzz.GenerateFizzBuzz(100);
        Assert.Equal(100, result.Count);
        Assert.Equal("1", result[0]);
        Assert.Equal("2", result[1]);
        Assert.Equal("Fizz", result[2]);
        Assert.Equal("4", result[3]);
        Assert.Equal("Buzz", result[4]);
        Assert.Equal("Fizz", result[5]);
        Assert.Equal("FizzBuzz", result[14]);
        Assert.Equal("FizzBuzz", result[29]);
        Assert.Equal("FizzBuzz", result[44]);
        Assert.Equal("Buzz", result[99]);
    }

    [Fact]
    public void PrintFizzBuzz_WritesToProvidedTextWriter()
    {
        using var stringWriter = new StringWriter();
        FizzBuzz.FizzBuzz.PrintFizzBuzz(5, stringWriter);
        var result = stringWriter.ToString().Trim().Split(Environment.NewLine);
        Assert.Equal(["1", "2", "Fizz", "4", "Buzz"], result);
    }

    [Fact]
    public void PrintFizzBuzzToConsole_WritesToConsoleOut()
    {
        var originalOut = Console.Out;
        try
        {
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            FizzBuzz.FizzBuzz.PrintFizzBuzz(3);
            var result = stringWriter.ToString().Trim().Split(Environment.NewLine);
            Assert.Equal(["1", "2", "Fizz"], result);
        }
        finally
        {
            Console.SetOut(originalOut);
        }
    }
}
