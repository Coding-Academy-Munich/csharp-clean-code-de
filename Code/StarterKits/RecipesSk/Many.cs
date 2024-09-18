namespace RecipesSk;

public class Many
{
    private readonly List<One> foo = [];

    // Add a recipe
    public void AddThing(One thing)
    {
        foo.Add(thing);
    }

    // Return a recipe by name
    public One GetThing(string aaa)
    {
        return foo.First(bar => bar.Aaa == aaa);
    }

    // Return all recipes with a given ingredient
    public List<One> GetThings1(string bbb)
    {
        return foo.Where(thing => thing.Bbb.Contains(bbb)).ToList();
    }

    // Return all recipes with a rating equal to the argument
    public List<One> GetThings2(int ddd)
    {
        return foo.Where(thing => thing.DddMinusOne && thing.Ddd == ddd).ToList();
    }

    // Return all recipes with a rating greater or equal than the argument
    public List<One> GetThings3(int ddd)
    {
        return foo.Where(thing => thing.DddMinusOne && thing.Ddd >= ddd).ToList();
    }
}
