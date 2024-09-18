namespace XunitBasics;

using static GeometryCalculator;

public class GeometryCalculatorTests
{
    [Fact]
    public void CalculateCircleArea_ValidRadius_ReturnsCorrectArea()
    {
        // Arrange
        const double radius = 5;

        // Act
        double result = CalculateCircleArea(radius);

        // Assert
        Assert.Equal(78.54, result, 2); // Assert.Equal with precision of 2 decimal places
    }

    [Fact]
    public void CalculateCircleArea_NegativeRadius_ThrowsArgumentException()
    {
        // Arrange
        const double radius = -5;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalculateCircleArea(radius));
    }

    [Fact]
    public void CalculateRectanglePerimeter_ValidInputs_ReturnsCorrectPerimeter()
    {
        // Arrange
        const double length = 4;
        const double width = 7;

        // Act
        double result = CalculateRectanglePerimeter(length, width);

        // Assert
        Assert.Equal(22, result);
    }

    [Fact]
    public void CalculateRectanglePerimeter_NegativeInputs_ThrowsArgumentException()
    {
        // Arrange
        const double length = -4;
        const double width = 7;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalculateRectanglePerimeter(length, width));
    }

    [Fact]
    public void CalculateTriangleArea_ValidInputs_ReturnsCorrectArea()
    {
        // Arrange
        const double baseLength = 10;
        const double height = 5;

        // Act
        double result = CalculateTriangleArea(baseLength, height);

        // Assert
        Assert.Equal(25, result);
    }

    [Fact]
    public void CalculateTriangleArea_NegativeInputs_ThrowsArgumentException()
    {
        // Arrange
        const double baseLength = -10;
        const double height = 5;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => CalculateTriangleArea(baseLength, height));
    }

    [Fact]
    public void CalculateTriangleArea_ZeroHeight_ReturnsZero()
    {
        // Arrange
        const double baseLength = 10;
        const double height = 0;

        // Act
        double result = CalculateTriangleArea(baseLength, height);

        // Assert
        Assert.Equal(0, result);
        Assert.True(result == 0); // Another way to check
    }
}
