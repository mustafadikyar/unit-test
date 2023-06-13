using UnitTest.Data;
using UnitTest.Models;
using UnitTest.Processors;

namespace UnitTest.Test.Processors
{
    public class DataProcessorTests : IDisposable
    {
        private readonly FakeCoffeeCountStore _coffeeCountStore;
        private readonly DataProcessor _dataProcessor;

        public DataProcessorTests()
        {
            _coffeeCountStore = new FakeCoffeeCountStore();
            _dataProcessor = new DataProcessor(_coffeeCountStore);
        }

        [Fact]
        public void ShouldSaveCountPerCoffeeType()
        {
            // Arrange
            var items = new[]
            {
            new DataItem("Cappuccino",new DateTime(2022,10,27,8,0,0)),
            new DataItem("Cappuccino",new DateTime(2022,10,27,9,0,0)),
            new DataItem("Espresso",new DateTime(2022,10,27,10,0,0))
        };

            // Act
            _dataProcessor.ProcessItems(items);

            // Assert
            Assert.Equal(2, _coffeeCountStore.SavedItems.Count);

            var item = _coffeeCountStore.SavedItems[0];
            Assert.Equal("Cappuccino", item.CoffeeType);
            Assert.Equal(2, item.Count);

            item = _coffeeCountStore.SavedItems[1];
            Assert.Equal("Espresso", item.CoffeeType);
            Assert.Equal(1, item.Count);
        }

        [Fact]
        public void ShouldClearPreviousCoffeeCount()
        {
            // Arrange
            var items = new[]
            {
            new DataItem("Cappuccino",new DateTime(2022,10,27,8,0,0))
        };

            // Act
            _dataProcessor.ProcessItems(items);
            _dataProcessor.ProcessItems(items);

            // Assert
            Assert.Equal(2, _coffeeCountStore.SavedItems.Count);
            foreach (var item in _coffeeCountStore.SavedItems)
            {
                Assert.Equal("Cappuccino", item.CoffeeType);
                Assert.Equal(1, item.Count);
            }
        }

        public void Dispose()
        {
            // This runs after every test
        }
    }

    public class FakeCoffeeCountStore : ICoffeeCountStore
    {
        public List<CoffeeCountItem> SavedItems { get; } = new();

        public void Save(CoffeeCountItem item)
        {
            SavedItems.Add(item);
        }
    }
}
