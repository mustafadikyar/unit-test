using UnitTest.Data;

namespace UnitTest.Processors;

public class DataProcessor
{
    private readonly Dictionary<string, int> _countPerCoffeeType = new();
    private readonly ICoffeeCountStore _coffeeCountStore;

    public DataProcessor(ICoffeeCountStore coffeeCountStore) => _coffeeCountStore = coffeeCountStore;

    public void ProcessItems(DataItem[] dataItems)
    {
        _countPerCoffeeType.Clear();

        foreach (var dataItem in dataItems)
            ProcessItem(dataItem);

        SaveCountPerCoffeeType();
    }

    private DataItem previousItem;
    private void ProcessItem(DataItem dataItem)
    {
        if (!IsNewerThanPreviousItem(dataItem))
            return;

        if (!_countPerCoffeeType.ContainsKey(dataItem.CoffeeType))
            _countPerCoffeeType.Add(dataItem.CoffeeType, 1);
        else
            _countPerCoffeeType[dataItem.CoffeeType]++;

        previousItem = dataItem;
    }

    private bool IsNewerThanPreviousItem(DataItem dataItem) 
        => previousItem is null || previousItem.CreatedAt < dataItem.CreatedAt;

    private void SaveCountPerCoffeeType()
    {
        foreach (var entry in _countPerCoffeeType)
            _coffeeCountStore.Save(new CoffeeCountItem(entry.Key, entry.Value));
    }
}
