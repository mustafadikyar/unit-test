namespace UnitTest.Parsers;

public class LineParsers
{
    public static DataItem[] Parse(List<string> lines)
    {
        var dataItems = new List<DataItem>();

        foreach (var line in lines)
        {
            var dataItem = Parse(line);

            dataItems.Add(dataItem);
        }

        return dataItems.ToArray();
    }

    private static DataItem Parse(string line)
    {
        var lineItems = line.Split(';');

        return new DataItem(lineItems[0], DateTime.Parse(lineItems[1], CultureInfo.InvariantCulture));
    }
}
