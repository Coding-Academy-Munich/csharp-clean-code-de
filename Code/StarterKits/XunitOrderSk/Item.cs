namespace XunitOrderSk;

public class Item
{
    public string Name { get; }
    private double _price;

    public Item(string name, double price)
    {
        Name = name;
        SetPrice(price);
    }

    public double GetPrice() => _price;

    public void SetPrice(double value)
    {
        _price = Math.Abs(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Item item &&
               Name == item.Name &&
               Math.Abs(_price - item._price) < 0.001;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }

    public override string ToString()
    {
        return $"{Name}, {_price}";
    }
}
