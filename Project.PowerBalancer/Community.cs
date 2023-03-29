using System.Collections.Concurrent;
using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Interfaces;
using Project.Util.Models.Import;
using Serilog;

namespace Project.PowerBalancer;

public class Community
{
    public string Name { get; }
    private readonly IList<BaseProducer> _producers;
    private readonly IList<BaseConsumer> _consumers;
    private readonly IClock _clock;
    private readonly GraphDistanceResolver _graphDistanceResolver;
    public bool IsActive = true;
    private double PowerAvailable => Math.Max(0, GetCurrentPower());
    private double PowerNeeded => Math.Min(0, GetCurrentPower());
    public double CurrentPower => GetCurrentPower();
    public double CurrentLocalPower => GetCurrentLocalPower();
    private bool HasPowerAvailable => CurrentPower > 0;
    private readonly ConcurrentDictionary<Community, double> _powerBought = new();
    private readonly ConcurrentDictionary<Community, double> _powerSold = new();
    private readonly Dictionary<string, double> _distances;
    public IList<Tuple<string, double>> PowerBoughtReport => _powerBought.Select(s => new Tuple<string, double>(s.Key.Name, s.Value)).ToList();
    public IList<Tuple<string, double>> PowerSoldReport => _powerSold.Select(s => new Tuple<string, double>(s.Key.Name, s.Value)).ToList();
    public bool IsDone { get; private set; }

    public Community(ImportCommunity community, IList<BaseConsumer>? consumers, IList<BaseProducer>? producers, GraphDistanceResolver graphDistanceResolver, IClock clock)
    {
        Name = community.Name;
        if (consumers == null)
            _consumers = new List<BaseConsumer>();
        else
            _consumers = consumers;

        if (producers == null)
            _producers = new List<BaseProducer>();
        else
            _producers = producers;
        _clock = clock;
        _graphDistanceResolver = graphDistanceResolver;
        _distances = _graphDistanceResolver.GetDistancesFromSource(Name);
    }


    public void StartBalancingProcess(bool isSequential = false)
    {
        while (IsActive)
        {
            Log.Information($"{Name}: Starting balancing process for Time {_clock.Time}");
            if (HasPowerAvailable)
                RemoveUnneededBoughtEnergy();
            else
                GetPowerFromDifferentCommunity();

            IsDone = true;
            Log.Information($"{Name}:Waiting for other communities to finish");
            while (IsDone)
            {
                if (isSequential) return;
                Thread.Sleep(100);
            }
        }
    }

    public void SetUnDone()
    {
        IsDone = false;
    }

    private void GetPowerFromDifferentCommunity()
    {
        for (int i = 0; i < _distances.Count && PowerNeeded < 0; i++)
        {
            var community = _graphDistanceResolver.GetCommunity(_distances.ElementAt(i).Key);
            if (community == null || !community.HasPowerAvailable) continue;

            Monitor.Enter(community);
            var receipt = community.BuyPower(this, PowerNeeded);
            if (receipt != null && receipt.Value.Value > 0)
            {
                _powerBought.Remove(receipt.Value.Key, out var _);
                _powerBought.TryAdd(receipt.Value.Key, receipt.Value.Value);
            }

            Monitor.Exit(community);
        }
    }

    private void RemoveUnneededBoughtEnergy()
    {
        if (_powerBought.Count > 0)
        {
            int iterator = 0;
            while (HasPowerAvailable)
            {
                var community = _powerBought.ElementAt(iterator).Key;
                community.StopBuyingPower(this, CurrentPower);
                _powerBought.TryRemove(community, out var _);
            }
        }
    }

    private double GetCurrentPower()
    {
        var result = GetCurrentLocalPower() + _powerBought.Sum(s => s.Value);
        return result;
    }

    private double GetCurrentLocalPower()
    {
        return _producers.Sum(c => c.GetPowerProduction()) - _consumers.Sum(p => p.GetPowerConsumption()) - _powerSold.Sum(s => s.Value);
    }


    private KeyValuePair<Community, double>? BuyPower(Community community, double amount)
    {
        amount = Math.Abs(amount);
        if (PowerAvailable == 0)
            return null;
        if (community == this) return null;

        _powerSold.TryRemove(community, out var powerAvailable);

        if (amount > CurrentLocalPower)
        {
            _powerSold.TryAdd(community, CurrentLocalPower);
            return new KeyValuePair<Community, double>(this, CurrentLocalPower);
        }

        _powerSold.TryAdd(community, amount);
        return new KeyValuePair<Community, double>(this, amount);
    }

    private void StopBuyingPower(Community community, double amount)
    {
        if (community == this) return;

        if (_powerSold.TryGetValue(community, out var value))
        {
            if (amount <= value)
                _powerSold.TryRemove(community, out var _);
            else
                _powerSold[community] = amount;
        }
    }
}