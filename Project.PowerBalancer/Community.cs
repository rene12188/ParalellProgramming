using System.Collections;
using Project.PowerBalancer.Interfaces;

namespace Project.PowerBalancer;

public class Community
{
    private IList<IProducer> _producers;
    private IList<IConsumer> _consumers;
    private GraphResolver _graphResolver;

    public Community(IList<IConsumer> consumers, IList<IProducer> producers, GraphResolver graphResolver)
    {
        _consumers = consumers;
        _producers = producers;
        _graphResolver = graphResolver;
    }


}