using Project.PowerBalancer.BaseClasses;

namespace Project.PowerBalancer.Modules.Clocks;

public class WaitingClock : BaseClock
{
    private readonly int? _stopTime;
    public PowerBalancingMediator Mediator { get; }
    public bool IsActive { get; private set; } = true;

    public WaitingClock(PowerBalancingMediator mediator)
    {
        Mediator = mediator;
    }


    public WaitingClock(PowerBalancingMediator mediator, int startTime)
    {
        Mediator = mediator;
        Time = startTime;
    }

    public WaitingClock(PowerBalancingMediator mediator, int startTime, int stopTime)
    {
        _stopTime = stopTime;
        Mediator = mediator;
        Time = startTime;
    }

    public override void Start()
    {
        while (IsActive)
        {
            if (Mediator.IsAllDone)
            {
                Tick();
                if (_stopTime.HasValue && Time > _stopTime.Value)
                {
                    Mediator.StopSimulation();
                    IsActive = false;
                }

                Mediator.StartNextCycle();
            }

            Thread.Sleep(10);
        }
    }
}