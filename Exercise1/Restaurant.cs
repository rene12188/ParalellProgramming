namespace Exerise1;

public class Restaurant
{
    private readonly int _thinkingTime;
    private readonly int _eatingTime;
    private bool IsOpen;

    readonly IList<Philosopher> _philosophers = new List<Philosopher>();
    readonly IList<Fork> _forks = new List<Fork>();


    public Restaurant(int n, int thinkingTime, int eatingTime)
    {
        _thinkingTime = thinkingTime;
        _eatingTime = eatingTime;
        for (int i = 0; i < n; i++)
        {
            _philosophers.Add(new Philosopher(this));
        }

        for (int i = 0; i < n; i++)
        {
            _forks.Add(new Fork());
        }
    }

    public Fork TakeFork(int index)
    {
        if(index == _forks.Count)
            index = 0;
        
        return _forks[index];
    }

    public void Start()
    {
        foreach (var philosopher in _philosophers)
        {
            Thread myThread = new Thread(() => philosopher.Start(_thinkingTime, _eatingTime));
            myThread.Start();
        }

        Console.WriteLine("Press any key to exit");
        Console.ReadLine();
        
        foreach (var philosopher in _philosophers)
        {
            philosopher.IsOpen = false;
        }
    }
}