using Project.Util.Models.Import;

namespace Project.PowerBalancer;

public class PowerBalancerEngine
{
    private readonly Clock _clock;
    private readonly GraphDistanceResolver _graphDistanceResolver;
    private readonly IList<Community> _communities = new List<Community>();
    private bool isActive = true;

    public PowerBalancerEngine(IList<ImportCommunity> importCommunities, PowerSystemConfig powerSystemConfig, Clock clock)
    {
        if (importCommunities == null || powerSystemConfig == null) throw new ArgumentNullException("Parameter is null");

        _clock = clock;

        _graphDistanceResolver = new GraphDistanceResolver(importCommunities);

        foreach (var importCommunity in importCommunities)
            _communities.Add(new Community(importCommunity, powerSystemConfig.GetConsumer(importCommunity.Name), powerSystemConfig.GetProducer(importCommunity.Name),
                _graphDistanceResolver, clock));

        _graphDistanceResolver.SetCommunities(_communities);
    }

    public void Start()
    {
        var clockThread = new Task(_clock.Start);
        var visualThread = new Task(Visualize);
        clockThread.Start();
        visualThread.Start();
        Parallel.ForEach(_communities, community => community.StartBalancingProcess());

        Console.ReadLine();
        isActive = false;
        foreach (var community in _communities) community.IsActive = false;
        _clock.IsActive = false;
    }

    public void Visualize()
    {
        while (isActive)
        {
            Thread.Sleep(2000);
            foreach (var community in _communities)
            {
                Console.WriteLine(community.Name);
                Console.WriteLine($"Current power: {community.CurrentPower}");
                foreach (var powerBought in community.PowerBoughtReport) Console.WriteLine($"Power bought from {powerBought.Item1}: {powerBought.Item2}");

                foreach (var powerSold in community.PowerSoldReport) Console.WriteLine($"Power bought from {powerSold.Item1}: {powerSold.Item2}");
                Console.WriteLine();
            }
        }
    }
}