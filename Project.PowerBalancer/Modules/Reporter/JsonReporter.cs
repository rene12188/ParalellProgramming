using Newtonsoft.Json;
using Project.PowerBalancer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.PowerBalancer.Modules.Reporter
{
    public class JsonReporter : IReporter
    {
        List<Record> records = new List<Record>();

        public void Report(IList<Community> communities)
        {
            foreach (var item in communities)
            {
                var newRecord = new Record();
                newRecord.CommunityName = item.Name;
                //newRecord.time = item.

                foreach (var boughtReceipt in item.PowerBoughtReport)
                {
                    newRecord.bought.Add(new TransactionRecord()
                    {
                        name = boughtReceipt.Item1, amount = boughtReceipt.Item2
                    });
                }

                foreach (var soldReceipt in item.PowerSoldReport)
                {
                    newRecord.sold.Add(new TransactionRecord()
                    {
                        name = soldReceipt.Item1, amount = soldReceipt.Item2
                    });
                }
                
                //Console.WriteLine("\n\n" + newRecord + "\n\n");
                records.Add(newRecord);
            }
        }

        public void FlushReport(IList<Community> communities)
        {
            Report(communities);

            ExportToJson(records, "./records.json");
        }

        public static void ExportToJson(List<Record> records, string filePath)
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

   
}