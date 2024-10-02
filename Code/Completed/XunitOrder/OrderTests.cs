namespace XunitOrder;

public class OrderTests
{
    [Fact]
    public void EmptyOrder()
    {
        var order = new Order();
        Assert.Equal(0.0, order.TotalPrice());
        Assert.Empty(order.Items);
    }

    [Fact]
    public void OrderWithItems()
    {
        var order = new Order(new[] {new Item("Item 1", 1.0), new Item("Item 2", -2.0)});
        Assert.Equal(3.0, order.TotalPrice());
        Assert.Equal(2, order.Items.Count);
    }

    [Fact]
    public void AddItemsToOrder()
    {
        var order = new Order();

        order.AddItem("Item 1", 1.0);
        Assert.Single(order.Items);
        Assert.Equal(1.0, order.TotalPrice());

        order.AddItem("Item 2", 2.0);
        Assert.Equal(2, order.Items.Count);
        Assert.Equal(3.0, order.TotalPrice());
    }

    [Fact]
    public void ChangeItemPriceInOrder()
    {
        var order = new Order(new[] {new Item("Item 1", 1.0), new Item("Item 2", -2.0)});
        var firstItem = order.Items[0];
        firstItem.SetPrice(3.0);
        Assert.Equal(5.0, order.TotalPrice());
    }

    [Fact]
    public void OrderOutputFormat()
    {
        var order = new Order(new[] {new Item("Item 1", 1.0), new Item("Item 2", -2.0)});
        Assert.Equal("Order: Item 1, 1; Item 2, 2", order.ToString());
    }
}
