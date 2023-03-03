using Project.PowerBalancer.Interfaces;
using Project.Util.Models.Import;

namespace Project.PowerBalancer;

public class Community
{
    private IList<IProducer> _producers;
    private IList<IConsumer> _consumers;
    private GraphDistanceResolver _graphDistanceResolver;

    public Community(ImportCommunity community,IList<IConsumer> consumers, IList<IProducer> producers, GraphDistanceResolver graphDistanceResolver)
    {
        _consumers = consumers;
        _producers = producers;
        _graphDistanceResolver = graphDistanceResolver;
    }
}