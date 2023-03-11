using System.Diagnostics;
using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Modules.Clocks;
using Project.Util.Models.Import;
using Serilog;

namespace Project.PowerBalancer;

public class PowerBalancerEngine
{
    private readonly WaitingClock _waitingClock;
    private readonly GraphDistanceResolver _graphDistanceResolver;
    private readonly IList<Community> _communities = new List<Community>();
    private readonly PowerBalancingMediator PowerBalancingMediator;
    public bool isActive { get; private set; } = true;

    public PowerBalancerEngine(IList<ImportCommunity> importCommunities, BasePowerSystemConfig powerSystemConfig, WaitingClock waitingClock)
    {
        if (importCommunities == null || powerSystemConfig == null) throw new ArgumentNullException("Parameter is null");

        _waitingClock = waitingClock;
        PowerBalancingMediator = waitingClock.Mediator;
        _graphDistanceResolver = new GraphDistanceResolver(importCommunities);

        foreach (var importCommunity in importCommunities)
        {
            var newCommunity = new Community(importCommunity, powerSystemConfig.GetConsumer(importCommunity.Name), powerSystemConfig.GetProducer(importCommunity.Name),
                _graphDistanceResolver, waitingClock);
            _communities.Add(newCommunity);
            waitingClock.Mediator.AddCommunity(newCommunity);
        }

        _graphDistanceResolver.SetCommunities(_communities);
    }

    public long StartConcurrent()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var clockThread = new Task(_waitingClock.Start);
        clockThread.Start();
        var communityThreads = new List<Thread>();
        foreach (var community in _communities)
        {
            var newThread = new Thread(() => community.StartBalancingProcess());
            newThread.Start();
            communityThreads.Add(newThread);
        }
        
        while (_waitingClock.IsActive)
        {
        }

        stopwatch.Stop();
        foreach (var thread in communityThreads)
        {
            thread.Join();
        }
        Log.Information("Time for Complete Execution: " + stopwatch.ElapsedMilliseconds);
        return stopwatch.ElapsedMilliseconds;
    }

    public long StartSequential()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var clockThread = new Task(_waitingClock.Start);
        clockThread.Start();
      

        while (_waitingClock.IsActive)
        {
            Thread.Sleep(1000);
            foreach (var community in _communities)
            {
                community.StartBalancingProcess(true);
            }
           
        }

        stopwatch.Stop();

        Log.Information("Time for Complete Execution: " + stopwatch.ElapsedMilliseconds);
        return stopwatch.ElapsedMilliseconds;
    }

   
}