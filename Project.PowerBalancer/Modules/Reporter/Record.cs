namespace Project.PowerBalancer.Modules.Reporter;

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