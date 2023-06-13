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

    [Fact]
    public void ShouldSkipEmptyLines()
    {
        // Arrange
        List<string> lines = new() { "", "  " };

        // Act
        var dataItems = LineParsers.Parse(lines);

        // Assert
        Assert.NotNull(dataItems);
        Assert.Empty(dataItems);
    }

    //[Fact]
    //public void ShouldThrowExceptionForInvalidLine()
    //{
    //    // Arrange
    //    var lines = new List<string> { "Cappuccino" };

    //    // Act
    //    Assert.Throws<Exception>(() => LineParsers.Parse(lines));
    //}

    [InlineData("Cappuccino", "Invalid line")]
    [InlineData("Cappuccino;InvalidDateTime", "Invalid datetime in line -fail test")]
    [Theory]
    public void ShouldThrowExceptionForInvalidLine(string line, string expectedMessagePrefix)
    {
        // Arrange
        var lines = new List<string> { line };

        // Act and Assert
        var exception = Assert.Throws<Exception>(() => LineParsers.Parse(lines));

        Assert.Equal($"{expectedMessagePrefix}: {line}", exception.Message);
    }
}