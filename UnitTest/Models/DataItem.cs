namespace UnitTest.Models;

public class DataItem
{
    public DataItem(string coffeeType, DateTime createdAt)
    {
        CoffeeType = coffeeType;
        CreatedAt = createdAt;
    }

    public string CoffeeType { get; }
    public DateTime CreatedAt { get; }
}
