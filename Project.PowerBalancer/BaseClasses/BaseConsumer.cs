namespace Project.PowerBalancer.BaseClasses;

public abstract class BaseConsumer
{
    protected readonly double MaxPowerConsumption;

    protected BaseConsumer(double maxPowerConsumption)
    {
        MaxPowerConsumption = maxPowerConsumption;
    }

    public abstract double GetPowerConsumption();
}