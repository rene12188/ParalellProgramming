using Project.PowerBalancer.Interfaces;

namespace Project.PowerBalancer.BaseClasses;

public abstract class BaseClock : IClock
{
    protected bool _isActive = true;
    public int Time { get; protected set; }
    public abstract void Start();

    public void Deactivate()
    {
        _isActive = false;
    }

    public bool IsActive => _isActive;

    protected void Tick()
    {
        if (Time == 86400 - 1)
            _isActive = false;
        else
            Time++;
    }
}