using Project.PowerBalancer.BaseClasses;
using Project.Util.Models.Import;

namespace Project.PowerBalancer;

public class Community
{
    private readonly string _name;
    private IList<BaseProducer> _producers;
    private readonly Clock _clock;
    private IList<BaseConsumer> _consumers;
    private GraphDistanceResolver _graphDistanceResolver;

    public Community(ImportCommunity community,IList<BaseConsumer> consumers, IList<BaseProducer> producers, GraphDistanceResolver graphDistanceResolver, Clock clock)
    {
        _name = community.Name;
        _consumers = consumers;
        _producers = producers;
        _clock = clock;
        _graphDistanceResolver = graphDistanceResolver;
    }
}