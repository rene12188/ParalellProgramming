using Project.PowerBalancer.Interfaces;
using Project.PowerBalancer.Modules.Reporter;
using Serilog;

namespace Project.PowerBalancer;

public class PowerBalancingMediator
{
    private readonly IList<Community> _communities = new List<Community>();
    private readonly JsonReporter _reporter = new JsonReporter();
    public ManualResetEvent _communityDoneResetEvent;



    public bool IsAllDone => _communities.All(c => c.IsDone);


    public void StartNextCycle()
    {
        _reporter.Report(_communities);
        foreach (var community in _communities) community.SetUnDone();
        _communityDoneResetEvent.Set();
        _communityDoneResetEvent.Reset();

    }

    public void AddCommunity(Community community)
    {
        _communities.Add(community);
    }

    public void StopSimulation()
    {
        Visualize();
        _reporter.FlushReport(_communities);
        foreach (var community in _communities) community.IsActive = false;
    }

    public void Visualize()
    {
        foreach (var community in _communities)
        {
            Log.Information(community.Name);
            Log.Information($"Current power: {community.CurrentPower}");
            foreach (var powerBought in community.PowerBoughtReport) Log.Information($"Power bought from {powerBought.Item1}: {powerBought.Item2}");
            foreach (var powerSold in community.PowerSoldReport) Log.Information($"Power sold to {powerSold.Item1}: {powerSold.Item2}");
        }
    }
}