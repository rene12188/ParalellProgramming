namespace Project.PowerBalancer.BaseClasses;

public abstract class BaseProducer
{
    public double MaxPowerProduction { get; private set; }

    protected BaseProducer(double maxPowerProduction)
    {
        MaxPowerProduction = maxPowerProduction;
    }

    public abstract double GetPowerProduction();
}