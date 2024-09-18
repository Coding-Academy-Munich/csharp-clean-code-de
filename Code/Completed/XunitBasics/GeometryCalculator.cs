namespace XunitBasics;

public static class GeometryCalculator
{
    public static double CalculateCircleArea(double radius)
    {
        if (radius < 0)
            throw new ArgumentException("Radius cannot be negative.");

        return Math.PI * Math.Pow(radius, 2);
    }

    public static double CalculateRectanglePerimeter(double length, double width)
    {
        if (length < 0 || width < 0)
            throw new ArgumentException("Length and width cannot be negative.");

        return 2 * (length + width);
    }

    public static double CalculateTriangleArea(double baseLength, double height)
    {
        if (baseLength < 0 || height < 0)
            throw new ArgumentException("Base length and height cannot be negative.");

        return 0.5 * baseLength * height;
    }
}
