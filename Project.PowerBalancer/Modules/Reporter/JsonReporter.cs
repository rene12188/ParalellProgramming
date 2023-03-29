using Newtonsoft.Json;
using Project.PowerBalancer.Interfaces;

namespace Project.PowerBalancer.Modules.Reporter;

public class JsonReporter : IReporter
{
    private List<Record> records = new();

    public void Report(IList<Community> communities)
    {
        foreach (var item in communities)
        {
            var newRecord = new Record();
            newRecord.CommunityName = item.Name;

            foreach (var boughtReceipt in item.PowerBoughtReport)
                newRecord.bought.Add(new TransactionRecord
                {
                    name = boughtReceipt.Item1, amount = boughtReceipt.Item2
                });

            foreach (var soldReceipt in item.PowerSoldReport)
                newRecord.sold.Add(new TransactionRecord
                {
                    name = soldReceipt.Item1, amount = soldReceipt.Item2
                });
            records.Add(newRecord);
        }
    }

    public void FlushReport(IList<Community> communities)
    {
        Report(communities);

        ExportToJson(records, "./records.json");
    }

    public void ExportToJson(List<Record> records, string filePath)
    {
        try
        {
            var json = JsonConvert.SerializeObject(records);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error exporting to JSON file: {ex.Message}");
        }
    }
}