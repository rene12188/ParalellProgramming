using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Interfaces;
using Project.PowerBalancer.PowerComponents.Consumers;
using Project.PowerBalancer.PowerComponents.Producers;

namespace Project.PowerBalancer.Modules.PowerSystemConfig;

public class FictitiousPowerSystemConfig : BasePowerSystemConfig
{
    private double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
    {
        var random = new Random();
        var rDouble = random.NextDouble();
        var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
        return rRangeDouble;
    }

    public FictitiousPowerSystemConfig(IClock timeClock)
    {
        for (int i = 0; i < 1000; i++)
        {
            Consumers.Add($"C{i}", new List<BaseConsumer> { new ConstantConsumer(250) });
            if (i % 10 == 0)
                Producers.Add($"C{i}", new List<BaseProducer>
                {
                    new ConstantProducer(500),
                    new SolarProducer(700, timeClock)
                });
            else
                Producers.Add($"C{i}", new List<BaseProducer>
                {
                    new ConstantProducer(200)
                });
        }

        Producers.Add("External", new List<BaseProducer>
        {
            new ConstantProducer(500000)
        });
    }
}