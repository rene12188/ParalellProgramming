using Project.PowerBalancer.BaseClasses;

namespace Project.PowerBalancer.PowerComponents.Consumers;

public class ConstantConsumer : BaseConsumer
{
    public ConstantConsumer(double maxPowerConsumption) : base(maxPowerConsumption)
    {
    }

    public override double GetPowerConsumption()
    {
        return MaxPowerConsumption;
    }
}