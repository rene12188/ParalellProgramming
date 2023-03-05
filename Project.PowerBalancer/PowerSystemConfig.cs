using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.PowerComponents.Consumers;
using Project.PowerBalancer.PowerComponents.Producers;

namespace Project.PowerBalancer;

public class PowerSystemConfig
{
    private readonly Dictionary<string, List<BaseProducer>> _producers = new();
    private readonly Dictionary<string, List<BaseConsumer>> _consumers = new();

    public PowerSystemConfig(Clock clock)
    {
        _consumers.Add("Bruck-Mürzzuschlag", new List<BaseConsumer> { new ConstantConsumer(42116 * 0.000019) });
        _consumers.Add("Deutschlandsberg", new List<BaseConsumer> { new ConstantConsumer(92217 * 0.000019) });
        _consumers.Add("Graz", new List<BaseConsumer> { new ConstantConsumer(298199 * 0.000019) });
        _consumers.Add("Graz-Umgebung", new List<BaseConsumer> { new ConstantConsumer(101974 * 0.000019) });
        _consumers.Add("Hartberg-Fürstenfel", new List<BaseConsumer> { new ConstantConsumer(70000 * 0.000019) });
        _consumers.Add("Leibnitz", new List<BaseConsumer> { new ConstantConsumer(48921 * 0.000019) });
        _consumers.Add("Leoben", new List<BaseConsumer> { new ConstantConsumer(5700019 * 0.000019) });
        _consumers.Add("Liezen", new List<BaseConsumer> { new ConstantConsumer(62498 * 0.000019) });
        _consumers.Add("Murau", new List<BaseConsumer> { new ConstantConsumer(26149 * 0.000019) });
        _consumers.Add("Murtal", new List<BaseConsumer> { new ConstantConsumer(42116 * 0.000019) });
        _consumers.Add("Südoststeiermark", new List<BaseConsumer> { new ConstantConsumer(80000 * 0.000019) });
        _consumers.Add("Voitsberg", new List<BaseConsumer> { new ConstantConsumer(9800 * 0.000019) });
        _consumers.Add("Weiz", new List<BaseConsumer> { new ConstantConsumer(9000 * 0.000019) });
        _consumers.Add("External", new List<BaseConsumer>());

        Console.WriteLine($"Total Consumption {_consumers.Sum(s => s.Value.Sum(m => m.GetPowerConsumption()))} kwh per Second");

        _producers.Add("Bruck-Mürzzuschlag", new List<BaseProducer> { new ConstantProducer(33.8) });
        _producers.Add("Deutschlandsberg", new List<BaseProducer> { new ConstantProducer(1.4) });
        _producers.Add("Graz", new List<BaseProducer> { new ConstantProducer(20.3) });
        _producers.Add("Graz-Umgebung", new List<BaseProducer> { new ConstantProducer(18.5), new SolarProducer(1.2, clock) });
        _producers.Add("Hartberg-Fürstenfel", new List<BaseProducer> { new ConstantProducer(1.2), new SolarProducer(0.4, clock) });
        _producers.Add("Leibnitz", new List<BaseProducer> { new ConstantProducer(4.4), new SolarProducer(0.5, clock) });
        _producers.Add("Leoben", new List<BaseProducer> { new ConstantProducer(1.2) });
        _producers.Add("Liezen", new List<BaseProducer> { new ConstantProducer(12) });
        _producers.Add("Murau", new List<BaseProducer> { new ConstantProducer(1) });
        _producers.Add("Murtal", new List<BaseProducer> { new ConstantProducer(0.5), new SolarProducer(0.2, clock) });
        _producers.Add("Südoststeiermark", new List<BaseProducer> { new ConstantProducer(0.5) });
        _producers.Add("Voitsberg", new List<BaseProducer> { new ConstantProducer(0.8) });
        _producers.Add("Weiz", new List<BaseProducer> { new ConstantProducer(6.0), new SolarProducer(0.2, clock) });
        _producers.Add("External", new List<BaseProducer> { new ConstantProducer(100) });
        Console.WriteLine($"Total Consumption {_producers.Sum(s => s.Value.Sum(m => m.MaxPowerProduction))} kwh per Second");
    }


    public IList<BaseProducer> GetProducer(string importCommunityName)
    {
        if (_producers.TryGetValue(importCommunityName, out var producers)) ;
        {
            return producers.ToList();
        }
        return null;
    }

    public IList<BaseConsumer> GetConsumer(string importCommunityName)
    {
        if (_consumers.TryGetValue(importCommunityName, out var comsumers)) ;
        {
            return comsumers;
        }
        throw new ArgumentException("Argument is not valid");
    }
}