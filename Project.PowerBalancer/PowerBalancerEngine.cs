using Project.Util.Models.Import;

namespace Project.PowerBalancer;

public class PowerBalancerEngine
{
    private readonly GraphDistanceResolver _graphDistanceResolver;
    
    private readonly IList<Community> _communities;

    public PowerBalancerEngine(IList<ImportCommunity> importCommunities, PowerSystemConfig powerSystemConfig)
    {
        if (importCommunities == null || powerSystemConfig == null)
        {
            throw new ArgumentNullException("Parameter is null");
        }
        
        _graphDistanceResolver = new GraphDistanceResolver(importCommunities);
        

        foreach (var importCommunity in importCommunities)
        {
            _communities.Add(new Community(importCommunity, powerSystemConfig.GetProducer(importCommunity.Name),powerSystemConfig.GetConsumer(importCommunity.Name), _graphDistanceResolver));
        }
        
    }

}