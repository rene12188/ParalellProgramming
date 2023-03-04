namespace Project.PowerBalancer.BaseClasses;

//todo add Producer that is dependent on the Clock
public abstract class BaseProducer
{
    public double MaxPowerProduction { get; private set; }

    protected BaseProducer(double maxPowerProduction)
    {
        MaxPowerProduction = maxPowerProduction;
    }

    public abstract double GetPowerProduction();
}