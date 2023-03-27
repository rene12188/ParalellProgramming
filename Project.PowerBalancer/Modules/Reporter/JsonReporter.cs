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
                TransactionRecord bought = new TransactionRecord();
                if(item.PowerBoughtReport.Count > 0)
                {
                    bought.name = item.PowerBoughtReport.LastOrDefault().Item1;
                    bought.amount = item.PowerBoughtReport.LastOrDefault().Item2;
                }
                

                TransactionRecord sold = new TransactionRecord();
                if (item.PowerSoldReport.Count > 0)
                {
                    sold.name = item.PowerSoldReport.LastOrDefault().Item1;
                    sold.amount = item.PowerSoldReport.LastOrDefault().Item2;
                }
                newRecord.bought.Add(bought);
                newRecord.sold.Add(sold);
                //Console.WriteLine("\n\n" + newRecord + "\n\n");
                records.Add(newRecord);
            }
        }

        public void FlushReport(IList<Community> communities)
        {
            foreach (var item in communities)
            {
                var newRecord = new Record();
                newRecord.CommunityName = item.Name;
                //newRecord.time = item.
                TransactionRecord bought = new TransactionRecord();
                if (item.PowerBoughtReport.Count > 0)
                {
                    bought.name = item.PowerBoughtReport.LastOrDefault().Item1;
                    bought.amount = item.PowerBoughtReport.LastOrDefault().Item2;
                }


                TransactionRecord sold = new TransactionRecord();
                if (item.PowerSoldReport.Count > 0)
                {
                    sold.name = item.PowerSoldReport.LastOrDefault().Item1;
                    sold.amount = item.PowerSoldReport.LastOrDefault().Item2;
                }
                newRecord.bought.Add(bought);
                newRecord.sold.Add(sold);
                //Console.WriteLine("\n\n" + newRecord + "\n\n");
                records.Add(newRecord);
            }
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
    public class Record
    {
        public string CommunityName = "";
        public long time = 0;
        public List<TransactionRecord> bought = new List<TransactionRecord>();
        public List<TransactionRecord> sold = new List<TransactionRecord>();
    }
    public class TransactionRecord
    {
        public string name = "";
        public double amount = 0;
    }
}
