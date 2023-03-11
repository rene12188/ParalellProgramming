using Serilog;

namespace Exerise1;

public class Fork
{
    private static int n;

    public string Name { get; set; }

    public Philosopher UsedBy { get; set; }

    public Fork()
    {
        Name = $"Fork {n++}";
    }

    public void UseFork(Philosopher user)
    {
        UsedBy = user;
        Log.Information($"{Name} is used by {UsedBy.Name}");
    }


    public void PutDownFork()
    {
        Console.WriteLine($"{Name} is no longer used by {UsedBy.Name}");
        UsedBy = null;
    }
}