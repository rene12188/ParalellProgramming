namespace Project.PowerBalancer.Interfaces;

public interface IClock
{
    public int Time { get;  }
    void Start();
    void Deactivate();
}