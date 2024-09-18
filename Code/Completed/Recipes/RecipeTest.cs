namespace Recipes;

public class RecipeTest
{
    [Fact]
    public void RecipeHasNoRatingByDefault()
    {
        var recipe = new Recipe("Test Recipe", [], "");
        Assert.False(recipe.HasRating);
    }

    [Fact]
    public void RecipeHasRatingWhenProvidedToConstructor()
    {
        var recipe = new Recipe("Test Recipe", [], "", 5);
        Assert.True(recipe.HasRating);
    }
}
