using Project.PowerBalancer.BaseClasses;

namespace Project.PowerBalancer.PowerComponents.Producers;

public class ConstantProducer : BaseProducer
{
    public ConstantProducer(double maxPowerProduction) : base(maxPowerProduction)
    {
    }


    public override double GetPowerProduction()
    {
        return MaxPowerProduction;
    }
}