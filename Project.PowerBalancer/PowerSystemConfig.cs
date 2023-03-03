using Project.PowerBalancer.Interfaces;

namespace Project.PowerBalancer;

public class PowerSystemConfig
{
    public IList<IConsumer> GetProducer(string importCommunityName)
    {
        throw new NotImplementedException();
    }

    public IList<IProducer> GetConsumer(string importCommunityName)
    {
        throw new NotImplementedException();
    }
}