namespace XunitOrder;

public class Order
{
    private readonly List<Item> _items = [];

    public Order()
    {
    }

    public Order(IEnumerable<Item> items)
    {
        _items.AddRange(items);
    }

    public IReadOnlyList<Item> Items => _items.AsReadOnly();

    public Order AddItem(string name, double price)
    {
        _items.Add(new Item(name, price));
        return this;
    }

    public Order AddItem(Item item)
    {
        _items.Add(item);
        return this;
    }

    public double TotalPrice()
    {
        return _items.Sum(item => item.GetPrice());
    }

    public override string ToString()
    {
        return $"Order: {string.Join("; ", _items)}";
    }
}
