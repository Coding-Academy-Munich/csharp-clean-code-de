namespace Recipes;

public class RecipeBookTest
{
    private readonly RecipeBook unit;

    public RecipeBookTest()
    {
        unit = new RecipeBook();
        unit.AddRecipe(new Recipe("recipe 1",
            ["ingredient 1", "ingredient 2"],
            "instructions...",
            5));
        unit.AddRecipe(new Recipe("recipe 2",
            ["ingredient 1", "ingredient 3"],
            "do this...",
            4));
        unit.AddRecipe(new Recipe("recipe 3",
            ["ingredient 2", "ingredient 3"],
            "do that...",
            3));
    }

    [Fact]
    public void TestGetRecipeByName()
    {
        Recipe recipe = unit.GetRecipeByName("recipe 2");
        Assert.Equal("recipe 2", recipe.Name);
    }

    [Fact]
    public void TestGetRecipesWithIngredient()
    {
        List<Recipe> things = unit.GetRecipesWithIngredient("ingredient 2");
        Assert.Equal(2, things.Count);
        Assert.Equal("recipe 1", things[0].Name);
        Assert.Equal("recipe 3", things[1].Name);
    }

    [Fact]
    public void TestGetRecipesWithRating()
    {
        List<Recipe> things = unit.GetRecipesWithRating(4);
        Assert.Single(things);
        Assert.Equal("recipe 2", things[0].Name);
    }

    [Fact]
    public void TestGetRecipesWithRatingAtLeast()
    {
        List<Recipe> things = unit.GetRecipesWithRatingAtLeast(4);
        Assert.Equal(2, things.Count);
        Assert.Equal("recipe 1", things[0].Name);
        Assert.Equal("recipe 2", things[1].Name);
    }

    [Fact]
    public void GetThingsThrowsStuff()
    {
        Assert.Throws<InvalidOperationException>(() => unit.GetRecipeByName("nonexistent recipe"));
    }
}
