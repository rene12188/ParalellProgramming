using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Interfaces;
using Project.PowerBalancer.PowerComponents.Consumers;
using Project.PowerBalancer.PowerComponents.Producers;
using Serilog;

namespace Project.PowerBalancer.Modules.PowerSystemConfig;

public class RealisticPowerSystemConfig : BasePowerSystemConfig
{
    public RealisticPowerSystemConfig(IClock timeClock)
    {
        Consumers.Add("Bruck-Mürzzuschlag", new List<BaseConsumer> { new ConstantConsumer(42116 * 0.000019) });
        Consumers.Add("Deutschlandsberg", new List<BaseConsumer> { new ConstantConsumer(92217 * 0.000019) });
        Consumers.Add("Graz", new List<BaseConsumer> { new ConstantConsumer(298199 * 0.000019) });
        Consumers.Add("Graz-Umgebung", new List<BaseConsumer> { new ConstantConsumer(101974 * 0.000019) });
        Consumers.Add("Hartberg-Fürstenfel", new List<BaseConsumer> { new ConstantConsumer(70000 * 0.000019) });
        Consumers.Add("Leibnitz", new List<BaseConsumer> { new ConstantConsumer(48921 * 0.000019) });
        Consumers.Add("Leoben", new List<BaseConsumer> { new ConstantConsumer(5700019 * 0.000019) });
        Consumers.Add("Liezen", new List<BaseConsumer> { new ConstantConsumer(62498 * 0.000019) });
        Consumers.Add("Murau", new List<BaseConsumer> { new ConstantConsumer(26149 * 0.000019) });
        Consumers.Add("Murtal", new List<BaseConsumer> { new ConstantConsumer(42116 * 0.000019) });
        Consumers.Add("Südoststeiermark", new List<BaseConsumer> { new ConstantConsumer(80000 * 0.000019) });
        Consumers.Add("Voitsberg", new List<BaseConsumer> { new ConstantConsumer(9800 * 0.000019) });
        Consumers.Add("Weiz", new List<BaseConsumer> { new ConstantConsumer(9000 * 0.000019) });
        Consumers.Add("External", new List<BaseConsumer>());

        Log.Information($"Total Consumption {Consumers.Sum(s => s.Value.Sum(m => m.GetPowerConsumption()))} kwh per Second");

        Producers.Add("Bruck-Mürzzuschlag", new List<BaseProducer> { new ConstantProducer(33.8) });
        Producers.Add("Deutschlandsberg", new List<BaseProducer> { new ConstantProducer(1.4) });
        Producers.Add("Graz", new List<BaseProducer> { new ConstantProducer(20.3) });
        Producers.Add("Graz-Umgebung", new List<BaseProducer> { new ConstantProducer(18.5), new SolarProducer(1.2, timeClock) });
        Producers.Add("Hartberg-Fürstenfel", new List<BaseProducer> { new ConstantProducer(1.2), new SolarProducer(0.4, timeClock) });
        Producers.Add("Leibnitz", new List<BaseProducer> { new ConstantProducer(4.4), new SolarProducer(0.5, timeClock) });
        Producers.Add("Leoben", new List<BaseProducer> { new ConstantProducer(1.2) });
        Producers.Add("Liezen", new List<BaseProducer> { new ConstantProducer(12) });
        Producers.Add("Murau", new List<BaseProducer> { new ConstantProducer(1) });
        Producers.Add("Murtal", new List<BaseProducer> { new ConstantProducer(0.5), new SolarProducer(0.2, timeClock) });
        Producers.Add("Südoststeiermark", new List<BaseProducer> { new ConstantProducer(0.5) });
        Producers.Add("Voitsberg", new List<BaseProducer> { new ConstantProducer(0.8) });
        Producers.Add("Weiz", new List<BaseProducer> { new ConstantProducer(6.0), new SolarProducer(0.2, timeClock) });
        Producers.Add("External", new List<BaseProducer> { new ConstantProducer(100) });
        Log.Information($"Total Consumption {Producers.Sum(s => s.Value.Sum(m => m.MaxPowerProduction))} kwh per Second");
    }
}