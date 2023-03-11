using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Interfaces;
using Project.PowerBalancer.PowerComponents.Consumers;
using Project.PowerBalancer.PowerComponents.Producers;
using Project.Util.Models.Import;

namespace Project.PowerBalancer.Modules.PowerSystemConfig;

public class FictitiousPowerSystemConfig : BasePowerSystemConfig
{
    double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
    {
        var random = new Random();
        var rDouble = random.NextDouble();
        var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
        return rRangeDouble;
    }
    public FictitiousPowerSystemConfig(IClock timeClock)
    {
        for (int i = 0; i < 100; i++)
        {
            Consumers.Add($"C{i}", new List<BaseConsumer>(){ new ConstantConsumer( GetPseudoDoubleWithinRange(10, 500))});
            if(i% 10 == 0)
            {
                Producers.Add($"C{i}", new List<BaseProducer>
                {
                    new ConstantProducer( GetPseudoDoubleWithinRange(10, 50)), 
                        new SolarProducer(GetPseudoDoubleWithinRange(10, 500), timeClock)  
                });

            }else
            {
                Producers.Add($"C{i}", new List<BaseProducer>
                {
                    new ConstantProducer( GetPseudoDoubleWithinRange(10, 500)),
                });
            }
            
        }
        Producers.Add($"External", new List<BaseProducer>
        {
            new ConstantProducer( 500000),
        });
    }

}