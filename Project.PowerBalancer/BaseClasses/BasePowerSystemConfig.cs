namespace Project.PowerBalancer.BaseClasses;

public abstract class BasePowerSystemConfig
{
    protected readonly Dictionary<string, List<BaseProducer>> Producers = new();
    protected readonly Dictionary<string, List<BaseConsumer>> Consumers = new();

    public IList<BaseProducer> GetProducer(string importCommunityName)
    {
        if (Producers.TryGetValue(importCommunityName, out var producers)) ;
        {
            return producers.ToList();
        }
        return null;
    }

    public IList<BaseConsumer> GetConsumer(string importCommunityName)
    {
        if (Consumers.TryGetValue(importCommunityName, out var comsumers)) ;
        {
            return comsumers;
        }
        throw new ArgumentException("Argument is not valid");
    }
}