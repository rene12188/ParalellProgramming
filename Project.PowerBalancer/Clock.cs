namespace Project.PowerBalancer;

public class Clock
{
    private readonly int _sleepTime;

    public int _time { get; private set; }

    public bool IsActive = true;

    public Clock(int sleepTime)
    {
        _sleepTime = sleepTime;
    }
    
    public void Start()
    {
        while (IsActive)
        {
            Tick();
        }
    }

    private void Tick()
    {
        Thread.Sleep(_sleepTime);

        if (_time == 86400 - 1)
            _time = 0;
        else
            _time++;
    }
}