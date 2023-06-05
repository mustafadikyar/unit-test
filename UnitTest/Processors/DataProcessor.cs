namespace UnitTest.Processors;

public class DataProcessor
{
    private readonly Dictionary<string, int> _countPerCoffeeType = new();

    public void ProcessItems(DataItem[] dataItems)
    {
        _countPerCoffeeType.Clear();

        foreach (var dataItem in dataItems)
        {
            ProcessItem(dataItem);
        }

        SaveCountPerCoffeeType();
    }

    private void ProcessItem(DataItem dataItem)
    {
        if (!_countPerCoffeeType.ContainsKey(dataItem.CoffeeType))
            _countPerCoffeeType.Add(dataItem.CoffeeType, 1);
        else
            _countPerCoffeeType[dataItem.CoffeeType]++;
    }

    private void SaveCountPerCoffeeType()
    {
        foreach (var entry in _countPerCoffeeType)
            Console.WriteLine($"{entry.Key}:{entry.Value}");
    }
}
