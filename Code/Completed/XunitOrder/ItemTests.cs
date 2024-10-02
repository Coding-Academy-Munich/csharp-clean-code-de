namespace XunitOrder;

public class ItemTests
{
    [Theory]
    [InlineData("Item 1", 1.0)]
    [InlineData("Item 2", -2.0)]
    public void CreateItem(string name, double inputPrice)
    {
        var item = new Item(name, inputPrice);
        Assert.Equal(name, item.Name);
        Assert.Equal(Math.Abs(inputPrice), item.GetPrice());
    }

    [Theory]
    [InlineData(1.0, 2.0, 2.0)]
    [InlineData(1.0, -2.0, 2.0)]
    [InlineData(-2.0, 1.0, 1.0)]
    [InlineData(-2.0, -1.0, 1.0)]
    public void ChangeItemPrice(double initialPrice, double newPrice, double expectedPrice)
    {
        var item = new Item("Test Item", initialPrice);
        item.SetPrice(newPrice);
        Assert.Equal(expectedPrice, item.GetPrice());
    }

    [Fact]
    public void CompareItems()
    {
        var item1 = new Item("Item 1", 1.0);
        var item2 = new Item("Item 1", 1.0);
        var item3 = new Item("Item 2", 1.0);
        var item4 = new Item("Item 1", 2.0);

        Assert.Equal(item1, item2);
        Assert.NotEqual(item1, item3);
        Assert.NotEqual(item1, item4);
    }
}
