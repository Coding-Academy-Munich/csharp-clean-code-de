namespace Recipes;

public class RecipeBook
{
    private readonly List<Recipe> recipes = [];

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    public Recipe GetRecipeByName(string name)
    {
        return recipes.First(recipe => recipe.Name == name);
    }

    public List<Recipe> GetRecipesWithIngredient(string ingredient)
    {
        return recipes.Where(recipe => recipe.Ingredients.Contains(ingredient)).ToList();
    }

    public List<Recipe> GetRecipesWithRating(int rating)
    {
        return recipes.Where(recipe => recipe.HasRating && recipe.Rating == rating).ToList();
    }

    public List<Recipe> GetRecipesWithRatingAtLeast(int rating)
    {
        return recipes.Where(recipe => recipe.HasRating && recipe.Rating >= rating).ToList();
    }
}
