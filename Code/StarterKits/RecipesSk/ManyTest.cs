namespace RecipesSk;

public class ManyTest
{
    private readonly Many many;

    public ManyTest()
    {
        many = new Many();
        many.AddThing(new One("recipe 1",
            ["ingredient 1", "ingredient 2"],
            "instructions...",
            5));
        many.AddThing(new One("recipe 2",
            ["ingredient 1", "ingredient 3"],
            "do this...",
            4));
        many.AddThing(new One("recipe 3",
            ["ingredient 2", "ingredient 3"],
            "do that...",
            3));
    }

    [Fact]
    public void GetThing()
    {
        One one = many.GetThing("recipe 2");
        Assert.Equal("recipe 2", one.Aaa);
    }

    [Fact]
    public void GetThings1Test()
    {
        List<One> things = many.GetThings1("ingredient 2");
        Assert.Equal(2, things.Count);
        Assert.Equal("recipe 1", things[0].Aaa);
        Assert.Equal("recipe 3", things[1].Aaa);
    }

    [Fact]
    public void GetThings2Test()
    {
        List<One> things = many.GetThings2(4);
        Assert.Single(things);
        Assert.Equal("recipe 2", things[0].Aaa);
    }

    [Fact]
    public void GetThings3Test()
    {
        List<One> things = many.GetThings3(4);
        Assert.Equal(2, things.Count);
        Assert.Equal("recipe 1", things[0].Aaa);
        Assert.Equal("recipe 2", things[1].Aaa);
    }

    [Fact]
    public void GetThingsThrowsStuff()
    {
        Assert.Throws<InvalidOperationException>(() => many.GetThing("nonexistent recipe"));
    }
}
