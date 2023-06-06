namespace UnitTest.Parsers;

public class LineParsers
{
    public static DataItem[] Parse(List<string> lines)
    {
        var dataItems = new List<DataItem>();

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var dataItem = Parse(line);
            dataItems.Add(dataItem);
        }
        return dataItems.ToArray();
    }

    private static DataItem Parse(string line)
    {
        var lineItems = line.Split(';');

        if (lineItems.Length != 2)
            throw new Exception($"Invalid line: {line}");

        if (!DateTime.TryParse(lineItems[1], CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            throw new Exception($"Invalid datetime in line: {line}");

        return new DataItem(lineItems[0], dateTime);
    }
}