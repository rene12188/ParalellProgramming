using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Project.PowerBalancer.BaseClasses;
using Project.PowerBalancer.Interfaces;


namespace Project.PowerBalancer.PowerComponents.Producers;

public class IncrementingProducer : BaseProducer
{
    private readonly IClock _clock;
    private readonly Random _random;
    private double _factor;

    public IncrementingProducer(double maxPowerProduction, IClock clock) : base(maxPowerProduction)
    {
        _clock = clock;
        _random = new Random();
        _factor = 0.001d;

        // NextDouble returns a number >= 0 and < 1
        _goalNumbers = new(_random.NextDouble(), _random.NextDouble(), _random.NextDouble());
    }

    public override double GetPowerProduction()
    {
        CalculateMultiplicationFactor();

        if (_factor is > 1 or < 0)
        {
            throw new InvalidOperationException(
                "Multiplication factor can't be greater than one or less than 0! You fucked up in your calculations");
        }

        return _factor * MaxPowerProduction;
    }

    // A day has 86400 seconds
    // 3 points: 28800 57600 86400
    // 3 ranges: 0 - 28800, 28800 - 57600,57600 - 86400
    // define 3 random numbers to each the factor should strive to during the range
    private (double, double) GetTargetFactorAndRemainingTicks()
    {
        // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/pattern-matching#relational-patterns

        var currentTime = _clock.Time;
        return currentTime switch
        {
            (>= 0) and (< 28800) => (_goalNumbers.Item1, 28800 - currentTime),
            (>= 28800) and (< 57600) => (_goalNumbers.Item2, 57600 - currentTime),
            (>= 57600) and (< 86400) => (_goalNumbers.Item3, 86400 - currentTime),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private readonly Tuple<double, double, double> _goalNumbers;
    private void CalculateMultiplicationFactor()
    {
        // we are at _factor and want to reach goalFactor
        (double goalFactor, double remainingTicks) = GetTargetFactorAndRemainingTicks();

        // we must reach goalFactor from _factor in count(remainingTicks) steps
        var incrementStep = (goalFactor - _factor) / remainingTicks;

        _factor += incrementStep;
    }
}

