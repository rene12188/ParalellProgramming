using Project.PowerBalancer.Interfaces;
using Project.PowerBalancer.Modules.Clocks;
using Serilog;

namespace Project.PowerBalancer;

public class PowerBalancingMediator
{
    private readonly IList<Community> _communities = new List<Community>();


    public bool AllDone()
    {
        return _communities.All(c => c.IsDone);
    }

    public void StartNextCycle()
    {
        foreach (var community in _communities)
        {
            community.SetUnDone();
        }
    }

    public void AddCommunity(Community community)
    {
        _communities.Add(community);
    }

    public void StopSimulation()
    {
        Visualize();
        foreach (var community in _communities)
        {
            community.IsActive = false;
        }
    }

    public void Visualize()
    {
        foreach (var community in _communities)
        {
            Log.Information(community.Name);
            Log.Information($"Current power: {community.CurrentPower}");
            foreach (var powerBought in community.PowerBoughtReport) Log.Information($"Power bought from {powerBought.Item1}: {powerBought.Item2}");

            foreach (var powerSold in community.PowerSoldReport) Log.Information($"Power bought from {powerSold.Item1}: {powerSold.Item2}");
        }
    }
}