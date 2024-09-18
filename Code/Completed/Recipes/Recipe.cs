namespace Recipes;

public class Recipe(string name, List<string> ingredients, string description, int rating = -1)
{
    public string Name { get; } = name;
    private readonly List<string> ingredients = [..ingredients];
    public string Description { get; } = description;
    public int Rating { get; } = rating;

    public List<string> Ingredients => [..ingredients];

    public bool HasRating => Rating != -1;
}
