namespace RecipesSk;

public class OneTest
{
    [Fact]
    public void ThingDddIsMinusOne()
    {
        var recipe = new One("Test Recipe", [], "");
        Assert.Equal(-1, recipe.Ddd);
    }
}
