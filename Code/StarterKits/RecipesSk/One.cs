namespace RecipesSk;

public class One(string aaa, List<string> bbb_, string ccc, int ddd = -1)
{
    // The name of the recipe
    public string Aaa { get; } = aaa;

    // The list of ingredients
    private readonly List<string> bbb = [..bbb_];

    // The description of the recipe
    public string Ccc { get; } = ccc;

    // The rating of the recipe (or -1 if there is no rating)
    public int Ddd { get; } = ddd;

    // Return the list of ingredients
    public List<string> Bbb => [..bbb];

    // Return true if the recipe has a valid rating
    public bool DddMinusOne => Ddd != -1;
}
