using System.Security.Cryptography;

namespace Exerise1;

public class Philosopher
{
    private readonly Restaurant _restaurant;

    private static int n;

    public bool IsOpen { get; set; } = true;

    public string Name { get; set; }

    public int Index { get; set; }

    public Philosopher(Restaurant restaurant)
    {
        _restaurant = restaurant;
        Index = n;
        Name = $"Philosopher {n++}";
    }

    public void Start(int thinkingTime, int eatingTime)
    {
        while (IsOpen)
        {
            Console.WriteLine($"{Name} is thinking");
            Thread.Sleep(RandomNumberGenerator.GetInt32(thinkingTime * 1000));
            Console.WriteLine($"{Name} finished thinking");
            var fork1 = _restaurant.TakeFork(Index);
            var fork2 = _restaurant.TakeFork(Index + 1);
            lock (fork2)
            {
                fork2.UseFork(this);


                lock (fork1)
                {
                    fork1.UseFork(this);

                    Thread.Sleep(RandomNumberGenerator.GetInt32(eatingTime * 1000));
                    _restaurant.TakeFork(Index + 1).PutDownFork();
                    _restaurant.TakeFork(Index).PutDownFork();
                }
            }
        }
    }
}