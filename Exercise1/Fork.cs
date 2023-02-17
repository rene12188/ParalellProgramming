namespace Exerise1;

public class Fork
{
    static int n = 0;

    public string Name { get; set; }

    public Philosopher UsedBy { get; set; } = null;
    
    public Fork()
    {
        Name = $"Fork {n++}";
    }
    
    public void UseFork(Philosopher user)
    {
        UsedBy = user;
        Console.WriteLine($"{Name} is used by {UsedBy.Name}");
    }  
    
    
    public void PutDownFork()
    {
        Console.WriteLine($"{Name} is no longer used by {UsedBy.Name}");
        UsedBy = null;
       
    }
}