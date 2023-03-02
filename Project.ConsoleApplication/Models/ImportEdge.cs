namespace Project.ConsoleApplication.Models;

public class ImportEdge
{
    public ImportEdge(string from, string to, double cost)
    {
        Cost = cost;
        From = from;
        To = to;
    }

    public double Cost { get; private set; }

    public string From { get; private set; }

    public string To { get; private set; }
}