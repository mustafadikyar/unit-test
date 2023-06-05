namespace UnitTest.Test.Parsers;

public class LineParserTests
{
    [Fact]
    public void ShouldParseValidLine()
    {
        //Arrange
        List<string> lines = new() { "Cappuccino; 10/27/2022 8:22:01 AM" };

        //Act
        var dataItems = LineParsers.Parse(lines);

        //Assert
        Assert.NotNull(dataItems);
        Assert.Single(dataItems);
        Assert.Equal("Cappuccino", dataItems[0].CoffeeType);
        Assert.Equal(new DateTime(2022, 10, 27, 8, 22, 01), dataItems[0].CreatedAt);
    }
}